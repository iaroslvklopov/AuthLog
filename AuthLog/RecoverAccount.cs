using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net.Mail;
using System.Net;
using HashLibrary;

namespace AuthLog
{
    public partial class RecoverAccount : Form
    {
        private Authorization _authorization;
        private int _code;
        private bool _flag = true;
        private User _user;
        public RecoverAccount(Authorization authorization)
        {
            InitializeComponent();
            tbCode.ReadOnly = true;
            tbNewPassword.ReadOnly = true;
            _authorization = authorization;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (_flag)
            {
                //try
                //{
                    Random random = new Random();
                    MailAddress from = new MailAddress("klopovyaroslav05@mail.ru", "Yaroslav Sergeevich");
                    MailAddress to = new MailAddress(textBoxEmail.Text);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Тест";
                    _code = random.Next(100000, 999999);
                    using (UserContext db = new UserContext())
                    {
                        foreach (User user in db.Users)
                        {
                            if (textBoxEmail.Text == user.Email)
                            {
                                _user = user;
                                m.Body = "<h1>Пароль: " + _code + "</h1>";
                            }
                        }
                    }
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.inbox.ru", 587);
                    smtp.Credentials = new NetworkCredential("klopovyaroslav05@mail.ru", "YTLntbFakvj9gznejKLX");
                    smtp.EnableSsl = true;
                    smtp.Send(m);
                //}
                //catch
                //{
                //    MessageBox.Show("Error!");
                //}
                textBoxEmail.ReadOnly = true;
                tbCode.ReadOnly = false;
                tbNewPassword.ReadOnly = false;
                _flag = false;
                return;
            }
            else if (_flag == false)
            {
                if (_code == Convert.ToInt32(tbCode.Text))
                {
                    foreach (User user in UserContext.GetContext().Users)
                    {
                        if (user.Email == textBoxEmail.Text)
                        {
                            user.Password = HashClass.GetHashString(tbNewPassword.Text);
                        }
                    }
                    UserContext.GetContext().SaveChanges();
                    MessageBox.Show("Success");
                    _flag = true;
                    this.Hide();
                    _authorization.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Error");
                }
            }
        }

        private void Closed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
