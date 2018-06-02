using SalaryContracts;
using System;

namespace SalaryBuinessLayer
{
    public sealed class TaxStrategy2017 : TaxStrategyAbstract
    {
        public TaxStrategy2017(int taxYear) : base(taxYear)
        {

        }
        public override IPayslip CreateTaxedPayslip(ITaxableEmployee taxableEmployee)
        {
            var payslip = (new PayslipFactory(this.TaxYear)).CreatePayslip(taxableEmployee);

            switch (taxableEmployee.AnualSalary)
            {
                //C# 7.1
                case int salary when (salary < 18200):
                default:

                    payslip.GrossIncome = Math.Round(taxableEmployee.AnualSalary / 12.0, MidpointRounding.AwayFromZero);
                    payslip.IncomeTax = 0;
                    payslip.SuperAmount = Math.Round(taxableEmployee.SuperRate * payslip.GrossIncome / 100, MidpointRounding.AwayFromZero);
                    payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;

                    break;
                case int salary when (salary > 18200 && salary < 37000):
                    payslip.GrossIncome = Math.Round(taxableEmployee.AnualSalary / 12.0, MidpointRounding.AwayFromZero);
                    payslip.IncomeTax = Math.Round(((taxableEmployee.AnualSalary - 18200) * 0.19) / 12.0, MidpointRounding.AwayFromZero);
                    payslip.SuperAmount = Math.Round(taxableEmployee.SuperRate * payslip.GrossIncome / 100, MidpointRounding.AwayFromZero);
                    payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
                    break;
                case int salary when (salary > 37000 && salary < 87000):
                    payslip.GrossIncome = Math.Round(taxableEmployee.AnualSalary / 12.0, MidpointRounding.AwayFromZero);
                    payslip.IncomeTax = Math.Round((3572 + (taxableEmployee.AnualSalary - 37000) * .325) / 12.0, MidpointRounding.AwayFromZero);
                    payslip.SuperAmount = Math.Round(taxableEmployee.SuperRate * payslip.GrossIncome / 100, MidpointRounding.AwayFromZero);
                    payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
                    break;
                case int salary when (salary > 87000 && salary < 180000):
                    payslip.GrossIncome = Math.Round(taxableEmployee.AnualSalary / 12.0, MidpointRounding.AwayFromZero);
                    payslip.IncomeTax = Math.Round((19822 + (taxableEmployee.AnualSalary - 87000) * .37) / 12.0, MidpointRounding.AwayFromZero);
                    payslip.SuperAmount = Math.Round(taxableEmployee.SuperRate * payslip.GrossIncome / 100, MidpointRounding.AwayFromZero);
                    payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;

                    break;
                case int salary when (salary > 180000):
                    payslip.GrossIncome = Math.Round(taxableEmployee.AnualSalary / 12.0, MidpointRounding.AwayFromZero);
                    payslip.IncomeTax = Math.Round((54232 + (taxableEmployee.AnualSalary - 180000) * .45) / 12.0, MidpointRounding.AwayFromZero);
                    payslip.SuperAmount = Math.Round(taxableEmployee.SuperRate * payslip.GrossIncome / 100, MidpointRounding.AwayFromZero);
                    payslip.NetIncome = payslip.GrossIncome - payslip.IncomeTax;
                    break;
            }

            return payslip;
        }
    }
}
