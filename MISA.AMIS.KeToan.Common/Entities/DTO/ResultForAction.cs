using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.AMIS.KeToan.Common.Entities.DTO
{
    public class ResultForAction
    {
        public int NumberOfRowsAffected { get; set; }

        public Guid? RecordID { get; set; }
    }
}
