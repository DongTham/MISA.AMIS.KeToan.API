using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Dapper;
using MySqlConnector;
using MISA.AMIS.KeToan.API.Entities;
using MISA.AMIS.KeToan.API.Entities.DTO;
using MISA.AMIS.KeToan.API.Enums;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesFakeController : ControllerBase
    {
        /// <summary>
        /// API Lấy danh sách thông tin tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách thông tin tất cả nhân viên</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpGet]
        public IActionResult GetAllEmployees()
        {
            return StatusCode(StatusCodes.Status200OK, new List<Employee>
            {
                new Employee
                {
                    EmployeeID = Guid.NewGuid(),
                    EmployeeCode = "NV00001",
                    EmployeeName = "Nguyễn Quốc Đông",
                    Gender = Gender.Male,
                    DateOfBirth = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Quốc Đông",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Nguyễn Quốc Đông",
                    BankAccountNumber = 384128912867,
                    BankBranchName = "Chi nhánh hội sở",
                    BankName = "Ngân hàng Quân Đội",
                    DepartmentID = Guid.NewGuid(),
                    Email = "fourchracters39@gmail.com",
                    EmployeeAddress = "Duy Tân, Cầu Giấy, Hà Nội",
                    IdentityIssueDate = DateTime.Now,
                    IdentityIssuePlace = "Duy Tân, Cầu Giấy, Hà Nội",
                    IdentityNumber = 132557744,
                    IsCustomer = false,
                    IsSuplier = false,
                    JobPositionName = "Nhân viên",
                    MobilePhone = "0962446883",
                    TelePhone = "+2430584579"
                },
                new Employee
                {
                    EmployeeID = Guid.NewGuid(),
                    EmployeeCode = "NV00002",
                    EmployeeName = "Nguyễn Quốc Đông",
                    Gender = Gender.Female,
                    DateOfBirth = DateTime.Now,
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Quốc Đông",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Nguyễn Quốc Đông",
                    BankAccountNumber = 384128912867,
                    BankBranchName = "Chi nhánh hội sở",
                    BankName = "Ngân hàng Quân Đội",
                    DepartmentID = Guid.NewGuid(),
                    Email = "fourchracters39@gmail.com",
                    EmployeeAddress = "Duy Tân, Cầu Giấy, Hà Nội",
                    IdentityIssueDate = DateTime.Now,
                    IdentityIssuePlace = "Duy Tân, Cầu Giấy, Hà Nội",
                    IdentityNumber = 132557744,
                    IsCustomer = false,
                    IsSuplier = false,
                    JobPositionName = "Nhân viên",
                    MobilePhone = "0962446883",
                    TelePhone = "+2430584579"
                }
            });
        }

        /// <summary>
        /// API Lấy thông tin 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần lấy</param>
        /// <returns>Thông tin 1 nhân viên theo ID</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpGet("{employeeID}")]
        public IActionResult GetEmployeeByID([FromRoute] Guid employeeID)
        {
            return StatusCode(StatusCodes.Status200OK, new Employee
            {
                EmployeeID = employeeID,
                EmployeeCode = "NV00001",
                EmployeeName = "Nguyễn Quốc Đông",
                Gender = Gender.Male,
                DateOfBirth = DateTime.Now,
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Quốc Đông",
                ModifiedDate = DateTime.Now,
                ModifiedBy = "Nguyễn Quốc Đông",
                BankAccountNumber = 384128912867,
                BankBranchName = "Chi nhánh hội sở",
                BankName = "Ngân hàng Quân Đội",
                DepartmentID = Guid.NewGuid(),
                Email = "fourchracters39@gmail.com",
                EmployeeAddress = "Duy Tân, Cầu Giấy, Hà Nội",
                IdentityIssueDate = DateTime.Now,
                IdentityIssuePlace = "Duy Tân, Cầu Giấy, Hà Nội",
                IdentityNumber = 132557744,
                IsCustomer = false,
                IsSuplier = false,
                JobPositionName = "Nhân viên",
                MobilePhone = "0962446883",
                TelePhone = "+2430584579"
            });
        }

        /// <summary>
        /// API Lấy danh sách thông tin nhân viên theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <param name="sort">Cột muốn sắp xếp theo</param>
        /// <returns>Danh sách thông tin nhân viên và tổng số bản ghi</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpGet("filter")]
        public IActionResult GetEmployeesByFilterAndPaging(
            [FromQuery] string? keyword,
            [FromQuery] string? sort,
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1
            )
        {
            return StatusCode(StatusCodes.Status200OK, new PagingResult
            {
                Data = new List<Employee>
                {
                    new Employee
                    {
                        EmployeeID = Guid.NewGuid(),
                        EmployeeCode = "NV00001",
                        EmployeeName = "Nguyễn Quốc Đông",
                        Gender = Gender.Male,
                        DateOfBirth = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "Nguyễn Quốc Đông",
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = "Nguyễn Quốc Đông",
                        BankAccountNumber = 384128912867,
                        BankBranchName = "Chi nhánh hội sở",
                        BankName = "Ngân hàng Quân Đội",
                        DepartmentID = Guid.NewGuid(),
                        Email = "fourchracters39@gmail.com",
                        EmployeeAddress = "Duy Tân, Cầu Giấy, Hà Nội",
                        IdentityIssueDate = DateTime.Now,
                        IdentityIssuePlace = "Duy Tân, Cầu Giấy, Hà Nội",
                        IdentityNumber = 132557744,
                        IsCustomer = false,
                        IsSuplier = false,
                        JobPositionName = "Nhân viên",
                        MobilePhone = "0962446883",
                        TelePhone = "+2430584579"
                    },
                    new Employee
                    {
                        EmployeeID = Guid.NewGuid(),
                        EmployeeCode = "NV00002",
                        EmployeeName = "Nguyễn Quốc Đông",
                        Gender = Gender.Female,
                        DateOfBirth = DateTime.Now,
                        CreatedDate = DateTime.Now,
                        CreatedBy = "Nguyễn Quốc Đông",
                        ModifiedDate = DateTime.Now,
                        ModifiedBy = "Nguyễn Quốc Đông",
                        BankAccountNumber = 384128912867,
                        BankBranchName = "Chi nhánh hội sở",
                        BankName = "Ngân hàng Quân Đội",
                        DepartmentID = Guid.NewGuid(),
                        Email = "fourchracters39@gmail.com",
                        EmployeeAddress = "Duy Tân, Cầu Giấy, Hà Nội",
                        IdentityIssueDate = DateTime.Now,
                        IdentityIssuePlace = "Duy Tân, Cầu Giấy, Hà Nội",
                        IdentityNumber = 132557744,
                        IsCustomer = false,
                        IsSuplier = false,
                        JobPositionName = "Nhân viên",
                        MobilePhone = "0962446883",
                        TelePhone = "+2430584579"
                    }
                },
                TotalCount = 2
            });
        }

        /// <summary>
        /// API Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpGet("maxRecord")]
        public IActionResult GetMaxEmployeeCode()
        {
            return StatusCode(StatusCodes.Status200OK, "NV00039");
        }

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>ID của nhân viên vừa thêm mới</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status201Created, Guid.NewGuid());
        }

        /// <summary>
        /// API Sửa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param>
        /// <param name="employee">Đối tượng nhân viên cần sửa</param>
        /// <returns>ID của nhân viên vừa sửa</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpPut("{employeeID}")]
        public IActionResult UpdateEmpoyee([FromRoute] Guid employeeID, [FromBody] Employee employee)
        {
            return StatusCode(StatusCodes.Status200OK, employeeID);
        }

        /// <summary>
        /// API Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần xóa</param>
        /// <returns>ID của nhân viên vừa xóa</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpDelete("{employeeID}")]
        public IActionResult DeleteEmployee([FromRoute] Guid employeeID)
        {
            return StatusCode(StatusCodes.Status200OK, employeeID);
        }

        /// <summary>
        /// API Xóa nhiều nhân viên theo danh sách ID
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Status code 200</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpPost("deleteBatch")]
        public IActionResult DeleteMultipleEmployees([FromBody] ListEmployeeID listEmployeeID)
        {
            return StatusCode(StatusCodes.Status200OK);
        }
    }
}
