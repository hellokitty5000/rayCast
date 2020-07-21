using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rayCastAidanMcNabb
{
    public partial class Form1 : Form
    {
        Graphics gfx;

        Bitmap canvas;
        public Form1()
        {
            InitializeComponent();
        }
        Point basePoint;
        int accuracy = 100;
      
        //start at top

        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(canvas);
        }
        void generateRay(double angle)
        {
          
           double x = basePoint.X;
           double y = basePoint.Y;
           while (x > 0 && y > 0 && x < ClientSize.Width && y < ClientSize.Height)
           {
                x +=  Math.Cos(angle);
                y +=  Math.Sin(angle);
           }
           gfx.DrawLine(Pens.White, basePoint, new Point((int)x,(int)y));
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           
        }   

        private void timer1_Tick(object sender, EventArgs e)
        {
            gfx.Clear(Color.Black);
            Point cursor = this.PointToClient(Cursor.Position);
            basePoint = cursor;//new Point(.X, e.Y);
            for (int i = 0; i < accuracy; i++)
            {
                //gfx.DrawLine(Pens.Black, basePoint, new Point(basePoint.X + (int)(700 * Math.Cos(2 * Math.PI * i / accuracy)), basePoint.Y + (int)(700 * Math.Sin(2 * Math.PI * i / accuracy))));
                generateRay(2 * Math.PI * i / accuracy);
            }

            pictureBox1.Image = canvas;
        }
    }
}
