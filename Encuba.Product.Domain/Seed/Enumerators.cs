namespace Encuba.Product.Domain.Seed;


/// <summary>
///     Expiration time added to current date
/// </summary>
public static class PublicAccessTokenExpiresIn
{
    public static int Minutes => 5;
}

public static class Scopes
{
    public static class PublicAccessTokenScopes
    {
        public static string Client => nameof(Client).ToUpper();
        public static string Worker => nameof(Worker).ToUpper();
        public static string Administrator => nameof(Administrator).ToUpper();
    }
}

public static class BCryptWorkFactor
{
    public static int Value => 4;
}