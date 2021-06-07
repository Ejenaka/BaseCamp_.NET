namespace AutoShop.Core.Interfaces
{
    public interface IRepositoryManager
    {
        IUserRepository Users { get; }
        ICarRepository Cars { get; }
    }
}
