using AutoMapper;
using KondiProject.API.Enums;
using KondiProject.API.Models.Domains;
using KondiProject.API.Models.Dtos.Requests;
using KondiProject.API.Options;
using KondiProject.API.Repositories.GymRepository;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace KondiProject.API.Services.GymService
{
    public class GymService : IGymService
    {
        private readonly IGymRepository _repository;
        private readonly IMapper _mapper;
        private readonly JwtOptions _jwtOptions;
        public GymService(IGymRepository repository, IMapper mapper, IOptions<JwtOptions> jwtOptions)
        {
            _repository = repository;
            _mapper = mapper;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task RegisterAsync(CreateGymRequest reguest)
        {
            var gym = _mapper.Map<Gym>(reguest);

            gym.PasswordHash = Encoding.UTF8.GetBytes(
                BCrypt.Net.BCrypt.HashPassword(reguest.Password));

            await _repository.CreateAsync(gym);
        }

        public async Task<Gym?> GetByIdAsync(int id)
        {
            return await _repository.GetByIdAsync(id);
        }

        public async Task<Gym?> GetByEmailAsync(string email)
        {
            return await _repository.GetByEmailAsync(email);
        }

        public async Task<List<Gym>?> GetAllAsync()
        {
            return await _repository.GetAllAsync();
        }

        public Task<bool> EmailIsExistAsync(string email)
        {
            return _repository.EmailIsExistAsync(email);
        }

        public Task<bool> NameIsExistAsync(string name)
        {
            return _repository.NameIsExistAsync(name);
        }

        public Task<bool> PhoneNumberIsExistAsync(string phoneNumber) 
        {
            return _repository.PhoneNumberIsExistAsync(phoneNumber);
        }

        public Task DeleteAsync(Gym gym)
        {
            _repository.DeleteAsync(gym);
            return Task.CompletedTask;
        }

        public bool VerifyLogin(Gym gym, string password)
        {
            return BCrypt.Net.BCrypt.Verify(password, Encoding.UTF8.GetString(gym.PasswordHash));
        }

        public string CreateToken(Gym gym)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("gymId", gym.Id.ToString()),
                new Claim("role", Role.Gym.ToString()),
                new Claim("gymname", gym.Name.ToString()),
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
