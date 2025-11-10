namespace WepAPI.Extensions
{
    public static class HttpContextExtensions
    {
        public static Guid GetUserId(this HttpContext context)
        {
            if (context.Items.TryGetValue("UserId", out var userId) && userId is Guid guidValue)
            {
                return guidValue;
            }

            throw new UnauthorizedAccessException("User ID not found in context");
        }

        public static Guid? GetUserIdOrNull(this HttpContext context)
        {
            if (context.Items.TryGetValue("UserId", out var userId) && userId is Guid guidValue)
            {
                return guidValue;
            }

            return null;
        }
    }
}
