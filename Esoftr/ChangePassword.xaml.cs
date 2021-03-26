using Esoftr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Esoftr
{
    /// <summary>
    /// Логика взаимодействия для ChangePassword.xaml
    /// </summary>
    public partial class ChangePassword : Window
    {
        string log;
        public ChangePassword(string login)
        {
            InitializeComponent();
            log = login;
        }

        private void save(object sender, RoutedEventArgs e)
        {
           using(Model1 db= new Model1())
            {
                if(passwordold.Text.Length>0 && passwordnew.Text.Length > 0)
                {
                    User user = db.User.Where(p => p.Login.Equals(log)).FirstOrDefault();
                    if(passwordnew.Text == passwordold.Text)
                    {
                        user.Password = GetHash(passwordnew.Text);
                        db.SaveChanges();
                        MessageBox.Show("Пароль сохранен");
                        Close();
                    }
                    else
                    {
                        MessageBox.Show("Пароли не совпадают");
                    }
                }
                else
                {
                    MessageBox.Show("Введите новый пароль");
                }
            }
        }
        public string GetHash(string input)
        {
            var md5 = MD5.Create();
            var hash = md5.ComputeHash(Encoding.UTF8.GetBytes(input));

            return Convert.ToBase64String(hash);
        }
    }
}
