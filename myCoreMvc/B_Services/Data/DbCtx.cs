﻿using Microsoft.EntityFrameworkCore;
using myCoreMvc.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace myCoreMvc.Services
{
    public class DbCtx : DbContext
    {
        public DbCtx() {} // Only to get Get-DbContext command of EF to work

        public DbCtx(DbContextOptions options) : base(options) { }

        public DbSet<WorkItem> WorkItems { get; set; }
        public DbSet<WorkPlan> WorkPlans { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("SERVER=.\\sqlexpress; Database=myCoreMvc; MultipleActiveResultSets=True; Integrated Security=SSPI;");
        }
    }
}
