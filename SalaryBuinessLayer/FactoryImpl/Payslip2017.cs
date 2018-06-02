using Newtonsoft.Json;
using SalaryContracts;
using System;

namespace SalaryBuinessLayer.SalaryContractsImpl
{
    public sealed class Payslip2017 : IPayslip
    {
        [JsonProperty("FullName")]
        public string FullName { get; set; }

        [JsonProperty("PayPeriod")]
        public DateTime PayPeriod { get; set; }

        [JsonProperty("GrossIncome")]
        public double GrossIncome { get; set; }

        [JsonProperty("IncomeTax")]
        public double IncomeTax { get ; set ; }

        [JsonProperty("NetIncome")]
        public double NetIncome { get ; set ; }

        [JsonProperty("SuperAmount")]
        public double SuperAmount { get ; set ; }

        public Payslip2017()
        {

        }
    }
}
