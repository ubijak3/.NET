using Exercise5.Models;

namespace Exercise5.DAL
{
    public interface IWarehouseRepository
    {
        Task<bool> Exists(int id);

    }
}
