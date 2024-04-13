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
		IEnumerable<Spending> Spendings;
		Spendings = await bankableContext.Spendings.ToListAsync();
		return Spendings;
	}

	public async Task<Spending> GetItemByID(Guid id)
	{
		var spending = await bankableContext.Spendings.SingleAsync(e => e.Id == id);
		return spending;
	}

	public async Task<IEnumerable<Spending>> GetAllSpendingsByCategorInMonth(Category category, DateTime? month)
	{
		var spending = await bankableContext.Spendings.Where(e => e.Date.Month == month.Value.Month && category.Id == e.CategoryId).ToListAsync();
		return spending;
	}

	public async Task<List<Spending>> GetItemsByCategory(Category category)
	{
		return await bankableContext.Spendings.Where(e => category.Id == e.CategoryId).ToListAsync();
	}

	public async Task<EntityEntry<Spending>> AddItem(Spending Spending)
	{
		var addedSpending = bankableContext.Add(Spending);
		await bankableContext.SaveChangesAsync();
		return addedSpending;
	}

	public async Task<EntityEntry<Spending>> UpdateItem(Spending Spending)
	{
		var updatedSpending = bankableContext.Update(Spending);
		await bankableContext.SaveChangesAsync();
		return updatedSpending;
	}

	public async Task<string> RemoveItem(Spending Spending)
	{
		bankableContext.Remove(Spending);
		await bankableContext.SaveChangesAsync();
		return "Item has been removed";
	}
}
