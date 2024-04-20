using System;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
namespace Bankable.Services;

public class TokenService
{
	BankableContext bankableContext = new();
	UserService userService = new();
	public async Task<Guid> CreateToken()
	{
		try
		{
			Token newToken = new Token
			{
				Value = Guid.NewGuid(),
				UserId = userService.GetLastCreatedItem().Result.Id
			};
			bankableContext.Tokens.Add(newToken);
			await bankableContext.SaveChangesAsync();
			return newToken.Value;
		}
		catch (Exception)
		{
			throw;
		}
	}

	public async Task<Token> GetToken()
	{
		var token = await bankableContext.Tokens.FirstAsync();
		return token!;
	}

	public void DeleteToken(Guid index)
	{
		Token token = bankableContext.Tokens.Find(index)!;
		bankableContext.Tokens.Remove(token);
		bankableContext.SaveChanges();
	}
}
