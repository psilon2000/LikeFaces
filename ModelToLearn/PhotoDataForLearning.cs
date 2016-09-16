using System;
using FileHelpers;

namespace BaseToCsvConverter
{
    [DelimitedRecord(",")]
    public class PhotoDataForLearning
    {
        public PhotoDataForLearning() { }

        public long Id { get; set; }

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
        public bool Like { get; set; }

        public void Normalize()
        {
            MoveByTop();
            MoveByLeft();
            NormByWidth();
            NormByHeight();
        }

        private void MoveByLeft()
        {
            var leftPosition = FacePositionLeft;
            foreach (var property in typeof(PhotoDataForLearning).GetProperties())
            {
                if (property.Name.EndsWith("X") && property.PropertyType == typeof(int))
                {
                    property.SetValue(this, (int)property.GetValue(this) - leftPosition);
                }
                if (property.Name.EndsWith("X") && property.PropertyType == typeof(double))
                {
                    property.SetValue(this, (double)property.GetValue(this) - leftPosition);
                }
            }
            FacePositionLeft = 0;
        }

        private void MoveByTop()
        {
            var topPosition = FacePositionTop;
            foreach (var property in typeof(PhotoDataForLearning).GetProperties())
            {
                if (property.Name.EndsWith("Y") && property.PropertyType == typeof(int))
                {
                    property.SetValue(this, (int)property.GetValue(this) - topPosition);
                }
                if (property.Name.EndsWith("Y") && property.PropertyType == typeof(double))
                {
                    property.SetValue(this, (double)property.GetValue(this) - topPosition);
                }
            }
            FacePositionTop = 0;
        }

        private void NormByWidth()
        {
            var faceWidth = FaceWidth;
            foreach (var property in typeof(PhotoDataForLearning).GetProperties())
            {
                if (property.Name.EndsWith("X") && property.PropertyType == typeof(int))
                {
                    property.SetValue(this, (int)property.GetValue(this) / faceWidth);
                }
                if (property.Name.EndsWith("X") && property.PropertyType == typeof(double))
                {
                    property.SetValue(this, (double)property.GetValue(this) / faceWidth);
                }
            }
            FaceWidth = 1;
        }

        private void NormByHeight()
        {
            var faceHeight = FaceHeight;
            foreach (var property in typeof(PhotoDataForLearning).GetProperties())
            {
                if (property.Name.EndsWith("Y") && property.PropertyType == typeof(int))
                {
                    property.SetValue(this, (int)property.GetValue(this) / faceHeight);
                }
                if (property.Name.EndsWith("Y") && property.PropertyType == typeof(double))
                {
                    property.SetValue(this, (double)property.GetValue(this) / faceHeight);
                }
            }
            FaceHeight = 1;
        }
    }
}
