using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama_v1
{
    class Kaminek
    {
        private int _velikost;
        private int _x;
        private int _y;
        private string _color;
        private int _belongToHrac;
        private int _index;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        public int BelongToHrac { get => _belongToHrac; set => _belongToHrac = value; }
        public int Index { get => _index; set => _index = value; }

        public int Velikost { get => _velikost; set => _velikost = value; }
        public string Color { get => _color; set => _color = value; }
        public Kaminek(int velikost, int x, int y, int belongToHrac)
        {
            this._velikost = velikost;
            this._x = x;
            this._y = y;
            this._belongToHrac = belongToHrac;
        }


        public void vykresli_Kaminek(Graphics g)
        {
            SolidBrush myBrush = new SolidBrush(ColorTranslator.FromHtml(Color));
            g.FillEllipse(myBrush, _x, _y, _velikost, _velikost);
        }
    }
}
