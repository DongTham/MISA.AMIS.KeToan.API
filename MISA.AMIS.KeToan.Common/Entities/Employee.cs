using MISA.AMIS.KeToan.Common.Enums;
using System.ComponentModel.DataAnnotations;
using static MISA.AMIS.KeToan.Common.Attributes.PrimeryKeyAttribute;

namespace MISA.AMIS.KeToan.Common.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee : BaseEntity
    {
        #region Property

        /// <summary>
        /// ID nhân viên
        /// </summary>
        [Key]
        [PropertyName("ID nhân viên")]
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        [NotEmpty]
        [PropertyName("Mã nhân viên")]
        public string? EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        [NotEmpty]
        [PropertyName("Tên nhân viên")]
        public string? EmployeeName { get; set; }

        /// <summary>
        /// Có phải là khách hàng hay không
        /// </summary>
        public bool IsCustomer { get; set; }

        /// <summary>
        /// Có phải là nhà cung cấp hay không
        /// </summary>
        public bool IsSupplier { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        [PropertyName("Giới tính")]
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        [NotEmpty]
        [PropertyName("ID đơn vị")]
        public Guid DepartmentID { get; set; }

        /// <summary>
        /// Số CMND
        /// </summary>
        public long? IdentityNumber { get; set; }

        /// <summary>
        /// Ngày cấp CMND
        /// </summary>
        public DateTime? IdentityIssueDate { get; set; }

        /// <summary>
        /// Nơi cấp CMND
        /// </summary>
        public string? IdentityIssuePlace { get; set; }

        /// <summary>
        /// Chức danh
        /// </summary>
        public string? JobPositionName { get; set; }

        /// <summary>
        /// Địa chỉ
        /// </summary>
        public string? EmployeeAddress { get; set; }

        /// <summary>
        /// Số điện thoại di động
        /// </summary>
        public string? MobilePhone { get; set; }

        /// <summary>
        /// Số điện thoại cố định
        /// </summary>
        public string? TelePhone { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? Email { get; set; }

        /// <summary>
        /// Số tài khoản ngân hàng
        /// </summary>
        public long? BankAccountNumber { get; set; }

        /// <summary>
        /// Tên ngân hàng
        /// </summary>
        public string? BankName { get; set; }

        /// <summary>
        /// Tên chi nhánh ngân hàng
        /// </summary>
        public string? BankBranchName { get; set; }        

        #endregion
    }
}
