using System;
using Bankable.Models;
using Faker;

namespace Bankable.Services;

public class DataFaker
{
	BankAccountService bankAccountService = new();
	UserService userService = new();

	// Method that generates random data
	public async void GenerateData()
	{
		AuthenticationService authenticationService = new();

		// Create a new user
		User user = new()
		{
			Password = Faker.RandomNumber.Next(0, 10000).ToString(),
			Username = Internet.UserName(),
			CreatedAt = DateTime.UtcNow,
			FirstName = Name.First(),
			LastName = Name.Last()
		};

		// Register the user
		await authenticationService.Register(user);

		// Login the user
		await authenticationService.Login(user.Username, user.Password);

		IncomingService incomingService = new();

		// Create a new bank account
		BankAccount bankAccount = new()
		{
			Amount = RandomNumber.Next(0, 10000),
			Description = Lorem.Sentence(),
			UserId = userService.GetLastCreatedItem().Result.Id,
			CreatedAt = DateTime.UtcNow,
			Name = Name.FullName(),
		};

		// Add the bank account
		await bankAccountService.AddItem(bankAccount);

		CategoryService categoryService = new();

		// Create 10 categories
		for (int i = 0; i < 10; i++)
		{
			Category category = new()
			{
				Name = Name.First(),
			};
			await categoryService.AddItem(category);
		}

		// Create 10 incoming transactions
		for (int i = 0; i < 10; i++)
		{
			Incoming incoming = new()
			{
				Amount = RandomNumber.Next(0, 10000),
				Description = Lorem.Sentence(),
				BankAccountId = bankAccountService.GetItemsByUser().Result[0].Id,
				CategoryId = categoryService.GetAllItems().Result[i].Id,
				Title = Faker.Name.Middle(),
			};
			await incomingService.AddItem(incoming);
		}

		SpendingService spendingService = new();

		// Create 10 spending transactions
		for (int i = 0; i < 10; i++)
		{
			Spending spending = new()
			{
				Amount = RandomNumber.Next(0, 10000),
				Description = Lorem.Sentence(),
				BankAccountId = bankAccountService.GetItemsByUser().Result[0].Id,
				CategoryId = categoryService.GetAllItems().Result[i].Id,
				Title = Faker.Lorem.Sentence(4),
			};
			await spendingService.AddItem(spending);
		}
	}
}
