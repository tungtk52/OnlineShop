using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Libs
{
    public static class Ultility
    {
        /// <summary>
        /// Convert object to <c>int</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static int ToInt(this object obj, int? defaultValue)
        {
            int ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToInt32(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }


        /// <summary>
        /// Convert object to <c>long</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static long ToLong(this object obj, long? defaultValue)
        {
            long ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToInt64(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }


        /// <summary>
        /// Convert object to <c>double</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static double ToDouble(this object obj, double? defaultValue)
        {
            Double ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToDouble(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert object to <c>float</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static float ToFloat(this object obj, float? defaultValue)
        {
            float ret = defaultValue ?? 0;
            try
            {
                ret = Convert.ToSingle(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert object to <c>float</c> value if exception occur return default value(if defaultValue == null return 0)
        /// </summary>
        /// <param name="obj">obj value to convert</param>
        /// <param name="defaultValue">default return value</param>
        /// <returns></returns>
        public static bool ToBoolean(this object obj, bool? defaultValue)
        {
            bool ret = defaultValue ?? false;
            try
            {
                ret = Convert.ToBoolean(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        public static DateTime ToDateTime(this object obj, DateTime? defaultValue)
        {
            DateTime ret = defaultValue ?? DateTime.MinValue;
            try
            {
                ret = Convert.ToDateTime(obj);
            }
            catch
            {
                ;
            }

            return ret;
        }

        /// <summary>
        /// Convert from VietNam DateTime string to standard UTC DateTime value
        /// </summary>
        /// <param name="vnDateTime">input vndatetime <c>string</c></param>
        /// <param name="splitChar">split <c>char</c> used in input string</param>
        /// <param name="defaultValue">the value u want to return if cannot convert</param>
        /// <param name="hour">Hour and munite if it have <c>int</c> </param>
        /// <returns>Utc <c>DateTime</c> value</returns>
        public static DateTime? FromVnDateTimeToUtc(this string vnDateTime, char splitChar, DateTime? defaultValue, params int[] hour)
        {
            if (!string.IsNullOrEmpty(vnDateTime))
            {
                var arrDateTime = vnDateTime.Split(splitChar);
                DateTime utcDateTime;
                try
                {
                    utcDateTime = hour.Length == 2 ?
                                        new DateTime(int.Parse(arrDateTime[2]), int.Parse(arrDateTime[1]), int.Parse(arrDateTime[0]), hour[0], hour[1], 0)
                                        : new DateTime(int.Parse(arrDateTime[2]), int.Parse(arrDateTime[1]), int.Parse(arrDateTime[0]));
                }
                catch
                {
                    return defaultValue;
                }
                return utcDateTime;
            }
            return null;
        }

        public static string ToElapsed(this DateTime orgDate, DateTime compareDate)
        {
            string ret = string.Empty;

            if (compareDate > orgDate)
            {
                TimeSpan disTime = compareDate - orgDate;
                ret = string.Format("Cách đây{0}{1}{2}",
                                    (disTime.Days > 0 ? " " + disTime.Days + " ngày" : string.Empty),
                                   " " + disTime.Hours + " giờ", " " + disTime.Minutes + " phút");
            }
            else
            {
                TimeSpan disTime = orgDate - compareDate;
            }
            return ret;
        }

        #region stringConverter
        public static string Format = "&#{0};";
        private const string HTML_PREFIX = "http://";
        private static char[] PHRASE_SEPARATORS = new char[] { 
            '.', ',', ';', ':', '?', '!', '"', '\'', '\t', '\r', '\n', '|', '(', ')', '{', '}', '-', '+'
        };
        public const char SEPARATOR = '-';
        private const char SPACE = ' ';
        private const string SPACE_CHARS = " \t\r\n";
        private const string HtmlTagPattern = "<.*?>";
        private static char[] SPECIAL_CHARS = new char[] { '\x00ba' };
        private static char[][] VnVowelTbl = new char[][] { new char[] { 
                'a', 'ã', '\x00e2', 'e', '\x00ea', 'i', 'o', '\x00f4', 'õ', 'u', 'ý', 'y', '\x00e0', '?', '?', '\x00e8', 
                '?', '\x00ec', '\x00f2', '?', '?', '\x00f9', '?', '?', '\x00e1', '?', '?', '\x00e9', '?', '\x00ed', '\x00f3', '?', 
                '?', '\x00fa', '?', '\x00fd', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', '?', 
                '\x00e3', '?', '?', '?', '?', '?', '\x00f5', '?', '?', '?', '?', '?', '?', '?', '?', '?', 
                '?', '?', '?', '?', '?', '?', '?', '?', 'ð', '\x00f0'
            }, new char[] { 
                'a', 'a', 'a', 'e', 'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y', 'a', 'a', 'a', 'e', 
                'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y', 'a', 'a', 'a', 'e', 'e', 'i', 'o', 'o', 
                'o', 'u', 'u', 'y', 'a', 'a', 'a', 'e', 'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y', 
                'a', 'a', 'a', 'e', 'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y', 'a', 'a', 'a', 'e', 
                'e', 'i', 'o', 'o', 'o', 'u', 'u', 'y', 'd', 'd'
            } 
        };
        private static char[] WORD_SEPARATORS = new char[] { 
            ' ', '.', ',', ';', ':', '?', '!', '"', '\'', '\t', '\r', '\n', '|', '(', ')', '{', '}', '-', '+', '>', '<', '='
        };

        public static string ToSEOString(this string txt)
        {
            var builder = new StringBuilder();
            string str = txt.ToFTS();
            foreach (char ch in str)
            {
                if (ch == ' ')
                {
                    builder.Append('-');
                }
                else if (IsLetterOrDigit(ch))
                {
                    builder.Append(ch);
                }
            }

            return builder.ToString().Replace(":", "").Replace("\"", "");
        }

        public static string ToFTS(this string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return "";
            }
            string str = ToSingleSpaceString(txt.ToLower());
            var builder = new StringBuilder();
            foreach (char ch in str)
            {
                if ((((ch != '"') && (ch != ' ')) && (ch != '/')) && !IsLetterOrDigit(ch))
                {
                    continue;
                }
                int index = 0;
                while (index < VnVowelTbl[0].Length)
                {
                    if (ch == VnVowelTbl[0][index])
                    {
                        break;
                    }
                    index++;
                }
                builder.Append((index == VnVowelTbl[0].Length) ? ch : VnVowelTbl[1][index]);
            }
            return builder.ToString();
        }

        public static string ToSingleSpaceString(this string txt)
        {
            if (string.IsNullOrEmpty(txt))
            {
                return "";
            }
            string str = txt;
            var builder = new StringBuilder();
            int num = 0;
            while (num < str.Length)
            {
                char ch = str[num++];
                switch (ch)
                {
                    case '\t':
                    case '\r':
                    case '\n':
                        ch = ' ';
                        break;
                }
                if ((ch != ' ') || ((builder.Length > 0) && (builder[builder.Length - 1] != ' ')))
                {
                    builder.Append(ch);
                }
            }
            return builder.ToString();
        }
        public static bool IsLetterOrDigit(char c)
        {
            return ((Enumerable.All<char>(Path.GetInvalidPathChars(), (Func<char, bool>)(i => (i != c))) && Enumerable.All<char>(SPECIAL_CHARS, (Func<char, bool>)(i => (i != c)))) && char.IsLetterOrDigit(c));
        }

        public static string ConvertFromSignedtoUnsigned(string input)
        {
            if (!string.IsNullOrEmpty(input))
            {
                var regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
                string temp = input.Normalize(NormalizationForm.FormD);
                return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
            }
            return input;
        }

        public static string RemoveHtmlTag(this string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return string.Empty;
            }
            return Regex.Replace(input, HtmlTagPattern, string.Empty);
        }

        public static List<string> DetectPhoneNumber(string input)
        {
            var lstPhone = new List<string>();
            if (string.IsNullOrEmpty(input)) return lstPhone;
            input = Regex.Replace(input.Trim(), "(\\+84)([0-9]+)", delegate(Match m)
            {
                return "0" + m.Groups[2].Value;
            }, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            input = Regex.Replace(input, "[.,]", string.Empty, RegexOptions.IgnoreCase | RegexOptions.Singleline);
            var reg = new Regex("([0-9]+)", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection matches = reg.Matches(input);
            foreach (Match m in matches)
            {
                var phone = m.Groups[0].Value;
                if (!string.IsNullOrEmpty(phone) && phone.Length > 6)
                {
                    var one = phone.Substring(0, 1);
                    var two = phone.Substring(1, 1);
                    if ("0".Equals(one))
                    {
                        if ("9".Equals(two) || "1".Equals(two))
                        {
                            if (phone.Length > 9 && !lstPhone.Contains(phone))
                            {
                                lstPhone.Add(phone);
                            }
                        }
                        else
                        {
                            if (phone.Length > 8 && !lstPhone.Contains(phone))
                            {
                                lstPhone.Add(phone);
                            }
                        }
                    }
                    else
                    {
                        if (!lstPhone.Contains(phone))
                        {
                            lstPhone.Add(phone);
                        }
                    }
                }
            }
            return lstPhone;
        }

        public const string uniChars =
            "àáảãạâầấẩẫậăằắẳẵặèéẻẽẹêềếểễệđìíỉĩịòóỏõọôồốổỗộơờớởỡợùúủũụưừứửữựỳýỷỹỵÀÁẢÃẠÂẦẤẨẪẬĂẰẮẲẴẶÈÉẺẼẸÊỀẾỂỄỆĐÌÍỈĨỊÒÓỎÕỌÔỒỐỔỖỘƠỜỚỞỠỢÙÚỦŨỤƯỪỨỬỮỰỲÝỶỸỴÂĂĐÔƠƯ";

        public const string koDauChars =
            "aaaaaaaaaaaaaaaaaeeeeeeeeeeediiiiiooooooooooooooooouuuuuuuuuuuyyyyyAAAAAAAAAAAAAAAAAEEEEEEEEEEEDIIIIIOOOOOOOOOOOOOOOOOUUUUUUUUUUUYYYYYAADOOU";

        public static string UnicodeToKoDau(string s)
        {
            string retVal = String.Empty;
            int pos;
            if (!string.IsNullOrEmpty(s))
            {
                for (int i = 0; i < s.Length; i++)
                {
                    pos = uniChars.IndexOf(s[i].ToString());
                    if (pos >= 0)
                        retVal += koDauChars[pos];
                    else
                        retVal += s[i];
                }
            }
            return retVal;
        }

        public static string UnicodeToKoDauAndGach(string s)
        {
            const string strChar = "abcdefghijklmnopqrstxyzuvxw0123456789 -";
            //string retVal = UnicodeToKoDau(s);
            s = UnicodeToKoDau(s.ToLower().Trim());
            string sReturn = "";
            for (int i = 0; i < s.Length; i++)
            {
                if (strChar.IndexOf(s[i]) > -1)
                {
                    if (s[i] != ' ')
                        sReturn += s[i];
                    else if (i > 0 && s[i - 1] != ' ' && s[i - 1] != '-')
                        sReturn += "-";
                }
            }
            while (sReturn.IndexOf("--") != -1)
            {
                sReturn = sReturn.Replace("--", "-");
            }
            return sReturn;
        }

        public static string UnicodeToKoDauAndSpace(string s)
        {
            if (string.IsNullOrEmpty(s)) return string.Empty;
            string retVal = String.Empty;
            int pos;
            for (int i = 0; i < s.Length; i++)
            {
                pos = uniChars.IndexOf(s[i].ToString());
                if (pos >= 0)
                    retVal += koDauChars[pos];
                else
                    retVal += s[i];
            }
            return retVal;
        }
        public static string ReplaceSpecialChar(string str)
        {
            return UnicodeToKoDauAndSpace(str.Replace("%", "").Replace("/", "").Replace(" ", "-").TrimEnd('.'));
        }
        public static string RemoveSpecialCharacters(string url)
        {
            return Regex.Replace(url, "[^0-9a-zA-Z]+", "");
            //Regex re = new Regex(@"@#$[()<>"";+\n\r`]|^&+|&+$");
            //return re.Replace(url, "");
        }

        #endregion        
    }
}
