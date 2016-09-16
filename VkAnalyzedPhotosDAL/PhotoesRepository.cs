using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;

namespace VkAnalyzedPhotosDAL
{
    public class PhotoesRepository
    {
        private readonly string _connection;

        public PhotoesRepository(string connection)
        {
            _connection = connection;
        }

        public List<AnalyzedPhoto> Select()
        {
            using (var context = NewContext())
            {
                return context.AnalyzedPhotos.ToList();
            }
        }

        private PhotosContext NewContext()
        {
            return new PhotosContext(_connection);
        }

        public void Add(AnalyzedPhoto model)
        {
            using (var context = NewContext())
            {
                context.AnalyzedPhotos.Add(model);
                context.SaveChanges();
            }
        }
    }
}
