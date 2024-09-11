﻿using BLL.Interfaces;
using DAL.Data.Context;
using DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Repositories
{
    public class DepartmentRepo :GenericRepo<Department>,IDepartmentRepo
    {
        public DepartmentRepo(AppDBContext DB):base(DB)
        { 
        }

    }
}
