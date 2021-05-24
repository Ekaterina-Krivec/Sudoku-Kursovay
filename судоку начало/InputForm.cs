using System;
using System.Windows.Forms;

namespace Судоку
{
    public partial class frmInput : Form
    {
        private int number;

        public frmInput()
        {
            InitializeComponent();
        }

        private void btnNumber_Click(object sender, EventArgs e)
        {
            number = Int32.Parse((sender as Button).Text);
        }

        internal int getNumber()
        {
            return number;
        }

        private void frmInput_Click(object sender, EventArgs e)
        {
            number = 0;
            if ((sender as Button).Name == "btnClear")
            {
                this.Hide();
                number = 10;
            }
            else if ((sender as Button).Name == "btnClose")
            {
                this.Hide();
            }
            else if ((sender as Button).Name == "btnSolve")
            {
                this.Hide();
                number = 100;
            }
        }
    }
}
