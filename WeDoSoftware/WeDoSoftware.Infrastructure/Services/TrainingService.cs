using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WeDoSoftware.Application.DTOs.TrainingDTO;
using WeDoSoftware.Application.ServiceInterfaces;
using WeDoSoftware.Domain.Entities;
using WeDoSoftware.Infrastructure.Data;

namespace WeDoSoftware.Infrastructure.Services
{
    public class TrainingService : ITrainingService
    {
        private readonly TrainingTrackerDbContext _context;

        public TrainingService(TrainingTrackerDbContext context)
        {
            _context = context;
        }
        public async Task<int> CreateAsync(CreateTrainingDto dto)
        {
            var training = new Training
            {
                UserId = dto.UserId,
                DateTime = dto.DateTime,
                ExerciseType = dto.ExerciseType,
                DurationInMinutes = dto.DurationInMinutes,
                Calories = dto.Calories,
                Intensity = dto.Intensity,
                Fatigue = dto.Fatigue,
                Notes = dto.Notes
            };

            _context.Trainings.Add(training);
            await _context.SaveChangesAsync();

            return training.Id;
        }

        public async Task DeleteAsync(int id)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training != null)
            {
                _context.Trainings.Remove(training);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException($"Training with id {id} not found.");
            }
        }

        public async Task<IEnumerable<TrainingDto>> GetAllAsync()
        {
            return await _context.Trainings
                .Select(t => new TrainingDto
                {
                    Id = t.Id,
                    UserId = t.UserId,
                    DateTime = t.DateTime,
                    ExerciseType = t.ExerciseType,
                    DurationInMinutes = t.DurationInMinutes,
                    Calories = t.Calories,
                    Intensity = t.Intensity,
                    Fatigue = t.Fatigue,
                    Notes = t.Notes
                })
                .ToListAsync();
        }

        public async Task<TrainingDto> GetByIdAsync(int id)
        {
            var t = await _context.Trainings.FindAsync(id);
            if (t == null)
                return null;

            return new TrainingDto
            {
                Id = t.Id,
                UserId = t.UserId,
                DateTime = t.DateTime,
                ExerciseType = t.ExerciseType,
                DurationInMinutes = t.DurationInMinutes,
                Calories = t.Calories,
                Intensity = t.Intensity,
                Fatigue = t.Fatigue,
                Notes = t.Notes
            };
        }

        public async Task UpdateAsync(int id, UpdateTrainingDto dto)
        {
            var training = await _context.Trainings.FindAsync(id);
            if (training == null)
            {
                throw new KeyNotFoundException($"Training with id {id} not found.");
            }

            training.UserId = dto.UserId;
            training.DateTime = dto.DateTime;
            training.ExerciseType = dto.ExerciseType;
            training.DurationInMinutes = dto.DurationInMinutes;
            training.Calories = dto.Calories;
            training.Intensity = dto.Intensity;
            training.Fatigue = dto.Fatigue;
            training.Notes = dto.Notes;

            await _context.SaveChangesAsync();
        }

    }
}
