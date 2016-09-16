using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Common;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using VkAnalyzedPhotosDAL;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace FillVkDatabase
{
    class Program
    {
        public static string APIKeyFace = "<Face API Key Here>";
        public static string APIKeyEmotion = "<Emotion API Key Here>";

        static void Main(string[] args)
        {

            ulong appID = 5630879;                      // ID приложения
            string email = "<Email Here>";         // email или телефон
            string pass = "<Pass Here>";               // пароль для авторизации
            Settings scope = Settings.Friends;      // Приложение имеет доступ к друзьям
            var vk = new VkApi();
            vk.Authorize(new ApiAuthParams
            {
                ApplicationId = appID,
                Login = email,
                Password = pass,
                Settings = scope
            });

            var myfriends = vk.Friends.Get(new FriendsGetParams() {Fields = ProfileFields.PhotoMaxOrig});

            List<string> urls = new List<string>();

            foreach (var myfriend in myfriends)
            {
                var urlToAdd = myfriend.PhotoMaxOrig.ToString();
                if (urlToAdd != "http://vk.com/images/camera_400.png")
                {
                    if (!urls.Any(u => u.Equals(urlToAdd)))
                    {
                        urls.Add(urlToAdd);
                    }
                }

                try
                {
                    var friendsOfMyFriend =
                        vk.Friends.Get(new FriendsGetParams()
                        {
                            UserId = myfriend.Id,
                            Fields = ProfileFields.PhotoMaxOrig
                        });
                    foreach (var friendOfMyFriend in friendsOfMyFriend)
                    {
                        var anotherUrlToAdd = friendOfMyFriend.PhotoMaxOrig.ToString();
                        if (anotherUrlToAdd != "http://vk.com/images/camera_400.png")
                        {
                            if (!urls.Any(u => u.Equals(anotherUrlToAdd)))
                            {
                                urls.Add(anotherUrlToAdd);
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    // No need to handle this for now.
                }
            }
            
            // If we need to download them.

            //int i = 0;
            //foreach (var url in urls)
            //{
            //    i++;
            //    WebClient webClient = new WebClient();
            //    var path = string.Format(@"E:\MyFriendsOfFriends\{0}.jpg", i);

            //    if (!File.Exists(path))
            //    {
            //        try
            //        {
            //            webClient.DownloadFile(url, path);
            //        }
            //        catch (Exception e)
            //        {
            //            // No need to handle it for now also.
            //        }
            //    }
            //}


            //var testImageUrl = @"https://pp.vk.me/c604629/v604629126/70fc/wmMPgTyNMko.jpg";
            
            var faceClient = new FaceServiceClient(APIKeyFace);
            var emotionClient = new EmotionServiceClient(APIKeyEmotion);

            List<FaceAttributeType> faceAttributesToGet = Enum.GetValues(typeof(FaceAttributeType)).Cast<FaceAttributeType>().ToList();

            foreach (var url in urls.Skip(99).Take(1901))
            {
                var detectFaceTask = faceClient.DetectAsync(url, true, true, faceAttributesToGet);
                Thread.Sleep(3000);
                try
                {
                    detectFaceTask.Wait();
                    if (detectFaceTask.Result.Any())
                    {
                        Face f = detectFaceTask.Result[0];

                        var detectEmotionTask = emotionClient.RecognizeAsync(url,
                            new Rectangle[]
                            {
                                new Rectangle()
                                {
                                    Height = f.FaceRectangle.Height,
                                    Left = f.FaceRectangle.Left,
                                    Top = f.FaceRectangle.Top,
                                    Width = f.FaceRectangle.Width
                                }
                            });

                        detectEmotionTask.Wait();

                        Emotion e = detectEmotionTask.Result[0];

                        var dto = new AnalyzedPhoto(url, f, e);

                        VkPhotosAPI.Create(dto);
                    }
                }
                catch (Exception ex)
                {
                    // Still can fail if VK url server is down.
                }
            }
        }
    }
}
