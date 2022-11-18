using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.DL;
using System.Data;

namespace MISA.AMIS.KeToan.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: NQDONG (10/11/2022)
        public dynamic GetBiggestEmployeeCode()
        {
            return _employeeDL.GetBiggestEmployeeCode();
        }

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>ID của nhân viên vừa thêm mới, số bản ghi ảnh hưởng</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction InsertEmployee(Employee employee)
        {
            return _employeeDL.InsertEmployee(employee);
        }

        /// <summary>
        /// API Sửa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param>
        /// <param name="employee">Đối tượng nhân viên cần sửa</param>
        /// <returns>ID của nhân viên vừa sửa, số bản ghi ảnh hưởng</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction UpdateEmpoyee(Guid employeeID, Employee employee)
        {
            return _employeeDL.UpdateEmpoyee(employeeID, employee);
        }

        /// <summary>
        /// API Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần xóa</param>
        /// <returns>ID của nhân viên vừa xóa</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction DeleteEmployee(Guid employeeID)
        {
            return _employeeDL.DeleteEmployee(employeeID);
        }

        /// <summary>
        /// API Xóa nhiều nhân viên theo danh sách ID
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Status code 200</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction DeleteMultipleEmployees(ListEmployeeID listEmployeeID)
        {
            string employeeIDText = "";

            foreach (var el in listEmployeeID.EmployeeIDs.Select((x, i) => new { Value = x, Index = i }))
            {
                employeeIDText += el.Value + ",";
            }

            return _employeeDL.DeleteMultipleEmployees(employeeIDText);
        }

        /// <summary>
        /// API Xuất khẩu file excel danh sách tất cả nhân viên
        /// </summary>
        /// <returns>Dữ liệu DataTable để xuất file</returns>
        /// Created by: NQDONG (18/11/2022)
        public DataTable ExportEmployeesToExcel()
        {
            // Lấy danh sách tất cả nhân viên
            var employees = _employeeDL.GetAllRecords();

            // using System.Data;
            DataTable dataTable = new()
            {
                TableName = "DANH SÁCH NHÂN VIÊN"
            };

            // Thêm 1 hàng tên cột của bảng
            dataTable.Columns.AddRange(new DataColumn[9] {new DataColumn("STT"), new DataColumn("Mã nhân viên"), new DataColumn("Tên nhân viên"), new DataColumn("Giới tính"), new DataColumn("Ngày sinh"), new DataColumn("Chức danh"), new DataColumn("Tên đơn vị"), new DataColumn("Số tài khoản"), new DataColumn("Tên ngân hàng") });

            int indexOfEmployee = 1;
            foreach (var employee in employees)
            {
                // Thêm dữ liệu của từng nhân viên cho 1 hàng
                dataTable.Rows.Add(indexOfEmployee, employee.EmployeeCode, employee.EmployeeName, employee.Gender, employee.DateOfBirth?.ToString("dd-MM-yyyy"), employee.JobPositionName, employee.DepartmentID, employee.BankAccountNumber, employee.BankName);
                indexOfEmployee++;
            }

            return dataTable;
        }

        #endregion
    }
}
