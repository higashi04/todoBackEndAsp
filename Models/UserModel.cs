using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace backendServer.Models
{
    public class UserModel
    {
        [Key]
        private int _IdUser;
        public int IdUser
        {
            get { return _IdUser; }
            set { _IdUser = value; }
        }
        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; }
        }
        private string _LastName;
        public string LastName
        {
            get { return _LastName; }
            set { _LastName = value; }
        }
        private string _Email;
        public string Email
        {
            get { return _Email; }
            set { _Email = value; }
        }
        private string _Password;
        public string Password
        {
            get { return _Password; }
            set { _Password = value; }
        }
    }
}