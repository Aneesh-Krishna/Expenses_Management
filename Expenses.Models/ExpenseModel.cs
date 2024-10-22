using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Expenses.Models
{
    public class ExpenseModel
    {
        public int Id { get; set; }
        [Required]

        public string? Title { get; set; }

        [Required]
        public decimal MoneySpent { get; set; }

        [Required]
        public DateTime ExpenseDate { get; set; }

        [Required]
        public string? Category { get; set; }
    }
}
