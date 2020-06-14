using Lab5.State;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Lab5.Form1;

namespace Lab5
{
    public class Cockroach
    {
        Bitmap image;
        const int step = 30;
        int x, y;
        IDirection direction;

        public Cockroach(Bitmap _Image)
        {
            image = _Image;
            direction = new DirectionUp(Image);
        }
        public int X
        {
            get => x;
            set => x = value;
        }
        public int Y
        {
            get => y;
            set => y = value;
        }
        public Bitmap Image
        {
            get => image;
            set => image = value;
        }
        public void newcoord(int dx, int dy)
        {
            x = dx;
            y = dy;
        }
        public void Step()
        {
            direction.Step(ref x, ref y);
        }

        //Изменение направления, параметр – первая буква направления
        public void ChangeTrend(string s)
        {
            direction = direction.ChangeTrend(s);
        }
    }
}
