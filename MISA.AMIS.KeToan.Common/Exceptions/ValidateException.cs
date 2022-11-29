using MISA.AMIS.KeToan.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Exceptions
{
    /// <summary>
    /// Các trường thêm của exception
    /// </summary>
    public class ValidateException: Exception
    {
        #region Field

        private string? _msgErrorValidate = null;
        private ErrorCode _errorCode = ErrorCode.Exception;

        #endregion

        #region Constractor

        public ValidateException(string message, ErrorCode errorCode)
        {
            _msgErrorValidate = message;
            _errorCode = errorCode;
        }

        #endregion

        #region Method

        /// <summary>
        /// Thông báo trả về
        /// </summary>
        public override string Message => _msgErrorValidate;

        /// <summary>
        /// Mã lỗi trả về
        /// </summary>
        public ErrorCode ErrorCodeValidate => _errorCode; 

        #endregion

    }
}
