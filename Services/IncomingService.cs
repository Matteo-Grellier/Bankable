using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class IncomingService
{
	BankableContext bankableContext = new();

	public async Task<IEnumerable<Incoming>> GetAllItems()
	{
		try
		{
			IEnumerable<Incoming> Incomings;
			Incomings = await bankableContext.Incomings.ToListAsync();
			return Incomings;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<List<Incoming>> GetAllInMonth(DateTime date)
	{
		try
		{
			List<Incoming> incomings;
			incomings = await bankableContext.Incomings.Where(e =>
				e.Date.Month == date.Date.Month
				&& e.Date.Year == date.Date.Year
				&& e.BankAccount.UserId == BankableContext.CurrentConnectedUser.Id).ToListAsync();
			return incomings;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<IEnumerable<Incoming>> GetAllRecurringSpendingsInMonth(int month)
	{
		try
		{
			var incomings = await bankableContext.Incomings.Where(e => e.Date.Month == month || e.IsRecurring).ToListAsync();
			return incomings;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<Incoming> GetItemByID(Guid id)
	{
		try
		{
			var incoming = await bankableContext.Incomings.SingleAsync(e => e.Id == id);
			return incoming;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<List<Incoming>> GetItemsByCategory(Category category)
	{
		try
		{
			return await bankableContext.Incomings.Where(e => category.Id == e.CategoryId).ToListAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public List<Incoming> GetItemsForUser()
	{
		try
		{
			return bankableContext.Incomings.Where(e => e.BankAccount.UserId == BankableContext.CurrentConnectedUser.Id).ToList();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<EntityEntry<Incoming>> AddItem(Incoming incoming)
	{
		try
		{
			var addedIncoming = bankableContext.Add(incoming);
            var bankAccount = bankableContext.BankAccounts.SingleAsync(e => e.Id == incoming.BankAccountId).Result;
            bankAccount.Amount += incoming.Amount;
            bankableContext.Update(bankAccount);
			await bankableContext.SaveChangesAsync();
			return addedIncoming;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<EntityEntry<Incoming>> UpdateItem(Incoming incoming)
	{
		try
		{
			var updatedIncoming = bankableContext.Update(incoming);
			await bankableContext.SaveChangesAsync();
			return updatedIncoming;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<string> RemoveItem(Incoming incoming)
	{
		try
		{
			bankableContext.Remove(incoming);
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
