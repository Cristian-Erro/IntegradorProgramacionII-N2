using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AForge.Video;
using AForge.Video.DirectShow;

namespace CapaPrincipal
{
    public partial class frmCamara : Form
    {
        private bool HayDispositivos;
        private FilterInfoCollection MiDispositivos;
        private VideoCaptureDevice MiWebCam = null;
        public frmCamara()
        {
            InitializeComponent();
        }

        private void frmCamara_Load(object sender, EventArgs e)
        {
            CargaDispositivos();
        }

        public void CargaDispositivos()
        {
            MiDispositivos = new FilterInfoCollection(FilterCategory.VideoInputDevice);
            if(MiDispositivos.Count > 0 )
            {
                HayDispositivos = true;
                for(int i = 0;i< MiDispositivos.Count; i++)
                {
                    comboBox1.Items.Add(MiDispositivos[i].Name.ToString());
                }
                comboBox1.Text = MiDispositivos[0].ToString();
            }
            else
            {
                HayDispositivos = false;
            }
        }
        public void CerrarWebCam()
        {
            if(MiWebCam != null && MiWebCam.IsRunning)
            {
                MiWebCam.SignalToStop();
                MiWebCam = null;
            }
        }

        private void iconButton1_Click(object sender, EventArgs e)
        {
            CerrarWebCam();
            int i = comboBox1.SelectedIndex;
            string NombreVideo = MiDispositivos[i].MonikerString;
            MiWebCam = new VideoCaptureDevice(NombreVideo);
            MiWebCam.NewFrame += new NewFrameEventHandler(Capturando);
            MiWebCam.Start();

        }
        private void Capturando(object sender,NewFrameEventArgs eventArgs)
        {
            Bitmap Imagen=(Bitmap)eventArgs.Frame.Clone();
            pictureBox1.Image = Imagen;
        }

        private void frmCamara_FormClosed(object sender, FormClosedEventArgs e)
        {
            CerrarWebCam();
        }
    }
}
