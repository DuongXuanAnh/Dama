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
        private bool _disable;
        private bool _muliSkok;
        private bool _isDama;

        public int X { get => _x; set => _x = value; }
        public int Y { get => _y; set => _y = value; }

        public int BelongToHrac { get => _belongToHrac; set => _belongToHrac = value; }
        public int Index { get => _index; set => _index = value; }

        public int Velikost { get => _velikost; set => _velikost = value; }
        public string Color { get => _color; set => _color = value; }
        public bool Disable { get => _disable; set => _disable = value; }
        public bool MultiSkok { get => _muliSkok; set => _muliSkok = value; }
        public bool IsDama { get => _isDama; set => _isDama = value; }
        public Kaminek(int velikost, int x, int y, int belongToHrac)
        {
            this._velikost = velikost;
            this._x = x;
            this._y = y;
            this._belongToHrac = belongToHrac;
            this._disable = false;
            this._muliSkok = false;
            this._isDama = false;
        }


        public void vykresli_Kaminek(Graphics g)
        {
            SolidBrush myBrush = new SolidBrush(ColorTranslator.FromHtml(Color));
            g.FillEllipse(myBrush, _x, _y, _velikost, _velikost);


            //if Dama
            if (this._isDama)
            {
                g.FillRectangle(Brushes.Black, _x + _velikost / 8 + 1, _y + _velikost / 8 + 1, _velikost / 2 + 10, _velikost / 2 + 10);
                using (Font myFont = new Font("Arial", 8))
                {
                    g.DrawString("Dáma", myFont, Brushes.White, new Point(_x + Square.width / 7 + 1, _y + Square.width / 4 + 3));
                }
            }
          
        }
    }
}
