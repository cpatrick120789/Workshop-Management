using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using Domain;
using Domain.Managers;
using Syncfusion.Olap.Manager;

namespace WpfDemoTaller
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow:Window
    {
        private System.Timers.Timer Timer { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            
        }

        private void BtnClose_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Manager manager = new Manager("Resources\\Images");
                var user = manager.User.Login(TBoxUserName.Text, TBoxPass.Password);
                if (user == null)
                {
                    //Message
                }
                else
                {
                    PrimeWindow prime = new PrimeWindow(user);
                    this.Hide();
                    prime.ShowDialog();
                }
            }
            catch (DomainException)
            {
                MessageBox.Show("Debe adquirir una licencia");
                Application.Current.Shutdown();
            }
            catch (Exception)
            {
                
            }
          
           
        }

 
        public delegate void CloseD();
        void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            CloseD close = Close;
            Dispatcher.BeginInvoke(close,DispatcherPriority.Normal);
        }

        public void Close()
        {
            VideoElement.Visibility = Visibility.Collapsed;
            ContainerPanel.Visibility = Visibility.Visible;
        }

        int i = 0;
        private void MediaElement_MediaOpened(object sender, RoutedEventArgs e)
        {
            Timer = new System.Timers.Timer(5000);
            Timer.Elapsed += timer_Elapsed;
            Timer.Start();

        }
        
       
     
    }
}
