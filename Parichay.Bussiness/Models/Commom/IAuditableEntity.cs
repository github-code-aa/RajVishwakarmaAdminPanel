using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Parichay.Bussiness.Model
{
   public interface IAuditableEntity
    {
        DateTime CreatedDate { get; set; }
        DateTime? ModifiedDate { get; set; }
        int CreatedBy { get; set; }
        int? ModifiedBy { get; set; }
        bool IsActive { get; set; }
        bool? IsDeleted { get; set; }
    }
}
