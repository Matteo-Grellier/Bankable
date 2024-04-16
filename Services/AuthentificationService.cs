using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bankable.Models;

namespace Bankable.Services;
public class LoginService
{
	UserService userService = new();
	TokenService tokenService = new();

	// login and generate token
	public async Task<string> Login(string username, string password)
	{
		var user = await userService.VerifyUser(username, password);
		if (user != null)
		{
			BankableContext.CurrentConnectedUser = user;
		}
		return "Invalid username or password";
	}
	public void Logout(Guid guid)
	{
		tokenService.DeleteToken(guid);
	}

	public async Task<Guid> Register(string username, string password, string firstName, string lastName)
	{
		var sha256 = SHA256.Create();
		var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

		// Get the hashed string.
		var hash = BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();

		User newUser = new User
		{
			Username = username,
			Password = hash,
			FirstName = firstName,
			LastName = lastName,
			CreatedAt = DateTime.UtcNow,
		};
		Console.WriteLine(newUser.TokenId);
		await userService.AddItem(newUser);
		userService.GetLastCreatedItem().Result.TokenId = await tokenService.CreateToken();
		Console.WriteLine(newUser.TokenId);
		return Guid.NewGuid();
	}
}
