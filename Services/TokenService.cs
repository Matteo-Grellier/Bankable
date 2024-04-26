using System;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
namespace Bankable.Services;

public class TokenService
{
	BankableContext bankableContext = new();
	public async Task<Guid> CreateToken(User user)
	{
		try
		{
			Token newToken = new Token
			{
				Value = Guid.NewGuid(),
				UserId = user.Id
			};

			//Delete current token before create new token
			this.DeleteToken();

			bankableContext.Tokens.Add(newToken);
			await bankableContext.SaveChangesAsync();
			return newToken.Id;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<Token> GetToken()
	{
		try
		{
			var token = await bankableContext.Tokens.FirstAsync();
			return token!;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}

	}

	public async void DeleteToken()
	{
		try
		{
			var token = await bankableContext.Tokens.FirstOrDefaultAsync();

			if (token != null)
				bankableContext.Tokens.Remove(token);

			await bankableContext.SaveChangesAsync();
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}

	}
}
