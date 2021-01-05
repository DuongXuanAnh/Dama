using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dama_v1
{
    public partial class Form1 : Form
    {
        ChessBoard chessBoard = new ChessBoard();
      
        Size size = new Size(Square.width *8, Square.width*8);

        private int _turnHrace = 1;
        Kaminek selected_kamen;
        int origin_x;
        int origin_y;
        private bool selected = false;


        public Form1()
        {
            InitializeComponent();
            canvas.Size = size;
            chessBoard.createChessBoard();
            chessBoard.createKaminky();

        }

        private void canvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            chessBoard.vykreslitChessBoard(g);
            chessBoard.vykreslitKaminky(g);

        }

        #region Zmnenit barvu kaminku
        private void button1_Click(object sender, EventArgs e)
        {
            if(colorDialog1.ShowDialog() == DialogResult.OK)
            {
                string color = (colorDialog1.Color.ToArgb() & 0x00FFFFFF).ToString("X6");

               for(int i = 0; i < chessBoard.hrac1.kaminky.Count; i++)
                {
                    chessBoard.hrac1.kaminky[i].Color = "#"+color;
                }
               button1.BackColor = ColorTranslator.FromHtml("#"+color);
            }
            canvas.Refresh();
        }
  
        private void button2_Click(object sender, EventArgs e)
        {
            if (colorDialog1.ShowDialog() == DialogResult.OK)
            {
                string color = (colorDialog1.Color.ToArgb() & 0x00FFFFFF).ToString("X6");

                for (int i = 0; i < chessBoard.hrac2.kaminky.Count; i++)
                {
                    chessBoard.hrac2.kaminky[i].Color = "#" + color;
                }
                button2.BackColor = ColorTranslator.FromHtml("#" + color);
            }
            canvas.Refresh();
        }
        #endregion

        private void canvas_MouseUp(object sender, MouseEventArgs e)
        {
           if(selected_kamen != null) // Kdyz mam spravne vybrany kaminek
            {
                selected = false;
                if (origin_x != 0 && origin_y != 0)
                {
                    if (chessBoard.umistitKamen(selected_kamen) == 0) // Nedostupne policko
                    {
                        selected_kamen.X = origin_x;
                        selected_kamen.Y = origin_y;
                    }
                    else if(chessBoard.umistitKamen(selected_kamen) == 1) // Umisteni success
                    {
                        _turnHrace = _turnHrace == 1 ? 2 : 1;
                    }
                    else if (chessBoard.umistitKamen(selected_kamen) == 2) // Zkontrolovat multiskok pro normalni kaminek
                    {
                        if (chessBoard.Skoc_Dal(selected_kamen) == true)
                        {
                            selected_kamen.MultiSkok = true;
                            chessBoard.DisableOtherKamen(selected_kamen);
                            chessBoard.Skoc_Dal(selected_kamen);                 
                        }
                        else
                        {
                            chessBoard.EnableAllKaminek(selected_kamen);
                            selected_kamen.MultiSkok = false;
                            _turnHrace = _turnHrace == 1 ? 2 : 1;
                        }
                    }
                    else if (chessBoard.umistitKamen(selected_kamen) == 3) // Zkontrolovat skok Damy
                    {
                        if (chessBoard.CanDamaJumpMore(selected_kamen) == true)
                        {                        
                            chessBoard.DisableOtherKamen(selected_kamen);                         
                            chessBoard.CanDamaJumpMore(selected_kamen);
                        }
                        else
                        {
                            chessBoard.EnableAllKaminek(selected_kamen);
                            _turnHrace = _turnHrace == 1 ? 2 : 1;
                        }
                    }

                }
                chessBoard.clearDostupnePole(); // Smazat dostupné pole
                canvas.Refresh();
                UkoncitHru(); // Ukončí hru jakmile to bude možné
            }
        }

        private void canvas_MouseDown(object sender, MouseEventArgs e)
        {
            
            Kaminek kamen = chessBoard.selectKaminek(_turnHrace, e.X, e.Y);

            if (kamen != null && kamen.Disable == false)
            {
                if (kamen.BelongToHrac != _turnHrace)
                {
                    return;
                }
                selected_kamen = kamen;
                origin_x = kamen.X;
                origin_y = kamen.Y;
                selected = true;
                chessBoard.ukazatDostupnePole(kamen, _turnHrace);
                canvas.Refresh();
            }
            else
            {
                selected_kamen = null; // Kdyz si vybiram spatne kaminky
            }
            
            
        }

        private void canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (selected)
            {
                selected_kamen.X = e.X - selected_kamen.Velikost/2;
                selected_kamen.Y = e.Y - selected_kamen.Velikost / 2;
                canvas.Refresh();
            }
           
        }

        void UkoncitHru()
        {
            if (chessBoard.IsGameOver())
            {
                DialogResult dr = MessageBox.Show("Vyhrál hráč " + (_turnHrace == 1 ? 2 : 1), "Chcete novou hru?", MessageBoxButtons.YesNo);

                if (dr == DialogResult.No)
                {
                    System.Windows.Forms.Application.Exit();
                }
                if (dr == DialogResult.Yes)
                {
                    Application.Restart();
                }
            }
        }
    }
}
