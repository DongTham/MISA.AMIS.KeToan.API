using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.BL;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Enums;
using MISA.AMIS.KeToan.Common.Resources;
using MISA.AMIS.KeToan.DL;
using MySqlConnector;
using System.Data;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field

        private IBaseBL<T> _baseBL;

        #endregion

        #region Constructor

        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy danh sách thông tin tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách thông tin tất cả bản ghi</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet]
        public IActionResult GetAllRecords()
        {
            try
            {
                var records = _baseBL.GetAllRecords();

                // Xử lý kết quả trả về
                if (records != null)
                {
                    return StatusCode(StatusCodes.Status200OK, records);
                }

                return StatusCode(StatusCodes.Status200OK, new List<T>());
            }
            catch (Exception ex)
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
        /// API Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi cần lấy</param>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                var record = _baseBL.GetRecordByID(recordID);

                // Xử lý kết quả trả về
                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }

                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch (Exception ex)
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
        /// API Lấy danh sách thông tin bản ghi theo bộ lọc và phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <param name="sort">Cột muốn sắp xếp theo</param>
        /// <param name="order">Sắp xếp theo tăng dần hoặc giảm dần</param>
        /// <param name="ids">Danh sách giá trị mà muốn đặt lên đầu khi kết quả trả về</param>
        /// <returns>Danh sách thông tin bản ghi và tổng số bản ghi</returns>
        /// Created by: NQDONG (06/11/2022)
        [HttpGet("filter")]
        public IActionResult GetRecordsByFilter(
            [FromQuery] string? keyword,
            [FromQuery] string? sort,
            [FromQuery] string? ids,
            [FromQuery] string? order = "ASC", 
            [FromQuery] int pageSize = 25,
            [FromQuery] int pageNumber = 1
            )
        {
            try
            {
                var dataResult = _baseBL.GetRecordsByFilter(keyword, sort, order, ids, pageSize, pageNumber);

                return StatusCode(StatusCodes.Status200OK, new { dataResult });
                // Xử lý kết quả trả về
                //if (records.Count > 0)
                //{
                //    return StatusCode(StatusCodes.Status200OK, new { records, totalRecord, totalPage });
                //}

                //return StatusCode(StatusCodes.Status200OK, new { records = new List<Record>(), totalRecord, totalPage });
            }
            catch (Exception ex)
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
        /// API kiểm tra mã thêm mới đã tồn tại hay chưa
        /// </summary>
        /// <param name="recordCode">Mã muốn kiểm tra</param>
        /// <param name="recordID">ID nhân viên đã tồn tại để lấy mã nhân viên tương ứng</param>
        /// <returns>Số lượng mã đã tồn tại</returns>
        /// Created by: NQDONG (18/11/2022)
        [HttpGet("checkDuplicateCode")]
        public IActionResult CheckDuplicateCode([FromQuery] string recordCode, [FromQuery] Guid recordID)
        {
            try
            {
                var dataResult = _baseBL.CheckDuplicateCode(recordCode, recordID);
                return StatusCode(StatusCodes.Status200OK, new { IsDuplicateCode = dataResult });
            }
            catch (Exception ex)
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
