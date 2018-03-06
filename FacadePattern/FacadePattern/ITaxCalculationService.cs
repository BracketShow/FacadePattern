namespace FacadePattern
{
    public interface ITaxCalculationService
    {
        decimal GetTaxRate(Province province);
    }
}