using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.TrainingDTO;
using WeDoSoftware.Application.Exceptions;
using WeDoSoftware.Application.ServiceInterfaces;
using WeDoSoftware.Domain.Entities;
using WeDoSoftware.Infrastructure.Data;

namespace WeDoSoftware.Infrastructure.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly TrainingTrackerDbContext _context;
        private readonly IMapper _mapper;

        public TrainingService(TrainingTrackerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<int> CreateAsync(CreateTrainingDto dto)
        {
            var training = _mapper.Map<Training>(dto);

            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();

            return training.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null)
            {
                throw new NotFoundException($"Training with id {id} not found.");
            }

            _context.Trainings.Remove(training);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TrainingDto>> GetAllAsync()
        {
            var trainings = await _context.Trainings.ToListAsync();
            return _mapper.Map<IEnumerable<TrainingDto>>(trainings);
        }

        public async Task<TrainingDto> GetByIdAsync(int id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null)
                throw new NotFoundException($"Training with ID {id} not found.");

            return _mapper.Map<TrainingDto>(training);
        }

        public async Task UpdateAsync(int id, UpdateTrainingDto dto)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null)
            {
                throw new NotFoundException($"Training with id {id} not found.");
            }

            _mapper.Map(dto, training);

            await _context.SaveChangesAsync();
        }

    }
}
