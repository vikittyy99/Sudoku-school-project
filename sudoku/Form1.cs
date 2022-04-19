using System;
using System.Drawing;
using System.Windows.Forms;


namespace sudoku
{
    public partial class Form1 : Form
    {
        public Form1()
        {
           
            InitializeComponent();
            createcell();
                 
        }

        sudokucells[,] cells = new sudokucells[9, 9];

        private void createcell()
        {
            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    cells[i, j] = new sudokucells();
                    cells[i, j].Font = new Font(SystemFonts.DefaultFont.FontFamily, 20);
                    cells[i, j].Size = new Size(40, 40);
                    cells[i, j].ForeColor = SystemColors.ControlDarkDark;
                    cells[i, j].Location = new Point(i * 40, j * 40);
                    cells[i, j].BackColor = ((i / 3) + (j / 3)) % 2 == 0 ? SystemColors.Control : Color.LightSkyBlue;
                    cells[i, j].FlatStyle = FlatStyle.Flat;
                    cells[i, j].FlatAppearance.BorderColor = Color.Purple; //cveta na cqlata ramka
                    cells[i, j].X = i;
                    cells[i, j].Y = j;

                    cells[i, j].KeyPress += cellKey;

                    panel1.Controls.Add(cells[i, j]);

                }
            }
        }
        private void cellKey(object sender, KeyPressEventArgs e)
        {
            var cell = sender as sudokucells;          
            int Text;

            if (int.TryParse(e.KeyChar.ToString(), out Text))
            {

                if (Text == 0)
                    cell.Clear();
                else
                    cell.Text = Text.ToString();

                cell.ForeColor = SystemColors.ControlDarkDark;
            }
        }
       
        private bool SudokuSolving()
        {         
            if (SolveBoard(0, -1) == true)
            {
                return SolveBoard(0, -1);             
            }
           
            return false;
        }
       
        private  bool SolveBoard(int row, int col)
        {
            bool checkEmptySpace = false;

            //proverqva dali sudoku-to e resheno i ako ne e resheno namira sledvashtata prazna poziciq
            for (row = 0; row < 9; row++)
             {

                for (col = 0; col < 9; col++)
                {
                    
                    if (cells[row, col].Text == "0")
                    {   
                        checkEmptySpace = true;
                        break;
                    }
                }
                if (checkEmptySpace == true)
                {                 
                    break;
                }

             }
            // ako nqma poveche prazni mesta sudoku-to e resheno
                if (checkEmptySpace == false)
                {
                   return true;
                }

       // populva praznite mesta s validno chislo
            for (int num = 1; num <= 9; num++)
            {
               
                // proverqva dali tova chislo ve4e ne e ve4e populneno                
                if (isNotUsed( row, col, num))
                {
                    cells[row, col].Text = num.ToString();

                    if (SolveBoard(0,-1))
                    {      
                        return true;
                    }

                    
                    cells[row, col].Text = "0";
                }
                                          
            }
           
            return false;
           
        }

        private  bool isNotUsed( int row, int col, int num)
        {
            
            return (
                !usedInRow( row, num) &&
                !usedInCol( col, num) &&
                !usedInBlock((row - (row %3)), (col - (col % 3)), num)
                );

            
        }

        private  bool usedInBlock(int RowInBlock, int ColInBLock, int num)
        {
            for (int row = 0; row < 3; row++)
            {
                
                for (int col = 0; col < 3; col++)
                {
                 
                    if (cells[row + RowInBlock, col + ColInBLock].Text == num.ToString())
                    {
                        
                        return true;
                    }
                }
            }
            return false;
        }

        private  bool usedInCol( int col, int num)
        {
            
            for (int row = 0; row < cells.GetLength(1); row++)
            {
                
                if (cells[row, col].Text == num.ToString())
                {
                    return true;
                    
                }
            }

            return false;
        }
     

        private  bool usedInRow( int row, int num)
        {
            
            for (int col = 0; col < cells.GetLength(0); col++)
            {
           
                if (cells[row, col].Text == num.ToString())
                {
                    
                    return true;
                }
            }
            return false;
        }    

        private void Solve_Click(object sender, EventArgs e)
        {    
            
            getSudoku();
            SudokuSolving();      

        }

        private void getSudoku()
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (cells[row, col].Text == "")
                        cells[row, col].Text = "0";
                }
            }
        }
        private void Restart_Click(object sender, EventArgs e)
{
            foreach (var cell in cells)
            {
                cell.Clear();
            }
        }
            
    }
}



