﻿namespace MISA.AMIS.KeToan.Common.Entities
{
    /// <summary>
    /// Kết quả trả về của API Lấy danh sách nhân viên theo bộ lọc và phân trang
    /// </summary>
    public class PagingResult<T>
    {
        /// <summary>
        /// Danh sách nhân viên
        /// </summary>
        public List<dynamic> Data { get; set; }

        /// <summary>
        /// Tổng số bản ghi
        /// </summary>
        public long totalRecords { get; set; }

        /// <summary>
        /// Tổng số trang
        /// </summary>
        public decimal totalPages { get; set; }
    }
}
