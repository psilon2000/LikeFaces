using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ProjectOxford.Emotion.Contract;
using Microsoft.ProjectOxford.Face.Contract;

namespace VkAnalyzedPhotosDAL
{
    public class AnalyzedPhoto
    {
        public AnalyzedPhoto(string url, Face face, Emotion emotion)
        {
            Url = url;

            FaceId = face.FaceId;
            FaceWidth = face.FaceRectangle.Width;
            FaceHeight = face.FaceRectangle.Height;
            FacePositionLeft = face.FaceRectangle.Left;
            FacePositionTop = face.FaceRectangle.Top;
            PupilLeftX = face.FaceLandmarks.PupilLeft.X;
            PupilLeftY = face.FaceLandmarks.PupilLeft.Y;

            PupilRightX = face.FaceLandmarks.PupilRight.X;
            PupilRightY = face.FaceLandmarks.PupilRight.Y;

            NoseTipX = face.FaceLandmarks.NoseTip.X;
            NoseTipY = face.FaceLandmarks.NoseTip.Y;

            MouthLeftX = face.FaceLandmarks.MouthLeft.X;
            MouthLeftY = face.FaceLandmarks.MouthLeft.Y;

            MouthRightX = face.FaceLandmarks.MouthRight.X;
            MouthRightY = face.FaceLandmarks.MouthRight.Y;

            EyebrowLeftOuterX = face.FaceLandmarks.EyebrowLeftOuter.X;
            EyebrowLeftOuterY = face.FaceLandmarks.EyebrowLeftOuter.Y;

            EyebrowLeftInnerX = face.FaceLandmarks.EyebrowLeftInner.X;
            EyebrowLeftInnerY = face.FaceLandmarks.EyebrowLeftInner.Y;

            EyeLeftOuterX = face.FaceLandmarks.EyeLeftOuter.X;
            EyeLeftOuterY = face.FaceLandmarks.EyeLeftOuter.Y;

            EyeLeftTopX = face.FaceLandmarks.EyeLeftTop.X;
            EyeLeftTopY = face.FaceLandmarks.EyeLeftTop.Y;

            EyeLeftBottomX = face.FaceLandmarks.EyeLeftBottom.X;
            EyeLeftBottomY = face.FaceLandmarks.EyeLeftBottom.Y;

            EyeLeftInnerX = face.FaceLandmarks.EyeLeftInner.X;
            EyeLeftInnerY = face.FaceLandmarks.EyeLeftInner.Y;

            EyebrowRightOuterX = face.FaceLandmarks.EyebrowRightOuter.X;
            EyebrowRightOuterY = face.FaceLandmarks.EyebrowRightOuter.Y;

            EyebrowRightInnerX = face.FaceLandmarks.EyebrowRightInner.X;
            EyebrowRightInnerY = face.FaceLandmarks.EyebrowRightInner.Y;

            EyeRightOuterX = face.FaceLandmarks.EyeRightOuter.X;
            EyeRightOuterY = face.FaceLandmarks.EyeRightOuter.Y;

            EyeRightTopX = face.FaceLandmarks.EyeRightTop.X;
            EyeRightTopY = face.FaceLandmarks.EyeRightTop.Y;

            EyeRightBottomX = face.FaceLandmarks.EyeRightBottom.X;
            EyeRightBottomY = face.FaceLandmarks.EyeRightBottom.Y;

            EyeRightInnerX = face.FaceLandmarks.EyeRightInner.X;
            EyeRightInnerY = face.FaceLandmarks.EyeRightInner.Y;


            NoseRootLeftX = face.FaceLandmarks.NoseRootLeft.X;
            NoseRootLeftY = face.FaceLandmarks.NoseRootLeft.X;

            NoseRootRightX = face.FaceLandmarks.NoseRootRight.X;
            NoseRootRightY = face.FaceLandmarks.NoseRootRight.Y;

            NoseLeftAlarTopX = face.FaceLandmarks.NoseLeftAlarTop.X;
            NoseLeftAlarTopY = face.FaceLandmarks.NoseLeftAlarTop.Y;

            NoseRightAlarTopX = face.FaceLandmarks.NoseRightAlarTop.X;
            NoseRightAlarTopY = face.FaceLandmarks.NoseRightAlarTop.Y;

            NoseLeftAlarOutTipX = face.FaceLandmarks.NoseLeftAlarOutTip.X;
            NoseLeftAlarOutTipY = face.FaceLandmarks.NoseLeftAlarOutTip.Y;

            NoseRightAlarOutTipX = face.FaceLandmarks.NoseRightAlarOutTip.X;
            NoseRightAlarOutTipY = face.FaceLandmarks.NoseRightAlarOutTip.Y;

            UpperLipTopX = face.FaceLandmarks.UpperLipTop.X;
            UpperLipTopY = face.FaceLandmarks.UpperLipTop.Y;

            UpperLipBottomX = face.FaceLandmarks.UpperLipBottom.X;
            UpperLipBottomY = face.FaceLandmarks.UpperLipBottom.Y;

            UnderLipTopX = face.FaceLandmarks.UnderLipTop.X;
            UnderLipTopY = face.FaceLandmarks.UnderLipTop.Y;

            UnderLipBottomX = face.FaceLandmarks.UnderLipBottom.X;
            UnderLipBottomY = face.FaceLandmarks.UnderLipBottom.Y;

            Age = face.FaceAttributes.Age;

            Gender = face.FaceAttributes.Gender;

            Smile = face.FaceAttributes.Smile;

            FacialHairMustache = face.FaceAttributes.FacialHair.Moustache;

            FacialHairBeard = face.FaceAttributes.FacialHair.Beard;

            FacialHairSideburns = face.FaceAttributes.FacialHair.Sideburns;

            Glasses = face.FaceAttributes.Glasses.ToString();

            HeadPoseRoll = face.FaceAttributes.HeadPose.Roll;

            HeadPoseYaw = face.FaceAttributes.HeadPose.Yaw;

            HeadPosePitch = face.FaceAttributes.HeadPose.Pitch;

            ScoreFear = emotion.Scores.Fear;
            ScoreAnger = emotion.Scores.Anger;
            ScoreContempt = emotion.Scores.Contempt;
            ScoreDisgust = emotion.Scores.Disgust;
            ScoreHappiness = emotion.Scores.Happiness;
            ScoreNeutral = emotion.Scores.Neutral;
            ScoreSandess = emotion.Scores.Sadness;
            ScoreSurprise = emotion.Scores.Surprise;
        }

