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
                //var query = from a in db.User
                //            join b in db.Task on a.ID equals b.ExecutorID
                //            join c in db.Executor on a.ID equals c.ID
                //            join d in db.User on c.ManagerID equals d.ID
                //            where a.ID.Equals(i) 
                //            group a by b.Status into g
                //            select new
                //            {
                //                Исполнитель = a.FirstName + a.MiddleName + " " + a.LastName,
                //                Грейд = c.Grade,
                //                Менеджер = d.FirstName + d.MiddleName + " " + d.LastName,
                //                Статус = b.Status,
                //                id = a.ID
                //            };
                var query = from a in db.Task
                            //where a.ID == id
                            group a by a.Status into g
                            select new
                            {
                                id=db.User.Where(p=>p.ID==i).FirstOrDefault().ID,
                                Исполнитель = db.User.Where(p=>p.ID==i).FirstOrDefault().FirstName+ " "+ db.User.Where(p => p.ID == i).FirstOrDefault().MiddleName + " " + db.User.Where(p => p.ID == i).FirstOrDefault().LastName,
                                Грейд = db.Executor.Where(p=>p.ID==i).FirstOrDefault().Grade,
                                Менеджер = db.Executor.Join(db.User,p=>p.ManagerID,c=>c.ID,(p,c)=>c.FirstName+ c.MiddleName+c.LastName)
                            };
                lExec.ItemsSource = query.ToList();
                
            }
        }
    }
}
