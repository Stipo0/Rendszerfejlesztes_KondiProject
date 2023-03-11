namespace KondiProject.API.Helpers.HttpAccesor
{
    public class HttpAccessor : IHttpAccessor
    {
        private readonly IHttpContextAccessor _context;

        public HttpAccessor(IHttpContextAccessor context)
        {
            _context = context;
        }

        public int GetUserId()
        {
            var userId = _context.HttpContext!.User.FindFirst("userId")!.Value;

            return Convert.ToInt32(userId);
        }
        public string GetRole()
        {
            var role = _context.HttpContext!.User.FindFirst("role")!.Value;

            return role;
        }
    }
}
