using BLL.Interfaces;
using BLL.Repositories;
using DAL.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDBContext DB;
        private IDepartmentRepo deptRepo;
        private IEmployeeReop empRepo;
        public UnitOfWork(AppDBContext DB)
        {
            this.DB=DB;
            deptRepo=new DepartmentRepo(DB);
            empRepo=new EmployeeRepo(DB);
            
        }
        public IEmployeeReop EmployeeReop => empRepo;

        public IDepartmentRepo DepartmentRepo => deptRepo;
    }
}
