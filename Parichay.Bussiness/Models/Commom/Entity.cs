using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Parichay.Bussiness.Model
{
    [Serializable]
    public abstract partial class BaseEntity
    {
    }
    [Serializable]
    public abstract class Entity<T> : BaseEntity, IEntity<T>
    {
        public virtual T Id { get; set; }
    }
}
