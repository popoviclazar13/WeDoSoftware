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
    public class GetAllTrainingsByUserIdQueryHandler : IRequestHandler<GetAllTrainingsByUserIdQuery, IEnumerable<TrainingDto>>
    {
        private readonly ITrainingSessionRepository _trainingRepository;
        private readonly IMapper _mapper;

        public GetAllTrainingsByUserIdQueryHandler(ITrainingSessionRepository trainingRepository, IMapper mapper)
        {
            _trainingRepository = trainingRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<TrainingDto>> Handle(GetAllTrainingsByUserIdQuery request, CancellationToken cancellationToken)
        {
            var trainings = await _trainingRepository.GetAllByUserIdAsync(request.UserId);
            return _mapper.Map<IEnumerable<TrainingDto>>(trainings);
        }
    }
}
