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

namespace AuthLog
{
    public partial class RecoverAccount : Form
    {
        public RecoverAccount()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                    MailAddress from = new MailAddress("iaroslavklopov@vk.com", "Yaroslav Sergeevich"); 
                    MailAddress to = new MailAddress(textBoxEmail.Text);
                    MailMessage m = new MailMessage(from, to);
                    m.Subject = "Тест";
                    using (UserContext db = new UserContext())
                    {
                        foreach (User user in db.Users)
                        {
                            if (textBoxEmail.Text == user.Email)
                            {
                                m.Body = "<h1>Пароль: " + user.Password + "</h1>";
                            }
                        }
                    }
                    m.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient("smtp.mail.ru", 587);
                    smtp.Credentials = new NetworkCredential("iaroslavklopov@vk.com", "123");
                    smtp.EnableSsl = true;
                    smtp.Send(m);

            }
            catch
            {
                MessageBox.Show("Error!");
            }
        }

        private void Closed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }
    }
}
