using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities
{
    /// <summary>
    /// Các trường thêm của nhân viên
    /// </summary>
    public class EmployeeExtend : Employee
    {
        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? DepartmentName { get; set; }
    }
}
