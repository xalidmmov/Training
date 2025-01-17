using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.BL.ViewModels.Trainer;
using Training.Core.Entities;

namespace Training.BL.Service.Abstracts
{
    public interface ITrainerService
    {
        Task<List<Trainer>> GetAllAsync();
        Task<bool> Create(TrainerCreateVM vm);
        Task<bool> Delete(int? id);
        Task<bool> UpdateAsync(int id, TrainerCreateVM vm);
        Task<TrainerCreateVM> Get(int id);

    }
}
