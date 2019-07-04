using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;


namespace project
{
    public class balls
    {
       public int x, y, w, h,draw,warn,color;
       public float move = 0.01f;
    }
    public partial class Form1 : Form
    {

        int lx, ly;
          Bitmap off,image2;
        int XC=500, YC=400,xs=500,ys=420,xe,ye;
        int drawballs = 0,countick=0;

        balls ptrav = new balls();
        List<balls> l_balls = new List<balls>();
        int vvv,first=0;
        Timer t = new Timer();
       

        bizzercruve my_cruve = new bizzercruve();
        
        int lastx, lasty;
        transform my_chara = new transform();

        line my_line = new line();
        List<line> l_line = new List<line>();

        line my_newball = new line();
        List<line> l_newballs = new List<line>();

        public Form1()
        {
            this.WindowState = FormWindowState.Maximized;
            this.Paint += new PaintEventHandler(Form1_Paint);
            this.Load += new EventHandler(Form1_Load);
            this.KeyDown += new KeyEventHandler(Form1_KeyDown);
            this.MouseDown+=new MouseEventHandler(Form1_MouseDown);
            this.MouseMove += new MouseEventHandler(Form1_MouseMove);
            t.Tick += new EventHandler(t_Tick);
            t.Interval =70;
            t.Start();
        }
        double rad, deg;
        Bitmap zum = new Bitmap("frog.PNG");

        void Form1_MouseMove(object sender, MouseEventArgs e)
        {

           
                xe = e.X;
                ye = e.Y;
                lx = xs+20;
                ly = ys - 50 ;
                my_line.xs = xs;
                my_line.ys = ys;
                my_line.xe = e.X;
                my_line.ye = e.Y;
                l_line.Add(my_line);
            /////////////////////////////////
                int dx = e.X - (int)my_chara.ps.X;
                int dy = e.Y - (int)my_chara.ps.Y;
                
                rad = Math.Atan2(dy, dx);
                deg = (rad * 180) / Math.PI;

                my_chara.image = zum;
                my_chara.image = rotateImage(my_chara.image, (float)deg - 90);


           

        }


        public Bitmap rotateImage(Bitmap bitmap, float angle)
        {
            Bitmap returnBitmap = new Bitmap(bitmap.Width, bitmap.Height);
            Graphics graphics = Graphics.FromImage(returnBitmap);
            graphics.TranslateTransform((float)bitmap.Width / 2, (float)bitmap.Height / 2);
            graphics.RotateTransform(angle);
            graphics.TranslateTransform(-(float)bitmap.Width / 2, -(float)bitmap.Height / 2);
            graphics.DrawImage(bitmap, new Point(0, 0));
            return returnBitmap;
        }
        void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            
            if (e.KeyCode == Keys.Space)
            {
                drawballs = 1;
                my_newball = new line();
                my_newball.xs = xs;
                my_newball.xe = l_line[0].xe;
                my_newball.ys = ys;
                my_newball.ye = l_line[0].ye;
                //////////////////////--------random balls on frog
                Random rr = new Random();
                
                
                my_newball.draw = 1;
                my_newball.Calc();
                if (first == 0)
                {
                    int v = rr.Next(1, 4);
                    my_newball.color = v;
                }
                if (first == 1)
                {
                    my_newball.color = vvv;
                }
                l_newballs.Add(my_newball);
                //////////////////////////////////---------------random expected bal
                Random rrr = new Random();
                vvv = rrr.Next(1, 4);
                first = 1;



            }
            
        }

        void  Form1_MouseDown(object sender, MouseEventArgs e)
        {
            my_cruve.SetControlPoint(new Point(e.X,e.Y));
            lastx = e.X;
            lasty = e.Y;
            
        }

