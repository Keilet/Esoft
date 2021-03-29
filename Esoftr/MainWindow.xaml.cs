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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Esoftr
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void chg_Click(object sender, RoutedEventArgs e)
        {
            using (Model1 db = new Model1()) {
                if (login.Text.Length > 0)
                {
                    User user = db.User.Where(p => p.Login.Equals(login.Text)).FirstOrDefault();
                    if(user!=null)
                    {
                        ChangePassword change = new ChangePassword(login.Text);
                        if (change.ShowDialog() == true)
                        {
                            change.ShowDialog();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя нет");
                    }
                }
                else
                {
                    MessageBox.Show("Введите пользователя");
                }
            }
        }

        private void signin_Click(object sender, RoutedEventArgs e)
        {
            using(Model1 db= new Model1()) 
            {
                if (login.Text.Length>0 && password.Password.Length>0)
                {
                    string pass = GetHash(password.Password);
                    User user = db.User.Where(p => p.Login.Equals(login.Text) && p.Password.Equals(pass)).FirstOrDefault();
                    if(user!=null)
                    {
                        int role;
                        int id;
                        id = user.ID; ;
                        if (id > 10)
                        {
                            role = 1;
                            ListTasks lt = new ListTasks(role,id);
                            lt.Show();
                            //MainWindow w = new MainWindow(this);
                            //w.Show();
                            //this.Hide();
                        }
                        else
                        {
                            role = 0;
                            ListTasks lt = new ListTasks(role,id);
                            lt.Show();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Такого пользователя  или пароля не существует");
                    }
                }
                else if(login.Text.Length == 0 && password.Password.Length == 0)
                {
                    MessageBox.Show("Введите данные");
                }
                else if(login.Text.Length > 0 && password.Password.Length == 0)
                {
                    MessageBox.Show("Введите пароль");
                }
                else if(login.Text.Length == 0 && password.Password.Length > 0)
                {
                    MessageBox.Show("Введите логин");
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
