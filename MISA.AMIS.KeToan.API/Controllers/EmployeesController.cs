using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using Dapper;
using System.Data;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        // Khởi tạo kết nối tới DB MySQL
        private readonly string connectionString = "Server=localhost;Port=3306;Database=misa.web09.ctm.nqdong;Uid=root;Pwd=Dongtham030900!;";

        #region Method
        /// <summary>
        /// API Lấy danh sách thông tin tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách thông tin tất cả nhân viên</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_GetAllEmployees";

                // Thực hiện gọi vào DB
                var employees = mySqlConnection.Query(storeProcedureName, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (employees != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employees);
                }

                return StatusCode(StatusCodes.Status200OK, new List<Employee>());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception.",
                    UserMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Lấy thông tin 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần lấy</param>
        /// <returns>Thông tin 1 nhân viên theo ID</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet("{employeeID}")]
        public IActionResult GetEmployeeByID([FromRoute] Guid employeeID)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_GetEmployeeByID";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);

                // Thực hiện gọi vào DB
                var employees = mySqlConnection.QueryFirstOrDefault<Employee>(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (employees != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employees);
                }

                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception.",
                    UserMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Lấy danh sách thông tin nhân viên theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <param name="sort">Cột muốn sắp xếp theo</param>
        /// <returns>Danh sách thông tin nhân viên và tổng số bản ghi</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet("filter")]
        public IActionResult GetEmployeesByFilterAndPaging(
            [FromQuery] string? keyword,
            [FromQuery] string? sort = "EmployeeID ASC",
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1
            )
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_SearchAndFilterEmployee";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();

                int limit = pageSize * pageNumber;

                parameters.Add("@Keyword", keyword);
                parameters.Add("@Sort", sort);
                parameters.Add("@Limit", pageSize);
                parameters.Add("@Offset", limit);

                // Thực hiện gọi vào DB
                var resultReturn = mySqlConnection.QueryMultiple(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
                var employees = resultReturn.Read<dynamic>().ToList();
                var countList = resultReturn.Read<dynamic>().ToList();
                long totalRecord = countList?.FirstOrDefault()?.TotalRecord;
                decimal totalPage = Math.Ceiling((decimal)totalRecord / pageSize);

                // Xử lý kết quả trả về
                if (employees.Count > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, new { employees, totalRecord, totalPage });
                }

                return StatusCode(StatusCodes.Status200OK, new { employees = new List<Employee>(), totalRecord, totalPage });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception.",
                    UserMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet("maxRecord")]
        public IActionResult GetMaxEmployeeCode()
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_GetBiggestEmployeeCode";

                // Thực hiện gọi vào DB
                var employeeCode = mySqlConnection.Query(storeProcedureName, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (employeeCode != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeCode);
                }

                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception.",
                    UserMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>ID của nhân viên vừa thêm mới</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_CreateEmployee";

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

                //var newParameters = new Dictionary<dynamic, dynamic>();
                //newParameters.Add("@EmployeeID", newEmployeeID);

                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: System.Data.CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status201Created, newEmployeeID);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 2,
                    DevMsg = "Database insert failed.",
                    UserMsg = "Thêm mới nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception.",
                    UserMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Sửa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param>
        /// <param name="employee">Đối tượng nhân viên cần sửa</param>
        /// <returns>ID của nhân viên vừa sửa</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpPut("{employeeID}")]
        public IActionResult UpdateEmpoyee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_UpdateEmployeeByID";

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

                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeID);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 2,
                    DevMsg = "Database update failed.",
                    UserMsg = "Cập nhật nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception.",
                    UserMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần xóa</param>
        /// <returns>ID của nhân viên vừa xóa</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpDelete("{employeeID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_DeleteEmployeeByID";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@EmployeeID", employeeID);

                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeID);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 2,
                    DevMsg = "Database delete failed.",
                    UserMsg = "Xóa nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception.",
                    UserMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        /// <summary>
        /// API Xóa nhiều nhân viên theo danh sách ID
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Status code 200</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpPost("deleteBatch")]
        public IActionResult DeleteMultipleEmployees([FromBody] ListEmployeeID listEmployeeID)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_employee_DeleteMultipleEmployees";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                string employeeIDText = "";

                foreach (var el in listEmployeeID.EmployeeIDs.Select((x, i) => new { Value = x, Index = i }))
                {
                    employeeIDText += el.Value + ",";
                }

                parameters.Add("@ListEmployeeID", employeeIDText);

                // Thực hiện gọi vào DB
                int numberOfRowsAffected = mySqlConnection.Execute(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (numberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 2,
                    DevMsg = "Database delete multiple failed.",
                    UserMsg = "Xóa nhiều nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new
                {
                    ErrorCode = 1,
                    DevMsg = "Catched an exception.",
                    UserMsg = "Có lỗi xảy ra! Vui lòng liên hệ với MISA.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/1",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        } 
        #endregion
    }
}
