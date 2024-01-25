using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace RegistrUsers1
{
    public partial class AuthorizationPage : UserControl
    {
        public AuthorizationPage()
        {
            string connString = "Host=myserver;Username=mylogin;Password=mypass;Database=mydatabase";
            myconnString = "Server=localhost;Port=5432;Username=postgres;Password=123098;Database=RegistrUsers1DB";
            this.Controls.Add(adminPage);
            manamerPage.Dock = DockStyle.Fill;
            manamerPage.Hide();
            this.Controls.Add(manamerPage);
            adminPage.Dock = DockStyle.Fill;
            adminPage.Hide();
            InitializeComponent();
        }

        string myconnString;
        UserControl adminPage = new AdminPage();
        UserControl manamerPage = new ManagerPage();

        public void SwitchPages(int role)
        {
            switch (role)
            {
                case 0:
                    adminPage.Show();
                    TextBoxDispose();
                    break;
                case 1:
                    manamerPage.Show();
                    TextBoxDispose();
                    break;
            }
        }

        public void TextBoxDispose()
        {
            textBox1.Clear();
            textBox2.Clear();
        }

        public void Authorization(string login, string password)
        {
            string loginfromdb = "";
            string passwordfromdb = "";
            Roles rolefromdb = Roles.Empty;

            using (NpgsqlConnection conn = new NpgsqlConnection(myconnString))
            {
                conn.Open();

                using (NpgsqlCommand cmd = new NpgsqlCommand($"select * from users where login = '{login}';", conn))
                using (NpgsqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        loginfromdb = reader.GetString(1);
                        passwordfromdb = reader.GetString(2);
                        rolefromdb = (Roles)reader.GetInt32(3);
                    }
                }
            }

            if (loginfromdb == login && passwordfromdb == password)
            {
                SwitchPages((int)rolefromdb);
            }
            else
            {
                MessageBox.Show("Неверный логин или пароль");
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text) || String.IsNullOrEmpty(textBox2.Text))
            {
                MessageBox.Show("Не все поля заполнены");
            }
            else
            {
                Authorization(textBox1.Text, textBox2.Text);
            }
        }
    }
}
