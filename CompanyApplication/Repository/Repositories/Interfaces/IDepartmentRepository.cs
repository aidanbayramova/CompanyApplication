﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Repository.Repositories.Interfaces
{
    public  interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task DeleteDepartmentAsync(int id);
        Task<List<Department>> SearchAsync(string searchName);

    }
}
