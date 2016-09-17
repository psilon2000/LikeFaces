
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CallRequestResponseService
{

    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[][] Values { get; set; }
    }

    class Program
    {
        public const string WebServiceKey = "an83p7djFT4v8/vDH0NRzBCBbqvQP+YiaSzArJMgUwBHK7gQca5M2qljvBnaJDSOymwaYFGq3IQt9mx1tfA6kA==";

        private static readonly string[] ColumnNames = new[]
    {
            "FaceWidth", "FaceHeight", "FacePositionLeft", "FacePositionTop",
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
        static void Main(string[] args)
        {
         //   InvokeRequestResponseService().Wait();
        }

        //    static async Task InvokeRequestResponseService()
        //    {
        //        var sampleData = LoadPhotoesFromDatabase();
        //        var type = typeof(PhotoDataForLearning);
        //        var properties = type.GetProperties();
        //        var stringData = sampleData.Select(data => ColumnNames
        //           .Select(columnName => properties.First(p => p.Name == columnName)
        //           .GetValue(data).ToString()).ToArray()).ToArray();

        //        using (var client = new HttpClient())
        //        {
        //            var scoreRequest = new
        //            {

        //                Inputs = new Dictionary<string, StringTable>() {
        //                    {
        //                        "input1",
        //                        new StringTable()
        //                        {
        //                            ColumnNames = ColumnNames,
        //                            Values = new string[,] {  { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "value", "0", "0", "0", "0", "value", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },  { "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "value", "0", "0", "0", "0", "value", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0", "0" },  }
        //                        }
        //                    },
        //                },
        //                GlobalParameters = new Dictionary<string, string>()
        //                {
        //                }
        //            };

        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", WebServiceKey);

        //            client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/b73b66623fff4cdbae8f403ebdd34ed7/services/610b3de5bc52483a807e896a40cccc11/execute?api-version=2.0&details=true");

        //            // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
        //            // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
        //            // For instance, replace code such as:
        //            //      result = await DoSomeTask()
        //            // with the following:
        //            //      result = await DoSomeTask().ConfigureAwait(false)


        //            HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                string result = await response.Content.ReadAsStringAsync();
        //                Console.WriteLine("Result: {0}", result);
        //            }
        //            else
        //            {
        //                Console.WriteLine($"The request failed with status code: {response.StatusCode}");

        //                // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
        //                Console.WriteLine(response.Headers.ToString());

        //                string responseContent = await response.Content.ReadAsStringAsync();
        //                Console.WriteLine(responseContent);
        //            }
        //        }
        //    }
        //
    }
}