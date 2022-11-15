using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.AMIS.KeToan.Common.Entities;
using MySqlConnector;
using System.Data;
using Dapper;
using MISA.AMIS.KeToan.DL;
using MISA.AMIS.KeToan.BL;

namespace MISA.AMIS.KeToan.API.Controllers
{
    [ApiController]
    public class DepartmentsController : BasesController<Department>
    {
        #region Field

        //private IDepartmentBL _departmentBL;

        #endregion

        #region Constructor

        public DepartmentsController(IBaseBL<Department> baseBL) : base(baseBL)
        {
        }

        #endregion
    }
}
