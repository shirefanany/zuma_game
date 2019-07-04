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
    class transform
    {
        public int x, y;
        public PointF ps;
        public PointF pe;
        public Bitmap image;
            

        public void DrawYourSelf(Graphics g)
        {
            Pen p = new Pen(Color.Green, 5);
            g.DrawLine(p, ps.X, ps.Y, pe.X, pe.Y);
            g.FillEllipse(Brushes.Blue, ps.X - 5, ps.Y - 5, 10, 10);
            g.FillEllipse(Brushes.Red, pe.X - 5, pe.Y - 5, 10, 10);

        }

        public void Translation(float tx, float ty)
        {
            ps.X += tx;
            ps.Y += ty;

            pe.X += tx;
            pe.Y += ty;
        }

        public void Rotate(float th_degg)
        {
            double xn, yn;
            double th_red = th_degg * Math.PI / 180;

            xn = ps.X * Math.Cos(th_red) - ps.Y * Math.Sin(th_red);
            yn = ps.X * Math.Sin(th_red) + ps.Y * Math.Cos(th_red);
            ps.X = (float)xn;
            ps.Y = (float)yn;

            xn = pe.X * Math.Cos(th_red) - pe.Y * Math.Sin(th_red);
            yn = pe.X * Math.Sin(th_red) + pe.Y * Math.Cos(th_red);
            pe.X = (float)xn;
            pe.Y = (float)yn;

        }

        public void RotateAround(float th_degg, PointF refPt)
        {
            Translation(-refPt.X, -refPt.Y);
            Rotate(th_degg);
            Translation(refPt.X, refPt.Y);
        }
    }

}

