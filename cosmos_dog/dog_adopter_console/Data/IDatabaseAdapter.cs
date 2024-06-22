using System.Collections.Generic;
using System.Threading.Tasks;
using dog_adopter.Models;

namespace dog_adopter.Data
{
    public interface IDatabaseAdapter
    {
        Task<bool> CreateRescueDog(RescueDog rescueDog);

        Task<bool> UpdateRescueDog(RescueDog rescueDog);

    }
}