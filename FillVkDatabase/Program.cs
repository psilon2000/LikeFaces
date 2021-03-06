﻿using System;
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
                catch (Exception)
                {
                    // No need to handle this for now.
                }
            }
            
            // If we need to download them.
            var faceClient = new FaceServiceClient(APIKeyFace);
            var emotionClient = new EmotionServiceClient(APIKeyEmotion);

            List<FaceAttributeType> faceAttributesToGet = Enum.GetValues(typeof(FaceAttributeType)).Cast<FaceAttributeType>().ToList();
            var photoDatabase = new PhotoesRepository("name=VkPhotosDB");

            foreach (var url in urls.Skip(99).Take(1901))
            {
                var dto  = AnalyzedPhoto.Detect(faceClient, url, faceAttributesToGet, emotionClient);
                photoDatabase.Add(dto);
            }
            
        }


    }
}
