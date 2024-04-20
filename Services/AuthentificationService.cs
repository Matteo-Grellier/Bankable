using System;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Bankable.Models;

namespace Bankable.Services;
public class AuthenticationService
{
	UserService userService = new();
	TokenService tokenService = new();

	// login and generate token
	public async Task<string> Login(string username, string password)
	{
		var user = await userService.VerifyAndGetUser(username, password);
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

	public async Task<Guid> Register(User user)
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
		await userService.AddItem(newUser);
		userService.GetLastCreatedItem().Result.TokenId = await tokenService.CreateToken();
		return Guid.NewGuid();
	}
}
