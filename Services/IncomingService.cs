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
	BankAccountService bankAccountService = new();

	public async Task<IEnumerable<Incoming>> GetAllItems()
	{
		IEnumerable<Incoming> Incomings;
		Incomings = await bankableContext.Incomings.ToListAsync();
		return Incomings;
	}

	public async Task<Incoming> GetItemByID(Guid id)
	{
		var incoming = await bankableContext.Incomings.SingleAsync(e => e.Id == id);
		return incoming;
	}

	public async Task<List<Incoming>> GetItemsByCategory(Category category)
	{
		return await bankableContext.Incomings.Where(e => category.Id == e.CategoryId).ToListAsync();
	}

	public List<Incoming> GetItemsForUser()
	{
		return bankableContext.Incomings.Where(e => e.BankAccount.UserId == BankableContext.CurrentConnectedUser.Id).ToList();
	}

	public async Task<EntityEntry<Incoming>> AddItem(Incoming incoming)
	{
		var addedIncoming = bankableContext.Add(incoming);
		await bankableContext.SaveChangesAsync();
		return addedIncoming;
	}

	public async Task<EntityEntry<Incoming>> UpdateItem(Incoming incoming)
	{
		var updatedIncoming = bankableContext.Update(incoming);
		await bankableContext.SaveChangesAsync();
		return updatedIncoming;
	}

	public async Task<string> RemoveItem(Incoming incoming)
	{
		bankableContext.Remove(incoming);
		await bankableContext.SaveChangesAsync();
		return "Item has been removed";
	}

}
