using BlueDataWeb.Models.Entites;

namespace MVC_Repository.Repositories
{
    public interface INewsRepository : IRepository<News>
    {
    }

    public class NewsRepository : GenericRepository<News>, INewsRepository

    {
    }
}