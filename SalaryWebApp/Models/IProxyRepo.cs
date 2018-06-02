using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryWebApp.Models
{
    public interface IProxyRepo<T>
    {
        List<T> Entities { get; set; }
    }
}
