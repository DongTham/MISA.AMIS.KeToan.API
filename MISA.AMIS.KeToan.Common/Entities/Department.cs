namespace MISA.AMIS.KeToan.Common.Entities
{
    /// <summary>
    /// Đơn vị
    /// </summary>
    public class Department : BaseEntity
    {
        #region Property

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

        #endregion
    }
}
