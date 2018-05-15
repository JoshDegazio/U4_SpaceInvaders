using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace U4_SpaceInvaders
{
    class Spaceship
    {
        Point playerPos = new Point();
        int counter = 0;
        private Point point;
        public Point Point { get => point; }
        Canvas canvas;
        MainWindow window;
        Rectangle playerRectangle;
        double dbl_playerpos;

        public Spaceship(Canvas c, MainWindow w)
        {
            //Generate Player
            canvas = c;
            window = w;
            point = new Point(317, 565);
            playerPos = point;
            playerRectangle = new Rectangle();
            playerRectangle.Fill = Brushes.Green;
            playerRectangle.Height = 64;
            playerRectangle.Width = 64;
            canvas.Children.Add(playerRectangle);
            Canvas.SetTop(playerRectangle, point.Y);
            Canvas.SetLeft(playerRectangle, point.X);
        }

        public void Tick()
        {




            if (Keyboard.IsKeyDown(Key.Left))
            {
                double.TryParse(playerPos.X, out dbl_playerpos);
            }
        }
    }
}
