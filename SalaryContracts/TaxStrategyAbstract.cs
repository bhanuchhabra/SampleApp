namespace SalaryContracts
{
    public abstract class TaxStrategyAbstract
    {
        public TaxStrategyAbstract(int taxYear)
        {
            this.TaxYear = taxYear;
        }

        public int TaxYear { get; set; }
        public abstract IPayslip CreateTaxedPayslip(ITaxableEmployee taxableEmployee);
    }
}
