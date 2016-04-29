using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Design;
using System.Security;
using System.IO;

namespace WindowsForms5
{
    public partial class FormPlaying : Form
    {
        private int tableIndex;
        private int side;
        private DotColor[,] grid = new DotColor[15, 15];
        private Bitmap blackBitmap;
        private Bitmap whiteBitmap;
        //if order is from server
        private bool isReceiveCommand = false;
        private ClientService service;
        delegate void LabelDelegate(Label label, string str);
        delegate void ButtonDelegate(Button button,bool falg);
        delegate void RadioButtonDelegate(RadioButton radiobutton, bool flag);
        delegate void SetDotDelegate(int i, int j, int dotColor);
        LabelDelegate labelDelegate;
        ButtonDelegate buttonDelegate;
        RadioButtonDelegate radioButtonDelegate;
        public FormPlaying()
        {
            InitializeComponent();
        }
        public FormPlaying(int TableIndex, int Side, StreamWriter sw)
        {
            InitializeComponent();
            this.tableIndex = TableIndex;
            this.side = Side;
            //labelDelegate = new LabelDelegate(SetLabel);
            //buttonDelegate = new ButtonDelegate(SetButton);
        }
        
    }
}
