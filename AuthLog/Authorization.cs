using System.Security.Cryptography;
using System.Text;

namespace AuthLog
{
    public partial class Authorization : Form
    {
        public Authorization()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (UserContext db = new UserContext())
            {
                foreach (User user in db.Users)
                {
                    if (textBoxLog.Text == user.Login &&
                    this.GetHashString(textBoxPass.Text) == user.Password)
                    {
                        MessageBox.Show("Вход успешен!");
                        UserForm userForm = new UserForm(this);
                        //userForm.label1.Text = user.Login;
                        this.Hide();
                        MessageBox.Show("Вы вошли в вашу учетную запись: " + user.Login);
                        userForm.Show();                        
                        return;
                    }
                }
                MessageBox.Show("Логин или пароль указан неверно!");

            }
        }
        private string GetHashString(string s)
        {
            byte[] bytes = Encoding.Unicode.GetBytes(s);
            MD5CryptoServiceProvider CSP = new MD5CryptoServiceProvider();
            byte[] byteHash = CSP.ComputeHash(bytes);
            string hash = "";
            foreach (byte b in byteHash)
            {
                hash += string.Format("{0:x2}", b);
            }
            return hash;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked) textBoxPass.UseSystemPasswordChar = false;
            else textBoxPass.UseSystemPasswordChar = true;
        }

        private void label4_Click(object sender, EventArgs e)
        {
            Registration registration = new Registration();
            registration.Show();
            this.Hide();
        }

        private void label5_Click(object sender, EventArgs e)
        {
            this.Hide();
            RecoverAccount recoverAccount = new RecoverAccount(this);
            recoverAccount.Show();
            
        }
    }
}