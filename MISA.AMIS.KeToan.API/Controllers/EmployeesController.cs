using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Resources;
using MISA.AMIS.KeToan.Common.Enums;
using ClosedXML.Excel;

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
                if (employeeCode != null)
                {
                    return StatusCode(StatusCodes.Status200OK, employeeCode);
                }

                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = "Database insert failed.",
                    UserMsg = "Thêm mới nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = "Database update failed.",
                    UserMsg = "Cập nhật nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = "Database delete failed.",
                    UserMsg = "Xóa nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult
                {
                    ErrorCode = ErrorCode.InvalidData,
                    DevMsg = "Database delete multiple failed.",
                    UserMsg = "Xóa nhiều nhân viên thất bại.",
                    MoreInfo = "https://openapi.misa.com.vn/errorcode/2",
                    TraceId = HttpContext.TraceIdentifier
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
        /// API kiểm tra mã nhân viên đã tồn tại hay chưa
        /// </summary>
        /// <param name="employeeCode">Mã nhân viên muốn kiểm tra</param>
        /// <returns>Kết quả đã tồn tại hay chưa</returns>
        /// Created by: NQDONG (18/11/2022)
        [HttpGet("checkDuplicateCode")]
        public IActionResult CheckDuplicateEmployeeCode([FromQuery] string employeeCode)
        {
            try
            {
                var dataResult = _employeeBL.CheckDuplicateEmployeeCode(employeeCode);
                return StatusCode(StatusCodes.Status200OK, new { IsDuplicateEmployeeCode = dataResult });
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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
        public IActionResult ExportEmployeesToExcel()
        {
            try
            {
                var dataResult = _employeeBL.ExportEmployeesToExcel();

                //using ClosedXML.Excel;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    var ws = wb.AddWorksheet(dataResult);

                    // Điều chỉnh độ rộng của ô vừa với độ dài của dữ liệu
                    ws.Columns("A:I").AdjustToContents();

                    // Tùy chỉnh style cho cột từ A đến I
                    ws.Columns("A:I").Style.Font.SetFontName("Times New Roman").Font.SetFontSize(11).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Left).Alignment.SetWrapText(true);

                    // Lấy dòng đầu tiên của bảng tính
                    var table = ws.Tables.FirstOrDefault();
                    if (table != null)
                    {
                        // Bỏ filter của bảng
                        table.ShowAutoFilter = false;

                        // Set kiểu viền và màu cho background
                        table.Cells().Style.Border.SetOutsideBorder(XLBorderStyleValues.Thin).Fill.SetBackgroundColor(XLColor.White);

                        // Tùy chỉnh style cho header của bảng
                        table.Row(1).Style.Font.SetFontColor(XLColor.FromTheme(XLThemeColor.Text1)).Fill.SetBackgroundColor(XLColor.LightGray).Font.SetFontName("Arial").Font.SetFontSize(10).Font.SetBold(true).Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                        // Căn giữa cho văn bản ở cột E (Ngày sinh)
                        table.Column("E").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
                    }

                    // Thêm 2 dòng phía trên
                    ws.Row(1).InsertRowsAbove(2);

                    // Gán giá trị cho ô A1
                    ws.Cell("A1").SetValue("DANH SÁCH NHÂN VIÊN");

                    // Tùy chỉnh style cho ô A1 và A2
                    ws.Cells("A1,A2").Style.Font.SetBold(true).Font.SetFontSize(16).Font.SetFontName("Arial").Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);

                    // Merge từ A1 đến I1, từ A2 đến I2
                    ws.Range("A1:I1").Merge();
                    ws.Range("A2:I2").Merge();

                    using (MemoryStream stream = new MemoryStream())
                    {
                        wb.SaveAs(stream);
                        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Danh_sach_nhan_vien" + DateTime.Now.ToString("dd-MM-yyyy hh-mm-ss") + ".xlsx");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
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

        //private ServiceResponse ValidateRequestData(Employee employee)
        //{
        //    var properties = typeof(Employee).GetProperties();
        //    var validateFailures = new List<string>();
        //    foreach(var property in properties)
        //    {
        //        var propertyValue = property.GetValue(employee);
        //        var requiredAttribute = (RequiredAttribute?)Attribute.GetCustomAttribute(property, typeof(RequiredAttribute));
        //        if(requiredAttribute != null && string.IsNullOrEmpty(propertyValue?.ToString()))
        //        {
        //            validateFailures.Add(requiredAttribute.ErrorMessage);
        //        }
        //    }

        //    if(validateFailures.Count > 0)
        //    {
        //        return new ServiceResponse
        //        {
        //            Success = false
        //        }
        //    }
        //}

        #endregion
    }
}
