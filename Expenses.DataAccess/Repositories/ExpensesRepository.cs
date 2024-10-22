using Expenses.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses.DataAccess.Repositories
{
    public class ExpensesRepository : IExpensesRepository
    {
        private readonly ApplicationDbContext _context;

        public ExpensesRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public void Add(ExpenseModel expense)
        {
            try
            {
                _context.ExpensesTable.Add(expense);
                _context.SaveChanges();
            }
            catch(Exception) 
            {
                throw;
            }
        }

        public void Delete(int id)
        {
            try
            {
                var expense=_context.ExpensesTable.Find(id);
                if (expense != null)
                {
                    _context.ExpensesTable.Remove(expense);
                    _context.SaveChanges();
                }
            }
            catch(Exception)
            {
                throw;
            }
        }

        public IEnumerable<ExpenseModel> GetAllExpenses()
        {
            try
            {
                var expenses = _context.ExpensesTable.ToList();
                return expenses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ExpenseModel GetExpenseById(int id)
        {
            try
            {
                var expenses= _context.ExpensesTable.Find(id);
                return expenses;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<ExpenseModel> SearchExpenses(string searchString)
        {
            try
            {
                var searchExpense = GetAllExpenses().Where(x => x.Title.ToLower().Contains(searchString));
                return searchExpense;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public int Update(ExpenseModel expense)
        {
            try
            {
                _context.Entry(expense).State=Microsoft.EntityFrameworkCore.EntityState.Modified;
                _context.SaveChanges();
                return 1;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
