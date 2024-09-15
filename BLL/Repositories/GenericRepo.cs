using BLL.Interfaces;
using DAL.Data.Context;
using DAL.Models;
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
        public int Add(T entity)
        {
            DB.Add(entity);
            return DB.SaveChanges();
        }

        public int Delete(T entity)
        {
            DB.Remove(entity);
            return DB.SaveChanges();    
        }

        public T Get(int id)
        {
            return DB.Set<T>().Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return DB.Set<T>().ToList();
        }

        public int Update(T entity)
        {
            DB.Update(entity);
            return DB.SaveChanges();
        }
    }
}
