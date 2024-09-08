﻿using DAL.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Data.Configration
{
    public class DepartmentConfig:IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder) 
        { 
            builder.Property(D=>D.Id).UseIdentityColumn(10,10);
        }
    }
}
