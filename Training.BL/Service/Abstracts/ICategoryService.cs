using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BL.ViewModels.Category;
using Training.Core.Entities;

namespace Training.BL.Service.Abstracts
{
    public interface ICategoryService
    {
        Task<List<Category>> GetAllAsync();
        Task<bool> Create(CategoryCreateVM  vm);
        Task<bool> Update(int? id,CategoryCreateVM vm);
        Task<bool> Delete(int? id);
        Task<CategoryCreateVM> Get(int id);
    }
}
