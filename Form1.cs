using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Emgu.CV;
using Emgu.CV.CvEnum;
using Emgu.CV.Structure;
using Emgu.Util;

namespace Resolucion1
{
    public partial class Form1 : Form
    {
        static Image<Bgr, Byte> img;
         Image<Gray, Byte> grayImage;
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Buscar_foto();
            draw();
        }

        private void draw()
        {
            Image<Bgr, Byte> img2 = img.Clone();
            grayImage = img.Convert<Gray, Byte>();
            Image<Gray, byte> canny = grayImage.Canny(trackBar1.Value, trackBar2.Value);
            pictureBox1.Image = img.ToBitmap();
            Emgu.CV.Util.VectorOfVectorOfPoint contorno = new Emgu.CV.Util.VectorOfVectorOfPoint();
            Mat hier = new Mat();
            CvInvoke.FindContours(canny, contorno, hier, Emgu.CV.CvEnum.RetrType.List, Emgu.CV.CvEnum.ChainApproxMethod.ChainApproxNone);
            pictureBox2.Image = canny.ToBitmap();
            CvInvoke.DrawContours(img, contorno, -1, new MCvScalar(255, 0, 0));
            pictureBox1.Image = img.ToBitmap();
            pictureBox3.Image = img2.ToBitmap();
            img = img2;
        }

        private void Buscar_foto()
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();
            string ruta;

            try
            {

                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    ruta = openFileDialog1.FileName;
                    img = new Image<Bgr, Byte>(ruta);
                    Console.WriteLine(ruta);
                }
            }
            catch
            {
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            draw();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            draw();
        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }
    }
}
