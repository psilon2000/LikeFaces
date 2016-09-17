using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ModelToLearn;
using VkAnalyzedPhotosDAL;

namespace CheckPhoto
{
    public class StringTable
    {
        public string[] ColumnNames { get; set; }
        public string[][] Values { get; set; }
    }

    class CheckPhoto
    {
        public const string WebServiceKey = "9zFhBWNGtDbl6NcDYYXB16pqYvICRPrqqnKpC0Li/W1nbmTV/5sYIqcFuka96QaQB1BW3gN0Ss9syRTVptjgCA==";

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

        public static async Task<string> InvokeRequestResponseService(PhotoDataForLearning analyzedPhoto)
        {
            NumberFormatInfo nfi = new NumberFormatInfo {NumberDecimalSeparator = "."};

            var sampleData = new List<PhotoDataForLearning> { analyzedPhoto};
            var type = typeof(PhotoDataForLearning);
            var properties = type.GetProperties();
            
            var stringData = sampleData.Select(data => ColumnNames
               .Select(columnName =>
               {
                   var propInfo = properties.First(p => p.Name == columnName);
                   var prop = propInfo.GetValue(data);
                   return propInfo.PropertyType == typeof(double) ? ((double)prop).ToString(nfi) : prop.ToString();
               }).ToArray()).ToArray();

            using (var client = new HttpClient())
            {
                var scoreRequest = new
                {

                    Inputs = new Dictionary<string, StringTable>() {
                        {
                            "input1",
                            new StringTable()
                            {
                                ColumnNames = ColumnNames,
                                Values = stringData
                            }
                        },
                    },
                    GlobalParameters = new Dictionary<string, string>()
                    {
                    }
                };

                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", WebServiceKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/b73b66623fff4cdbae8f403ebdd34ed7/services/7d3a798775a544a1ad1409c3f1455ff7/execute?api-version=2.0&details=true");

                // WARNING: The 'await' statement below can result in a deadlock if you are calling this code from the UI thread of an ASP.Net application.
                // One way to address this would be to call ConfigureAwait(false) so that the execution does not attempt to resume on the original context.
                // For instance, replace code such as:
                //      result = await DoSomeTask()
                // with the following:
                //      result = await DoSomeTask().ConfigureAwait(false)


                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);

                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Trace.WriteLine("Result: {0}", result);
                    return result;
                }
                else
                {
                    Trace.WriteLine($"The request failed with status code: {response.StatusCode}");

                    // Print the headers - they include the requert ID and the timestamp, which are useful for debugging the failure
                    Trace.WriteLine(response.Headers.ToString());

                    string responseContent = await response.Content.ReadAsStringAsync();
                    Trace.WriteLine(responseContent);
                    return responseContent;
                }
            }
        }
    }
}