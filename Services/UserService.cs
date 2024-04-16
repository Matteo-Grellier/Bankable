using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class UserService
{
	BankableContext bankableContext = new();

	public async Task<IEnumerable<User>> GetAllItems()
	{
		IEnumerable<User> categories;
		categories = await bankableContext.Users.ToListAsync();
		return categories;
	}

	public async Task<User> GetItemByID(Guid id)
	{
		var user = await bankableContext.Users.SingleAsync(e => e.Id == id);
		return user;
	}
	public async Task<User> GetLastCreatedItem()
	{
		var user = await bankableContext.Users.OrderByDescending(e => e.CreatedAt).FirstAsync();
		return user;
	}

	public async Task<EntityEntry<User>> AddItem(User user)
	{
		try
		{
			EntityEntry<User> addedCategory = bankableContext.Add(user);
			await bankableContext.SaveChangesAsync();
			return addedCategory;
		}
		catch (Exception)
		{
			throw;
		}
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

	public async Task<User> VerifyUser(string username, string password)
	{
		var sha256 = SHA256.Create();
		var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

		var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
		var user = await bankableContext.Users.SingleAsync(e => e.Username == username && e.Password == hash);
		return user;
	}
}
