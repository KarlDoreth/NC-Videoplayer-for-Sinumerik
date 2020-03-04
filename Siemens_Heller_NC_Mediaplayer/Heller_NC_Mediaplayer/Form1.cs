using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Heller_NC_Mediaplayer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            axWindowsMediaPlayer1.settings.autoStart = false;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.stretchToFit = true;
        }

        public void Abspielen(string VideoDateiName)
        {
            axWindowsMediaPlayer1.URL = VideoDateiName;
            axWindowsMediaPlayer1.uiMode = "none";
            axWindowsMediaPlayer1.stretchToFit = true;
            axWindowsMediaPlayer1.Ctlcontrols.play();
        }

        public void Stoppen()
        {
            axWindowsMediaPlayer1.Ctlcontrols.stop();
        }
    }
}
