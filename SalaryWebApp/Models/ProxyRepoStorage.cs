using SalaryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalaryWebApp.Models
{
    public static class ProxyRepoStorage
    {
        public static IProxyRepo<ITaxableEmployee> Repo { get; set; }
    }
}