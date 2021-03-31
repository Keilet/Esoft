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
    /// Логика взаимодействия для TwoWays.xaml
    /// </summary>
    public partial class TwoWays : Window
    {
        int i;
        int r;
        public TwoWays(int role, int id)
        {
            InitializeComponent();
            i = id;
            r = role;
        }

        private void tttask_Click(object sender, RoutedEventArgs e)
        {
            int id = i;
            int role = r;
            ListTasks lt = new ListTasks(role,id);
            lt.Show();
        }

        private void eeexec_Click(object sender, RoutedEventArgs e)
        {
            int id = i;
            int role = r;
            ExecList lt = new ExecList(role, id);
            lt.Show();
        }
    }
}
