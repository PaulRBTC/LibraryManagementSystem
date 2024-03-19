using System.Linq;
using System.Windows;

namespace LibraryManagementSystem
{
    public class Utilities
    {

        public static void ShowPopup(string message)
        {
            var firstActiveWindow = Application.Current.Windows.OfType<Window>().FirstOrDefault(w => w.IsActive);
            ShowPopup(firstActiveWindow?.Title ?? "Book Management System", message);
        }

        public static void ShowPopup(string title, string message)
        {
            MessageBox.Show(message, title);
        }

    }
}
