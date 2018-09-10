using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using Domain.Managers;
using Entities;
using Syncfusion.Data;
using Syncfusion.Data.Extensions;
using Syncfusion.Pdf;
using Syncfusion.UI.Xaml.Grid;
using Syncfusion.UI.Xaml.Grid.Converter;
using Syncfusion.UI.Xaml.Grid.Helpers;
using Syncfusion.XlsIO;
using Button = System.Windows.Controls.Button;
using MessageBox = System.Windows.MessageBox;
using MouseEventArgs = System.Windows.Input.MouseEventArgs;

namespace WpfDemoTaller
{
    /// <summary>
    /// Interaction logic for PrimeWindow.xaml
    /// </summary>
    public partial class PrimeWindow : Window
    {
        public PrimeWindow(User user)
        {
            InitializeComponent();
            getUser = user;

            if (user.RolEnum == Rol.OperadorSistema)
            {
                TabUser.Visibility = Visibility.Collapsed;
                TabClients.Visibility = Visibility.Collapsed;
                TabService.Visibility = Visibility.Collapsed;
                TabProveedores.Visibility = Visibility.Collapsed;
            }

            InitializeSources();
        }

        private SfDataGrid Grid { get; set; }

        private void InitializeSources()
        {
            DataContext = this;
            UserDataGrid.ItemsSource = getManager.User.Get().ToList();
            ServiceDataGrid.ItemsSource = getManager.Service.Get().ToList();
            MechanicDataGrid.ItemsSource = getManager.Mechanic.Get().ToList();
            ClientsDataGrid.ItemsSource = getManager.Client.Get().ToList();
            ProviderDataGrid.ItemsSource = getManager.Provider.Get().ToList();
            AccesoriesDataGrid.ItemsSource = getManager.Accessory.Get().ToList();
            RepairDataGrid.ItemsSource = getManager.Repair.Get().ToList();
        }

        public PrimeWindow()
        {
            InitializeComponent();
            getUser = getManager.User.Get().FirstOrDefault();
            InitializeSources();
        }

        public User getUser { get; set; }
        public Manager getManager
        {
            get { return Manager.SingleManager(); }
        }
        public IEnumerable<string> Roles
        {
            get { return Enum.GetNames(typeof(Rol)); }
        }

        private void AddDetailRecord(SfDataGrid datagrid, IEnumerable<object> list)
        {
            var views = datagrid.View.Records[datagrid.SelectedIndex].ChildViews;
            if (views != null && views.Any())
            {
                var childView = views.ElementAt(0);
                var records = childView.Value.View.Records;
                foreach (var item in list)
                {
                    var newRecord = records.CreateRecordEntry(item);
                    records.Add(newRecord);
                }
            }
        }

        #region Region User
        private void UserDataGrid_RecordDeleting(object sender, Syncfusion.UI.Xaml.Grid.RecordDeletingEventArgs args)
        {
            var user = args.Items.Cast<User>().ToArray();
            var errors = Remove(user);

            if (errors.Count > 0)
            {
                //Manejar Errores
            }
        }

        private void BtnPDF_Click(object sender, RoutedEventArgs e)
        {
            if (Grid == null) return;
            var document = Grid.ExportToPdf(new PdfExportingOptions() { AutoColumnWidth = true, AutoRowHeight = true, ExcludeColumns = new List<string>() { "Id", "Password", "Image" } });
            GeneratePDF(document);
        }

        private void BtnExcel_Click(object sender, RoutedEventArgs e)
        {
            GetValue(Grid);
        }


        private void UserDataGrid_RowValidating(object sender, RowValidatingEventArgs args)
        {
            if (UserDataGrid.IsAddNewIndex(args.RowIndex))
            {

                var user = args.RowData as User;
                var error = getManager.User.Validate(user);
                if (error.Count > 0)
                {
                    args.IsValid = false;
                    foreach (var item in error)
                        args.ErrorMessages.Add(item.Key, item.Value);
                }

            }
        }

        private void UserDataGrid_RowValidated(object sender, RowValidatedEventArgs args)
        {
            var user = (args.RowData as User);

            var objResult = user.Id == 0 ? getManager.User.Add(user) : getManager.User.Modify(user);
            if (objResult.Success)
                getManager.User.SaveChanges();
            else
            {
                //Manejar Excepciones
            }
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var user = ((e.Source as Button).DataContext as User);
            var errors = Remove(user);

            if (errors.Count > 0)
            {
                //Tratar Errores
            }
            RemoveRecord(UserDataGrid);
        }
        #endregion

