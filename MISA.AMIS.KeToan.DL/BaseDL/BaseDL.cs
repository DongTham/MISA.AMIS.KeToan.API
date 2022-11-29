using Dapper;
using MISA.AMIS.KeToan.Common.Constants;
using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public class BaseDL<T> : IBaseDL<T>
    {
        #region Field

        // Khởi tạo kết nối tới DB MySQL
        private readonly string connectionString = DatabaseContext.ConnectionString;

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: NQDong (10/11/2022)
        public dynamic GetAllRecords()
        {
            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.GET_ALL, typeof(T).Name);

            // Khởi tạo kết nối tới DB MySQL
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Thực hiện gọi vào DB

                return mySqlConnection.Query(storeProcedureName, commandType: CommandType.StoredProcedure);
            }
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Created by: NQDong (10/11/2022)
        public dynamic GetRecordByID(Guid recordID)
        {
            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.GET_BY_ID, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();
            parameters.Add($"@{typeof(T).Name}ID", recordID);

            // Khởi tạo kết nối tới DB MySQL
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Thực hiện gọi vào DB
                var record = mySqlConnection.QueryFirstOrDefault(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return record;
            }
        }

        /// <summary>
        /// Lấy danh sách bản ghi theo từ khóa, phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="sort">Cột muốn sắp xếp theo</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="offset">Số bản ghi bỏ qua</param>
        /// <param name="order">Sắp xếp theo tăng dần hoặc giảm dần</param>
        /// <param name="ids">Danh sách giá trị mà muốn đặt lên đầu khi kết quả trả về</param>
        /// <returns>Danh sách thông tin bản ghi và tổng số bản ghi</returns>
        /// Created by: NQDONG (10/11/2022)
        public PagingResult<T> GetRecordsByFilter(string? keyword, string sort, string order, string ids, int pageSize, int offset)
        {
            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.GET_BY_FILTER, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            parameters.Add("@Keyword", keyword);
            parameters.Add("@Sort", sort);
            parameters.Add("@Limit", pageSize);
            parameters.Add("@Offset", offset);
            parameters.Add("Order", order);
            parameters.Add("@IDs", ids);
            parameters.Add("@SortByIDs", $"{typeof(T).Name}ID");

            var records = new List<dynamic>();

            // Khởi tạo kết nối tới DB MySQL
            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Thực hiện gọi vào DB
                var resultReturn = mySqlConnection.QueryMultiple(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
                records = resultReturn.Read<dynamic>().ToList();
                var countList = resultReturn.Read<dynamic>().ToList();
                long totalRecords = countList?.FirstOrDefault()?.TotalRecord;
                decimal totalPages = Math.Ceiling((decimal)totalRecords / pageSize);

                var dataResult = new PagingResult<T> { Data = records, totalPages = totalPages, totalRecords = totalRecords };

                return dataResult;             
            }
        }

        /// <summary>
        /// API kiểm tra mã thêm mới đã tồn tại hay chưa
        /// </summary>
        /// <param name="recordCode">Mã muốn kiểm tra</param>
        /// <param name="recordID">ID nhân viên đã tồn tại để lấy mã nhân viên tương ứng</param>
        /// <returns>Số lượng mã đã tồn tại</returns>
        /// Created by: NQDONG (18/11/2022)
        public long CheckDuplicateCode(string recordCode, Guid recordID)
        {
            // Chuẩn bị câu lệnh SQL
            string storeProcedureName = String.Format(Procedure.CHECK_DUPLICATE_CODE, typeof(T).Name);

            // Chuẩn bị tham số đầu vào
            var parameters = new DynamicParameters();

            parameters.Add($"@{typeof(T).Name}Code", recordCode);
            parameters.Add($"@{typeof(T).Name}ID", recordID);

            using (var mySqlConnection = new MySqlConnection(connectionString))
            {
                // Thực hiện gọi vào DB
                var numberOfDuplicate = mySqlConnection.QueryFirstOrDefault(storeProcedureName, parameters, commandType: CommandType.StoredProcedure);
                return numberOfDuplicate.CountDuplicate;
            }
        }

        #endregion
    }
}
