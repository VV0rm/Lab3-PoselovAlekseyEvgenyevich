using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab3.Model
{
    public interface IDatabaseService
    {
        bool CheckDataExists(string login, string password, string confirmedPassword);

        Users GetResultFromDatabase(string login, string password, string confirmedPassword);

        void AddDataToDatabase(string login, string password, string confirmedPassword);
    }
    class DatabaseTransferService : IDatabaseService
    {
        public bool CheckDataExists(string login, string password, string confirmedPassword)
        {
            if (DataBinding.db.Users.Where(u => u.Login_User == login && u.Password_User == password && u.Confirm_password_User == confirmedPassword && u.Result_registration == "True").FirstOrDefault() != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public Users GetResultFromDatabase(string login, string password, string confirmedPassword)
        {
            Users searchUser = new Users();

            searchUser = DataBinding.db.Users.Where(u => u.Login_User == login && u.Password_User == password && u.Confirm_password_User == confirmedPassword && u.Result_registration == "True").FirstOrDefault();

            return searchUser;
        }

        public void AddDataToDatabase(string login, string password, string confirmedPassword)
        {
            Users newUser = new Users();
            newUser.Login_User = login;
            newUser.Password_User = password;
            newUser.Confirm_password_User = confirmedPassword;
            newUser.Result_registration = "True";
            newUser.Error_mes = null;
            DataBinding.db.Users.Add(newUser);
            DataBinding.db.SaveChanges();
        }
    }
}
