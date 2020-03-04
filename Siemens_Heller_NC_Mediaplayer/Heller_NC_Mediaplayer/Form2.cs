using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;

namespace Heller_NC_Mediaplayer
{
    public partial class Form2 : Form
    {
        Form1 Videofenster = new Form1();
        List<int> VideoStartzeiten = new List<int>();
        List<int> VideoEndzeiten = new List<int>();
        List<string> VideoPfade = new List<string>();
        int Status = 0;
        bool Played = false;
        int Zeit = 0;
        int Lastline = 0;

        public Form2()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Video 1
            //VideoStartzeiten.Add(1000);
            //VideoEndzeiten.Add(1500);
            //VideoPfade.Add("1.wmv");

            ////Video 2
            //VideoStartzeiten.Add(2000);
            //VideoEndzeiten.Add(3000);
            //VideoPfade.Add("2.wmv");

            Reset();
            Videofenster.Show();
            timer1.Start();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            string Abfrage = "http://ultrasonic10:8080/?request=/Channel/ProgramInfo/actLineNumber";
            Zeit = Convert.ToInt32(HTTPAbfrage(Abfrage));

            if (Zeit < Lastline)
            {
                Console.WriteLine("Reset");
                Reset();
            }
            Lastline = Zeit;
            if (Status < VideoPfade.Count)
            {
                if (Zeit > VideoStartzeiten[Status] && Zeit < VideoEndzeiten[Status] && Played == false)
                {
                    Videofenster.Abspielen(VideoPfade[Status]);
                    Played = true;
                }

                if (Zeit > VideoEndzeiten[Status] && Played == true)
                {
                    Videofenster.Stoppen();
                    Played = false;
                    Status++;
                }
            }
            Console.WriteLine(Status);
            Console.WriteLine(Zeit);
            Console.WriteLine(Lastline);

        }

        private void Reset()
        {
            Videofenster.Stoppen();
            Status = 0;
            Played = false;
        }

        string HTTPAbfrage(string requeststring)
        {
            HttpWebRequest Anfrage = (HttpWebRequest)WebRequest.Create(requeststring);
            HttpWebResponse Antwort = (HttpWebResponse)Anfrage.GetResponse();
            System.IO.Stream Antwort_Stream = Antwort.GetResponseStream();
            System.IO.StreamReader Antwort_Streamreader = new System.IO.StreamReader(Antwort_Stream);
            String Ausgabe = Antwort_Streamreader.ReadToEnd();
            return Ausgabe;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            System.IO.StreamReader SR = new System.IO.StreamReader("Videos.csv");

            while (SR.EndOfStream == false)
            {
                String[] Zeile = SR.ReadLine().Split(';');
                VideoStartzeiten.Add(Convert.ToInt32(Zeile[1]));
                VideoEndzeiten.Add(Convert.ToInt32(Zeile[2]));
                VideoPfade.Add(Zeile[0]);
            }
            button2.Enabled = true;
            button1.Enabled = false;
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            button2.Enabled = false;
        }
    }
}
