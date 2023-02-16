using ClosedXML.Excel;
using DocumentFormat.OpenXml.Drawing.Charts;
using DocumentFormat.OpenXml.Office2010.Excel;
using DocumentFormat.OpenXml.Wordprocessing;
using MISA.AMIS.KeToan.Common.Entities;
using MISA.AMIS.KeToan.Common.Entities.DTO;
using MISA.AMIS.KeToan.Common.Enums;
using MISA.AMIS.KeToan.DL;
using System.Data;
using DataTable = System.Data.DataTable;

namespace MISA.AMIS.KeToan.BL
{
    public class EmployeeBL : BaseBL<Employee>, IEmployeeBL
    {
        #region Field

        private IEmployeeDL _employeeDL;

        #endregion

        #region Constructor

        public EmployeeBL(IEmployeeDL employeeDL) : base(employeeDL)
        {
            _employeeDL = employeeDL;
        }

        #endregion

        #region Method

        /// <summary>
        /// API Lấy mã nhân viên lớn nhất
        /// </summary>
        /// <returns>Mã nhân viên lớn nhất</returns>
        /// Created by: NQDONG (10/11/2022)
        public dynamic? GetBiggestEmployeeCode()
        {
            return _employeeDL.GetBiggestEmployeeCode();
        }

        /// <summary>
        /// API Thêm mới 1 nhân viên
        /// </summary>
        /// <param name="employee">Đối tượng nhân viên cần thêm mới</param>
        /// <returns>Số bản ghi ảnh hưởng</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction InsertEmployee(Employee employee)
        {
            //Kiểm tra dữ liệu nhập vào có hợp lệ hay không
            ValidateData(employee);

            return _employeeDL.InsertEmployee(employee);
        }

        /// <summary>
        /// API Sửa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần sửa</param>
        /// <param name="employee">Đối tượng nhân viên cần sửa</param>
        /// <returns>ID của nhân viên vừa sửa, số bản ghi ảnh hưởng</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction UpdateEmpoyee(Guid employeeID, Employee employee)
        {
            //Kiểm tra dữ liệu nhập vào có hợp lệ hay không
            ValidateData(employee);

            return _employeeDL.UpdateEmpoyee(employeeID, employee);
        }

        /// <summary>
        /// API Xóa 1 nhân viên theo ID
        /// </summary>
        /// <param name="employeeID">ID của nhân viên cần xóa</param>
        /// <returns>ID của nhân viên vừa xóa</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction DeleteEmployee(Guid employeeID)
        {
            return _employeeDL.DeleteEmployee(employeeID);
        }

        /// <summary>
        /// API Xóa nhiều nhân viên theo danh sách ID
        /// </summary>
        /// <param name="listEmployeeID">Danh sách ID của các nhân viên muốn xóa</param>
        /// <returns>Status code 200</returns>
        /// Created by: NQDONG (10/11/2022)
        public ResultForAction DeleteMultipleEmployees(ListEmployeeID listEmployeeID)
        {
            string employeeIDText = "";

            foreach (var el in listEmployeeID.EmployeeIDs.Select((x, i) => new { Value = x, Index = i }))
            {
                employeeIDText += el.Value + ",";
            }

            return _employeeDL.DeleteMultipleEmployees(employeeIDText);
        }

        /// <summary>
        /// API Xuất khẩu file excel danh sách tất cả nhân viên
        /// </summary>
        /// <returns>Dữ liệu DataTable để xuất file</returns>
        /// Created by: NQDONG (18/11/2022)
        public XLWorkbook ExportEmployeesToExcel(string? keyword, string? sort, string? order, string? ids, int pageSize, int pageNumber)
        {
            // Lấy danh sách tất cả nhân viên
            //var employees = _employeeDL.GetAllRecords();
            int offset = pageSize * (pageNumber - 1);
            if (string.IsNullOrEmpty(sort))
            {
                sort = $"EmployeeID";
            }
            if (string.IsNullOrEmpty(ids))
            {
                ids = "NoID";
            }

            var employees = _employeeDL.GetRecordsByFilter(keyword, sort, order, ids, pageSize, offset);

            // using System.Data;
            DataTable dataTable = new()
            {
                TableName = "DANH SÁCH NHÂN VIÊN"
            };

            // Thêm 1 hàng tên cột của bảng
            dataTable.Columns.AddRange(new DataColumn[9] {new DataColumn("STT"), new DataColumn("Mã nhân viên"), new DataColumn("Tên nhân viên"), new DataColumn("Giới tính"), new DataColumn("Ngày sinh"), new DataColumn("Chức danh"), new DataColumn("Tên đơn vị"), new DataColumn("Số tài khoản"), new DataColumn("Tên ngân hàng") });

            int indexOfEmployee = 1;
            foreach (var employee in employees.Data)
            {
                // Thêm dữ liệu của từng nhân viên cho 1 hàng
                dataTable.Rows.Add(indexOfEmployee, employee.EmployeeCode, employee.EmployeeName, ConvertToGenderVietnamese(employee.Gender), employee.DateOfBirth?.ToString("dd-MM-yyyy"), employee.JobPositionName, employee.DepartmentName, employee.BankAccountNumber, employee.BankName);
                indexOfEmployee++;
            }

            //using ClosedXML.Excel;
            XLWorkbook wb = new XLWorkbook();
            
            var ws = wb.AddWorksheet(dataTable);

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

            return wb;
        }

        private string? ConvertToGenderVietnamese(int? gender)
        {
            switch (gender)
            {
                case 0:
                    return "Nam";
                case 1:
                    return "Nữ";
                case 2:
                    return "Khác";
                default:
                    return gender.ToString();
            }
        }

        #endregion
    }
}
