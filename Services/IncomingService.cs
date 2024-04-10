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
		IEnumerable<Incoming> Incomings;
		Incomings = await bankableContext.Incomings.ToListAsync();
		return Incomings;
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

	public async Task<Incoming> GetItemByID(Guid id)
	{
		var incoming = await bankableContext.Incomings.SingleAsync(e => e.Id == id);
		return incoming;
	}

	public async Task<List<Incoming>> GetItemsByCategory(Category category)
	{
		Console.WriteLine(category.Id);
		return await bankableContext.Incomings.Where(e => category.Id == e.CategoryId).ToListAsync();
	}
}