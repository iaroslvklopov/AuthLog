using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AuthLog
{
    public partial class UserForm : Form
    {
        private Authorization authorization = null;
        public UserForm(Authorization authorization)
        {
            InitializeComponent();
            this.authorization = authorization;
        }

        private void UserForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            authorization.ShowDialog();
            
        }
    }
}
