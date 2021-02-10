using PaginationDemo.Dtos;
using System.Threading;
using System.Threading.Tasks;

namespace PaginationDemo.Interfaces
{
    public interface IHobbyService
    {
        Task<GetHobbyListResponseDto> GetByPageAsync(int limit, int page, CancellationToken cancellationToken);
    }
}
