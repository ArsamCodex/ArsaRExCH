using System.Security.Claims;

namespace ArsaRExCH.Extentionss
{
    public static class HttpContextExtensions
    {
        public static string GetUserEmail(this HttpContext context)
        {
            return context.User?.FindFirst(ClaimTypes.Email)?.Value;
        }
        public static string GetUserId(this HttpContext context)
        {
            return context.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }
    }

}
