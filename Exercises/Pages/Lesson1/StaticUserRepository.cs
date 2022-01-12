using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Pages.Lesson1
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

        public static AddUserResult AddUser(Guid guid, CafeUser cafeUser)
        {
            if (_users.Count(u => String.Equals(u.UserName, cafeUser.UserName, StringComparison.CurrentCultureIgnoreCase)) > 0)
            {
                return AddUserResult.UserNameIsNotUnique;
            }

            if (_users.Count(u => u.UniqueGuid == guid) > 0)
            {
                return AddUserResult.GuidIsNotUnique;
            }

            _users.Add(cafeUser);
            return AddUserResult.Success;
        }

        public static CafeUser GetUser(Guid guid)
        {
            return _users.SingleOrDefault(x => x.UniqueGuid == guid);
        }
    }

    public class CafeUser
    {
        public Guid UniqueGuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
