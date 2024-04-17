using System;
using Bankable.Models;
using Faker;

namespace Bankable.Services;

public class DataFaker
{
	BankAccountService bankAccountService = new();
	UserService userService = new();
	public async void GenerateData()
	{
		AuthenticationService authenticationService = new();

		User user = new()
		{
			Password = Faker.RandomNumber.Next(0, 10000).ToString(),
			Username = Internet.UserName(),
			CreatedAt = DateTime.UtcNow,
			FirstName = Name.First(),
			LastName = Name.Last()
		};

		await authenticationService.Register(user);
		await authenticationService.Login(user.Username, user.Password);

		IncomingService incomingService = new();

		BankAccount bankAccount = new()
		{
			Amount = RandomNumber.Next(0, 10000),
			Description = Lorem.Sentence(),
			UserId = userService.GetLastCreatedItem().Result.Id,
			CreatedAt = DateTime.UtcNow,
			Name = Name.FullName(),
		};

		await bankAccountService.AddItem(bankAccount);

		CategoryService categoryService = new();

		for (int i = 0; i < 10; i++)
		{
			Category category = new()
			{
				Name = Name.First(),
			};
			await categoryService.AddItem(category);
		}

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
