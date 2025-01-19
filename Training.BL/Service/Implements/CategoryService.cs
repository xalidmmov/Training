using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BL.Service.Abstracts;
using Training.BL.ViewModels.Category;
using Training.Core.Entities;
using Training.DAL.DAL;

namespace Training.BL.Service.Implements
{
    public class CategoryService(TrainingDbContext _context):ICategoryService
    {
        public async Task<bool> Create(CategoryCreateVM vm)
        {
            await _context.Categories.AddAsync(new Category
            {
                CName = vm.Name,
            });
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int? id)
        {
            var data = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.IsDeleted=true;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<CategoryCreateVM> Get(int id)
        {
            var data = await _context.Categories.FindAsync(id);

            CategoryCreateVM vm = new() { Name = data!.CName };
            return vm;
        }



        public async Task<List<Category>> GetAllAsync()
        {
            return await _context.Categories.ToListAsync();
        }

        public async Task<bool> Update(int? id, CategoryCreateVM vm)
        {
            var data = await _context.Categories.FirstOrDefaultAsync();
            if (data != null)
            {
                data.CName = vm.Name;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
