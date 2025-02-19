using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Data;
using Repository.Repositories.Interfaces;

namespace Repository.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        private readonly AppDbContext _context;

        public async Task<List<Department>> SearchAsync(string searchName)
        {
            return await _context.Departments.Where(d => d.Name.Contains(searchName)).ToListAsync();                     
        }
    }
}
