using CreditCalculator.Interfaces;
using CreditCalculator.Models;
using System.Globalization;

namespace CreditCalculator.Services
{
    public class LoanCalculation : ILoanCalculation
    {
        public List<LoanPayment> Calculation(EntryDataModel model)
        {
            var amount = model.Amount;
            var term = model.Term;
            var rate = (model.Rate / 100) / 12; 

            double mounthlyPayment = amount * (rate * Math.Pow(1 + rate, term)) / (Math.Pow(1 + rate, term) - 1);

            List<LoanPayment> loanPayments = new List<LoanPayment>();

            for (var i = 0; i < term; i++)
            {
                var loanPayment = new LoanPayment();

                if (loanPayments.Count == 0)
                {
                    loanPayment.PaymentNumber = i + 1;
                    loanPayment.PaymentDate = DateTime.Now.ToShortDateString();
                    loanPayment.AmmountPayment = Math.Round( mounthlyPayment,2);
                    loanPayment.BalanceDebt = amount;
                    loanPayment.Rate = Math.Round(amount * rate, 2);
                    loanPayment.PrincipalDebt = Math.Round(mounthlyPayment - (amount * rate), 2);

                    loanPayments.Add(loanPayment);
                }
                else
                {
                    loanPayment.PaymentNumber = i + 1;

                    var date = DateTime.ParseExact(loanPayments[i - 1].PaymentDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                    loanPayment.PaymentDate = date.AddMonths(1).ToShortDateString();
                    loanPayment.AmmountPayment = Math.Round(mounthlyPayment, 2);
                    loanPayment.BalanceDebt = Math.Round(loanPayments[i - 1].BalanceDebt - loanPayments[i - 1].PrincipalDebt, 2);
                    loanPayment.Rate = Math.Round(loanPayment.BalanceDebt * rate, 2);
                    loanPayment.PrincipalDebt = Math.Round(mounthlyPayment - loanPayment.Rate, 2);

                    loanPayments.Add(loanPayment);
                }

            }

            return loanPayments;
        }

        public List<LoanPayment> Calculation(EntryDataModelWithStep model)
        {
            var ammount = model.Amount;
            var term = model.Term;
            var rate = (model.Rate / 100);
            var step = model.Step;


            int m = (int)Math.Ceiling(term / step);

            List<LoanPayment> loanPayments = new List<LoanPayment>();


            double steplyPaiment = ammount * (rate * Math.Pow(1 + rate, m)) / (Math.Pow(1 + rate, m) - 1);



            double remainingDebt = ammount;

            var paymentNumber = 0;

            for (int day = step; day <= term; day += step)
            {
                paymentNumber++;
                var loanPayment = new LoanPayment();

                double interest = remainingDebt * rate; 
                double principal = steplyPaiment - interest;      

                remainingDebt -= principal; 

                loanPayment.AmmountPayment = steplyPaiment;
                loanPayment.Rate = Math.Round(interest, 2);

                if (loanPayments.Count == 0)
                {
                    loanPayment.PaymentNumber = paymentNumber;
                    loanPayment.PaymentDate = DateTime.Now.ToShortDateString();
                    loanPayment.AmmountPayment = Math.Round(steplyPaiment,2);
                    loanPayment.BalanceDebt = ammount;
                    loanPayment.Rate = Math.Round(interest, 2);
                    loanPayment.PrincipalDebt = Math.Round(principal, 2);

                    loanPayments.Add(loanPayment);
                }
                else
                {
                    loanPayment.PaymentNumber = paymentNumber;
                    var date = DateTime.ParseExact(loanPayments[paymentNumber - 2].PaymentDate, "dd.MM.yyyy", CultureInfo.InvariantCulture);

                    loanPayment.PaymentDate = date.AddDays(step).ToShortDateString();
                    loanPayment.AmmountPayment = Math.Round(steplyPaiment, 2);
                    loanPayment.BalanceDebt = Math.Round(loanPayments[paymentNumber - 2].BalanceDebt - loanPayments[paymentNumber - 2].PrincipalDebt, 2);
                    loanPayment.Rate = Math.Round(loanPayment.BalanceDebt * rate, 2);
                    loanPayment.PrincipalDebt = Math.Round(principal, 2);

                    loanPayments.Add(loanPayment);
                }

            }
            return loanPayments;

        }
    }
}


        
    
    

