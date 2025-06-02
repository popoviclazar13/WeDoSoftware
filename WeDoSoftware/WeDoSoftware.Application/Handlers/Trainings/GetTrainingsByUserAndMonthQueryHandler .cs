using AutoMapper;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.TrainingDTO;
using WeDoSoftware.Application.Queries.Trainings;
using WeDoSoftware.Domain.RepositoryInterfaces;

namespace WeDoSoftware.Application.Handlers.Trainings
{
    public class GetTrainingsByUserAndMonthQueryHandler : IRequestHandler<GetTrainingsByUserAndMonthQuery, IEnumerable<TrainingDto>>
    {
        private readonly ITrainingSessionRepository _trainingRepository;
        private readonly IMapper _mapper;

        public GetTrainingsByUserAndMonthQueryHandler(ITrainingSessionRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TrainingDto>> Handle(GetTrainingsByUserAndMonthQuery request, CancellationToken cancellationToken)
        {
            var trainings = await _trainingRepository.GetByUserAndMonthAsync(request.UserId, request.Year, request.Month);
            return _mapper.Map<IEnumerable<TrainingDto>>(trainings);
        }
    }
}
