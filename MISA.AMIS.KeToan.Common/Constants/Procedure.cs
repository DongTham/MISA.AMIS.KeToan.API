using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Constants
{
    /// <summary>
    /// Tên của các procedure
    /// </summary>
    public class Procedure
    {
        /// <summary>
        /// Format tên của procedure lấy tất cả bản ghi
        /// </summary>
        public static string GET_ALL = "Proc_{0}_GetAll";

        /// <summary>
        /// Format tên của procedure lấy 1 bản ghi theo ID
        /// </summary>
        public static string GET_BY_ID = "Proc_{0}_GetByID";

        /// <summary>
        /// Format tên của procedure lấy danh sách bản ghi theo từ khóa
        /// </summary>
        public static string GET_BY_FILTER = "Proc_{0}_GetByFilter";

        /// <summary>
        /// Tên của procedure lấy mã nhân viên lớn nhất
        /// </summary>
        public static string GET_BIGGEST_CODE_EMPLOYEE = "Proc_employee_GetBiggestCode";

        /// <summary>
        /// Tên của procedure thêm mới 1 nhân viên
        /// </summary>
        public static string INSERT_EMPLOYEE = "Proc_employee_CreateEmployee";

        /// <summary>
        /// Tên của procedure sửa 1 nhân viên theo ID
        /// </summary>
        public static string UPDATE_EMPLOYEE = "Proc_employee_UpdateByID";

        /// <summary>
        /// Tên của procedure xóa 1 nhân viên theo ID
        /// </summary>
        public static string DELETE_EMPLOYEE = "Proc_employee_DeleteByID";

        /// <summary>
        /// Tên của procedure xóa nhiều nhân viên
        /// </summary>
        public static string DELETE_MULTIPLE_EMPLOYEES = "Proc_employee_DeleteMultiple";

        /// <summary>
        /// Tên của procedure kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        public static string CHECK_DUPLICATE_EMPLOYEECODE = "Proc_employee_CheckDuplicateCode";
    }
}
