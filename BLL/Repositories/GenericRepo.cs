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
    
    public class GenericRepo<T> : IGenericRepo<T> where T : BaseEntity
    {
        private protected readonly AppDBContext DB;
        public GenericRepo(AppDBContext DB) { this.DB = DB; }
        public async Task<int> AddAsync(T entity)
        {
             DB.Add(entity);
            return await DB.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(T entity)
        {
            DB.Remove(entity);
            return await DB.SaveChangesAsync();    
        }

        public async Task<T> GetAsync(int id)
        {
            return await DB.Set<T>().FindAsync(id);
        }
        //Async returns - Void - Task - Task<>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await DB.Set<T>().ToListAsync();
        }

        public async Task<int> UpdateAsync(T entity)
        {
            DB.Update(entity);
            return await DB.SaveChangesAsync();
        }
    }
}
