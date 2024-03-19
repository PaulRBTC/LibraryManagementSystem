using System;

namespace LibraryManagementSystem.Extensions
{
    public static class ExceptionExtensions
    {

        public static string GetFullMessage(
            this Exception ex
        )
        {
            string message = string.Empty;

            do
            {
                message += ex.Message + Environment.NewLine + ex.StackTrace + Environment.NewLine + Environment.NewLine;
                ex = ex.InnerException;
            }
            while (ex != null);

            return message.Trim(Environment.NewLine.ToCharArray());
        }

    }
}