        #region Region Client
        private void BtnPdf_Client_Click(object sender, RoutedEventArgs e)
        {
            var document = ClientsDataGrid.ExportToPdf(new PdfExportingOptions() { AutoColumnWidth = true, AutoRowHeight = true, ExcludeColumns = new List<string>() { "Id", "Password" } });
            GeneratePDF(document);
        }

        private void BtnExcel_Client_Click(object sender, RoutedEventArgs e)
        {
            GetValue(ClientsDataGrid);
        }

        private void ClientsDataGrid_RecordDeleting(object sender, RecordDeletingEventArgs args)
        {
            var client = args.Items.Cast<Client>().ToArray();
            var errors = Remove(client);

            if (errors.Count > 0)
            {
                //Manejar Errores
            }
        }

        private void BtnRemoveClient_OnClick(object sender, RoutedEventArgs e)
        {
            var client = ((e.Source as Button).DataContext as Client);
            var errors = Remove(client);

            if (errors.Count > 0)
            {
                //Tratar Errores
            }
            RemoveRecord(ClientsDataGrid);
        }

        private void ClientsDataGrid_RowValidating(object sender, RowValidatingEventArgs args)
        {
            if (ClientsDataGrid.IsAddNewIndex(args.RowIndex))
            {

                var client = args.RowData as Client;
                var error = getManager.Client.Validate(client);
                if (error.Count > 0)
                {
                    args.IsValid = false;
                    foreach (var item in error)
                        args.ErrorMessages.Add(item.Key, item.Value);
                }

            }
        }

        private void ClientsDataGrid_RowValidated(object sender, RowValidatedEventArgs args)
        {
            var client = (args.RowData as Client);

            var objResult = client.Id == 0 ? getManager.Client.Add(client) : getManager.Client.Modify(client);
            if (objResult.Success)
                getManager.Client.SaveChanges();
            else
            {
                //Manejar Excepciones
            }
        }
        #endregion

        #region Region Provider
        private void ProviderDataGrid_RecordDeleting(object sender, RecordDeletingEventArgs args)
        {
            var user = args.Items.Cast<Provider>().ToArray();
            var errors = Remove(user);

            if (errors.Count > 0)
            {
                //Manejar Errores
            }
        }

        private void BtnProvider_OnClick(object sender, RoutedEventArgs e)
        {
            var provider = ((e.Source as Button).DataContext as Provider);
            var errors = Remove(provider);

            if (errors.Count > 0)
            {
                //Tratar Errores
            }
            RemoveRecord(ProviderDataGrid);
        }

        private void ProviderDataGrid_RowValidating(object sender, RowValidatingEventArgs args)
        {
            if (ProviderDataGrid.IsAddNewIndex(args.RowIndex))
            {

                var provider = args.RowData as Provider;
                var error = getManager.Provider.Validate(provider);
                if (error.Count > 0)
                {
                    args.IsValid = false;
                    foreach (var item in error)
                        args.ErrorMessages.Add(item.Key, item.Value);
                }

            }
        }

        private void ProviderDataGrid_RowValidated(object sender, RowValidatedEventArgs args)
        {
            var provider = (args.RowData as Provider);
            if (provider == null) return;
            var objResult = provider.Id == 0 ? getManager.Provider.Add(provider) : getManager.Provider.Modify(provider);
            if (objResult.Success)
                getManager.Provider.SaveChanges();
            else
            {
                //Manejar Excepciones
            }
        }

        private void BtnExcelProvider_Click(object sender, RoutedEventArgs e)
        {
            GetValue(ProviderDataGrid);
        }

        private void BtnPdfProvider_Click(object sender, RoutedEventArgs e)
        {
            var document = ProviderDataGrid.ExportToPdf(new PdfExportingOptions() { AutoColumnWidth = true, AutoRowHeight = true, ExcludeColumns = new List<string>() { "Id", "Password" } });
            GeneratePDF(document);
        }
        #endregion

