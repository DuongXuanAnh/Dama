using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama_v1
{
    class Square
    {
        public static int width = 60;
        private int _x;
        private int _y;
        private Color _color;
        private Kaminek _kamen;
        private Point _souradnice;
        private int _index;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }
        public Kaminek Kamen { get => _kamen; set => _kamen = value; }
        public Color Color { get => _color; set => _color = value; }
        public Point Souradnice { get => _souradnice; set => _souradnice = value; }
        public int Index { get => _index; set => _index = value; }

        public Square(int x, int y)
        {
            this._x = x;
            this._y = y;
        }

        public void Vykresli_Square(Graphics g)
        {
            SolidBrush myBrush = new SolidBrush(Color);
            g.FillRectangle(myBrush, _x, _y, width, width);
        }
    }
}
