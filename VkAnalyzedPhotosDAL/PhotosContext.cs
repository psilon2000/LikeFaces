using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;

namespace VkAnalyzedPhotosDAL
{
    public class PhotosContext : DbContext
    {
            public PhotosContext(string connection)
                : base(connection)
            {
                //Database.SetInitializer(new Migrations.Configuration.RebatesDbInitializer());
            }

            public virtual DbSet<AnalyzedPhoto> AnalyzedPhotos { get; set; }
    }
}
