﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Enums
{
    /// <summary>
    /// Enum sử dụng để mô tả các lỗi xảy ra khi gọi API
    /// </summary>
    public enum ErrorCode
    {
        /// <summary>
        /// Lỗi gặp exception
        /// </summary>
        Exception = 1,

        /// <summary>
        /// Lỗi trùng mã
        /// </summary>
        DuplicateCode = 2,

        /// <summary>
        /// Lỗi dữ liệu đầu vào không hợp lệ
        /// </summary>
        InvalidData = 3
    }
}
