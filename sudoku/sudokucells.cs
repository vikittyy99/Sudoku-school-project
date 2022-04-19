﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace sudoku
{
    
    class sudokucells : Button
    {
        public int X { get; set; }
        public int Y { get; set; }

        public override string Text  { get => base.Text; set => base.Text = value; }


        public void Clear()
        {
            Text = string.Empty;
           
        }
    }
}
