namespace CreditCalculator.Models
{
    public class LoanPayment
    {
        public int PaymentNumber { get; set; } 
        public string? PaymentDate { get; set; } 
        public double AmmountPayment { get; set; }
        public double PrincipalDebt { get; set; }
        public double Rate { get; set; }
        public double BalanceDebt { get; set; } 

    }
}
