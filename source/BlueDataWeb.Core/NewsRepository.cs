 

namespace BlueDataWeb.Core
{

    public interface INewsRepository : IRepository<News>
    {
        
    }


    public class NewsRepository : GenericRepository<News>, INewsRepository

    {

        
    }
}