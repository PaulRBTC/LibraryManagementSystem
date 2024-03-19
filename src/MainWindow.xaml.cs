using LibraryManagementSystem.Extensions;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows;

namespace LibraryManagementSystem
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly AddBookWindow _addBookWindow;
        private readonly EditBookWindow _editBookWindow;
        private readonly Services.IBooksService _booksService;

        public ObservableCollection<Models.Book> Books { get; private set; } = new();

        public MainWindow(
            AddBookWindow addBookWindow,
            EditBookWindow editBookWindow,
            Services.IBooksService booksService
        )
        {
            _addBookWindow = addBookWindow;
            _editBookWindow = editBookWindow;
            _booksService = booksService;

            RefreshBooks();

            InitializeComponent();
            DataContext = this;
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Current.Shutdown();
        }

        private void RefreshBooks()
        {
            Books.Clear();

            List<Models.Book> books = _booksService.GetAllBooks();
            foreach (var book in books)
            {
                Books.Add(book);
            }
        }

        private void DeleteButtonClick(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is not Models.Book book)
            {
                return;
            }

            if (!book.Id.HasValue)
            {
                return;
            }

            MessageBoxResult result = MessageBox.Show($"Are you sure you wish to delete book \"{book}\"?", "Are you sure?", MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    bool deleted = _booksService.DeleteBook(book.Id!.Value);
                    if (deleted)
                    {
                        Utilities.ShowPopup($"Book \"{book}\" has been deleted.");
                    }
                    else
                    {
                        Utilities.ShowPopup($"An unknown error occurred whilst trying to delete book \"{book}\", so it hasn't been deleted.");
                    }
                }
                catch (Exception ex)
                {
                    Utilities.ShowPopup($"The following error occurred whilst trying to delete book \"{book}\".{Environment.NewLine}{ex.GetFullMessage()}");
                }
                finally
                {
                    RefreshBooks();
                }
            }
            else
            {
                Utilities.ShowPopup($"Okay, book \"{book}\" won't be deleted.");
            }
        }

        private void EditButtonClicked(object sender, RoutedEventArgs e)
        {
            if (((FrameworkElement)sender).DataContext is not Models.Book book)
            {
                return;
            }

            _editBookWindow.SetBookToEdit(book);
            _editBookWindow.ShowDialog();
            RefreshBooks();
        }

        private void BtnAddClick(object sender, RoutedEventArgs e)
        {
            _addBookWindow.ShowDialog();
            RefreshBooks();
        }

        private void BtnRefreshClick(object sender, RoutedEventArgs e)
        {
            RefreshBooks();
        }
    }
}
