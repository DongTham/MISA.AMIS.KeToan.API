using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Attributes
{
    public sealed class PrimeryKeyAttribute : Attribute
    {
        #region Field

        public string? ErrorMessage { get; set; }

        #endregion

        #region Constructor 

        public PrimeryKeyAttribute(string? errorMessage)
        {
            ErrorMessage = errorMessage;
        } 

        #endregion
    }
}
