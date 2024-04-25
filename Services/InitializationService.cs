using System.Threading.Tasks;
using Bankable.Models;

namespace Bankable.Services;

public class InitializationService
{
    private readonly CategoryService _categoryService;

    private readonly string[] _categoriesNames = new string[]
    {
        "Others",
        "Leisure", 
        "Food",
        "Vehicle", 
        "Numeric", 
        "Health",
        "Everyday life",
        "Holidays",
        "Pets",
        "Tax"
    };
    
    // Use this method to initialize data (like categories) in the database
    public async Task InitializeData()
    {
       var categories = await _categoryService.GetAllItems();

       if (categories != null) return;

       foreach (var categoryName in _categoriesNames)
       {
           var category = new Category { Name = categoryName };
           await _categoryService.AddItem(category);
       }
    }
    
}