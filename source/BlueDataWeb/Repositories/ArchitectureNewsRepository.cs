using BlueDataWeb.Models.Entites;

namespace MVC_Repository.Repositories
{
    public interface IArchitectureNewsRepository : IRepository<ArchitectureNew>
    {
    }

    public class ArchitectureNewsRepository : GenericRepository<ArchitectureNew>, IArchitectureNewsRepository

    {
    }
}