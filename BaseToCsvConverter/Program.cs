using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using FileHelpers;
using VkAnalyzedPhotosDAL;

namespace BaseToCsvConverter
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Provide path to output file: {result file name}");
                return;
            }
            var photoes = LoadPhotoesFromDatabase();
            foreach (var photo in photoes)
            {
                photo.Normalize();
                photo.Like = photo.ScoreHappiness > 0.5;
            }

            WriteCsv(args[0], photoes);
        }

        private static void WriteCsv(string outpufile, List<PhotoDataForLearning> photoes)
        {
            var engine = new FileHelperEngine<PhotoDataForLearning>();
            engine.HeaderText = engine.GetFileHeader();
            engine.WriteFile(outpufile, photoes);
        }

        private static List<PhotoDataForLearning> LoadPhotoesFromDatabase()
        {
            var photoesRepository = new PhotoesRepository("name=VkPhotosDB");
            var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.CreateMap<AnalyzedPhoto, PhotoDataForLearning>(); }));

            var photoes = mapper.DefaultContext.Mapper.Map<List<PhotoDataForLearning>>(photoesRepository.Select());
            return photoes;
        }
    }
}
