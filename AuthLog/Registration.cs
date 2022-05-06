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
        bool _isRegistered = false;
        private void button1_Click(object sender, EventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                if (textBoxEmail.Text != "" && textBoxLog.Text != "" && textBoxPass.Text != "" )
                {
                    foreach (User user1 in db.Users)
                    {
                        if (user1.Login == textBoxLog.Text)
                        {
                           _isRegistered = true;
                        }
                    }
                    if (_isRegistered == false)
                    {
                        User user = new User(textBoxLog.Text, HashClass.GetHashString(textBoxPass.Text), textBoxEmail.Text, "User");
                        db.Users.Add(user);
                        db.SaveChanges();
                    }
                    else
                    {
                        MessageBox.Show("This login had created");
                    }
                }
                else
                {
                    MessageBox.Show("Error!");
                }
                
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
