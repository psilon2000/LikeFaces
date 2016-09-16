using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;

namespace VkAnalyzedPhotosDAL
{
    public class VkPhotosContext : DbContext
    {
            public VkPhotosContext()
                : base("name=VkPhotosDB")
            {
                //Database.SetInitializer(new Migrations.Configuration.RebatesDbInitializer());
            }

            public virtual DbSet<AnalyzedPhoto> AnalyzedPhotos { get; set; }
    }
}