        public long Id { get; set; }

        public string Url { get; set; }

        public Guid FaceId { get; set; }

        public int FaceWidth { get; set; }

        public int FaceHeight { get; set; }

        public int FacePositionLeft { get; set; }

        public int FacePositionTop { get; set; }

        public double PupilLeftX { get; set; }
        public double PupilLeftY { get; set; }

        public double PupilRightX { get; set; }
        public double PupilRightY { get; set; }

        public double NoseTipX { get; set; }
        public double NoseTipY { get; set; }

        public double MouthLeftX { get; set; }
        public double MouthLeftY { get; set; }

        public double MouthRightX { get; set; }
        public double MouthRightY { get; set; }

        public double EyebrowLeftOuterX { get; set; }
        public double EyebrowLeftOuterY { get; set; }

        public double EyebrowLeftInnerX { get; set; }
        public double EyebrowLeftInnerY { get; set; }

        public double EyeLeftOuterX { get; set; }
        public double EyeLeftOuterY { get; set; }

        public double EyeLeftTopX { get; set; }
        public double EyeLeftTopY { get; set; }

        public double EyeLeftBottomX { get; set; }
        public double EyeLeftBottomY { get; set; }

        public double EyeLeftInnerX { get; set; }
        public double EyeLeftInnerY { get; set; }

        public double EyebrowRightOuterX { get; set; }
        public double EyebrowRightOuterY { get; set; }

        public double EyebrowRightInnerX { get; set; }
        public double EyebrowRightInnerY { get; set; }

        public double EyeRightOuterX { get; set; }
        public double EyeRightOuterY { get; set; }

        public double EyeRightTopX { get; set; }
        public double EyeRightTopY { get; set; }

        public double EyeRightBottomX { get; set; }
        public double EyeRightBottomY { get; set; }

        public double EyeRightInnerX { get; set; }
        public double EyeRightInnerY { get; set; }

        public double NoseRootLeftX { get; set; }
        public double NoseRootLeftY { get; set; }

        public double NoseRootRightX { get; set; }
        public double NoseRootRightY { get; set; }

        public double NoseLeftAlarTopX { get; set; }
        public double NoseLeftAlarTopY { get; set; }

        public double NoseRightAlarTopX { get; set; }
        public double NoseRightAlarTopY { get; set; }

        public double NoseLeftAlarOutTipX { get; set; }
        public double NoseLeftAlarOutTipY { get; set; }

        public double NoseRightAlarOutTipX { get; set; }
        public double NoseRightAlarOutTipY { get; set; }

        public double UpperLipTopX { get; set; }
        public double UpperLipTopY { get; set; }

        public double UpperLipBottomX { get; set; }
        public double UpperLipBottomY { get; set; }

        public double UnderLipTopX { get; set; }
        public double UnderLipTopY { get; set; }

        public double UnderLipBottomX { get; set; }
        public double UnderLipBottomY { get; set; }

        public double Age { get; set; }

        public string Gender { get; set; }

        public double Smile { get; set; }

        public double FacialHairMustache { get; set; }

        public double FacialHairBeard { get; set; }

        public double FacialHairSideburns { get; set; }

        public string Glasses { get; set; }

        public double HeadPoseRoll { get; set; }

        public double HeadPoseYaw { get; set; }

        public double HeadPosePitch { get; set; }

        public double ScoreNeutral { get; set; }

        public double ScoreFear { get; set; }

        public double ScoreHappiness { get; set; }

        public double ScoreSandess { get; set; }

        public double ScoreAnger { get; set; }

        public double ScoreContempt { get; set; }

        public double ScoreDisgust { get; set; }

        public double ScoreSurprise { get; set; }
    }
}
