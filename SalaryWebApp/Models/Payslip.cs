using SalaryContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SalaryWebApp.Models
{
    public class Payslip : IPayslip
    {
        public double GrossIncome { get; set; }

        public double IncomeTax { get; set; }

        public double NetIncome { get; set; }

        public double SuperAmount { get; set; }

    }
}