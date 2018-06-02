using SalaryBuinessLayer.SalaryContractsImpl;
using SalaryContracts;

namespace SalaryBuinessLayer
{
    public class PayslipFactory
    {
        public readonly int taxYear;

        public PayslipFactory(int taxYear)
        {
            this.taxYear = taxYear;
        }

        public IPayslip CreatePayslip(ITaxableEmployee taxableEmployee)
        {
            switch (taxYear)
            {
                case 2017:
                default:
                    return new Payslip2017()
                    {
                        FullName = string.Format("{0} {1}", taxableEmployee.FirstName, taxableEmployee.LastName),
                        PayPeriod = taxableEmployee.PaymentStartDate,
                    };
            }
        }
    }
}
