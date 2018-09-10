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
using Syncfusion.UI.Xaml.Charts;

namespace WpfDemoTaller
{
    /// <summary>
    /// Interaction logic for ReportWindow.xaml
    /// </summary>
    public partial class ReportWindow : Window
    {
        public ReportWindow()
        {
            InitializeComponent();

            var items = Manager.SingleManager().Mechanic.Get().Select(t => new { Name = t.Name, Total = t.MechanicOrder.Count }).ToList();
            
            ColumnSeries series1 = new ColumnSeries();
            series1.Label = "Name";
            series1.ItemsSource = items;
            series1.XBindingPath = "Name";
            series1.YBindingPath = "Total";

            BarCharTotalRepair.Series.Add(series1);
        }
    }
}
