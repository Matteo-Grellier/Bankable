using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class CategoryService
{
	BankableContext bankableContext = new();

	public async Task<List<Category>> GetAllItems()
	{
		try
		{
			List<Category> categories;
			categories = await bankableContext.Categories.ToListAsync();
			return categories;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<Category> GetItemByID(Guid id)
	{
		try
		{
			var category = await bankableContext.Categories.SingleAsync(e => e.Id == id);
			return category;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<Category> AddItem(Category category)
	{
		try
		{
			var addedCategory = bankableContext.AddAsync(category).Result.Entity;
			await bankableContext.SaveChangesAsync();
			return addedCategory;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<EntityEntry<Category>> UpdateItem(Category category)
	{
		try
		{
			var updatedIncoming = bankableContext.Update(category);
			await bankableContext.SaveChangesAsync();
			return updatedIncoming;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<string> RemoveItem(Category category)
	{
		try
		{
			bankableContext.Remove(category);
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
