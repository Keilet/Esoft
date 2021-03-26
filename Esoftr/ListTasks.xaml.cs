using Esoftr.Model;
using System;
using System.Collections.Generic;
using System.Linq;
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
    /// Логика взаимодействия для ListTasks.xaml
    /// </summary>
    public partial class ListTasks : Window
    {
        int ro;
        int i;
        public ListTasks(int role, int id)
        {
            InitializeComponent();
            using(Model1 db=new Model1())
            {
                ro = role;
                i = id;
                if (ro == 1)
                {
                    var query = from a in db.Task
                                join b in db.User on a.ExecutorID equals b.ID
                                join c in db.Executor on a.ExecutorID equals c.ID
                                join d in db.User on c.ManagerID equals d.ID
                                orderby a.Deadline descending
                                where d.ID.Equals(i)
                                select new
                                {
                                    Title = a.Title,
                                    Status = a.Status,
                                    ExecutorFName = b.FirstName,
                                    ExecutorMName = b.MiddleName,
                                    ExecutorLName = b.LastName,
                                    ManagerFName = d.FirstName,
                                    ManagerMName = d.MiddleName,
                                    ManagerLName = d.LastName,
                                };
                    lTasks.ItemsSource = query.ToList();
                }
                else
                {
                    var query = from a in db.Task
                                join b in db.User on a.ExecutorID equals b.ID
                                join c in db.Executor on a.ExecutorID equals c.ID
                                join d in db.User on c.ManagerID equals d.ID
                                orderby a.Deadline descending
                                where b.ID.Equals(i)
                                select new
                                {
                                    Title = a.Title,
                                    Status = a.Status,
                                    ExecutorFName = b.FirstName,
                                    ExecutorMName = b.MiddleName,
                                    ExecutorLName = b.LastName,
                                    ManagerFName = d.FirstName,
                                    ManagerMName = d.MiddleName,
                                    ManagerLName = d.LastName,
                                };
                    lTasks.ItemsSource = query.ToList();
                    exec.Visibility = Visibility.Hidden;
                    label.Visibility = Visibility.Hidden;
                }
                List<User> query1 = db.User.ToList();
                foreach (User b in query1)
                    if (b.ID < 10)
                    {
                        exec.Items.Add(b.FirstName + b.MiddleName + b.LastName);
                    }
                stat.Items.Add("запланирована");
                stat.Items.Add("исполняется");
                stat.Items.Add("выполнена");
                stat.Items.Add("отменена");
            }
        }

        private void exec_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }

        private void stat_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Filtr();
        }
        private void Filtr()
        {
            if (exec.Text.Length != 0 && stat.Text.Length == 0)
            {
                using (Model1 db = new Model1())
                {
                    lTasks.ItemsSource = null;
                    string ex = exec.SelectedValue.ToString();
                    var query = from a in db.Task
                                join b in db.User on a.ExecutorID equals b.ID
                                join c in db.Executor on a.ExecutorID equals c.ID
                                join d in db.User on c.ManagerID equals d.ID
                                orderby a.Deadline descending
                                where d.ID.Equals(i) && b.FirstName + b.MiddleName + b.LastName == ex
                                select new
                                {
                                    Title = a.Title,
                                    Status = a.Status,
                                    ExecutorFName = b.FirstName,
                                    ExecutorMName = b.MiddleName,
                                    ExecutorLName = b.LastName,
                                    ManagerFName = d.FirstName,
                                    ManagerMName = d.MiddleName,
                                    ManagerLName = d.LastName,
                                };
                    lTasks.ItemsSource = query.ToList();
                }
            }
            else if(exec.Text.Length == 0 && stat.Text.Length != 0)
            {
                using (Model1 db = new Model1())
                {
                    lTasks.ItemsSource = null;
                    string st = stat.SelectedValue.ToString();
                    var query = from a in db.Task
                                join b in db.User on a.ExecutorID equals b.ID
                                join c in db.Executor on a.ExecutorID equals c.ID
                                join d in db.User on c.ManagerID equals d.ID
                                orderby a.Deadline descending
                                where (b.ID.Equals(i) || d.ID.Equals(i)) && a.Status == st
                                select new
                                {
                                    Title = a.Title,
                                    Status = a.Status,
                                    ExecutorFName = b.FirstName,
                                    ExecutorMName = b.MiddleName,
                                    ExecutorLName = b.LastName,
                                    ManagerFName = d.FirstName,
                                    ManagerMName = d.MiddleName,
                                    ManagerLName = d.LastName,
                                };
                    lTasks.ItemsSource = query.ToList();
                }
            }
            else if(exec.Text.Length != 0 && stat.Text.Length != 0)
            {
                using (Model1 db = new Model1())
                {
                    lTasks.ItemsSource = null;
                    string ex = exec.SelectedValue.ToString();
                    string st = stat.SelectedValue.ToString();
                    var query = from a in db.Task
                                join b in db.User on a.ExecutorID equals b.ID
                                join c in db.Executor on a.ExecutorID equals c.ID
                                join d in db.User on c.ManagerID equals d.ID
                                orderby a.Deadline descending
                                where d.ID.Equals(i) && b.FirstName + b.MiddleName + b.LastName == ex && a.Status==st
                                select new
                                {
                                    Title = a.Title,
                                    Status = a.Status,
                                    ExecutorFName = b.FirstName,
                                    ExecutorMName = b.MiddleName,
                                    ExecutorLName = b.LastName,
                                    ManagerFName = d.FirstName,
                                    ManagerMName = d.MiddleName,
                                    ManagerLName = d.LastName,
                                };
                    lTasks.ItemsSource = query.ToList();
                }
            }
        }

        private void add_Click(object sender, RoutedEventArgs e)
        {
            AddTask ad = new AddTask();
            ad.Show();
        }
    }
}
