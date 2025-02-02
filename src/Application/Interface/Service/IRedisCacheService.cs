using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interface.Service
{
    public interface IRedisCacheService
    {
        Task AddConnectionIdAsync(string userId, string connectionId);

        Task RemoveConnectionIdAsync(string userId, string connectionId);

        Task<List<string>> GetConnectionIdsAsync(string userId);
    }
}
