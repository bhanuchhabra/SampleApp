using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalaryContracts
{
    public interface IPayslip
    {
        double GrossIncome { get; set; }

        double IncomeTax { get; set; }

        double NetIncome { get; set; }

        double SuperAmount { get; set; }
    }
}
