using CreditCalculator.Interfaces;
using CreditCalculator.Models;
using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CreditCalculator.Controllers
{
    public class LoanController : Controller
    {
        private readonly ILoanCalculation _loanCalculation;

        public LoanController(ILoanCalculation loanCalculation)
        {
            _loanCalculation = loanCalculation;
        }


        [HttpGet]
        public IActionResult Index()
        {
            string loanPaymentsJson = Request.Query["loanPayments"]!;  
            if (!string.IsNullOrEmpty(loanPaymentsJson))
            {
                List<LoanPayment> loanPayments = JsonConvert.DeserializeObject<List<LoanPayment>>(loanPaymentsJson)!;  
                return View(loanPayments);
            }
            else
            {
                return View(); 
            }
        }

        [HttpPost]
        public IActionResult CalculateLoan(EntryDataModel model)
        {
            if (ModelState.IsValid) 
            {
                var loanData = _loanCalculation.Calculation(model);
                return Json(loanData);
            }
            else
            {
                return View(model); 
            }
        }

        [HttpPost]
        public IActionResult CalculateDaysLoan(EntryDataModelWithStep model)
        {
            if (ModelState.IsValid)
            {
                var loanData = _loanCalculation.Calculation(model);
                return Json(loanData);
            }
            else
            {
                return View(model);
            }
        }
    }
}
