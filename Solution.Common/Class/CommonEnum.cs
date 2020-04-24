using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace Parichay.Common
{
   [Serializable]
    public class CommonEnum
    {
        public enum Op
        {
            Equals,
            GreaterThan,
            LessThan,
            GreaterThanOrEqual,
            LessThanOrEqual,
            Contains,
            StartsWith,
            EndsWith
        }
        public enum Gender
        {
            Male,
            Female
        }
    }
}