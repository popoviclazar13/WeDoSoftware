using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.TrainingDTO;

namespace WeDoSoftware.Application.ServiceInterfaces
{
    public interface ITrainingService
    {
        Task<IEnumerable<TrainingDto>> GetAllAsync();
        Task<TrainingDto> GetByIdAsync(int id);
        Task<int> CreateAsync(CreateTrainingDto dto);
        Task UpdateAsync(int id, UpdateTrainingDto dto);
        Task DeleteAsync(int id);
    }
}
