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
    /// Логика взаимодействия для AddTask.xaml
    /// </summary>
    public partial class AddTask : Window
    {
        int t = 0;
        public AddTask()
        {
            InitializeComponent();
            using (Model1 db = new Model1())
            {
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

                wtype.Items.Add("анализ и проектирование");
                wtype.Items.Add("установка оборудования");
                wtype.Items.Add("техническое обслуживание и сопровождение");
            }
        }

        private void save_Click(object sender, RoutedEventArgs e)
        {
            using(Model1 db = new Model1())
            {
                Model.Task task = new Model.Task();
                task.Title = title.Text;
                task.Description = desc.Text;
                task.Deadline = datep1.SelectedDate.Value;
                task.CompletedDateTime= datep2.SelectedDate.Value;
                task.Difficulty = double.Parse(diff.Text);
                task.Time = int.Parse(time.Text);
                task.ExecutorID = int.Parse(exec.Text);
                task.Status = stat.Text;
                task.WorkType = wtype.Text;
                task.CreateDateTime = DateTime.Today;

                db.SaveChanges();
                MessageBox.Show("Сохранено");
                Close();
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
