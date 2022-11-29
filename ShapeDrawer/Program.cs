using System;
using SplashKitSDK;

/*Basically Abstract classes doesn't allow the creation of instances or objects of the abstract class.
If we want to prevent someone from creating an object of a class directly then we can use abstract class.
BASICALLY an Abstract method is like an override() method. That  is it allows a user to overwrite a class.
But in this case the derived class must be overwritten. That is the inherited class's method has to be overwritten and
it must have a body but the abstract class's abstract methods can't have a body.
*/
namespace ShapeDrawer
{
    public class Program
    {
        private enum ShapeKind// here rectangle value is 0 and then other values use auto increment and goes 1,2...
        {
            Rectangle,
            Circle,
            Line
        }
        public static void Main()
        {
            
            new Window("Shape Drawer", 800, 600);
            Drawing myDrawing = new Drawing();
            ShapeKind kindToAdd = ShapeKind.Circle;

            do
            { 
                SplashKit.ProcessEvents();
                SplashKit.ClearScreen();

                if (SplashKit.KeyTyped(KeyCode.RKey))
                {
                    kindToAdd = ShapeKind.Rectangle;// changes the enum type to rectangle if R key is pressed
                }

                if (SplashKit.KeyTyped(KeyCode.LKey))
                {
                    kindToAdd = ShapeKind.Line;
                }

                if (SplashKit.KeyTyped(KeyCode.CKey))
                {
                    kindToAdd = ShapeKind.Circle;
                }

                if (SplashKit.MouseClicked(MouseButton.LeftButton))
                {
                    Shape newShape;// declare a shape variable, we can't initiate an object of shape class here because shape class is abstract

                    if (kindToAdd == ShapeKind.Circle)// if the current enum value is circle we execute this block of if statement
                    {
                        MyCircle newCircle = new MyCircle();// a circle object is created to access the methods of circle class
                        //newCircle.X = SplashKit.MouseX();
                        //newCircle.Y = SplashKit.MouseY();
                        newShape = newCircle;// set the shape object value as an instance of the circle object.
                    }

                    else if (kindToAdd == ShapeKind.Rectangle)
                    {
                        MyRectangle newRect = new MyRectangle();
                        //newRect.X = SplashKit.MouseX();
                        //newRect.Y = SplashKit.MouseY();
                        newShape = newRect;
                    }

                    else
                    {
                        MyLine newLine = new MyLine();
                        newShape = newLine;
                        //newLine.X = SplashKit.MouseX();
                        //newLine.Y = SplashKit.MouseY();
                    }

                    newShape.X = SplashKit.MouseX();// pass the current X co-ordinate to float X attribute of shape class 
                    newShape.Y = SplashKit.MouseY();// pass the current Y co-ordinate to float X attribute of shape class
                    myDrawing.AddShape(newShape);

                }

                if (SplashKit.MouseClicked(MouseButton.RightButton))//
                {
                    myDrawing.SelectShapesAt(SplashKit.MousePosition());//passes current mouse position to the SelectShapesAt.
                }

                if (SplashKit.KeyTyped(KeyCode.SpaceKey))
                {
                    myDrawing.Background = SplashKit.RandomColor();
                }

                if ((SplashKit.KeyTyped(KeyCode.DeleteKey)) || (SplashKit.KeyTyped(KeyCode.BackspaceKey)))
                {
                    myDrawing.RemoveShape();

                }

                if (SplashKit.KeyTyped(KeyCode.SKey))
                {
                    myDrawing.Save("D:\\C#\\uniPrac\\ShapeDrawer\\Saves\\TestDrawing.txt");
                }

                if (SplashKit.KeyTyped(KeyCode.OKey))
                    try 
                    {
                        myDrawing.Load("D:\\C#\\uniPrac\\ShapeDrawer\\Saves\\TestDrawing.txt");
                    } catch (Exception e)
                    {
                        Console.Error.WriteLine("Error loading file: {0}", e.Message);
                    }
                
                    
                
                myDrawing.draw();

                SplashKit.RefreshScreen();
            } while (!SplashKit.WindowCloseRequested("Shape Drawer"));
            

            Console.ReadLine();
        }
    }
}
