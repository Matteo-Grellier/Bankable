using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class CategoryService
{
	BankableContext bankableContext = new();

	public async Task<EntityEntry<Category>> AddItem(Category category)
	{
		EntityEntry<Category> addedCategory = bankableContext.Add(category);
		await bankableContext.SaveChangesAsync();
		return addedCategory;
	}

	public async Task<EntityEntry<Category>> UpdateItem(Category category)
	{
		var updatedIncoming = bankableContext.Update(category);
		await bankableContext.SaveChangesAsync();
		return updatedIncoming;
	}

	public async Task<string> RemoveItem(Category category)
	{
		bankableContext.Remove(category);
		await bankableContext.SaveChangesAsync();
		return "Item has been removed";
	}

	public async Task<Category> GetItemByID(Guid id)
	{
		var category = await bankableContext.Categories.SingleAsync(e => e.Id == id);
		return category;
	}

	public async Task<IEnumerable<Category>> GetAllItems()
	{
		IEnumerable<Category> categories;
		categories = await bankableContext.Categories.ToListAsync();
		return categories;
	}

}
