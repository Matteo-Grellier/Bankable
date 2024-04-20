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
		IEnumerable<BankAccount> bankAccounts;
		bankAccounts = await bankableContext.BankAccounts.ToListAsync();
		return bankAccounts;
	}

	public async Task<BankAccount> GetItemByID(Guid id)
	{
		var bankAccount = await bankableContext.BankAccounts.SingleAsync(e => e.Id == id);
		return bankAccount;
	}

	public async Task<List<BankAccount>> GetItemsByUser()
	{
		return await bankableContext.BankAccounts.Where(e => BankableContext.CurrentConnectedUser.Id == e.UserId).ToListAsync();
	}
	public async Task<EntityEntry<BankAccount>> AddItem(BankAccount bankAccount)
	{
		var addedBankAccount = bankableContext.Add(bankAccount);
		Console.WriteLine(bankAccount.Id);
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
}
