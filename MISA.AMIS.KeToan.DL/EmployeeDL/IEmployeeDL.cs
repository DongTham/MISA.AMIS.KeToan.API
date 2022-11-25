using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public interface IEmployeeDL : IBaseDL<Employee>
    {
        /// <summary>
        /// API Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: NQDONG (10/11/2022)
        public dynamic GetBiggestEmployeeCode();

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction InsertEmployee(Employee employee);

        /// <summary>
        /// API Sửa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param>
        /// <param name="employee">Đối tượng nhân viên cần sửa</param>
        /// <returns>ID của nhân viên vừa sửa, số bản ghi ảnh hưởng</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction UpdateEmpoyee(Guid employeeID, Employee employee);

        /// <summary>
        /// API Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần xóa</param>
        /// <returns>ID của nhân viên vừa xóa</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction DeleteEmployee(Guid employeeID);

        /// <summary>
        /// API Xóa nhiều nhân viên theo danh sách ID
        /// </summary>
        /// <param name="employeeIDText">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Status code 200</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction DeleteMultipleEmployees(string employeeIDText);

        /// <summary>
        /// API kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên muốn kiểm tra</param>
        /// <returns>Số lượng mã nhân viên đã tồn tại</returns>
        /// Created by: NQDONG (18/11/2022)
        public long CheckDuplicateEmployeeCode(string employeeCode);
    }
}
