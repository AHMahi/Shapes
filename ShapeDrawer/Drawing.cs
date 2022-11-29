using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SplashKitSDK;
using System.IO;

namespace ShapeDrawer
{
    public class Drawing
    {
        private readonly List<Shape> _shapes;//attribute, can store objects of type Shape
        //readonly modifier ensures the field can only be given a value during its initialization or in its class constructor.
        private Color _background;

        public Drawing() : this(Color.White)
        {

        }

        public Drawing(Color background)
        {
            _shapes = new List<Shape>();// passing shapes into the attribute making connection between shape and drawing
            _background = background;

        }

        public Color Background
        {
            get 
            {
                return _background; 
            } 

            set
            {
                _background = value;//whatever value we assign will be stored in "value" and then get passed to _background.
            }
        }

        public int ShapeCount
        {
            get { return _shapes.Count; }
        }

        public void AddShape(Shape shape)
        {
            _shapes.Add(shape);
        }

        public void RemoveShape()
        {
            foreach (Shape s in _shapes.ToList())
            {
                if (s.Selected)
                {
                    _shapes.Remove(s);
                }
            }
        }

        public void draw()
        {
            SplashKit.ClearScreen(_background);// We clear the screen with whatever value we stored in the _background field/attribute.
            foreach(Shape s in _shapes)
            {
                s.Draw();
            }
        } 

        public void SelectShapesAt(Point2D pt)// Checks each stored instances in _shapes and if mouse click is in the range of shape boundaries then it passes value as "true" in the _selected method of shape class.
        {//passes the current mouse position received to the IsAt method of circle, rectangle etc
            foreach (Shape s in _shapes)
            {
                if (s.IsAt(pt))
                {
                    s.Selected = true;
                }

                else
                {
                    s.Selected = false;
                }
            }
        }

        public List<Shape> SelectedShapes()
        //this method checks if the _selected attribute value, stored in shape class is true or not.
        //If it is true then that specific object passed into the result list 
        //Isnt used anywhere in the program idk why this was needed to be added
        {
            List<Shape> result = new List<Shape>();
            foreach (Shape s in result)
            {
                if (s.Selected == true)
                {
                    result.Add(s);
                }
            }
            return result;
        }

        public void Save(string filename)
        {
            StreamWriter writer = new StreamWriter(filename);
            try
            {
                writer.WriteColor(_background);
                writer.WriteLine(ShapeCount);

                foreach (Shape s in _shapes)
                {
                    s.SaveTo(writer);
                }
            }
            finally
            {
                writer.Close();
            }
        }

        public void Load(string filename)
        {
            StreamReader reader = new StreamReader(filename); 
            try 
            {
                int count;
                Shape s;
                string kind;

                this.Background = reader.ReadColor();
                count = reader.ReadInteger();

                _shapes.Clear();

                for (int i = 0; i < count; i++)
                {
                    kind = reader.ReadLine();

                    if (kind == "Rectangle")
                    {
                        s = new MyRectangle();
                    }

                    else if (kind == "Circle")
                    {
                        s = new MyCircle();
                    }

                    else if (kind == "Line")//If we remove line or any other shape from here, the exception says unknown shape kind
                    {
                        s = new MyLine();
                    }

                    else
                    {
                        throw new InvalidDataException("Unknown shape kind: " + kind);
                    }

                    s.LoadFrom(reader);
                    AddShape(s);
                }

            } finally// here try and finally ensures that even if there is an error in the try block, the finally block still executes the code in try block so that the txt file can close 
            //as keeping the txt file open would mean we can't chnage or delete it
            {
                reader.Close();
            }
 
        }
    }
}
