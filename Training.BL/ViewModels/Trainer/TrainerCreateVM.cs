using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BL.ViewModels.Trainer
{
    public class TrainerCreateVM
    {
        [Required, MaxLength(32)]
        public string Name { get; set; }
        [MaxLength(32)]
        public string? Surname { get; set; }

        public int CategoryId { get; set; }
        public IFormFile? CoverImage { get; set; }
        public string? TrainerImg { get; set; }
    }
}
