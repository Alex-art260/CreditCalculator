using System.ComponentModel.DataAnnotations;

namespace CreditCalculator.Models
{
    public class EntryDataModel
    {
        [Required(ErrorMessage = "Сумма займа обязательна.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Сумма займа должна быть больше 0.")]
        public int Amount { get; set; }

        [Required(ErrorMessage = "Срок займа обязателен.")]
        [Range(1, int.MaxValue, ErrorMessage = "Срок займа должен быть больше 0.")]
        public int Term {  get; set; }

        [Required(ErrorMessage = "Процентная ставка обязательна.")]
        [Range(0, 100, ErrorMessage = "Процентная ставка должна быть в диапазоне от 0 до 100.")]
        public float Rate { get; set; } 
    }
}
