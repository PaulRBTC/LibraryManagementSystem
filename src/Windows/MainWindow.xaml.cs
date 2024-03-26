using LibraryManagementSystem.Extensions;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace LibraryManagementSystem.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AddBookWindow _addBookWindow;
        private readonly EditBookWindow _editBookWindow;
        private readonly Services.IBooksService _booksService;

        private readonly AddDvdWindow _addDvdWindow;
        private readonly EditDvdWindow _editDvdWindow;
        private readonly Services.IDvdsService _dvdsService;

        public Models.ListViewModelList ListItems { get; private set; } = new();

        public MainWindow(
            AddBookWindow addBookWindow,
            EditBookWindow editBookWindow,
            Services.IBooksService booksService,
            AddDvdWindow addDvdWindow,
            EditDvdWindow editDvdWindow,
            Services.IDvdsService dvdsService
        )
        {
            _addBookWindow = addBookWindow;
            _editBookWindow = editBookWindow;
            _booksService = booksService;

            _addDvdWindow = addDvdWindow;
            _editDvdWindow = editDvdWindow;
            _dvdsService = dvdsService;

            _ = RefreshListItems();

            InitializeComponent();
            DataContext = this;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private async Task RefreshListItems()
        {
            ListItems.Clear();

            await foreach (Models.Book book in _booksService.GetAllBooks())
            {
                ListItems.Add(new Models.ListViewModel
                {
                    Title = book.Name,
                    Owner = book.Author,
                    IsCheckedIn = book.IsCheckedIn,
                    Represents = typeof(Models.Book),
                    RepresentsId = book.Id!.Value,
                });
            }

            await foreach (var dvd in _dvdsService.GetAllDvds())
            {
                ListItems.Add(new Models.ListViewModel
                {
                    Title = dvd.Name,
                    Owner = dvd.Director,
                    IsCheckedIn = dvd.IsCheckedIn,
                    Represents = typeof(Models.Dvd),
                    RepresentsId = dvd.Id!.Value,
                });
            }
        }

        private async void BtnDeleteClick(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is not Models.ListViewModel lvm)
            {
                return;
            }

            string type = lvm.Represents == typeof(Models.Book) ? "book" : lvm.Represents == typeof(Models.Dvd) ? "DVD" : "";

            MessageBoxResult result = MessageBox.Show($"Are you sure you wish to delete {type} \"{lvm}\"?", "Are you sure?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bool deleted = false;
                    if (lvm.Represents == typeof(Models.Book))
                    {
                        deleted = await _booksService.DeleteBook(lvm.RepresentsId);
                    }
                    else if (lvm.Represents == typeof(Models.Dvd))
                    {
                        deleted = await _dvdsService.DeleteDvd(lvm.RepresentsId);
                    }
                    
                    if (deleted)
                    {
                        Utilities.ShowPopup($"{type.ToTitleCase()} \"{lvm}\" has been deleted.");
                    }
                    else
                    {
                        Utilities.ShowPopup($"An unknown error occurred whilst trying to delete {type} \"{lvm}\", so it hasn't been deleted.");
                    }
                }
                catch (Exception ex)
                {
                    Utilities.ShowPopup($"The following error occurred whilst trying to delete {type} \"{lvm}\".{Environment.NewLine}{ex.GetFullMessage()}");
                }
                finally
                {
                    _ = RefreshListItems();
                }
            }
            else
            {
                Utilities.ShowPopup($"Okay, {type} \"{lvm}\" won't be deleted.");
            }
        }

        private async void BtnEditClick(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is Models.ListViewModel lvm)
            {
                if (lvm.Represents == typeof(Models.Book))
                {
                    await _editBookWindow.SetBookToEdit(lvm.RepresentsId);
                    _editBookWindow.ShowDialog();
                }
                if (lvm.Represents == typeof(Models.Dvd))
                {
                    await _editDvdWindow.SetDvdToEdit(lvm.RepresentsId);
                    _editDvdWindow.ShowDialog();
                }

                _ = RefreshListItems();
            }
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            if (sender is FrameworkElement addButton)
            {
                addButton.ContextMenu.IsOpen = true;
            }
        }

        private void LvBooksSelectionChanged(
            object sender,
            SelectionChangedEventArgs e
        )
        {
            if (lvBooks.SelectedItem is Models.ListViewModel lvm)
            {
                if (lvm.IsCheckedIn)
                {
                    btnCheckIn.IsEnabled = false;
                    btnCheckOut.IsEnabled = true;
                }
                else
                {
                    btnCheckIn.IsEnabled = true;
                    btnCheckOut.IsEnabled = false;
                }

                return;
            }
            
            btnCheckIn.IsEnabled = false;
            btnCheckOut.IsEnabled = false;
        }

        private async void BtnCheckInClick(object sender, RoutedEventArgs e)
        {
            if (lvBooks.SelectedItem is Models.ListViewModel lvm)
            {
                if (lvm.Represents == typeof(Models.Book))
                {
                    await _booksService.CheckBookIn(lvm.RepresentsId);
                    Utilities.ShowPopup($"Book \"{lvm}\" has been checked in!");
                }
                else if (lvm.Represents == typeof(Models.Dvd))
                {
                    await _dvdsService.CheckDvdIn(lvm.RepresentsId);
                    Utilities.ShowPopup($"DVD \"{lvm}\" has been checked in!");
                }

                lvBooks.SelectedItems.Clear();
                _ = RefreshListItems();
            }
        }

        private async void BtnCheckOutClick(object sender, RoutedEventArgs e)
        {
            if (lvBooks.SelectedItem is Models.ListViewModel lvm)
            {
                if (lvm.Represents == typeof(Models.Book))
                {
                    await _booksService.CheckBookOut(lvm.RepresentsId);
                    Utilities.ShowPopup($"Book \"{lvm}\" has been checked out!");
                }
                else if (lvm.Represents == typeof(Models.Dvd))
                {
                    await _dvdsService.CheckDvdOut(lvm.RepresentsId);
                    Utilities.ShowPopup($"DVD \"{lvm}\" has been checked out!");
                }

                lvBooks.SelectedItems.Clear();
                _ = RefreshListItems();
            }
        }

        private void AddBookMenuItemClick(object sender, RoutedEventArgs e)
        {
            _addBookWindow.ShowDialog();
            _ = RefreshListItems();
        }

        private void AddDvdMenuItemClick(object sender, RoutedEventArgs e)
        {
            _addDvdWindow.ShowDialog();
            _ = RefreshListItems();
        }

        private void FileMenuCloseClick(object sender, RoutedEventArgs e)
        {
            var activeWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(wind => wind.IsActive);
            if (activeWindow != null)
            {
                activeWindow.Close();
            }
        }

        private void FileMenuRefreshClick(object sender, RoutedEventArgs e)
        {
            _ = RefreshListItems();
        }

    }
}
