using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Resources;
using MISA.AMIS.KeToan.Common.Enums;
using ClosedXML.Excel;
using MISA.AMIS.KeToan.Common.Exceptions;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [ApiController]
    public class EmployeesController : BasesController<Employee>
    {
        #region Field

        private IEmployeeBL _employeeBL;

        #endregion

        #region Constructor

        public EmployeesController(IEmployeeBL employeeBL) : base(employeeBL)
        {
            _employeeBL = employeeBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet("maxRecord")]
        public IActionResult GetBiggestEmployeeCode()
        {
            try
            {
                var employeeCode = _employeeBL.GetBiggestEmployeeCode();

                // Xử lý kết quả trả về
                if (employeeCode != null && employeeCode.EmployeeCode != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeCode);
                }  

                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
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
                var dataResult = _employeeBL.InsertEmployee(employee);

                // Xử lý kết quả trả về
                if (dataResult != null)
                {
                    return StatusCode(StatusCodes.Status201Created, new { EmployeeID = dataResult.RecordID });
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = Resources.DevMsg_Insert_Failed,
                    UserMsg = Resources.UserMsg_Insert_Failed,
                    MoreInfo = Resources.MoreInfo_Failed,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (ValidateException ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ex.ErrorCodeValidate,
                    DevMsg = ex.Message,
                    UserMsg = ex.Message,
                    MoreInfo = Resources.MoreInfo_Exception,
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
                var dataResult = _employeeBL.UpdateEmpoyee(employeeID, employee);

                // Xử lý kết quả trả về
                if (dataResult.NumberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, dataResult.RecordID);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = Resources.DevMsg_Update_Failed,
                    UserMsg = Resources.UserMsg_Update_Failed,
                    MoreInfo = Resources.MoreInfo_Failed,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (ValidateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ex.ErrorCodeValidate,
                    DevMsg = ex.Message,
                    UserMsg = ex.Message,
                    MoreInfo = Resources.MoreInfo_Exception,
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
                var dataResult = _employeeBL.DeleteEmployee(employeeID);

                // Xử lý kết quả trả về
                if (dataResult.NumberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK, dataResult.RecordID);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = Resources.DevMsg_Delete_Failed,
                    UserMsg = Resources.UserMsg_Delete_Failed,
                    MoreInfo = Resources.MoreInfo_Failed,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
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
                var dataResult = _employeeBL.DeleteMultipleEmployees(listEmployeeID);

                // Xử lý kết quả trả về
                if (dataResult.NumberOfRowsAffected > 0)
                {
                    return StatusCode(StatusCodes.Status200OK);
                }

                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = Resources.DevMsg_DeleteBatch_Failed,
                    UserMsg = Resources.UserMsg_DeleteBatch_Failed,
                    MoreInfo = Resources.MoreInfo_Failed,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }
        }

        

        /// <summary>
        /// API Xuất khẩu file excel danh sách tất cả nhân viên
        /// </summary>
        /// <returns>file excel thông tin tất cả nhân viên</returns>
        /// Created by: NQDONG (18/11/2022)
        [HttpGet("exportAllRecord")]
        public IActionResult ExportEmployeesToExcel([FromQuery] string? keyword,
            [FromQuery] string? sort,
            [FromQuery] string? ids,
            [FromQuery] string? order = "ASC",
            [FromQuery] int pageSize = 25,
            [FromQuery] int pageNumber = 1)
        {
            try
            {
                var dataResult = _employeeBL.ExportEmployeesToExcel(keyword, sort, order, ids, pageSize, pageNumber);
                using (MemoryStream stream = new MemoryStream())
                {
                    dataResult.SaveAs(stream);
                    return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet");
                }
            }
            catch (Exception)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.Exception,
                    DevMsg = Resources.DevMsg_Exception,
                    UserMsg = Resources.UserMsg_Exception,
                    MoreInfo = Resources.MoreInfo_Exception,
                    TraceId = HttpContext.TraceIdentifier
                });
            }

        }

        #endregion
    }
}