        #region Region Accesories
        private void AccesoriesDataGrid_RecordDeleting(object sender, RecordDeletingEventArgs args)
        {
            var accessory = args.Items.Cast<Accessory>().ToArray();
            var errors = Remove(accessory);

            if (errors.Count > 0)
            {
                //Manejar Errores
            }
        }

        private void AccesoriesDataGrid_RowValidated(object sender, RowValidatedEventArgs args)
        {
            var accesory = (args.RowData as Accessory);

            var objResult = accesory.Id == 0 ? getManager.Accessory.Add(accesory) : getManager.Accessory.Modify(accesory);
            if (objResult.Success)
                getManager.Provider.SaveChanges();
            else
            {
                //Manejar Excepciones
            }
        }

        private void AccesoriesDataGrid_RowValidating(object sender, RowValidatingEventArgs args)
        {
            if (ProviderDataGrid.IsAddNewIndex(args.RowIndex))
            {

                var accesory = args.RowData as Accessory;
                var error = getManager.Accessory.Validate(accesory);
                if (error.Count > 0)
                {
                    args.IsValid = false;
                    foreach (var item in error)
                        args.ErrorMessages.Add(item.Key, item.Value);
                }

            }
        }

        private void BtnAccesoryRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var accesory = ((e.Source as Button).DataContext as Accessory);
            var errors = Remove(accesory);

            if (errors.Count > 0)
            {
                //Tratar Errores
            }
            RemoveRecord(AccesoriesDataGrid);
        }

        private void BtnExcelAccesories_Click(object sender, RoutedEventArgs e)
        {
            GetValue(AccesoriesDataGrid);
        }

        private void BtnPdfAccesories_Click(object sender, RoutedEventArgs e)
        {
            var document = AccesoriesDataGrid.ExportToPdf(new PdfExportingOptions() { AutoColumnWidth = true, AutoRowHeight = true, ExcludeColumns = new List<string>() { "Id", "Password", "Image" } });
            GeneratePDF(document);
        }

        private void BtnAddProvider_OnClick(object sender, RoutedEventArgs e)
        {
            var accessory = ((e.Source as Button).DataContext as Accessory);
            AddProviderWindow win = new AddProviderWindow(accessory);
            win.UpdateAccessoryProvider += win_UpdateAccessoryProvider;
            win.ShowDialog();
        }

        void win_UpdateAccessoryProvider(List<ProviderAccessory> provider)
        {
            AddDetailRecord(AccesoriesDataGrid, provider);
            //var x =
            //    AccesoriesDataGrid.View.Records.Select(t => t.Data)
            //        .Cast<Accessory>()
            //        .FirstOrDefault(t => t.Id == provider.FirstOrDefault().IdAccessory);
            //x.Amount = provider.FirstOrDefault().Accessory.Amount;
            //AccesoriesDataGrid.View.Records.ResumeUpdates();

        }
        #endregion

        #region Region Service
        private void BtnPdfServices_OnClick(object sender, RoutedEventArgs e)
        {
            var options = new PdfExportingOptions()
            {
                AutoColumnWidth = true,
                AutoRowHeight = true,
                ExcludeColumns = new List<string>() { "Id", "btn" },
                ExportDetailsView = true
            };
            var document = ServiceDataGrid.ExportToPdf(options);
            GeneratePDF(document);
        }

        private void BtnExcelServices_OnClick(object sender, RoutedEventArgs e)
        {
            GetValue(ServiceDataGrid);
        }

        private void BtnServiceRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var service = ((e.Source as Button).DataContext as Service);
            var errors = Remove(service);

            if (errors.Count > 0)
            {
                //Tratar Errores
            }

