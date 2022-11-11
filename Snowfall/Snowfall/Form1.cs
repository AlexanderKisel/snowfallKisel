using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Snowfall
{
    public partial class Form1 : Form
    {
        private readonly IList<meaning> meanings;
        private readonly Timer time;
        Bitmap background, bgScreen, sfScreen,snow;
        private Graphics draw;

        public Form1()
        {
            InitializeComponent();
            meanings = new List<meaning>(); 
            background = (Bitmap)Properties.Resources.forest;
            snow = (Bitmap)Properties.Resources.snowflake;
            addsnowflakes();
            time = new Timer();
            time.Interval = 1;
            time.Tick += Timer_Tick;

            bgScreen = new Bitmap(background,
                                  Screen.PrimaryScreen.WorkingArea.Width,
                                  Screen.PrimaryScreen.WorkingArea.Height + 40);

            sfScreen = new Bitmap(Screen.PrimaryScreen.WorkingArea.Width,
                                  Screen.PrimaryScreen.WorkingArea.Height + 40);

            draw = Graphics.FromImage(sfScreen);

            addsnowflakes();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            time.Stop();
            Draw();
            time.Start();
        }

        private void addsnowflakes()
        {
            var random = new Random();
            for (int i = 0; i < 100; i++)
            {
                meanings.Add(new meaning
                {
                    Y = random.Next(Screen.PrimaryScreen.WorkingArea.Width),
                    X = random.Next(Screen.PrimaryScreen.WorkingArea.Height) + Screen.PrimaryScreen.WorkingArea.Height,
                    Size = random.Next(10, 20)
                });
            }
        }

        private void Form1_Click(object sender, EventArgs e)
        {
            if (time.Enabled)
            {
                time.Stop();
            }
            else
            {
                time.Start();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Form1_Paint(object sender, PaintEventArgs e)
        {
            Draw();
        }

        private void Draw()
        {
            draw.DrawImage(bgScreen, 0, 0);

            foreach (var sf in meanings)
            {
                if (sf.X < 0)
                {
                    sf.X = sf.Size + Screen.PrimaryScreen.WorkingArea.Height;
                }

                draw.DrawImage(snow,
                    new Rectangle(sf.Y,
                                  sf.X,
                                  sf.Size,
                                  sf.Size));

                sf.X -= sf.Size;
            }

            var graph = CreateGraphics();
            graph.DrawImage(sfScreen, 0, 0);
        }
    }
}
