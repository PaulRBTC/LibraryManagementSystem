using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Interop;

namespace LibraryManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for EditDvdWindow.xaml
    /// </summary>
    public partial class EditDvdWindow : Window
    {

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private readonly Services.IDvdsService _dvdsService;

        private Models.Dvd? _editingDvd;

        public EditDvdWindow(
            Services.IDvdsService dvdsService
        )
        {
            _dvdsService = dvdsService;

            InitializeComponent();

            // Disable 'close' button
            this.Loaded += (sender, e) =>
            {
                var hwnd = new WindowInteropHelper(this).Handle;
                SetWindowLong(hwnd, GWL_STYLE, GetWindowLong(hwnd, GWL_STYLE) & ~WS_SYSMENU);
            };
        }

        // Prevent ALT+F4 from working - force users to use 'Cancel' button to properly hide the window.
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            e.Cancel = true;
        }

        public async Task SetDvdToEdit(long id)
        {
            Models.Dvd dvd = await _dvdsService.GetDvd(id);
            _editingDvd = dvd;
            this.DataContext = dvd;
        }

        private void ClearFieldsAndHide()
        {
            _editingDvd = null;
            tbDvdName.Clear();
            tbDvdDirector.Clear();

            this.Hide();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            ClearFieldsAndHide();
        }

        private void BtnSubmitClick(object sender, RoutedEventArgs e)
        {
            string dvdName = tbDvdName.Text;
            string dvdDirector = tbDvdDirector.Text;

            _editingDvd.Name = dvdName;
            _editingDvd.Director = dvdDirector;

            _dvdsService.UpdateDvd(_editingDvd);

            Utilities.ShowPopup($"DVD \"{dvdName}\" has been edited!");

            ClearFieldsAndHide();
        }

    }
}
