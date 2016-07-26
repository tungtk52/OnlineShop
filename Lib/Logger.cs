using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Configuration;

namespace Libs
{
    public class Logger
    {
        private string FILE_NAME = string.Format("Log-{0:yyyyMMdd}.log", DateTime.Now);
        private static readonly object sync = new object();
        public Logger() {}

        public Logger(string filename)
        {
            FILE_NAME = filename;
        }

        public static Logger GetInstance()
        {
            return new Logger();
        }

        public void Write(Exception ex)
        {
            lock (sync)
            {
                string curPath = GetCurPath();
                StreamWriter sw = new StreamWriter(curPath + FILE_NAME, true);
                sw.WriteLine("=====================================");
                sw.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss.FFF}", DateTime.Now));
                sw.WriteLine(ex.Message);
                sw.WriteLine(ex.StackTrace);
                sw.Flush();
                sw.Close();
            }
        }

        public void Write(string msg)
        {
            lock (sync)
            {
                string curPath = GetCurPath();
                StreamWriter sw = new StreamWriter(curPath + FILE_NAME, true);
                sw.WriteLine("=====================================");
                sw.WriteLine(string.Format("{0:dd/MM/yyyy HH:mm:ss.FFF}", DateTime.Now));
                sw.WriteLine(msg);
                sw.Flush();
                sw.Close();
            }
        }

        private string GetCurPath()
        {
            string curPath = string.Empty;
            if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["Logfile"]))
            {
                curPath = ConfigurationManager.AppSettings["Logfile"];
            }
            else {
                curPath = System.Reflection.Assembly.GetExecutingAssembly().Location;
                curPath = curPath.Substring(0, curPath.LastIndexOf("\\"));
                curPath = curPath + "\\LogFile";
                if (!Directory.Exists(curPath)) Directory.CreateDirectory(curPath);
            }
            if (!curPath.EndsWith("\\")) curPath += "\\";
            return curPath;
        }
    }
}
