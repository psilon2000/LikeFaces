

// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using ModelToLearn;
using Newtonsoft.Json;
using VkAnalyzedPhotosDAL;

namespace RetrainModel
{
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[][] Values { get; set; }
    }

    internal class Program
    {
        private static void Main(string[] args)
        {
            InvokeRequestResponseService().Wait();
        }
        const string APIKey =
                    "AdzRtrX2k7brGb+ekfYecpRHn8Ufe2aldYPyFxIYGsz3+e0Dx2TyzbVylO/VGVgZ2cXwIixo5RgXR356oBRsjA==";

        private static readonly string[] ColumnNames = new[]
        {
            "Id", "FaceWidth", "FaceHeight", "FacePositionLeft", "FacePositionTop",
            "PupilLeftX", "PupilLeftY", "PupilRightX", "PupilRightY", "NoseTipX", "NoseTipY",
            "MouthLeftX", "MouthLeftY", "MouthRightX", "MouthRightY", "EyebrowLeftOuterX",
            "EyebrowLeftOuterY", "EyebrowLeftInnerX", "EyebrowLeftInnerY", "EyeLeftOuterX",
            "EyeLeftOuterY", "EyeLeftTopX", "EyeLeftTopY", "EyeLeftBottomX",
            "EyeLeftBottomY", "EyeLeftInnerX", "EyeLeftInnerY", "EyebrowRightOuterX",
            "EyebrowRightOuterY", "EyebrowRightInnerX", "EyebrowRightInnerY",
            "EyeRightOuterX", "EyeRightOuterY", "EyeRightTopX", "EyeRightTopY",
            "EyeRightBottomX", "EyeRightBottomY", "EyeRightInnerX", "EyeRightInnerY",
            "NoseRootLeftX", "NoseRootLeftY", "NoseRootRightX", "NoseRootRightY",
            "NoseLeftAlarTopX", "NoseLeftAlarTopY", "NoseRightAlarTopX", "NoseRightAlarTopY",
            "NoseLeftAlarOutTipX", "NoseLeftAlarOutTipY", "NoseRightAlarOutTipX",
            "NoseRightAlarOutTipY", "UpperLipTopX", "UpperLipTopY", "UpperLipBottomX",
            "UpperLipBottomY", "UnderLipTopX", "UnderLipTopY", "UnderLipBottomX",
            "UnderLipBottomY", "Age", "Gender", "Smile", "FacialHairMustache",
            "FacialHairBeard", "FacialHairSideburns", "Glasses", "HeadPoseRoll",
            "HeadPoseYaw", "HeadPosePitch", "ScoreNeutral", "ScoreFear", "ScoreHappiness",
            "ScoreSandess", "ScoreAnger", "ScoreContempt", "ScoreDisgust", "ScoreSurprise",
            "Like"
        };

        private static List<PhotoDataForLearning> LoadPhotoesFromDatabase()
        {
            var photoesRepository = new PhotoesRepository("name=VkPhotosDB");
            var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.CreateMap<AnalyzedPhoto, PhotoDataForLearning>(); }));

            var photoes = mapper.DefaultContext.Mapper.Map<List<PhotoDataForLearning>>(photoesRepository.Select());
            return photoes;
        }

        private static async Task InvokeRequestResponseService()
        {
            var sampleData = LoadPhotoesFromDatabase();
            var type = typeof (PhotoDataForLearning);
            var properties = type.GetProperties();
            var stringData =  sampleData.Select(data => ColumnNames
                .Select(columnName => properties.First(p => p.Name == columnName)
                .GetValue(data).ToString()).ToArray()).ToArray();

            using (var client = new HttpClient())
            {
                
                var scoreRequest = new
                {
                    
                    Inputs = new Dictionary<string, StringTable>
                    {
                        {
                            "input1",
                            new StringTable
                            {
                                ColumnNames = ColumnNames,
                                Values = stringData
                                    //new[,]
                                    //{
                                    //    {
                                    //        "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "value", "0", "0", "0", "0", "value", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "0", "0", "0", "0"
                                    //    },
                                    //    {
                                    //        "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "value", "0", "0", "0", "0", "value", "0", "0", "0", "0", "0", "0", "0", "0",
                                    //        "0", "0", "0", "0"
                                    //    }
                                    //}
                            }
                        }
                    },
                    GlobalParameters = new Dictionary<string, string>()
                };
                
                    // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);

                client.BaseAddress =
                    new Uri(
                        "https://ussouthcentral.services.azureml.net/workspaces/b73b66623fff4cdbae8f403ebdd34ed7/services/a5ca26e5f3804ce4a16ddca3bb46d915/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                var response = await client.PostAsJsonAsync("", scoreRequest);
                

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine($"The request failed with status code: {response.StatusCode}");

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Console.WriteLine(response.Headers.ToString());

                    var responseContent = await response.Content.ReadAsStringAsync();
                    Console.WriteLine(responseContent);
                }
            }
        }

        //private async Task OverwriteModel()
        //{
        //    var resourceLocations = new
        //    {
        //        Resources = new[]
        //        {
        //            new
        //            {
        //                Name = "Census Model [trained model]",
        //                Location = new AzureBlobDataReference
        //                {
        //                    BaseLocation = "https://esintussouthsus.blob.core.windows.net/",
        //                    RelativeLocation = "your endpoint relative location",
        //                    //from the output, for example: “experimentoutput/8946abfd-79d6-4438-89a9-3e5d109183/8946abfd-79d6-4438-89a9-3e5d109183.ilearner”
        //                    SasBlobToken = "your endpoint SAS blob token"
        //                    //from the output, for example: “?sv=2013-08-15&sr=c&sig=37lTTfngRwxCcf94%3D&st=2015-01-30T22%3A53%3A06Z&se=2015-01-31T22%3A58%3A06Z&sp=rl”
        //                }
        //            }
        //        }
        //    };

        //    using (var client = new HttpClient())
        //    {
        //        client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", APIKey);

        //        using (var request = new HttpRequestMessage(new HttpMethod("PATCH"), endpointUrl))
        //        {
        //            request.Content = new StringContent(JsonConvert.SerializeObject(resourceLocations), Encoding.UTF8,
        //                "application/json");
        //            var response = await client.SendAsync(request);

        //            if (!response.IsSuccessStatusCode)
        //            {
        //                await WriteFailedResponse(response);
        //            }

        //            // Do what you want with a successful response here.
        //        }
        //    }
        //}
    }
}

