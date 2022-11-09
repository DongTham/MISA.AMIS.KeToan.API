namespace MISA.AMIS.KeToan.API.Entities
{
    /// <summary>
    /// Đơn vị
    /// </summary>
    public class Department
    {
        /// <summary>
        /// ID của đơn vị
        /// </summary>
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Mã của đơn vị
        /// </summary>
        public string DepartmentCode { get; set; }

        /// <summary>
        /// Tên của đơn vị
        /// </summary>
        public string DepartmentName { get; set; }

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Thời gian sửa gần nhất
        /// </summary>
        public DateTime ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}
