using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Training.BL.ViewModels.Category
{
    public class CategoryCreateVM
    {
        [Required, MaxLength(32)]
        public string Name { get; set; } = null!;
    }
}
