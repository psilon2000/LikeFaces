using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping;
using System.Linq;

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

        public void Like(Guid faceId, bool like)
        {
            using (var context = NewContext())
            {
                var entity = context.AnalyzedPhotos
                    .First(f => f.FaceId == faceId);
                entity.Like = like;
                context.SaveChanges();
            }
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
