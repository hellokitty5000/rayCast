using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace rayCastAidanMcNabb
{
    public partial class Form1 : Form
    {
        Graphics gfx;

        Bitmap canvas;
        Random rand = new Random();
        public Form1()
        {
            InitializeComponent();
        }
        Point basePoint => this.PointToClient(Cursor.Position);
        int accuracy = 100;
        (Point first, Point second) barrier;


        //start at top
       
        private void Form1_Load(object sender, EventArgs e)
        {
            canvas = new Bitmap(pictureBox1.Width, pictureBox1.Height);
            gfx = Graphics.FromImage(canvas);
            barrier = generateBarrier();
           
        }
        double u(Point wall)
        {
     
            double parallel = ((barrier.first.X - barrier.second.X) *
                (basePoint.Y - wall.Y) -
                (barrier.first.Y - barrier.second.Y) *
                (basePoint.X - wall.X));

            if (parallel == 0)
            {
                return parallel;
            }
            
             return (barrier.first.X - basePoint.X) *
             (basePoint.Y - wall.Y) -
             (barrier.first.Y - basePoint.Y) *
              (basePoint.X - wall.X) / parallel;            
                
               
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
            Point bs = new Point((int)x, (int)y);
            if (u(bs) >= 0 && u(bs) <= 1)
            {
                gfx.DrawLine(Pens.Blue, basePoint, bs);
            }
            else
            {
                gfx.DrawLine(Pens.White, basePoint, bs);
            }

        }
        (Point, Point) generateBarrier()
        {
            return (new Point(100, 100), new Point(100, 200));
            //return (new Point(rand.Next(0, ClientSize.Width), rand.Next(0, ClientSize.Height)),
            //      new Point(rand.Next(0, ClientSize.Width), rand.Next(0, ClientSize.Height)));
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
           
        }   

        private void timer1_Tick(object sender, EventArgs e)
        {
            gfx.Clear(Color.Black);
            gfx.DrawLine(Pens.Red, barrier.first, barrier.second);
            //Point cursor = this.PointToClient(Cursor.Position);
            //basePoint = cursor;
            for (int i = 0; i < accuracy; i++)
            {
                generateRay(2 * Math.PI * i / accuracy);
            }

            pictureBox1.Image = canvas;
        }
    }
}
