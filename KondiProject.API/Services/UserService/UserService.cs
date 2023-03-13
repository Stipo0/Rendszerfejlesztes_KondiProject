using AutoMapper;
using KondiProject.API.Models.Domains;
using KondiProject.API.Models.Dtos.Requests;
using KondiProject.API.Options;
using KondiProject.API.Repositories.UserRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KondiProject.API.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;

        public UserService(IUserRepository userRepository, IMapper mapper, IOptions<JwtOptions> jwtOptions)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task RegisterAsync(UserRegisterRequest reguest)
        {
            var user = _mapper.Map<User>(reguest);

            user.PasswordHash = Encoding.UTF8.GetBytes(
                BCrypt.Net.BCrypt.HashPassword(reguest.Password));

            await _userRepository.CreateAsync(user);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _userRepository.GetByEmailAsync(email);
        }

        public async Task<List<User>?> GetAllAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        public Task<bool> EmailIsExistAsync(string email)
        {
            return _userRepository.EmailIsExistAsync(email);
        }

        public Task<bool> UsernameIsExistAsync(string username)
        {
            return _userRepository.UsernameIsExistAsync(username);
        }

        public Task DeleteAsync(User user)
        {
            _userRepository.DeleteAsync(user);
            return Task.CompletedTask;
        }

        public bool VerifyLogin(User user, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Encoding.UTF8.GetString(user.PasswordHash));
        }

        public string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("userId", user.Id.ToString()),
                new Claim("role",user.Role.ToString()),
                new Claim("username", user.Username.ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);
            var expires = DateTime.UtcNow.Add(TimeSpan.FromDays(Int32.Parse(_jwtOptions.TTL)));

            var token = new JwtSecurityToken(
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var jwtToken = new JwtSecurityTokenHandler().WriteToken(token);
            return jwtToken;
        }
    }
}
