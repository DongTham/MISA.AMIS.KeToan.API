using MISA.AMIS.KeToan.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: NQDong (10/11/2022)
        public dynamic GetAllRecords();

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Created by: NQDong (10/11/2022)
        public dynamic GetRecordByID(Guid recordID);

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
        public PagingResult<T> GetRecordsByFilter(string? keyword, string sort, string order, string ids, int pageSize, int offset);
    }
}
