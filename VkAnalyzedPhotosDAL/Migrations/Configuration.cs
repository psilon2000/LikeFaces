﻿using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VkAnalyzedPhotosDAL.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<VkPhotosContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(VkPhotosContext context)
        {
        }
    }
}
