using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.Common.Entities;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsFakeController : ControllerBase
    {
        /// <summary>
        /// API Lấy danh sách tất cả đơn vị
        /// </summary>
        /// <returns>Danh sách thông tin tất cả đơn vị</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpGet]
        public IActionResult GetAllDepartments()
        {
            return StatusCode(StatusCodes.Status200OK, new List<Department>
            {
                new Department
                {
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "DV00001",
                    DepartmentName = "Phòng CNTT",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Quốc Đông",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Nguyễn Quốc Đông"
                },
                new Department
                {
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "DV00002",
                    DepartmentName = "Phòng CNTT",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Quốc Đông",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Nguyễn Quốc Đông"
                },
                new Department
                {
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "DV00003",
                    DepartmentName = "Phòng CNTT",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Quốc Đông",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Nguyễn Quốc Đông"
                },
                new Department
                {
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "DV00004",
                    DepartmentName = "Phòng CNTT",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Quốc Đông",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Nguyễn Quốc Đông"
                },
                new Department
                {
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "DV00005",
                    DepartmentName = "Phòng CNTT",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Quốc Đông",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Nguyễn Quốc Đông"
                }
            });
        }

        /// <summary>
        /// API Lấy thông tin của 1 đơn vị theo ID
        /// </summary>
        /// <param name="departmentID">ID của đơn vị muốn lấy</param>
        /// <returns>Thông tin của đơn vị</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpGet("{departmentID}")]
        public IActionResult GetDepartmentByID([FromRoute] Guid departmentID)
        {
            return StatusCode(StatusCodes.Status200OK, new Department
            {
                DepartmentID = Guid.NewGuid(),
                DepartmentCode = "DV00001",
                DepartmentName = "Phòng CNTT",
                CreatedDate = DateTime.Now,
                CreatedBy = "Nguyễn Quốc Đông",
                ModifiedDate = DateTime.Now,
                ModifiedBy = "Nguyễn Quốc Đông"
            });
        }

        /// <summary>
        /// API Lấy danh sách đơn vị theo từ khóa
        /// </summary>
        /// <param name="keyword">Từ khóa người dùng nhập</param>
        /// <returns>Danh sách thông tin đơn vị</returns>
        /// Created by: NQDONG (01/11/2022)
        [HttpGet("filter")]
        public IActionResult GetDepartmentByFilter([FromQuery] string? keyword)
        {
            return StatusCode(StatusCodes.Status200OK, new List<Department>{
                new Department
                {
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "DV00001",
                    DepartmentName = "Phòng CNTT",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Quốc Đông",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Nguyễn Quốc Đông"
                },
                new Department
                {
                    DepartmentID = Guid.NewGuid(),
                    DepartmentCode = "DV00002",
                    DepartmentName = "Phòng CNTT",
                    CreatedDate = DateTime.Now,
                    CreatedBy = "Nguyễn Quốc Đông",
                    ModifiedDate = DateTime.Now,
                    ModifiedBy = "Nguyễn Quốc Đông"
                }
            });
        }
    }
}
