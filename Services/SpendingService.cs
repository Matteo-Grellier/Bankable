using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class SpendingService
{
	BankableContext bankableContext = new();

	public async Task<IEnumerable<Spending>> GetAllItems()
	{
		try
		{
			IEnumerable<Spending> Spendings;
			Spendings = await bankableContext.Spendings.ToListAsync();
			return Spendings;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<Spending> GetItemByID(Guid id)
	{
		try
		{
			var spending = await bankableContext.Spendings.SingleAsync(e => e.Id == id);
			return spending;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<List<Spending>> GetAllInMonth(DateTime date)
	{
		var spending = await bankableContext.Spendings.Where(
			e => e.Date.Month == date.Date.Month
				 && e.Date.Year == date.Date.Year && e.BankAccount.UserId == BankableContext.CurrentConnectedUser.Id).ToListAsync();
		return spending;
	}

	public async Task<IEnumerable<Spending>> GetAllSpendingsByCategorInMonth(Category category, DateTime? month)
	{
		try
		{
			var spending = await bankableContext.Spendings.Where(e => e.Date.Month == month.Value.Month && category.Id == e.CategoryId).ToListAsync();
			return spending;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<IEnumerable<Spending>> GetAllSpendingsInMonth(int month)
	{
		try
		{
			var spending = await bankableContext.Spendings.Where(e => e.Date.Month == month).ToListAsync();
			return spending;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
	public async Task<IEnumerable<Spending>> GetAllRecurringSpendingsInMonth(int month)
	{
		try
		{
			var spendings = await bankableContext.Spendings.Where(e => e.Date.Month == month || e.IsRecurring).ToListAsync();
			return spendings;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<List<Spending>> GetItemsByCategory(Category category)
	{
		try
		{
			return await bankableContext.Spendings.Where(e => category.Id == e.CategoryId).ToListAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public List<Spending> GetItemsForUser()
	{
		try
		{
			return bankableContext.Spendings.Where(e => e.BankAccount.UserId == BankableContext.CurrentConnectedUser.Id).ToList();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<EntityEntry<Spending>> AddItem(Spending Spending)
	{
		try
		{
			var addedSpending = bankableContext.Add(Spending);
            var bankAccount = bankableContext.BankAccounts.SingleAsync(e => e.Id == Spending.BankAccountId).Result;
            bankAccount.Amount -= Spending.Amount;
            bankableContext.Update(bankAccount);
			await bankableContext.SaveChangesAsync();
			return addedSpending;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<EntityEntry<Spending>> UpdateItem(Spending Spending)
	{
		try
		{
			var updatedSpending = bankableContext.Update(Spending);
			await bankableContext.SaveChangesAsync();
			return updatedSpending;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<string> RemoveItem(Spending Spending)
	{
		try
		{
			bankableContext.Remove(Spending);
			await bankableContext.SaveChangesAsync();
			return "Item has been removed";
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
}
