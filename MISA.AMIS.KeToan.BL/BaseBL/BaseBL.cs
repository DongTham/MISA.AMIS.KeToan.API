using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Enums;
using MISA.AMIS.KeToan.Common.Exceptions;
using MISA.AMIS.KeToan.Common.Resources;
using MISA.AMIS.KeToan.DL;
using static MISA.AMIS.KeToan.Common.Attributes.PrimeryKeyAttribute;

namespace MISA.AMIS.KeToan.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field

        private IBaseDL<T> _baseDL;

        #endregion

        #region Constructor

        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// Lấy danh sách tất cả bản ghi
        /// </summary>
        /// <returns>Danh sách tất cả bản ghi</returns>
        /// Created by: NQDong (10/11/2022)
        public dynamic GetAllRecords()
        {
            return _baseDL.GetAllRecords();
        }

        /// <summary>
        /// Lấy thông tin 1 bản ghi theo ID
        /// </summary>
        /// <param name="recordID">ID của bản ghi muốn lấy</param>
        /// <returns>Thông tin 1 bản ghi theo ID</returns>
        /// Created by: NQDong (10/11/2022)
        public dynamic GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// Lấy danh sách bản ghi theo từ khóa, phân trang
        /// </summary>
        /// <param name="keyword">Từ khóa muốn tìm kiếm</param>
        /// <param name="sort">Cột muốn sắp xếp theo</param>
        /// <param name="pageSize">Số bản ghi muốn lấy</param>
        /// <param name="pageNumber">Số chỉ mục của trang muốn lấy</param>
        /// <param name="order">Sắp xếp theo tăng dần hoặc giảm dần</param>
        /// <param name="ids">Danh sách giá trị mà muốn đặt lên đầu khi kết quả trả về</param>
        /// <returns>Danh sách thông tin bản ghi và tổng số bản ghi</returns>
        /// Created by: NQDONG (10/11/2022)
        public PagingResult<T> GetRecordsByFilter(string? keyword, string? sort, string? order, string? ids, int pageSize, int pageNumber)
        {
            int offset = pageSize * (pageNumber - 1);
            if (string.IsNullOrEmpty(sort))
            {
                sort = $"{typeof(T).Name}ID";
            }
            if (string.IsNullOrEmpty(ids))
            {
                ids = "NoID";
            }

            return _baseDL.GetRecordsByFilter(keyword, sort, order, ids,  pageSize, offset);
        }

        /// <summary>
        /// API kiểm tra mã thêm mới đã tồn tại hay chưa
        /// </summary>
        /// <param name="recordCode">Mã muốn kiểm tra</param>
        /// <param name="recordID">ID nhân viên đã tồn tại để lấy mã nhân viên tương ứng</param>
        /// <returns>Số lượng mã đã tồn tại</returns>
        /// Created by: NQDONG (18/11/2022)
        public bool CheckDuplicateCode(string recordCode, Guid recordID)
        {
            var result = _baseDL.CheckDuplicateCode(recordCode, recordID);

            if (result == 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        /// <summary>
        /// Kiểm tra dữ liệu nhập vào có hợp lệ hay không
        /// </summary>
        /// <param name="record">dữ liệu người dùng nhập vào</param>
        /// <exception cref="ValidateException">Exception lỗi dữ liệu không hợp lệ</exception>
        public void ValidateData(T record)
        {
            var props = record?.GetType().GetProperties();
            var propNotEmpty = props?.Where(p => Attribute.IsDefined(p, typeof(NotEmpty)));

            foreach (var prop in propNotEmpty)
            {
                var propValue = prop.GetValue(record);
                var propName = prop.Name;
                var nameDisplay = string.Empty;
                var propertyNames = prop.GetCustomAttributes(typeof(PropertyName), true);
                if (propertyNames.Length > 0)
                {
                    nameDisplay = (propertyNames[0] as PropertyName).Name;
                }
                if (propValue == null || string.IsNullOrEmpty(propValue.ToString()))
                {
                    nameDisplay = (nameDisplay == string.Empty ? propName : nameDisplay);
                    throw new ValidateException(string.Format(Resources.InfoNotEmpty, nameDisplay), ErrorCode.InvalidData);
                }
            }
        }

        #endregion
    }
}
