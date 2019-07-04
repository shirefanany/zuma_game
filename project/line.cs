using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace project
{
    class line
    {
        public float xs, ys, xe, ye;    // should be given
        public float dx, dy, m, inv_m;
        public float cx, cy;
        public int dir;
        public int color;
        public int draw = 0;
        public bool x_based = true;
        public int spd = 30;
        public void Calc()
        {
            dy = ye - ys;
            dx = xe - xs;
            m = dy / dx;
            cx = xs;
            cy = ys;

            if (Math.Abs(dx) > Math.Abs(dy))
            {
                x_based = true;
                if (xs < xe)
                    dir = 1;
                else
                    dir = -1;
            }
            else
            {
                x_based = false;
                if (ys < ye)
                    dir = 1;
                else
                    dir = -1;
            }


        }

        public void isStopY()
        {
            if (dir == 1)
            {
                if (cy > ye)
                {
                    // you finshied your trip
                    float t;
                    t = xs; xs = xe; xe = t;
                    t = ys; ys = ye; ye = t;
                    Calc();
                }
            }
            else
            {
                if (cy < ye)
                {
                    // you finshied your trip
                    float t;
                    t = xs; xs = xe; xe = t;
                    t = ys; ys = ye; ye = t;
                    Calc();
                }
            }
        }

        public void isStopX()
        {
            if (dir == 1)
            {
                if (cx > xe)
                {
                    // you finshied your trip
                    float t;
                    t = xs; xs = xe; xe = t;
                    t = ys; ys = ye; ye = t;
                    Calc();
                }
            }
            else
            {
                if (cx < xe)
                {
                    // you finshied your trip
                    float t;
                    t = xs; xs = xe; xe = t;
                    t = ys; ys = ye; ye = t;
                    Calc();
                }
            }
        }
        public void MoveStep()
        {
            if (x_based)
            {
                cx += dir * spd;
                cy += dir * m * spd;
                
            }
            else
            {
                cy += dir * spd;
                cx += dir * (1.0f / m) * spd;
               
            }

        }

        public void DrawAll(Graphics g)
        {
            Pen p = new Pen(Brushes.Black, 7);
            g.DrawLine(p, xs+40, ys+40, xe, ye);
        }

        public void DrawJustYou1(Graphics g)
        {
            g.FillEllipse(Brushes.Black, cx - 10, cy - 10, 30, 30);
        }
        public void DrawJustYou2(Graphics g)
        {
            g.FillEllipse(Brushes.Yellow, cx - 10, cy - 10, 30, 30);
        }
        public void DrawJustYou3(Graphics g)
        {
            g.FillEllipse(Brushes.Blue, cx - 10, cy - 10, 30, 30);
        }
        //public void DrawJustYou4(Graphics g)
        //{
        //    g.FillEllipse(Brushes.Brown, cx - 10, cy - 10, 30, 30);
        //}

        public void DrawStart_End(Graphics g)
        {
            g.FillEllipse(Brushes.SkyBlue, xs - 5, ys - 5, 10, 10);
            g.FillEllipse(Brushes.SkyBlue, xe - 5, ye - 5, 10, 10);
        }

    }
}
