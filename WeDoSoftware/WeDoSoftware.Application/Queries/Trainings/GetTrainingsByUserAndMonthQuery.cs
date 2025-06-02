using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.TrainingDTO;

namespace WeDoSoftware.Application.Queries.Trainings
{
    public record GetTrainingsByUserAndMonthQuery(int UserId, int Year, int Month) : IRequest<IEnumerable<TrainingDto>>;
}
