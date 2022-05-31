using BlueDataWeb.Models.Entites;

namespace MVC_Repository.Repositories
{
    public interface IArchitectureCategoriesRepository : IRepository<ArchitectureCategory>
    {
    }

    public class ArchitectureCategoriesRepository : GenericRepository<ArchitectureCategory>, IArchitectureCategoriesRepository

    {
    }
}