        void t_Tick(object sender, EventArgs e)
        {
            Graphics g = this.CreateGraphics();
            if (drawballs ==1)
            {
              
                ///////////////////////////// moveballs with frog on line
                for (int i = 0; i < l_newballs.Count; i++)
                {
                    l_newballs[i].MoveStep();
                   
                }
                ////////////////////////////// disapper two ball when hit
                for (int i = 0; i < l_newballs.Count; i++)
                {
                    for (int t = 0; t < l_balls.Count; t++)
                    {
                        if (l_newballs[i].cx > l_balls[t].x && l_newballs[i].cx < l_balls[t].x + 30 && l_newballs[i].cy > l_balls[t].y && l_newballs[i].cy < l_balls[t].y + 30)
                        {
                            if (l_newballs[i].color == l_balls[t].color)
                            {
                                l_balls[t].draw = 0;

                                l_newballs[i].draw = 0;


                                for (int y = t;    ; y++)
                                {
                                    if (l_balls[y].color == l_balls[y + 1].color)
                                    {
                                        l_balls[y+1].draw = 0;
                                    }
                                    else
                                    {
                                        break;

                                    }
                                }
                                for (int y = t;  ; y--)
                                {
                                    if (l_balls[y].color == l_balls[y - 1].color)
                                    {
                                        l_balls[y-1].draw = 0;
                                    }
                                    else
                                    {
                                        break;

                                    }
                                }

                            }
                        }
                    }
                }
                ////////////////////
                
                if (countick % 3==0)
                {
                    ////////
                   
                    ///////
                   ////////////////create single ball 
                    ptrav = new balls();
                    PointF pt = my_cruve.ControlPoints[0];
                    ptrav.x = (int)pt.X;
                    ptrav.y = (int)pt.Y;
                    ptrav.w = 30;
                    ptrav.h = 30;
                    ptrav.draw = 1;
                    ptrav.warn= 10;
                    Random rr = new Random();
                    //////////////////////--------random balls oncurve
                    int v = rr.Next(1, 4);
                    ptrav.color = v;
                    ptrav.move = 0.01f;
               
                    l_balls.Add(ptrav);
                    ///////////////////////////////

                    //////////change colors
                    
                   //////////////////////////////
                   

                }
                ////////////////////////////movement of the ball with theta on the curve 
                if (drawballs ==1)
                {
                    for (int i = 0; i < l_balls.Count; i++)
                    {
                        PointF kora = my_cruve.CalcCurvePointAtTime(l_balls[i].move);
                        
                        l_balls[i].x = (int)kora.X;
                        l_balls[i].y = (int)kora.Y;
                        l_balls[i].move += 0.01f;
                        if (l_balls[i].x >= lastx - 10 && l_balls[i].x <= lastx + 10 && l_balls[i].y >= lasty - 10 && l_balls[i].y <= lasty + 10)
                        {
                            l_balls[i].draw = 0;
                         
                        }
                       
                   }
                    

                }
                /////////////////////////////////
                countick++;
            }
            
            drawdubb(g);
        }

        void Form1_Load(object sender, EventArgs e)
        {
            off = new Bitmap(this.ClientSize.Width, this.ClientSize.Height);

            my_chara.ps.X = XC;
            my_chara.ps.Y = YC;
            my_chara.pe.X = XC + 100;
            my_chara.pe.Y = YC + 100;
            my_chara.image = new Bitmap("frog.PNG");

           
            
        }

        void Form1_Paint(object sender, PaintEventArgs e)
        {
            drawdubb(e.Graphics);
        }
       

        void drawscene(Graphics g2)
        {
            g2.Clear(Color.Lavender);
           
           
           
            image2 = new Bitmap("end.PNG");
            

           

            my_cruve.DrawCurve(g2);
         
            if (drawballs == 1)
            {
                l_line[0].DrawAll(g2);
                /////////////////////---- expect ball colour
                if (vvv == 1)
                {
                    g2.FillEllipse(Brushes.Black, lx, ly,30,30);
                }

                if (vvv == 2)
                {
                    g2.FillEllipse(Brushes.Yellow, lx, ly, 30, 30);
                }
                if (vvv == 3)
                {
                    g2.FillEllipse(Brushes.Blue, lx, ly , 30, 30);
                }
               

                for (int i = 0; i < l_newballs.Count; i++)
                {
                    if (l_newballs[i].draw == 1)
                    {
                        if (l_newballs[i].color == 1)
                        {
                            l_newballs[i].DrawJustYou1(g2);

                        }
                        if (l_newballs[i].color == 2)
                        {
                            l_newballs[i].DrawJustYou2(g2);
                        }
                        if (l_newballs[i].color == 3)
                        {
                            l_newballs[i].DrawJustYou3(g2);
                        }
                      
                    }
                    
                }
                for (int i = 0; i < l_balls.Count; i++)
                {
                    if (l_balls[i].draw == 1)
                    {
                        if (l_balls[i].color == 1)////// every number present a specific  color 
                        {

                            g2.FillEllipse(Brushes.Black, l_balls[i].x, l_balls[i].y, l_balls[i].w, l_balls[i].h);
                        }
                        if (l_balls[i].color == 2)
                        {

                            g2.FillEllipse(Brushes.Yellow, l_balls[i].x, l_balls[i].y, l_balls[i].w, l_balls[i].h);
                        }
                        if (l_balls[i].color == 3)
                        {

                            g2.FillEllipse(Brushes.Blue, l_balls[i].x, l_balls[i].y, l_balls[i].w, l_balls[i].h);
                        }
                        if (l_balls[i].color == 4)
                        {
                            g2.FillEllipse(Brushes.Brown, l_balls[i].x, l_balls[i].y, l_balls[i].w, l_balls[i].h);
                        }
                    }
                      
                    


                }
                g2.DrawImage(image2, my_cruve.ControlPoints[my_cruve.ControlPoints.Count - 1].X-5, my_cruve.ControlPoints[my_cruve.ControlPoints.Count - 1].Y-20);
                g2.DrawImage(my_chara.image, my_chara.ps.X, my_chara.ps.Y, 80, 80);
                my_chara.image.MakeTransparent(my_chara.image.GetPixel(0, 0));
            }

            
        }

        void drawdubb(Graphics g)
        {
            Graphics g2 = Graphics.FromImage(off);
            drawscene(g2);
            g.DrawImage(off, 0, 0);

        }
    }
}
