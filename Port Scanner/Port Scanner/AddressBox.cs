using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Port_Scanner
{
    public partial class AddressBox : Form
    {
        private String input;

        public AddressBox()
        {
            InitializeComponent();
        }

        private void enterButton_Click(object sender, EventArgs e)
        {
            input = addressTextBox.Text;
            this.Close();
        }

        public String Input
        {
            get { return input; }
        }
    }
}
