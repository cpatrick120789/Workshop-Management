using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Domain.Managers;
using Entities;
using Syncfusion.Data.Extensions;

namespace WpfDemoTaller
{
    /// <summary>
    /// Interaction logic for AddMechanic.xaml
    /// </summary>
    public partial class AddMechanic : Window
    {
        private Service service;
        public AddMechanic(Service service)
        {
            InitializeComponent();
            this.service = service;
            var listMechanic = getManager.Mechanic.Get().ToList();
            var diference = listMechanic.Except(service.Mechanic.ToList());

            CBoxMechanic.DisplayMemberPath = "Name";
            CBoxMechanic.ItemsSource = diference;
        }

        public Manager getManager
        {
            get { return Manager.SingleManager(); }
        }


        private void BtnAddMechanic_Copy_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAddMechanic_Click(object sender, RoutedEventArgs e)
        {
            var items = CBoxMechanic.SelectedItems;
            foreach (Mechanic iten in items)
                service.Mechanic.Add(iten);
           
            getManager.Service.SaveChanges();

            if (UpdateDataSource != null)
            {
                UpdateDataSource(items.Cast<Mechanic>());
            }
            this.Close();
        }

        public delegate void UpdateD(IEnumerable<Mechanic> list );
        public event UpdateD UpdateDataSource;


    }
}
