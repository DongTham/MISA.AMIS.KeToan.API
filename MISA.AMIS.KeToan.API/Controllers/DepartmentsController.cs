using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.API.Entities;
using MySqlConnector;
using System.Data;
using Dapper;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class DepartmentsController : ControllerBase
    {
        // Khởi tạo kết nối tới DB MySQL
        private readonly string connectionString = "Server=localhost;Port=3307;Database=misa.web09.ctm.nqdong;Uid=root;Pwd=Dongtham030900!;";

        /// <summary>
        /// API Lấy danh sách tất cả đơn vị
        /// </summary>
        /// <returns>Danh sách tất cả đơn vị</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet]
        public IActionResult GetAllFepartments()
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_department_GetAllDepartments";

                // Thực hiện gọi vào DB
                var departments = mySqlConnection.Query(storeProcedureName, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (departments != null)
                {
                    return StatusCode(StatusCodes.Status200OK, departments);
                }

                return StatusCode(StatusCodes.Status200OK, new List<Department>());
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
        /// API Lấy thông tin đơn vị theo ID
        /// </summary>
        /// <param name="departmentID"></param>
        /// <returns>Thông tin đơn vị</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet("{departmentID}")]
        public IActionResult GetDepartmentByID([FromRoute] Guid departmentID)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_department_GetDepartmentByID";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@DepartmentID", departmentID);

                // Thực hiện gọi vào DB
                var department = mySqlConnection.QueryFirstOrDefault<Department>(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (department != null)
                {
                    return StatusCode(StatusCodes.Status200OK, department);
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
        /// API Lấy danh sách đơn vị theo từ khóa
        /// </summary>
        /// <param name="keyword">Từ khóa người dùng nhập</param>
        /// <returns>Danh sách đơn vị theo từ khóa</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet("filter")]
        public IActionResult GetDepartmentsByFilter([FromQuery] string? keyword)
        {
            try
            {
                // Khởi tạo kết nối tới DB MySQL
                var mySqlConnection = new MySqlConnection(connectionString);

                // Chuẩn bị câu lệnh SQL
                string storeProcedureName = "Proc_department_GetDepartmentsByFilter";

                // Chuẩn bị tham số đầu vào
                var parameters = new DynamicParameters();
                parameters.Add("@Keyword", keyword);

                // Thực hiện gọi vào DB
                var departments = mySqlConnection.Query(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);

                // Xử lý kết quả trả về
                if (departments != null)
                {
                    return StatusCode(StatusCodes.Status200OK, departments);
                }

                return StatusCode(StatusCodes.Status200OK, new List<Department>());
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
    }
}
