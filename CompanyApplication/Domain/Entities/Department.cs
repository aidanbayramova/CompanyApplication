using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Common;

namespace Domain.Entities
{
    public class Department :BaseEntity
    {
        public string Name { get; set; }
        public int? Capacity { get; set; }
        public ICollection<Employee> Employees { get; set; }

    }
}
