namespace BookShop.Shared;

public static class ConstSettings
{
    public static string DefalutRole { get; } = "User";
    public static string AdminRole { get; } = "Admin";
    public static string CachedJWT { get; } = "CachedJWT";
    public static string DefaultJWTscheme { get; } = "Bearer";
    public static string AuthenticationType { get; } = "JwtAuth";

}
