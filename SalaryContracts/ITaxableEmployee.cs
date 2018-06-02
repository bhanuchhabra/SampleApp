using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryContracts
{
    public interface ITaxableEmployee
    {
        [JsonProperty("FirstName")]
        string FirstName { get; set; }

        [JsonProperty("LastName")]
        string LastName { get; set; }

        [JsonProperty("AnualSalary")]
        int AnualSalary { get; set; }

        [JsonProperty("SuperRate")]
        int SuperRate { get; set; }

        [JsonProperty("PaymentStartDate")]
        DateTime PaymentStartDate { get; set; }
    }
}
