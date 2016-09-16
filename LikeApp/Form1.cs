using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkAnalyzedPhotosDAL;

namespace LikeApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private PhotoesRepository _database;
        private List<AnalyzedPhoto> _photoes;
        private int _index = 0;
        private void Form1_Load(object sender, EventArgs e)
        {
            _database = new PhotoesRepository("name=VkPhotosDB");
            _photoes = _database.Select();
            _photoes.Shuffle();
            _webBrowser.Url = new Uri(_photoes[_index].Url);
        }

        private void like(object sender, EventArgs e)
        {
            _database.Like(_photoes[_index].FaceId, true);
            _index++;
            _webBrowser.Url = new Uri(_photoes[_index].Url);
        }

        private void dislike(object sender, EventArgs e)
        {
            _database.Like(_photoes[_index].FaceId, false);
            _index++;
            _webBrowser.Url = new Uri(_photoes[_index].Url);
        }
    }
}
