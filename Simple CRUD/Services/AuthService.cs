using Simple_CRUD.Models;

namespace Simple_CRUD.Services
{
    public class AuthService
    {
        private readonly AppDbContext _context;

        public AuthService(AppDbContext context)
        {
            _context = context;
        }

        public User Register(string username, string password)
        {
            var hashed = BCrypt.Net.BCrypt.HashPassword(password);

            var user = new User
            {
                Username = username,
                Password = hashed
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public User Login(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(x => x.Username == username);

            if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
                return null;

            return user;
        }

        
    }
}
