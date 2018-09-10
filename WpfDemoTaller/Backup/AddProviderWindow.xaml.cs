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
using Domain.Managers;
using Entities;

namespace WpfDemoTaller
{
    /// <summary>
    /// Interaction logic for AddProviderWindow.xaml
    /// </summary>
    public partial class AddProviderWindow : Window
    {
        private Accessory accessory;
        public AddProviderWindow(Accessory accesory)
        {
            InitializeComponent();
            this.accessory = accesory;
            var providers = getManager.Provider.Get().ToList();
            CBoxCantidad.DisplayMemberPath = "Name";
            CBoxCantidad.ItemsSource = providers;
        }

        private Manager getManager
        {
            get { return Manager.SingleManager(); }
        }

        private void BtnRemove_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            var cantidad = long.Parse(TBoxCantidad.Text);
            var date = DTimePicker.SelectedDate.Value;
            var providers = CBoxCantidad.SelectedItems;

            List<ProviderAccessory> providerAccessories = new List<ProviderAccessory>();
            foreach (Provider item in providers)
            {
                var providerAccesory = new ProviderAccessory()
                {
                    Amount = cantidad,
                    Date = date,
                    IdProvider = item.Id,
                    IdAccessory = this.accessory.Id
                };

                getManager.ProviderAccessory.Add(providerAccesory);
                providerAccessories.Add(providerAccesory);
            }

            getManager.Accessory.SaveChanges();
            UpdateAccessoryProvider(providerAccessories);

            this.Close();
        }

        public delegate void UpdateA(List<ProviderAccessory> provider);

        public event UpdateA UpdateAccessoryProvider;


    }
}
