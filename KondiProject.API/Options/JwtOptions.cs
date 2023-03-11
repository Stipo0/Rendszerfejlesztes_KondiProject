namespace KondiProject.API.Options
{
    public class JwtOptions
    {
        public string Key { get; set; } = null!;
        public string TTL { get; set; } = null!;
    }
}
