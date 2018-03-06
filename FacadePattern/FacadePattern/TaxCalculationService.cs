using System;

namespace FacadePattern
{
    public class TaxCalculationService : ITaxCalculationService
    {
        public decimal GetTaxRate(Province province)
        {
            switch (province)
            {
                case Province.Alberta:
                    return 0.05m;

                case Province.Saskatchewan:
                    return 0.11m;

                case Province.BritishColumbia:
                    return 0.12m;

                case Province.Ontario:
                case Province.Manitoba:
                    return 0.13m;

                case Province.Quebec:
                    return 0.14975m;

                case Province.NewBrunswick:
                case Province.NovaScotia:
                case Province.NewfoundlandLabrador:
                case Province.PrinceEdwardIsland:
                    return 0.15m;
                default:
                    throw new ArgumentOutOfRangeException(nameof(province), province, null);
            }
        }
    }
}