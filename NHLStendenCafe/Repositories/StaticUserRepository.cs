using NHLStendenCafe.Models;

namespace NHLStendenCafe.Repositories
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
            if (cafeUser.UserId == default(Guid))
            {
                cafeUser.UserId = new Guid();
            }
            
            if (_users.Find(x => x.UserId == cafeUser.UserId) != null)
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
            return _users.SingleOrDefault(x => x.UserId == guid);
        }
    }
}
