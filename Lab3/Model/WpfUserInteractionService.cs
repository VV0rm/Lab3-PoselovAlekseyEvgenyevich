using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab3.Model
{
    public interface IUserInteractionService
    {
        string ShowMessage();
    }

    public class WpfUserInteractionService : IUserInteractionService
    {
        public string login = "506_StX";
        public string password = "732%ЗоВ*";
        public string confirmedPassword = "732%ЗоВ*";

        public string ShowMessage()
        {

            string result = "Входные данные получены";

            MessageBox.Show($"{result}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);

            return result;
        }
    }
}
