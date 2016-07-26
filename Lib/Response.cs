using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Libs
{
    public enum SystemCode
    { 
        /// <summary>
        /// Thành công
        /// </summary>
        Success = 1,
        /// <summary>
        /// Lỗi
        /// </summary>
        Error = 0,
        /// <summary>
        /// Lỗi báo đối tượng được kiểm tra là đã tồn tại
        /// </summary>
        ErrorExist = 2,
        /// <summary>
        /// Không có dữ liệu
        /// </summary>
        DataNull = 3,
        /// <summary>
        /// Tham số không đúng
        /// </summary>
        ErrorParam = 4,
        /// <summary>
        /// Không có quyền thực thi
        /// </summary>
        NotPermitted = 5,
        /// <summary>
        /// Không hợp lệ
        /// </summary>
        NotValid = 6,
        /// <summary>
        /// Đã bị khóa
        /// </summary>
        Locked = 7,
        /// <summary>
        /// Lỗi liên quan đến các vấn đề vượt quá giới hạn
        /// </summary>
        Overflow = 8,
        /// <summary>
        /// Lỗi liên quan đến các vấn đề kết nối
        /// </summary>
        ErrorConnect = 9
    }

    public class Response
    {
        public Response()
        {
            this.Code = SystemCode.Success;
            this.Message = string.Empty;
        }

        public SystemCode Code { get; set; }

        public string Message { get; set; }

        public dynamic Data { get; set; }
    }
}
