using System;
using System.Collections.Generic;
using System.Linq;

namespace Exercises.Pages.Lesson1
{
    public static class StaticWeddingRepository
    {
        static List<WeddingCouple> _users = new List<WeddingCouple>();

        public enum AddUserResult
        {
            UserNameIsNotUnique,
            GuidIsNotUnique, //zou nooit moeten voorkomen
            Success
        }

        public static AddUserResult AddUser(Guid guid, WeddingCouple weddingCouple)
        {
            if (_users.Count(u => String.Equals(u.UserName, weddingCouple.UserName, StringComparison.CurrentCultureIgnoreCase)) > 0)
            {
                return AddUserResult.UserNameIsNotUnique;
            }

            if (_users.Count(u => u.UniqueGuid == guid) > 0)
            {
                return AddUserResult.GuidIsNotUnique;
            }

            _users.Add(weddingCouple);
            return AddUserResult.Success;
        }

        public static WeddingCouple GetUser(Guid guid)
        {
            return _users.SingleOrDefault(x => x.UniqueGuid == guid);
        }
    }

    public class WeddingCouple
    {
        public Guid UniqueGuid { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }

        public string Location { get; set; }
        public DateTime Date { get; set; }
    }
}
