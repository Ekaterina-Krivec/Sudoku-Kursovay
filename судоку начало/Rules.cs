using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Судоку
{
    public partial class FormRool : Form
    {
        int n;
        public FormRool()
        {
            InitializeComponent();
        }

        private void rdBtn1_Click(object sender, EventArgs e)
        {
            if (rdBtn1.Checked==true)
            {
                panel1.Visible = true;
                panel2.Visible = false;
                panel3.Visible = false;
            }
            else if (rdBtn2.Checked == true)
            {
                panel1.Visible = false;
                panel2.Visible = true;
                panel3.Visible = false;
            }
            else if (rdBtn3.Checked==true)
            {
                panel1.Visible = false;
                panel2.Visible = false;
                panel3.Visible = true;
            }
        }

        private void richTextBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
