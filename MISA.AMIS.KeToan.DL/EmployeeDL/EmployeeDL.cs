using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using System.Data;
using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities.DTO;

namespace MISA.AMIS.KeToan.DL
{
    public class EmployeeDL : BaseDL<Employee>, IEmployeeDL
    {
        #region Field

        // Khởi tạo kết nối tới DB MySQL
        private readonly string connectionString = DatabaseContext.ConnectionString;

        #endregion

        #region Method

        public dynamic GetBiggestEmployeeCode()
        {
            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = Procedure.GET_BIGGEST_CODE_EMPLOYEE;

            // Khởi tạo kết nối tới DB MySQL
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                var employeeCode = mySqlConnection.Query(storeProcedureName, commandType: CommandType.StoredProcedure);
                return employeeCode;
            }
        }

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>ID của nhân viên vừa thêm mới, số bản ghi ảnh hưởng</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction InsertEmployee(Employee employee)
        {
            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = Procedure.INSERT_EMPLOYEE;

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            var newEmployeeID = Guid.NewGuid();
            foreach (var prop in employee.GetType().GetProperties())
            {
                if (prop.Name == "EmployeeID")
                {
                    parameters.Add("@EmployeeID", newEmployeeID);
                }
                else
                {
                    parameters.Add("@" + prop.Name, prop.GetValue(employee, null));
                }
            }

            // Khởi tạo kết nối tới DB MySQL
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return new ResultForAction { RecordID = newEmployeeID, NumberOfRowsAffected = numberOfRowsAffected };
            }
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
            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = Procedure.UPDATE_EMPLOYEE;

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            //parameters.RemoveUnused = false;

            foreach (var prop in employee.GetType().GetProperties())
            {
                if (prop.Name == "EmployeeID")
                {
                    parameters.Add("@EmployeeID", employeeID);
                }
                else
                {
                    parameters.Add("@" + prop.Name, prop.GetValue(employee, null));
                }

            }

            // Khởi tạo kết nối tới DB MySQL
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return new ResultForAction { RecordID = employeeID, NumberOfRowsAffected = numberOfRowsAffected };
            }
        }

        /// <summary>
        /// API Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần xóa</param>
        /// <returns>ID của nhân viên vừa xóa</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction DeleteEmployee(Guid employeeID)
        {
            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = Procedure.DELETE_EMPLOYEE;

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add("@EmployeeID", employeeID);

            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return new ResultForAction { RecordID = employeeID, NumberOfRowsAffected = numberOfRowsAffected };
            }
        }

        /// <summary>
        /// API Xóa nhiều nhân viên theo danh sách ID
        /// </summary>
        /// <param name="employeeIDText">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Status code 200</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction DeleteMultipleEmployees(string employeeIDText)
        {
            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = Procedure.DELETE_MULTIPLE_EMPLOYEES;

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            parameters.Add("@ListEmployeeID", employeeIDText);

            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return new ResultForAction { NumberOfRowsAffected = numberOfRowsAffected };
            }

            // Thực hiện gọi vào DB
            
        }

        #endregion
    }
}
