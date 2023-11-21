using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Lab3.Model;

namespace Lab3.Model
{
    public class BusinessLogicController
    {
        private IDatabaseService databaseService;
        private IDataTransferService dataTransferService;
        private IUserInteractionService userInteractionService;

        public BusinessLogicController(IDatabaseService databaseService, IDataTransferService dataTransferService, IUserInteractionService userInteractionService)
        {
            this.databaseService = databaseService;
            this.dataTransferService = dataTransferService;
            this.userInteractionService = userInteractionService;
        }

        public string ProcessData()
        {
            WpfUserInteractionService interactionService = new WpfUserInteractionService();

            string loginInteractionService = interactionService.login;
            string passwordInteractionService = interactionService.password;
            string confirmedPasswordInteractionService = interactionService.confirmedPassword;

            Users resultDataUser = new Users();

            string result = "Операция выполнена";

            userInteractionService.ShowMessage();

            if (databaseService.CheckDataExists(loginInteractionService, passwordInteractionService, confirmedPasswordInteractionService) == true)
            {
                resultDataUser = databaseService.GetResultFromDatabase(loginInteractionService, passwordInteractionService, confirmedPasswordInteractionService);
                dataTransferService.SendData("Данные получены из базы данных");
            }
            else
            {
                databaseService.AddDataToDatabase(loginInteractionService, passwordInteractionService, confirmedPasswordInteractionService);
                dataTransferService.SendData("Данные добавлены в базу данных");
            }

            return result;
        }
    }
}
