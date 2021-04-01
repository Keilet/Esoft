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
    /// Логика взаимодействия для ExecList.xaml
    /// </summary>
    public partial class ExecList : Window
    {
        int ro;
        int i;
        public ExecList(int role, int id)
        {
            InitializeComponent();
            using (Model1 db = new Model1())
            {
                ro = role;
                i = id;

                var query = from a in db.User
                            where a.ID < 11 || a.ID > 13
                            group a by a.ID into g
                            select new
                            {
                                id = g.Key,
                                Исполнитель = db.User.Where(p => p.ID == g.Key).FirstOrDefault().FirstName + db.User.Where(p => p.ID == g.Key).FirstOrDefault().MiddleName + " " + db.User.Where(p => p.ID == g.Key).FirstOrDefault().LastName,
                                Грейд = db.Executor.Where(p => p.ID == g.Key).FirstOrDefault().Grade,
                                Запланирована = db.Task.Where(p => p.ExecutorID == g.Key && p.Status == "запланирована").Count(),
                                Исполняется = db.Task.Where(p => p.ExecutorID == g.Key && p.Status == "исполняется").Count(),
                                Выполнена = db.Task.Where(p => p.ExecutorID == g.Key && p.Status == "выполнена").Count(),
                                Отменена = db.Task.Where(p => p.ExecutorID == g.Key && p.Status == "отменена").Count(),
                                Менеджер = db.Executor.Where(p=>p.ID==g.Key).Join(db.User, p => p.ManagerID, c => c.ID, (p, c) => c.FirstName + c.MiddleName + " " + c.LastName).FirstOrDefault()
                            };
                lExec.ItemsSource = query.ToList();
                
            }
        }

        private void cbzap_Checked(object sender, RoutedEventArgs e)
        {
            
        }

        private void cbicp_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbvyp_Checked(object sender, RoutedEventArgs e)
        {

        }

        private void cbotm_Checked(object sender, RoutedEventArgs e)
        {

        }
    }
}
