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
    public class DepartmentRepo :IDepartmentRepo
    {

        private readonly AppDBContext DB;
        public DepartmentRepo(AppDBContext DB)
        {
            this.DB = DB;
        }
        public int Add(Department entity)
        {
            DB.Add(entity); 
            return DB.SaveChanges();
        }

        public Department Get(int id)
        {
            //return DB.Departments.FirstOrDefault(D=>D.Id==id);
            return DB.Departments.Find(id);
        }

         public IEnumerable<Department> GetAll()
        {
            return DB.Departments.ToList();
        }

        public int Update(Department entity)
        {
            DB.Update(entity);
            return DB.SaveChanges();
        }
        public int Delete(Department entity)
        {
           DB.Remove(entity);   
           return DB.SaveChanges();
        }


    }
}
