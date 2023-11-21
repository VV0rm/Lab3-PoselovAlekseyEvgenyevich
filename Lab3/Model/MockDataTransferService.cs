using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Lab3.Model
{
    public interface IDataTransferService
    {
        void SendData(string messageResult);
    }

    class MockDataTransferService : IDataTransferService
    {
        public void SendData(string messageResult)
        {
            MessageBox.Show($"{messageResult}", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
