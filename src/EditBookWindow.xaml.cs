using System;
using System.ComponentModel;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Interop;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for EditBookWindow.xaml
    /// </summary>
    public partial class EditBookWindow : Window
    {

        private const int GWL_STYLE = -16;
        private const int WS_SYSMENU = 0x80000;
        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);

        private readonly Services.IBooksService _booksService;

        private Models.Book? _editingBook;

        public EditBookWindow(
            Services.IBooksService booksService
        )
        {
            _booksService = booksService;

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

        public void SetBookToEdit(Models.Book book)
        {
            _editingBook = book;
            this.DataContext = book;
        }

        private void ClearFieldsAndHide()
        {
            _editingBook = null;
            tbBookTitle.Clear();
            tbBookAuthor.Clear();

            this.Hide();
        }

        private void BtnCancelClick(object sender, RoutedEventArgs e)
        {
            ClearFieldsAndHide();
        }

        private void BtnSubmitClick(object sender, RoutedEventArgs e)
        {
            string bookTitle = tbBookTitle.Text;
            string bookAuthor = tbBookAuthor.Text;

            _editingBook.Name = bookTitle;
            _editingBook.Author = bookAuthor;

            _booksService.UpdateBook(_editingBook);

            Utilities.ShowPopup($"Book \"{bookTitle}\" has been edited!");

            ClearFieldsAndHide();
        }

    }
}
