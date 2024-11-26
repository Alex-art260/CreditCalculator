using CreditCalculator.Models;

namespace CreditCalculator.Interfaces
{
    public interface ILoanCalculation
    {
        List<LoanPayment> Calculation(EntryDataModel model);
        List<LoanPayment> Calculation(EntryDataModelWithStep model);
    }
}
