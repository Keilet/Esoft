using Esoftr.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        int t = 0;
        int i;
        private bool Put;
        public AddTask()
        {
            InitializeComponent();
            using (Model1 db = new Model1())
            {
                List<User> query1 = db.User.ToList();
                foreach (User b in query1)
                    if (b.ID < 10)
                    {
                        exec.Items.Add(b.FirstName + b.MiddleName + " " + b.LastName);
                    }
                stat.Items.Add("запланирована");
                stat.Items.Add("исполняется");
                stat.Items.Add("выполнена");
                stat.Items.Add("отменена");

                wtype.Items.Add("анализ и проектирование");
                wtype.Items.Add("установка оборудования");
                wtype.Items.Add("техническое обслуживание и сопровождение");
            }
        }

        public AddTask(int id,int ro)
        {
            InitializeComponent();
            i = id;
            using (Model1 db = new Model1())
            {
                Model.Task task = db.Task.Where(p => p.ID.Equals(id)).FirstOrDefault();
                title.Text = task.Title;
                desc.Text = task.Description;
                datep1.SelectedDate = task.Deadline;
                datep2.SelectedDate = task.CompletedDateTime;
                diff.Text = task.Difficulty.ToString();
                time.Text = task.Time.ToString();
                List<User> query1 = db.User.ToList();
                foreach (User b in query1)
                    if (b.ID < 10)
                    {
                        exec.Items.Add(b.FirstName + b.MiddleName + " " + b.LastName);
                    }
                stat.Items.Add("запланирована");
                stat.Items.Add("исполняется");
                stat.Items.Add("выполнена");
                stat.Items.Add("отменена");
                stat.Text = task.Status;
                if (stat.Text == "выполнена" || stat.Text == "отменена") {
                    title.IsEnabled = false;
                    desc.IsEnabled = false;
                    datep1.IsEnabled = false;
                    datep2.IsEnabled = false;
                    diff.IsEnabled = false;
                    time.IsEnabled = false;
                    exec.IsEnabled = false; 
                    wtype.IsEnabled = false;
                }

                wtype.Items.Add("анализ и проектирование");
                wtype.Items.Add("установка оборудования");
                wtype.Items.Add("техническое обслуживание и сопровождение");
                wtype.Text = task.WorkType;
                
                var names = from a in db.Task
                            join b in db.User on a.ExecutorID equals b.ID
                            where a.ID.Equals(task.ID)
                            select new
                            {
                                FIO = b.FirstName + b.MiddleName + " " + b.LastName
                            };
                foreach (var t in names)
                {
                    exec.Text = t.FIO;
                }
                if (ro == 0) { exec.IsEnabled = false; }
                Put = true;
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            if (Put == false)
            {
                using (Model1 db = new Model1())
                {
                    Model.Task task = new Model.Task();
                    task.Title = title.Text;
                    task.Description = desc.Text;
                    try
                    {
                        task.Deadline = datep1.SelectedDate.Value;
                        task.CompletedDateTime = datep2.SelectedDate.Value;
                    }
                    catch (Exception ex)
                    {

                    }
                    task.Difficulty = double.Parse(diff.Text);
                    task.Time = int.Parse(time.Text);
                    string name = exec.Text;
                    string[] mas = name.Split(' ');
                    string firstName = mas[0];
                    string middleName = mas[1];
                    string lastName = mas[2];
                    var names = from a in db.User
                                where a.FirstName.Equals(firstName) && a.MiddleName.Equals(middleName) && a.LastName.Equals(lastName)
                                select a;
                    foreach (var u in names)
                    {
                        task.ExecutorID = u.ID;
                    }
                    task.Status = stat.Text;
                    task.WorkType = wtype.Text;
                    task.CreateDateTime = DateTime.Today;
                    db.Task.Add(task);
                    db.SaveChanges();
                    MessageBox.Show("Сохранено");
                    Close();
                }
            }
            else
            {
                using (Model1 db = new Model1())
                {
                    Model.Task task = db.Task.Where(p => p.ID.Equals(i)).FirstOrDefault();
                    task.Title = title.Text;
                    task.Description = desc.Text;
                    try
                    {
                        task.Deadline = datep1.SelectedDate.Value;
                        task.CompletedDateTime = datep2.SelectedDate.Value;
                    }
                    catch (Exception ex)
                    {

                    }
                    task.Difficulty = double.Parse(diff.Text);
                    task.Time = int.Parse(time.Text);
                    string name = exec.Text;
                    string[] mas = name.Split(' ');
                    string firstName = mas[0];
                    string middleName = mas[1];
                    string lastName = mas[2];
                    var names = from a in db.User
                                where a.FirstName.Equals(firstName) && a.MiddleName.Equals(middleName) && a.LastName.Equals(lastName)
                                select a;
                    foreach (var u in names)
                    {
                        task.ExecutorID = u.ID;
                    }
                    task.Status = stat.Text;
                    task.WorkType = wtype.Text;
                    db.Entry(task).State = EntityState.Modified;
                    db.SaveChanges();
                    MessageBox.Show("Сохранено");
                    Close();
                }
            }
        }

        private void time_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((Char.IsDigit(e.Text, 0) || ((e.Text == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString())
                && (DS_Count(((TextBox)sender).Text) < 1))));
        }
        public int DS_Count(string s)
        {
            string substr = System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString();
            int count = (s.Length - s.Replace(substr, "").Length) / substr.Length;
            return count;
        }

        private void diff_TextChanged(object sender, TextChangedEventArgs e)
        {
            try
            {
                int times = int.Parse(diff.Text);
                if (times > 50 || times <= 0)
                {
                    MessageBox.Show("Ошибка");
                    diff.Clear();
                }
                else
                {
                    t = int.Parse(diff.Text);
                }
            }
            catch (Exception ex)
            {

            }
        }

        private void diff_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !((Char.IsDigit(e.Text, 0) || ((e.Text == System.Globalization.CultureInfo.CurrentCulture.NumberFormat.NumberDecimalSeparator[0].ToString())
                && (DS_Count(((TextBox)sender).Text) < 1))));
        }
    }
}
