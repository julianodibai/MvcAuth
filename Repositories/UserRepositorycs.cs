using MvcAuth.Models;

namespace MvcAuth.Repositories
{
    public static class UserRepositorycs
    {
        public static User Get(string username, string password)
        {
            var users = new List<User>
            {
                new User { Id = 1, Username = "Juliano", Password = "1234", Role = "manager"},
                new User { Id = 2, Username = "Raul", Password = "1234", Role = "employee"}

            };

            return users.Where(u =>
                        u.Username?.ToLower() == username.ToLower() &&
                        u.Password == password).First();

        }
    }
}
