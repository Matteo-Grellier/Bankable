using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class BankAccountService
{
	BankableContext bankableContext = new();

	// Get all bank accounts for a user
	public async Task<IEnumerable<BankAccount>> GetAllItems()
	{
		try
		{
			IEnumerable<BankAccount> bankAccounts;
			bankAccounts = await bankableContext.BankAccounts.ToListAsync();
			return bankAccounts;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<BankAccount> GetItemByID(Guid id)
	{
		try
		{
			var bankAccount = await bankableContext.BankAccounts.SingleAsync(e => e.Id == id);
			return bankAccount;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<List<BankAccount>> GetItemsByUser()
	{
		try
		{
			return await bankableContext.BankAccounts.Where(e => BankableContext.CurrentConnectedUser.Id == e.UserId).ToListAsync();
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}
	public async Task<EntityEntry<BankAccount>> AddItem(BankAccount bankAccount)
	{
		try
		{
			var addedBankAccount = bankableContext.Add(bankAccount);
			Console.WriteLine(bankAccount.Id);
			await bankableContext.SaveChangesAsync();
			return addedBankAccount;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<EntityEntry<BankAccount>> UpdateItem(BankAccount bankAccount)
	{
		try
		{
			var updatedBankAccount = bankableContext.Update(bankAccount);
			await bankableContext.SaveChangesAsync();
			return updatedBankAccount;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<string> RemoveItem(BankAccount bankAccount)
	{
		try
		{
			bankableContext.Remove(bankAccount);
			await bankableContext.SaveChangesAsync();
			return "Item has been removed";
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}
}
