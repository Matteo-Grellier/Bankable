using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bankable.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace Bankable.Services;

public class SavingService
{
    private readonly BankableContext _bankableContext = new();
    
    public async Task<List<Saving>> GetItemsBySavingProject(Guid savingProjectId)
    {
        try
        {
            return await _bankableContext.Savings.Where(e => e.SavingProjectId == savingProjectId).ToListAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

	public async Task<IEnumerable<Saving>> GetAllSavingsInMonthBySavingProject(int month, Guid savingProjectId)
	{
		try
		{
			return await _bankableContext.Savings.Where(e => e.Date.Month == month && e.SavingProjectId == savingProjectId).ToListAsync();
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<Saving> GetById(Guid id)
	{
		try
		{
			var savingProject = await _bankableContext.Savings.SingleAsync(e => e.Id == id);
			return savingProject;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<EntityEntry<Saving>> Add(Saving saving)
	{
		try
		{
			var addedSpending = _bankableContext.Add(saving);
			await _bankableContext.SaveChangesAsync();
			return addedSpending;
		}
		catch (Exception e)
		{
			Console.WriteLine(e);
			throw;
		}
	}

	public async Task<EntityEntry<Saving>> Update(Saving saving)
	{
		try
		{
			var updatedSaving = _bankableContext.Update(saving);
			await _bankableContext.SaveChangesAsync();
			return updatedSaving;
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

	public async Task<string> Remove(Saving saving)
	{
		try
		{
			_bankableContext.Remove(saving);
			await _bankableContext.SaveChangesAsync();
			return "Item has been removed";
		}
		catch (Exception err)
		{
			Console.WriteLine(err);
			throw;
		}
	}

}
