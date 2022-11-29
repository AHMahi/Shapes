using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class MyCircle : Shape
    {
        private int _radius;

        public MyCircle(Color color, int radius) : base(color)
        {
            _radius = 50;
            _color = color;
        }

        public MyCircle() : this(Color.Blue, 50) { }

        public int radius
        {
            get
            {
                return _radius;
            }

            set
            {
                _radius = value;
            }

        }

        public override void Draw()
        {
            if (Selected)//accessing private field of shape base class by using the Selected property
            {
                DrawOutline();
            }

            SplashKit.FillCircle(_color, X, Y, _radius);// did change here might effect badly later
        }

        public override void DrawOutline()//otherwise ouline is a rectangle
        {
            SplashKit.FillCircle(Color.Black, X, Y, radius + 4);
        }

        public override bool IsAt(Point2D point)//why did I override here? 
        {
            return SplashKit.PointInCircle(point, SplashKit.CircleAt(this.X, this.Y, radius));//idea from Github
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Circle");
            base.SaveTo(writer);
            writer.WriteLine(_radius);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _radius = reader.ReadInteger();
        }
    }
}
