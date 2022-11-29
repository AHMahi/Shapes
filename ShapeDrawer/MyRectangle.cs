using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class MyRectangle : Shape
    {
        private int _width;
        private int _height;

        public MyRectangle(Color color, float x, float y, int width, int height) : base(color)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public MyRectangle() : this(Color.Yellow, 0, 0, 100, 100) { }

        public int Width
        {
            get
            {
                return _width;
            }

            set
            {
                _width = value;
            }

        }

        public int Height
        {
            get
            {
                return _height;
            }

            set
            {
                _height = value;
            }

        }

        public override void Draw()
        {
            if (Selected)// if the selectshapesAt() method sets Selected = true then we draw the shape and outline
            {
                DrawOutline();//splashkit draws in order so we draw border first so that it can be overwritten by the green shape later.
            }

            SplashKit.FillRectangle(_color, X, Y, Width, Height);
        }

        public override void DrawOutline()
        {
            SplashKit.FillRectangle(Color.Black, X - 2, Y - 2, Width + 4, Height + 4);
        }

        public override bool IsAt(Point2D point)//compares the passed in X with the X value stored in the attribute
        {
            if (((point.X >= X) && (point.X <= (X + Width))) && ((point.Y >= Y) && (point.Y <= (Y + Height))))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public override void SaveTo(StreamWriter writer)
        {
            writer.WriteLine("Rectangle");
            base.SaveTo(writer);
            writer.WriteLine(_width);
            writer.WriteLine(_height);
        }

        public override void LoadFrom(StreamReader reader)
        {
            base.LoadFrom(reader);
            _width = reader.ReadInteger();
            _height = reader.ReadInteger();
        }
    }
}

    