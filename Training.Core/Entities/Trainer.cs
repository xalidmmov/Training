using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Core.Entities.Common;

namespace Training.Core.Entities
{
    public class Trainer:BaseEntity
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public string? Image { get; set; }
    }
}
