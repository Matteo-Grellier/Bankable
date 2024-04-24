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
		try
		{
			IEnumerable<User> categories;
			categories = await bankableContext.Users.ToListAsync();
			return categories;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<User> GetItemByID(Guid id)
	{
		try
		{
			var user = await bankableContext.Users.SingleAsync(e => e.Id == id);
			return user;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}
	public async Task<User> GetLastCreatedItem()
	{

		try
		{
			var user = await bankableContext.Users.OrderByDescending(e => e.CreatedAt).FirstAsync();
			return user;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<User> GetItemByToken(Guid token)
	{
		try
		{
			var user = await bankableContext.Users.SingleAsync(e => e.TokenId == token);
			return user;
		}
		catch (Exception)
		{
			throw new Exception("There is no user assigned to this token");
		}
	}

	public async Task<EntityEntry<User>> AddItem(User user)
	{
		try
		{
			EntityEntry<User> addedUser = bankableContext.Add(user);
			await bankableContext.SaveChangesAsync();
			return addedUser;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<EntityEntry<User>> UpdateItem(User user)
	{
		try
		{
			var updatedUser = bankableContext.Update(user);
			await bankableContext.SaveChangesAsync();
			return updatedUser;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}

	}

	public async Task<string> RemoveItem(User user)
	{

		try
		{
			bankableContext.Remove(user);
			await bankableContext.SaveChangesAsync();
			return "Item has been removed";
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<User> VerifyAndGetUser(string username, string password)
	{
		try
		{
			var sha256 = SHA256.Create();
			var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

			var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
			var user = await bankableContext.Users.SingleAsync(e => e.Username == username && e.Password == hash);
			return user;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}
}
