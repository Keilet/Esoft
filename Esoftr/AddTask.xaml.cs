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
        public AddTask()
        {
            InitializeComponent();
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
                task.ExecutorID =;
                task.Status =;
                task.WorkType =;
            }
        }
    }
}
