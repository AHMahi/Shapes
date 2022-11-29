using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class MyLine : Shape
    {
        private float _length;

        public MyLine(Color color, float x, float y, int length) : base(color)
        {
            _color = color;
            X = x;
            Y = y;
            _length = length;
        }

        public MyLine() : this(Color.Blue, 0, 0, 100) { }

        public float Length
        {
            get
            {
                return _length;
            }

            set
            {
                _length = value;
            }

        }

        public override void Draw()
        {
            if (Selected)
            {
                DrawOutline();
            }

            SplashKit.DrawLine(_color, X, Y, (X + _length), Y);

        }

        public override void DrawOutline()
        {
            SplashKit.FillCircle(Color.Black, X, Y, 5);
            SplashKit.FillCircle(Color.Black, (X + _length), Y, 5);
        }

        public override bool IsAt(Point2D pt)
        {
            Point2D pointStart = new Point2D();
            pointStart.X = X;
            pointStart.Y = Y;

            Point2D pointEnd = new Point2D();
            pointEnd.X = X + _length;
            pointEnd.Y = Y;

            Line line = SplashKit.LineFrom(pointStart, pointEnd);
            return SplashKit.PointOnLine(pt, line);
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Line");
            base.SaveTo(writer);
            writer.WriteLine(_length);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _length = reader.ReadInteger();
        }
    }
}
