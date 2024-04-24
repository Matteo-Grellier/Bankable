using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class SavingProjectService
{
	private readonly BankableContext _bankableContext = new();

	public async Task<List<SavingProject>> GetAll()
	{
		try
		{
			return await _bankableContext.SavingProjects.ToListAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<List<SavingProject>> GetItemsForUser()
	{
		try
		{
			return await _bankableContext.SavingProjects.Where(e => e.UserId == BankableContext.CurrentConnectedUser.Id).ToListAsync();
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<SavingProject> GetById(Guid id)
	{
		try
		{
			var savingProject = await _bankableContext.SavingProjects.SingleAsync(e => e.Id == id);
			return savingProject;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<EntityEntry<SavingProject>> Add(SavingProject savingProject)
	{
		try
		{
			var addedSpending = _bankableContext.Add(savingProject);
			await _bankableContext.SaveChangesAsync();
			return addedSpending;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<EntityEntry<SavingProject>> Update(SavingProject savingProject)
	{
		try
		{
			var updatedSavingProject = _bankableContext.Update(savingProject);
			await _bankableContext.SaveChangesAsync();
			return updatedSavingProject;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<string> Remove(SavingProject savingProject)
	{
		try
		{
			_bankableContext.Remove(savingProject);
			await _bankableContext.SaveChangesAsync();
			return "Item has been removed";
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

}
