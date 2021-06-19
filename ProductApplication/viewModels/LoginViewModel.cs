using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductApplication.viewModels
{
    class LoginViewModel
    {
        public string Login { get; set; }
        public string Password { get; set;}

        public bool isExist()
        {
            var user = App.dbContext.Users.Where(x => x.Login == Login).FirstOrDefault();
            if (user != null)
            {
                if (user.Password == Password)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
