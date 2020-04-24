using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Parichay.Bussiness.Model
{
    [System.Serializable]
    public abstract class AuditableEntity<T> : Entity<T>, IAuditableEntity
    {
        public virtual DateTime CreatedDate { get; set; }
        public virtual DateTime? ModifiedDate { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual int? ModifiedBy { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual bool? IsDeleted { get; set; }
    }
}