            RemoveRecord(ServiceDataGrid);
        }

        private void BtnMechanicServiceRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var service = ((button.Content as StackPanel).Children[1] as TextBlock).DataContext as Service;
            var mechanic = ((e.Source as Button).DataContext as Mechanic);
            getManager.Service.RemoveMechanic(service, mechanic);
            RemoveRecord(ServiceDataGrid.SelectedDetailsViewGrid);
        }

        private void BtnMechanicAdd_Click(object sender, RoutedEventArgs e)
        {
            var service = ((e.Source as Button).DataContext as Service);
            AddMechanic addmecaMechanicWindow = new AddMechanic(service);
            addmecaMechanicWindow.UpdateDataSource += addmecaMechanicWindow_UpdateDataSource;
            addmecaMechanicWindow.ShowDialog();

        }

        void addmecaMechanicWindow_UpdateDataSource(IEnumerable<Mechanic> list)
        {
            AddDetailRecord(ServiceDataGrid, list);
        }

        private void ServiceDataGrid_RecordDeleting(object sender, RecordDeletingEventArgs args)
        {
            var service = args.Items.Cast<Service>().ToArray();
            var errors = Remove(service);

            if (errors.Count > 0)
            {
                //Manejar Errores
            }
        }

        private void ServiceDataGrid_RowValidated(object sender, RowValidatedEventArgs args)
        {
            var service = (args.RowData as Service);

            var objResult = service.Id == 0 ? getManager.Service.Add(service) : getManager.Service.Modify(service);
            if (objResult.Success)
            {
                getManager.Service.SaveChanges();
//                ServiceDataGrid.ItemsSource=getManager.Service.Get().ToList();

            }
            else
            {
                //Manejar Excepciones
            }
        }

        private void ServiceDataGrid_RowValidating(object sender, RowValidatingEventArgs args)
        {
            if (ServiceDataGrid.IsAddNewIndex(args.RowIndex))
            {
                var service = args.RowData as Service;
                var error = getManager.Service.Validate(service);
                if (error.Count > 0)
                {
                    args.IsValid = false;
                    foreach (var item in error)
                        args.ErrorMessages.Add(item.Key, item.Value);
                }
            }
        }


        #endregion Service

        #region Region Mechanic
        private void BtnPdfMechanic_Click(object sender, RoutedEventArgs e)
        {
            var document = MechanicDataGrid.ExportToPdf(new PdfExportingOptions() { AutoColumnWidth = true, AutoRowHeight = true, ExcludeColumns = new List<string>() { "Id", "Password", "Image" } });
            GeneratePDF(document);
        }

        private void BtnExcelMechanic_Click(object sender, RoutedEventArgs e)
        {
            GetValue(MechanicDataGrid);
        }

        private void BtnMechanicRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var mechanic = ((e.Source as Button).DataContext as Mechanic);
            var errors = Remove(mechanic);

            if (errors.Count > 0)
            {
                //Tratar Errores
            }
            RemoveRecord(MechanicDataGrid);
        }

        private void MechanicDataGrid_RecordDeleting(object sender, RecordDeletingEventArgs args)
        {
            var mechanic = args.Items.Cast<Mechanic>().ToArray();
            var errors = Remove(mechanic);

            if (errors.Count > 0)
            {
                //Manejar Errores
            }
        }
        private void MechanicDataGrid_RowValidated(object sender, RowValidatedEventArgs args)
        {
            var mechanic = (args.RowData as Mechanic);

            var objResult = mechanic.Id == 0 ? getManager.Mechanic.Add(mechanic) : getManager.Mechanic.Modify(mechanic);
            if (objResult.Success)
                getManager.Provider.SaveChanges();
            else
            {
                //Manejar Excepciones
            }
        }

        private void MechanicDataGrid_RowValidating(object sender, RowValidatingEventArgs args)
        {
            if (MechanicDataGrid.IsAddNewIndex(args.RowIndex))
            {
                var mechanic = args.RowData as Mechanic;
                var error = getManager.Mechanic.Validate(mechanic);
                if (error.Count > 0)
                {
                    args.IsValid = false;
                    foreach (var item in error)
                        args.ErrorMessages.Add(item.Key, item.Value);
                }
            }
        }

        #endregion

        private List<string> Remove<T>(params T[] entity) where T : class
        {
            var errors = new List<string>();
            var manager = getManager.GetManager<T>();
            foreach (var item in entity)
            {
                var result = manager.Delete(item);
                if (!result.Success)
                    errors.AddRange(result.Errors);
            }
            manager.SaveChanges();
            return errors;
        }

        private void GeneratePDF(PdfDocument document)
        {
            SaveFileDialog sfd = new SaveFileDialog
            {
                Filter = "PDF Files(*.Pdf)|*.Pdf"
            };
            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = sfd.OpenFile())
                {
                    document.Save(stream);
                }

                //Message box confirmation to view the created PDF file.
                if (MessageBox.Show("Do you want to view the PDF file?", "PDF file has been created",
                      MessageBoxButton.YesNo, MessageBoxImage.Information) == MessageBoxResult.Yes)
                {
                    //Launching the PDF file using the default Application.
                    System.Diagnostics.Process.Start(sfd.FileName);
                }
            }
        }

        private void GetValue(SfDataGrid grid)
        {
            ExcelExportingOptions exportingOptions = new ExcelExportingOptions(ExcelVersion.Excel2013, true, false, null, null);
            exportingOptions.ChildExportingEventHandler = handler;
            exportingOptions.ExportMode = ExportMode.Text;
            exportingOptions.ExcludeColumns = new List<string>() { "Id", "Password", "Image", "Mechanic.Image" };

            ExcelEngine excelEngine = grid.ExportToExcel(grid.View, exportingOptions);
            IWorkbook workBook = excelEngine.Excel.Workbooks[0];
            SaveFileDialog sfd = new SaveFileDialog
            {
                FilterIndex = 2,
                Filter = "Excel 97 to 2003 Files(*.xls)|*.xls|Excel 2007 to 2010 Files(*.xlsx)|*.xlsx"
            };

            if (sfd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                using (Stream stream = sfd.OpenFile())
                {
                    if (sfd.FilterIndex == 1)
                        workBook.Version = ExcelVersion.Excel97to2003;
                    else
                        workBook.Version = ExcelVersion.Excel2010;
                    workBook.SaveAs(stream);
                }
            }
        }

        private void handler(object sender, GridChildExportingEventArgs e)
        {
            e.ExcludeColumns.Add("Image");
            e.ExcludeColumns.Add("btn");
        }

        private void RemoveRecord(SfDataGrid datagrid)
        {
            int item = datagrid.SelectedIndex;
            var entity = datagrid.SelectedItem;
            if (item < datagrid.View.Records.Count && item >= 0)
            {
                datagrid.View.Records.RemoveAt(item);
//                var list = (datagrid.ItemsSource as IEnumerable<Object>).ToList();
//                list.Remove(entity);
//                datagrid.ItemsSource = list;
            }

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Application.Current.Shutdown();
        }

        #region Region Repair

        private void BtnRepairRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var repair = ((e.Source as Button).DataContext as Repair);
            var errors = Remove(repair);

            if (errors.Count > 0)
            {
                //Tratar Errores
            }
            RemoveRecord(RepairDataGrid);
        }

        private void BtnExcelRepair_Click(object sender, RoutedEventArgs e)
        {
            GetValue(RepairDataGrid);
        }

        private void BtnPdfRepair_Click(object sender, RoutedEventArgs e)
        {
            var document = RepairDataGrid.ExportToPdf(new PdfExportingOptions() { AutoColumnWidth = true, AutoRowHeight = true, ExcludeColumns = new List<string>() { "Id", "Password", "Image" } });
            GeneratePDF(document);
        }

        #endregion

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            ReportWindow rw = new ReportWindow();
            rw.ShowDialog();
        }

        private void TabControl_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (e.AddedItems.Count > 0)
            {
                var control = e.AddedItems[0] as TabItem;
                var name = control.Header.ToString().ToLower();
                switch (name)
                {
                    case "usuarios":
                        BtnGraphic.Visibility = Visibility.Hidden;
                        Grid = UserDataGrid;break;
                    case "servicios":
                        BtnGraphic.Visibility = Visibility.Hidden;
                        Grid = ServiceDataGrid;break;
                    case "clientes":
                        BtnGraphic.Visibility = Visibility.Hidden;
                        Grid = ClientsDataGrid;break;
                    case "proveedores":
                        BtnGraphic.Visibility = Visibility.Hidden;
                        Grid = ProviderDataGrid; break;
                    case "mecanicos":
                        BtnGraphic.Visibility = Visibility.Hidden;
                        Grid = MechanicDataGrid;break;
                    case "accesorios":
                        BtnGraphic.Visibility = Visibility.Hidden;
                        Grid = AccesoriesDataGrid;break;
                    case "refacciones":
                        Grid = RepairDataGrid;
                        BtnGraphic.Visibility = Visibility.Visible;
                        break;
                }
            }
        }


        private void BtnPdf_OnMouseEnter(object sender, MouseEventArgs e)
        {
            
        }
    }
}
