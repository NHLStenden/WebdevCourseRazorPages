using System;
using System.Collections.Generic;
using System.Linq;
using Exercises.Pages.Lesson1.Models;

namespace Exercises.Pages.Lesson1.Repository
{
    public static class StaticUserRepository
    {
        static List<CafeUser> _users = new List<CafeUser>();

        public enum AddUserResult
        {
            UserNameIsNotUnique,
            GuidIsNotUnique, //zou nooit moeten voorkomen
            Success
        }
        
        public static AddUserResult AddUser(CafeUser cafeUser)
        {
            if (cafeUser.UniqueGuid == default(Guid))
            {
                cafeUser.UniqueGuid = new Guid();
            }
            
            if (_users.Find(x => x.UniqueGuid == cafeUser.UniqueGuid) != null)
            {
                return AddUserResult.GuidIsNotUnique;
            }

            if (_users.Find(x =>
                    x.UserName.Equals(cafeUser.UserName, StringComparison.InvariantCultureIgnoreCase)) != null)
            {
                return AddUserResult.UserNameIsNotUnique;
            }
            
            _users.Add(cafeUser);
            return AddUserResult.Success;
        }

        public static CafeUser GetUser(string username, string password)
        {
            return _users.FirstOrDefault(x =>
                x.UserName.Equals(username, StringComparison.OrdinalIgnoreCase) &&
                x.Password.Equals(password));
        }

        public static CafeUser GetUser(Guid guid)
        {
            return _users.SingleOrDefault(x => x.UniqueGuid == guid);
        }
    }
}
