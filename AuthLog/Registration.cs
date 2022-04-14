using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Cryptography;
using HashLibrary;

namespace AuthLog
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                User user = new User(textBoxLog.Text,
                HashClass.GetHashString(textBoxPass.Text), textBoxEmail.Text, "User");
                db.Users.Add(user);
                db.SaveChanges();
            }
            Authorization authorization = new Authorization();
            authorization.Show();
            this.Hide();
        }


        private void Closed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
