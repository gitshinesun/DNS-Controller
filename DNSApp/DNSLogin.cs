using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DNSApp
{
    public partial class DNSLogin : Form
    {
        public DNSLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
        }

        private void DNSLogin_Load(object sender, EventArgs e)
        {
            CenterToScreen();
        }

        public string YourPassword
        {
            get
            {
                return txtPassword.Text;
            }
        }
    }
}
