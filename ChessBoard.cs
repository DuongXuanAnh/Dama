using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dama_v1
{
    class ChessBoard
    {
        public List<Square> squares = new List<Square>();
        public Hrac hrac1 = new Hrac();
        public Hrac hrac2 = new Hrac();
        List<Square> dostupePolicka = new List<Square>();  

        public void createChessBoard()
        {
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        Square square = new Square(j * Square.width, i * Square.width);
                        square.Color = Color.DarkSlateGray;
                        square.Souradnice = new Point(j, i);
                        squares.Add(square);
                    }                    
                }
            }
        }

        public void vykreslitChessBoard(Graphics g)
        {
            foreach (Square square in squares)
            {
                square.Vykresli_Square(g);
            }
        }

        public void createKaminky()
        {
            for(int i = 0; i < 12; i++)
            {
                Kaminek k1 = new Kaminek(Square.width - 10, squares[i].X + 5, squares[i].Y + 5, 1);
                k1.Color = "#000000";
                squares[i].Kamen = k1;
                hrac1.kaminky.Add(k1);

                Kaminek k2 = new Kaminek(Square.width - 10, squares[32 - i - 1].X + 5, squares[32 - i - 1].Y + 5, 2);
                k2.Color = "#FFFFFF";
                squares[32 - i - 1].Kamen = k2;
                hrac2.kaminky.Add(k2);
            }

        }

        public void vykreslitKaminky(Graphics g)
        {
            foreach (Kaminek k in hrac1.kaminky)
            {
                k.vykresli_Kaminek(g);
            }
            foreach (Kaminek k in hrac2.kaminky)
            {
                k.vykresli_Kaminek(g);
            }
        }

        public Kaminek selectKaminek(int turnHrace, int x, int y)
        {
                foreach (Kaminek k in (turnHrace == 1) ? hrac1.kaminky : hrac2.kaminky)
                {
                    if (
                        (k.X <= x && k.X + k.Velikost >= x) && 
                        (k.Y <= y && k.Y + k.Velikost >= y)
                       )
                    {
                        return k;
                    }
                }

            return null;
        }

        Square originSquare = null;

        public void ukazatDostupnePole(Kaminek k, int hrac)
        {

            int choosenKamen_x = (k.X) / 60;
            int choosenKamen_y = (k.Y) / 60;

            // Kdyz je hrac 1 tak berem ty spodny pole, jinak berem horni
            int dostupnyPolicko_y = hrac == 1 ? choosenKamen_y + 1 : choosenKamen_y - 1;

                for (int i = 0; i < squares.Count(); i++)
                {
                    if(dostupnyPolicko_y == squares[i].Souradnice.Y && 
                      (choosenKamen_x+1 == squares[i].Souradnice.X || choosenKamen_x - 1 == squares[i].Souradnice.X)
                      && squares[i].Kamen == null
                      )
                    {
                        squares[i].Color = Color.Red;
                        dostupePolicka.Add(squares[i]);
                    }

                    if(k.X == squares[i].X + 5 && k.Y == squares[i].Y + 5)
                {
                    originSquare = squares[i]; // Pole obsahuje puvodni pozici origin_kaminka
                }
            }
            
        }

        public bool umistitKamen(Kaminek k)
        {
            foreach(Square sq in dostupePolicka)
            {
                if(sq.X < k.X + k.Velikost/2
                && sq.X + Square.width > k.X + k.Velikost / 2
                && sq.Y < k.Y + k.Velikost / 2
                && sq.Y + Square.width > k.Y + k.Velikost / 2)
                {
                    k.X = sq.X + 5;
                    k.Y = sq.Y + 5;
                    sq.Kamen = k;
                    originSquare.Kamen = null;
                    return true;
                }
            }
            return false;
        }
        public void clearDostupnePole()
        {
            foreach(Square sq in dostupePolicka)
            {
                sq.Color = Color.DarkSlateGray;
            }
            dostupePolicka = new List<Square>();
        }
    }
}
