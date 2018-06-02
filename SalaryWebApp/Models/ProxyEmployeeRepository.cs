using SalaryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalaryWebApp.Models
{
    public class ProxyEmployeeRepository : IProxyRepo<ITaxableEmployee>
    {
        public ProxyEmployeeRepository()
        {
            Entities = new List<ITaxableEmployee>();
        }

        public List<ITaxableEmployee> Entities { get; set; }
    }
}