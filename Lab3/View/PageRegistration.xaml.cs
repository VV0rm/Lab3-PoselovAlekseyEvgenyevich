using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Lab3.Model;

namespace Lab3.View
{
    /// <summary>
    /// Логика взаимодействия для PageRegistration.xaml
    /// </summary>
    public partial class PageRegistration : Page
    {
        private BusinessLogicController businessLogicController;
        
        public PageRegistration()
        {
            InitializeComponent();

            IUserInteractionService userInteractionService = new WpfUserInteractionService();
            IDatabaseService databaseService = new DatabaseTransferService();
            IDataTransferService dataTransferService = new MockDataTransferService();

            businessLogicController = new BusinessLogicController(databaseService, dataTransferService, userInteractionService);
        }

        private void RegistrationMethod(object sender, RoutedEventArgs e)
        {
            Users registeredUser = new Users();

            string regLoginMail = @"^[a-zA-Z0-9.%+-]+@[a-zA-Z0-9.%+-]+\.[a-zA-Z0-9.%+-]{2,}$";
            string regLoginPhoneNumber = @"^\+\d-\d{3}-\d{3}-\d{4}";

            switch (TypeLogin(LoginBox.Text))
            {
                case "Mail":
                    switch (Regex.IsMatch(LoginBox.Text, regLoginMail, RegexOptions.IgnoreCase))
                    {
                        case true:
                            if (DuplicateLogin(LoginBox.Text) == true)
                            {
                                switch (PasswordVerification(PswdBox.Password))
                                {
                                    case "Accept":
                                        if (PswdBox.Password == ConfirmPswdBox.Password)
                                        {
                                            registeredUser.Result_registration = "True";
                                            registeredUser.Login_User = LoginBox.Text;
                                            registeredUser.Password_User = PswdBox.Password;
                                            registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                            DataBinding.db.Users.Add(registeredUser);
                                            DataBinding.db.SaveChanges();

                                            MessageBox.Show("Успешная регистрация", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                            MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                        }
                                        else
                                        {
                                            registeredUser.Result_registration = "False";
                                            registeredUser.Login_User = LoginBox.Text;
                                            registeredUser.Password_User = PswdBox.Password;
                                            registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                            registeredUser.Error_mes = "Пароль и подтверждение пароля не совпадают";
                                            DataBinding.db.Users.Add(registeredUser);
                                            DataBinding.db.SaveChanges();

                                            MessageBox.Show("Регистрация отменена: Пароль и подтверждение пароля не совпадают", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                        }
                                        break;

                                    default:
                                        registeredUser.Result_registration = "False";
                                        registeredUser.Login_User = LoginBox.Text;
                                        registeredUser.Password_User = PswdBox.Password;
                                        registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                        registeredUser.Error_mes = PasswordVerification(PswdBox.Password);
                                        DataBinding.db.Users.Add(registeredUser);
                                        DataBinding.db.SaveChanges();

                                        MessageBox.Show($"Регистрация отменена: {PasswordVerification(PswdBox.Password)}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                        break;
                                }
                            }
                            else
                            {
                                registeredUser.Result_registration = "False";
                                registeredUser.Login_User = LoginBox.Text;
                                registeredUser.Password_User = PswdBox.Password;
                                registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                registeredUser.Error_mes = "Данный логин уже занят";
                                DataBinding.db.Users.Add(registeredUser);
                                DataBinding.db.SaveChanges();

                                MessageBox.Show($"Регистрация отменена: Данный логин уже занят", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            break;

                        case false:
                            registeredUser.Result_registration = "False";
                            registeredUser.Login_User = LoginBox.Text;
                            registeredUser.Password_User = PswdBox.Password;
                            registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                            registeredUser.Error_mes = "Неправильный формат эл. почты (xxx@xxx.xxx)";
                            DataBinding.db.Users.Add(registeredUser);
                            DataBinding.db.SaveChanges();

                            MessageBox.Show("Регистрация отменена: Неправильный формат эл. почты (xxx@xxx.xxx)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                    break;

                case "PhoneNumber":
                    switch (Regex.IsMatch(LoginBox.Text, regLoginPhoneNumber, RegexOptions.IgnoreCase))
                    {
                        case true:
                            if (DuplicateLogin(LoginBox.Text) == true)
                            {
                                switch (PasswordVerification(PswdBox.Password))
                                {
                                    case "Accept":
                                        if (PswdBox.Password == ConfirmPswdBox.Password)
                                        {
                                            registeredUser.Result_registration = "True";
                                            registeredUser.Login_User = LoginBox.Text;
                                            registeredUser.Password_User = PswdBox.Password;
                                            registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                            DataBinding.db.Users.Add(registeredUser);
                                            DataBinding.db.SaveChanges();

                                            MessageBox.Show("Успешная регистрация", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                            MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                        }
                                        else
                                        {
                                            registeredUser.Result_registration = "False";
                                            registeredUser.Login_User = LoginBox.Text;
                                            registeredUser.Password_User = PswdBox.Password;
                                            registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                            registeredUser.Error_mes = "Пароль и подтверждение пароля не совпадают";
                                            DataBinding.db.Users.Add(registeredUser);
                                            DataBinding.db.SaveChanges();

                                            MessageBox.Show("Регистрация отменена: Пароль и подтверждение пароля не совпадают", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                        }
                                        break;

                                    default:
                                        registeredUser.Result_registration = "False";
                                        registeredUser.Login_User = LoginBox.Text;
                                        registeredUser.Password_User = PswdBox.Password;
                                        registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                        registeredUser.Error_mes = PasswordVerification(PswdBox.Password);
                                        DataBinding.db.Users.Add(registeredUser);
                                        DataBinding.db.SaveChanges();

                                        MessageBox.Show($"Регистрация отменена: {PasswordVerification(PswdBox.Password)}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                        break;
                                }
                            }
                            else
                            {
                                registeredUser.Result_registration = "False";
                                registeredUser.Login_User = LoginBox.Text;
                                registeredUser.Password_User = PswdBox.Password;
                                registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                registeredUser.Error_mes = "Данный логин уже занят";
                                DataBinding.db.Users.Add(registeredUser);
                                DataBinding.db.SaveChanges();

                                MessageBox.Show($"Регистрация отменена: Данный логин уже занят", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            break;

                        case false:
                            registeredUser.Result_registration = "False";
                            registeredUser.Login_User = LoginBox.Text;
                            registeredUser.Password_User = PswdBox.Password;
                            registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                            registeredUser.Error_mes = "Неправильный формат номера телефона (+x-xxx-xxx-xxxx)";
                            DataBinding.db.Users.Add(registeredUser);
                            DataBinding.db.SaveChanges();

                            MessageBox.Show("Регистрация отменена: Неправильный формат номера телефона (+x-xxx-xxx-xxxx)", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                    break;

                case "JustString":
                    switch (LoginJustStringVerification(LoginBox.Text))
                    {
                        case "Accept":
                            if (DuplicateLogin(LoginBox.Text) == true)
                            {
                                switch (PasswordVerification(PswdBox.Password))
                                {
                                    case "Accept":
                                        if (PswdBox.Password == ConfirmPswdBox.Password)
                                        {
                                            registeredUser.Result_registration = "True";
                                            registeredUser.Login_User = LoginBox.Text;
                                            registeredUser.Password_User = PswdBox.Password;
                                            registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                            DataBinding.db.Users.Add(registeredUser);
                                            DataBinding.db.SaveChanges();

                                            MessageBox.Show("Успешная регистрация", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                            MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                        }
                                        else
                                        {
                                            registeredUser.Result_registration = "False";
                                            registeredUser.Login_User = LoginBox.Text;
                                            registeredUser.Password_User = PswdBox.Password;
                                            registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                            registeredUser.Error_mes = "Пароль и подтверждение пароля не совпадают";
                                            DataBinding.db.Users.Add(registeredUser);
                                            DataBinding.db.SaveChanges();

                                            MessageBox.Show("Регистрация отменена: Пароль и подтверждение пароля не совпадают", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                            MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                        }
                                        break;

                                    default:
                                        registeredUser.Result_registration = "False";
                                        registeredUser.Login_User = LoginBox.Text;
                                        registeredUser.Password_User = PswdBox.Password;
                                        registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                        registeredUser.Error_mes = PasswordVerification(PswdBox.Password);
                                        DataBinding.db.Users.Add(registeredUser);
                                        DataBinding.db.SaveChanges();

                                        MessageBox.Show($"Регистрация отменена: {PasswordVerification(PswdBox.Password)}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                        MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                                        break;
                                }
                            }
                            else
                            {
                                registeredUser.Result_registration = "False";
                                registeredUser.Login_User = LoginBox.Text;
                                registeredUser.Password_User = PswdBox.Password;
                                registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                                registeredUser.Error_mes = "Данный логин уже занят";
                                DataBinding.db.Users.Add(registeredUser);
                                DataBinding.db.SaveChanges();

                                MessageBox.Show($"Регистрация отменена: Данный логин уже занят", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                                MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                            }
                            break;

                        default:
                            registeredUser.Result_registration = "False";
                            registeredUser.Login_User = LoginBox.Text;
                            registeredUser.Password_User = PswdBox.Password;
                            registeredUser.Confirm_password_User = ConfirmPswdBox.Password;
                            registeredUser.Error_mes = LoginJustStringVerification(LoginBox.Text);
                            DataBinding.db.Users.Add(registeredUser);
                            DataBinding.db.SaveChanges();

                            MessageBox.Show($"Регистрация отменена: {LoginJustStringVerification(LoginBox.Text)}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                            MessageBox.Show($"{businessLogicController.ProcessData()}", "Informarion", MessageBoxButton.OK, MessageBoxImage.Information);
                            break;
                    }
                    break;
            }
        }

        public string TypeLogin(string login)
        {
            string type = "";
            if (Regex.IsMatch(login, @"^(?=.*@)(?=.*\.)", RegexOptions.IgnoreCase) == true)
            {
                type = "Mail";
            }
            else if (Regex.IsMatch(login, @"^\+", RegexOptions.IgnoreCase) == true)
            {
                type = "PhoneNumber";
            }
            else
            {
                type = "JustString";
            }
            return type;
        }

        public static string PasswordVerification(string password)
        {
            string message = password.Length >= 7 ?
                             Regex.IsMatch(password, @"^(?!.*[A-Za-z])", RegexOptions.None) ?
                             Regex.IsMatch(password, @"^(?=.*[а-я])", RegexOptions.None) ?
                             Regex.IsMatch(password, @"^(?=.*[А-Я])", RegexOptions.None) ?
                             Regex.IsMatch(password, @"^(?=.*\d)", RegexOptions.IgnoreCase) ?
                             Regex.IsMatch(password, @"^(?=.*\W)", RegexOptions.IgnoreCase) ?
                             "Accept" : "Отсутствие спецсимволов в пароле" :
                             "Отсутствие цифр в пароле" :
                             "Отсутствие заглавных букв на кириллице в пароле" :
                             "Отсутствие строчных букв на кириллице в пароле" :
                             "Наличие латинских букв в пароле" :
                             "Длина пароля долна быть минимум 7 символов";

            return message;
        }

        public static string LoginJustStringVerification(string login)
        {
            string message = login.Length >= 5 ?
                             Regex.IsMatch(login, @"^(?!.*[А-Яа-я])", RegexOptions.None) ?
                             Regex.IsMatch(login, @"^(?=.*[A-Za-z])", RegexOptions.None) ?
                             Regex.IsMatch(login, @"^(?=.*\d)", RegexOptions.IgnoreCase) ?
                             Regex.IsMatch(login, @"^(?=.*_)", RegexOptions.IgnoreCase) ?
                             "Accept" : "Отсутствие символа _ в логине-строке" :
                             "Отсутствие цифр в логине-строке" :
                             "Отсутствие латинских букв в логине-строке" :
                             "Наличие букв на кириллице в логине-строке" :
                             "Длина логина долна быть минимум 5 символов";

            return message;
        }

        public static bool DuplicateLogin(string login)
        {
            bool result = true;

            if(DataBinding.db.Users.Where(u => u.Login_User == login && u.Result_registration != "False").FirstOrDefault() != null)
            {
                result = false;
            }

            return result;
        }
    }
}
