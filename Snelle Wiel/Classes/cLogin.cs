using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Snelle_Wiel.Classes
{
    class cLogin
    {
        Database db;
        public cLogin(Database database)
        {
            this.db = database;
        }
        public void Login(string Name, string Pass)
        {
            string Salt = BCrypt.GenerateSalt();
            string HashedPass = BCrypt.HashPassword(Pass, Salt);
            Console.WriteLine(HashedPass);
            //myHash == "$2a$10$rBV2JDeWW3.vKyeQcM8fFO4777l4bVeQgDL6VIkxqlzQ7TCalQvla"
            bool doesPasswordMatch = BCrypt.CheckPassword(HashedPass, HashedPass);
        }
    }
}
