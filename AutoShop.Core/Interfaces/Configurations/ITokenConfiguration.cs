namespace AutoShop.Core.Interfaces.Configurations
{
    public interface ITokenConfiguration
    {
        string Secret { get; set; }
        string Issuer { get; set; }
        string Audience { get; set; }
        int TokenExpires { get; set; }
    }
}