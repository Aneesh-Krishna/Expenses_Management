
using Expenses.DataAccess.Repositories;
using Expenses.Models;
using Microsoft.AspNetCore.Mvc;

namespace Expenses.Web.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesRepository _expense;

        public ExpensesController(IExpensesRepository expense)
        {
            _expense = expense;
        }

        public IActionResult Index(string searching)
        {
            List<ExpenseModel> lists= new List<ExpenseModel>();
            if(string.IsNullOrEmpty(searching))
            {
                lists=_expense.GetAllExpenses().ToList();
            }
            else 
            {
                lists=_expense.SearchExpenses(searching).ToList();
            }

            return View(lists ?? new List<ExpenseModel>());
        }

        [HttpGet]
        public IActionResult ExpenseData(int id)
        {
            ExpenseModel model = new ExpenseModel();
            if(id > 0)
            {
                model=_expense.GetExpenseById(id);
            }
            return PartialView("_AddEditForm",model);
        }

        [HttpPost]
        public IActionResult ExpenseData(ExpenseModel model)
        {
            if (ModelState.IsValid) // Check if model is valid
            {
                if (model.Id > 0)
                {
                    _expense.Update(model);
                }
                else
                {
                    _expense.Add(model);
                }

                // Return the updated list of expenses
                return RedirectToAction("Index"); // Redirect to Index method
            }

            // If the model state is invalid, return the same view with the current model to display validation errors
            return PartialView("_AddEditForm", model);
        }
        public IActionResult Delete(int id)
        {
            if (id > 0)
            {
                ExpenseModel model = _expense.GetExpenseById(id);

                if (model != null)
                {
                    _expense.Delete(id);
                }
            }

            return View("Index");
        }
    }
}
