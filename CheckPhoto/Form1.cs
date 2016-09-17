using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AutoMapper;
using Microsoft.ProjectOxford.Emotion;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using ModelToLearn;
using VkAnalyzedPhotosDAL;

namespace CheckPhoto
{
    public partial class Form1 : Form
    {
        private const string APIKeyFace = "42bf42f5303a41c494cccf8b2452c3bd";
        public static string APIKeyEmotion = "b1688775e01f4a3b8139c40c6fd26541";
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            
            Task.Run(() =>Check());
        }

        private void Check()
        {
            var faceClient = new FaceServiceClient(APIKeyFace);
            var emotionClient = new EmotionServiceClient(APIKeyEmotion);
            List<FaceAttributeType> faceAttributesToGet = Enum.GetValues(typeof (FaceAttributeType))
                .Cast<FaceAttributeType>().ToList();

            var dto = VkAnalyzedPhotosDAL.AnalyzedPhoto.Detect(faceClient,
                textBox1.Text, faceAttributesToGet, emotionClient);
            var mapper = new Mapper(new MapperConfiguration(cfg => { cfg.CreateMap<AnalyzedPhoto, PhotoDataForLearning>(); }));
            var photoToAnalyse = mapper.DefaultContext.Mapper.Map<PhotoDataForLearning>(dto);
            photoToAnalyse.Normalize();
            var task = CheckPhoto.InvokeRequestResponseService(photoToAnalyse).ContinueWith(
                (result) => this.Invoke(new Action(
                    () => {
                        dynamic res = Newtonsoft.Json.JsonConvert.DeserializeObject(result.Result);
                        var values = Newtonsoft.Json.JsonConvert.DeserializeObject<List<string>>(
                                res.Results.output1.value.Values[0].ToString());
                        var mark = values[values.Count - 2];
                        MessageBox.Show((string)mark);
                    })));
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            try
            {
                webBrowser1.Url = new Uri(textBox1.Text);
            }
            catch { }
        }
    }
}
