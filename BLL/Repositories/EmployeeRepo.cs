using BLL.Interfaces;
using DAL.Data.Context;
using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class EmployeeRepo: GenericRepo<Employee>, IEmployeeReop
    {
        public EmployeeRepo(AppDBContext DB):base(DB)
        {
        }

        public async Task<IEnumerable<Employee>> GetByNameAsync(string name)
        {
           return await DB.Employees.Where(E => E.Name.ToLower().Contains(name.ToLower())).Include(E=>E.WorkFor).ToListAsync();
        }
    }
}
