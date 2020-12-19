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
            int index = 0; // Hodnota indexu cerneho pole
            for(int i = 0; i < 8; i++)
            {
                for(int j = 0; j < 8; j++)
                {
                    if ((i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0))
                    {
                        Square square = new Square(j * Square.width, i * Square.width);
                        square.Color = Color.DarkSlateGray;
                        square.Souradnice = new Point(j, i);                     
                        square.Index = index;
                        index++;
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
                k1.Index = i;
                squares[i].Kamen = k1;
                hrac1.kaminky.Add(k1);

                Kaminek k2 = new Kaminek(Square.width - 10, squares[32 - i - 1].X + 5, squares[32 - i - 1].Y + 5, 2);
                k2.Color = "#FFFFFF";
                k2.Index = 32 - i - 1;
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

            //Smazat kamen z pole
            if (k.X == squares[k.Index].X + 5 && k.Y == squares[k.Index].Y + 5)
            {
                originSquare = squares[k.Index]; // Pole obsahuje puvodni pozici origin_kaminka
            }

            if (hrac == 2)
            {
                if (squares[k.Index].Souradnice.Y % 2 == 0)
                {
                    temp2(k.Index, k.Index - 3, k.Index - 7);
                    temp2(k.Index, k.Index - 4, k.Index - 9);
                }
                else if (squares[k.Index].Souradnice.Y % 2 == 1)
                {
                    temp2(k.Index, k.Index - 4, k.Index - 7);
                    temp2(k.Index, k.Index - 5, k.Index - 9);
                }
            }
            else if (hrac == 1)
            {
                if (squares[k.Index].Souradnice.Y % 2 == 0)
                {
                    temp1(k.Index, k.Index + 4, k.Index + 7);
                    temp1(k.Index, k.Index + 5, k.Index + 9);
                }
                else if (squares[k.Index].Souradnice.Y % 2 == 1)
                {
                    temp1(k.Index, k.Index + 3, k.Index + 7);
                    temp1(k.Index, k.Index + 4, k.Index + 9);
                }
            }
        }


        void temp1(int indexOrigin, int index1, int index2)
        {
            if (
                index1 >= 0 && index1 < squares.Count
                && index2 >= 0 && index2 < squares.Count
                && squares[index1].Souradnice.Y == squares[indexOrigin].Souradnice.Y + 1)
            {
                if (squares[index1].Kamen == null)
                {
                    oznacitDostupnePole(squares[index1]);
                }
                else
                {
                    if (squares[index1].Kamen.BelongToHrac == 2
                     && squares[index2].Souradnice.Y == squares[indexOrigin].Souradnice.Y + 2)
                    {
                        oznacitDostupnePole(squares[index2]);
                    }
                }
            }
        }
        void temp2(int indexOrigin, int index1, int index2)
        {
             if (
                index1 >= 0 && index1 < squares.Count
                && index2 >= 0 && index2 < squares.Count
                && squares[index1].Souradnice.Y == squares[indexOrigin].Souradnice.Y - 1)
                    {
                        if (squares[index1].Kamen == null)
                        {
                            oznacitDostupnePole(squares[index1]);
                        }
                        else
                        {
                            if (squares[index1].Kamen.BelongToHrac == 1
                             && squares[index2].Souradnice.Y == squares[indexOrigin].Souradnice.Y - 2)
                            {
                                oznacitDostupnePole(squares[index2]);
                            }
                        }
                    }
        }

       // Ukládá odstupné pole do dostupných polích
       // Bude volaná i pro černý i bílé, rozhoduje to podle parametrů
    public void oznacitDostupnePole(Square policko)
                // Polícko o 1 řádek výš, polícko o 2 řádek výš, souřadnice o 1 řádek výš, souřadnice o 2 řádek výš
        {
                if (policko.Kamen == null)
                {
                    policko.Color = Color.Red;
                    dostupePolicka.Add(policko);
                }        
        }

    bool IsValidSquare(Square policko, int souradnice_Y, int hrac)
        {
            if (policko.Souradnice.Y == souradnice_Y)
            {
                if (policko.Kamen == null)
                {
                    return true;
                }
            }
                return false;
        }

        public int umistitKamen(Kaminek k)
        {
            foreach(Square sq in dostupePolicka)
            {
                if(sq.X < k.X + k.Velikost/2
                && sq.X + Square.width > k.X + k.Velikost / 2
                && sq.Y < k.Y + k.Velikost / 2
                && sq.Y + Square.width > k.Y + k.Velikost / 2)
                {
                   if(Math.Abs(sq.Index - originSquare.Index) > 6)
                    {
                        SebratKamen(originSquare.Index, sq.Index);
                        k.X = sq.X + 5;
                        k.Y = sq.Y + 5;
                        k.Index = sq.Index;
                        sq.Kamen = k;
                        originSquare.Kamen = null;
                        return 2;
                    }
                    else
                    {
                        k.X = sq.X + 5;
                        k.Y = sq.Y + 5;
                        k.Index = sq.Index;
                        sq.Kamen = k;
                        originSquare.Kamen = null;
                        return 1;
                    }
                }
            }
            return 0;
        }

        void SebratKamen(int indexOrigin, int destinationIndex)
        {
            //Pro hrac 1
            if (originSquare.Souradnice.Y % 2 == 1)
            {
                if (indexOrigin - destinationIndex == -7)
                {
                    hrac2.kaminky.RemoveAll(r => r.Index == indexOrigin + 3);
                    squares[indexOrigin + 3].Kamen = null;
                }
                if (indexOrigin - destinationIndex == -9)
                {
                    hrac2.kaminky.RemoveAll(r => r.Index == indexOrigin + 4);
                    squares[indexOrigin + 4].Kamen = null;
                }
            }
            else
            {
                if (indexOrigin - destinationIndex == -7)
                {
                    hrac2.kaminky.RemoveAll(r => r.Index == indexOrigin + 4);
                    squares[indexOrigin + 4].Kamen = null;
                }
                if (indexOrigin - destinationIndex == -9)
                {
                    hrac2.kaminky.RemoveAll(r => r.Index == indexOrigin + 5);
                    squares[indexOrigin + 5].Kamen = null;
                }
            }
            //Pro hrac 2
            if (originSquare.Souradnice.Y % 2 == 1)
            {
                if (indexOrigin - destinationIndex == 7)
                {
                    hrac1.kaminky.RemoveAll(r => r.Index == indexOrigin - 4);
                    squares[indexOrigin - 4].Kamen = null;
                }
                if (indexOrigin - destinationIndex == 9)
                {
                    hrac1.kaminky.RemoveAll(r => r.Index == indexOrigin - 5);
                    squares[indexOrigin - 5].Kamen = null;
                }
            }
            else
            {
                if (indexOrigin - destinationIndex == 7)
                {
                    hrac1.kaminky.RemoveAll(r => r.Index == indexOrigin - 3);
                    squares[indexOrigin - 3].Kamen = null;
                }
                if (indexOrigin - destinationIndex == 9)
                {
                    hrac1.kaminky.RemoveAll(r => r.Index == indexOrigin - 4);
                    squares[indexOrigin - 4].Kamen = null;
                }
            }
            

        }

        public bool Skoc_Dal(Kaminek k)
        {
            clearDostupnePole();
            ukazatDostupnePole(k, k.BelongToHrac);

            foreach (Square sq in dostupePolicka)
            {
                if (Math.Abs(squares[k.Index].Souradnice.Y - sq.Souradnice.Y) == 2)
                {
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
