using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bankable.Models;

namespace Bankable.Services;
public class AuthenticationService
{
	private readonly UserService _userService = new();
	private readonly TokenService _tokenService = new();

	// login and generate token
	public async Task<User> Login(string username, string password)
	{
		try
		{
			var user = await _userService.VerifyAndGetUser(username, password);
			BankableContext.CurrentConnectedUser = user;
			
			//Generate token and set to the current user
			_userService.GetLastCreatedItem().Result.TokenId = await _tokenService.CreateToken(user);
			await _userService.UpdateItem(_userService.GetLastCreatedItem().Result);
			
			return BankableContext.CurrentConnectedUser;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}
	public void Logout()
	{
		_tokenService.DeleteToken();
	}

	public async Task<User> Register(User user)
	{
		var sha256 = SHA256.Create();
		var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(user.Password));

		// Get the hashed string.
		var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

		User newUser = new User
		{
			Username = user.Username,
			Password = hash,
			FirstName = user.FirstName,
			LastName = user.LastName,
			CreatedAt = DateTime.UtcNow,
		};
		
		var addedUser = await _userService.AddItem(newUser);
		
		return addedUser.Entity;
	}
}
