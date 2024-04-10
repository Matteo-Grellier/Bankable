using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class UserService
{
	BankableContext bankableContext = new();

	public async Task<EntityEntry<User>> AddItem(User user)
	{
		EntityEntry<User> addedCategory = bankableContext.Add(user);
		await bankableContext.SaveChangesAsync();
		return addedCategory;
	}

	public async Task<EntityEntry<User>> UpdateItem(User user)
	{
		var updatedIncoming = bankableContext.Update(user);
		await bankableContext.SaveChangesAsync();
		return updatedIncoming;
	}

	public async Task<string> RemoveItem(User user)
	{
		bankableContext.Remove(user);
		await bankableContext.SaveChangesAsync();
		return "Item has been removed";
	}

	public async Task<User> GetItemByID(Guid id)
	{
		var user = await bankableContext.Users.SingleAsync(e => e.Id == id);
		return user;
	}

	public async Task<IEnumerable<User>> GetAllItems()
	{
		IEnumerable<User> categories;
		categories = await bankableContext.Users.ToListAsync();
		return categories;
	}

}
