using SalaryContracts;

namespace SalaryBuinessLayer
{
    public class TaxedPayslipGenerator
    {
        private TaxStrategyAbstract taxStrategy;
        public void SetTaxStrategy(TaxStrategyAbstract taxStrategy)
        {
            this.taxStrategy = taxStrategy;
        }

        public IPayslip GeneratePayslip(ITaxableEmployee taxableEmployee)
        {
            return this.taxStrategy.CreateTaxedPayslip(taxableEmployee);
        }
    }
}
