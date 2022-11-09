using MISA.AMIS.KeToan.API.Enums;

namespace MISA.AMIS.KeToan.API.Entities
{
    /// <summary>
    /// Nhân viên
    /// </summary>
    public class Employee
    {
        /// <summary>
        /// ID nhân viên
        /// </summary>
        public Guid EmployeeID { get; set; }

        /// <summary>
        /// Mã nhân viên
        /// </summary>
        public string EmployeeCode { get; set; }

        /// <summary>
        /// Tên nhân viên
        /// </summary>
        public string EmployeeName { get; set; }

        /// <summary>
        /// Có phải là khách hàng hay không
        /// </summary>
        public bool IsCustomer { get; set; }

        /// <summary>
        /// Có phải là nhà cung cấp hay không
        /// </summary>
        public bool IsSuplier { get; set; }

        /// <summary>
        /// Giới tính
        /// </summary>
        public Gender? Gender { get; set; }

        /// <summary>
        /// Ngày sinh
        /// </summary>
        public DateTime? DateOfBirth { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
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

        /// <summary>
        /// Thời gian tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Thời gian sửa gần nhất
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa gần nhất
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
