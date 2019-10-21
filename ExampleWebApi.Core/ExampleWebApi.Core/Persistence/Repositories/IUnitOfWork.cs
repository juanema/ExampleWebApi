using System.Threading.Tasks;

namespace ExampleWebApi.Core.Persistence.Repositories
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}
