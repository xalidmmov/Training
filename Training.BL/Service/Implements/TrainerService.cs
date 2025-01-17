using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BL.Service.Abstracts;
using Training.BL.ViewModels.Trainer;
using Training.Core.Entities;
using Training.DAL.DAL;

namespace Training.BL.Service.Implements
{
    public class TrainerService(TrainingDbContext _context):ITrainerService
    {
        public async Task<bool> Create(TrainerCreateVM vm)
        {
            Trainer trainer = new Trainer
            {
                Name = vm.Name,
                Surname = vm.Surname,
                CategoryId = vm.CategoryId,
                Image = vm.TrainerImg
            };
            await _context.Trainers.AddAsync(trainer);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> Delete(int? id)
        {
            var data = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                _context.Trainers.Remove(data);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        public async Task<TrainerCreateVM> Get(int id)
        {
            var trainer =await _context.Trainers.FindAsync(id);
            TrainerCreateVM vm = new TrainerCreateVM
            {
                Name=trainer.Name,
                CategoryId=trainer.CategoryId,
                TrainerImg=trainer.Image

               

            };
            return vm;
        }

        public async Task<List<Trainer>> GetAllAsync()
        {
            return await _context.Trainers.Where(x => x.IsDeleted == false).Include(x => x.Category).ToListAsync();
        }

        public async Task<bool> UpdateAsync(int id, TrainerCreateVM vm)
        {
            var data = await _context.Trainers.FirstOrDefaultAsync(x => x.Id == id);
            if (data != null)
            {
                data.Name = vm.Name;
                data.CategoryId= vm.CategoryId;
                data.Image = vm.TrainerImg;
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
