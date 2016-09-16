using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Face.Contract;

namespace VkAnalyzedPhotosDAL
{
    public class VkPhotosAPI
    {
        public static List<AnalyzedPhoto> GetWords()
        {
            using (var context = new VkPhotosContext())
            {
                return context.AnalyzedPhotos.ToList();
            }
        }

        public static void Create/*OrUpdate*/(AnalyzedPhoto model)
        {
            using (var context = new VkPhotosContext())
            {
                context.AnalyzedPhotos.Add(model);
                context.SaveChanges();
            }
        }
    }
}
