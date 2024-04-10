using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class BankAccountService
{
	BankableContext bankableContext = new();

	public async Task<IEnumerable<BankAccount>> GetAllItems()
	{
		IEnumerable<BankAccount> bankAccounts;
		bankAccounts = await bankableContext.BankAccounts.ToListAsync();
		return bankAccounts;
	}

	public async Task<EntityEntry<BankAccount>> AddItem(BankAccount bankAccount)
	{
		var addedBankAccount = bankableContext.Add(bankAccount);
		await bankableContext.SaveChangesAsync();
		return addedBankAccount;
	}

	public async Task<EntityEntry<BankAccount>> UpdateItem(BankAccount bankAccount)
	{
		var updatedBankAccount = bankableContext.Update(bankAccount);
		await bankableContext.SaveChangesAsync();
		return updatedBankAccount;
	}

	public async Task<string> RemoveItem(BankAccount bankAccount)
	{
		bankableContext.Remove(bankAccount);
		await bankableContext.SaveChangesAsync();
		return "Item has been removed";
	}

	public async Task<BankAccount> GetItemByID(Guid id)
	{
		var bankAccount = await bankableContext.BankAccounts.SingleAsync(e => e.Id == id);
		return bankAccount;
	}
}
