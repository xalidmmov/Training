using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Core.Entities.Common;

namespace Training.Core.Entities
{
    public class Category:BaseEntity
    {
        public string  CName { get; set; }
        public IEnumerable<Trainer> Trainers { get; set; }
    }
}
