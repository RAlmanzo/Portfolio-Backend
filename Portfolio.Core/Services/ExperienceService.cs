using Microsoft.Extensions.Logging;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services;
using Portfolio.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Services
{
    public class ExperienceService : IExperienceService
    {
        private readonly IExperienceRepository _experienceRepository;
        private readonly ILogger<ExperienceService> _logger;

        public ExperienceService(IExperienceRepository experienceRepository, ILogger<ExperienceService> logger)
        {
            _experienceRepository = experienceRepository;
            _logger = logger;
        }

        public async Task<ResultModel<Experience>> GetByIdAsync(string id)
        {
            try
            {
                //get experience
                var experience = await _experienceRepository.GetByIdAsync(id);
                //check if exists
                if (experience == null)
                {
                    return new ResultModel<Experience>
                    {
                        Success = false,
                        Errors = [$"No experience found with id: {id}"],
                    };
                }
                //if exists
                return new ResultModel<Experience>
                {
                    Success = true,
                    Value = experience,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving experience with id {Id} : {Message}", id, ex.Message);

                return new ResultModel<Experience>
                {
                    Success = false,
                    Errors = [$"An error occurred while retrieving experience with id: {id}. Please try again or contact support"]
                };
            }
        }

        public async Task<ResultModel<Experience>> CreateExperienceAsync(ExperienceCreateRequestModel ExperienceCreateRequestModel)
        {
            try
            {
                //create experience
                var experience = new Experience
                {
                    Location = ExperienceCreateRequestModel.Location,                  
                    Company = ExperienceCreateRequestModel.Company,
                    StartDate = ExperienceCreateRequestModel.StartDate,
                    EndDate = ExperienceCreateRequestModel.EndDate,
                    Position = ExperienceCreateRequestModel.Position,
                    Information = ExperienceCreateRequestModel.Information,
                };

                //save experience
                var result = await _experienceRepository.AddAsync(experience);

                if (!result)
                {
                    return new ResultModel<Experience>
                    {
                        Success = false,
                        Errors = ["Could not create new experience"],
                    };
                }

                return new ResultModel<Experience>
                {
                    Success = true,
                    Value = experience,
                };
            }
            catch (Exception ex)
            {                
                _logger.LogError("An error occurred while creating experience: {Message}", ex.Message);
                
                return new ResultModel<Experience>
                {
                    Success = false,
                    Errors = ["An error occurred while creating the experience. Please try again or contact support"]
                };
            }
        }

        public async Task<ResultModel<Experience>> DeleteExperienceAsync(string id)
        {
            try
            {
                //get experience
                var experience = await _experienceRepository.GetByIdAsync(id);

                //check if exists
                if (experience == null)
                {
                    return new ResultModel<Experience>
                    {
                        Success = false,
                        Errors = [$"No experience found with id: {id}"],
                    };
                }

                //delete experience
                await _experienceRepository.DeleteAsync(experience);

                //return success
                return new ResultModel<Experience> { Success = true, };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting experience with id {Id} : {Message}", id, ex.Message);

                return new ResultModel<Experience>
                {
                    Success = false,
                    Errors = [$"An error occurred while deleting the experience with id: {id}. Please try again or contact support"]
                };
            }
        }

        public async Task<ResultModel<IEnumerable<Experience>>> GetAllAsync()
        {
            try
            {
                //get all experiences
                var experiences = await _experienceRepository.GetAllAsync();

                //if no experiences found
                if (!experiences.Any())
                {
                    return new ResultModel<IEnumerable<Experience>>
                    {
                        Success = false,
                        Errors = ["No experiences found."],
                    };
                }

                //if exists
                return new ResultModel<IEnumerable<Experience>>
                {
                    Success = true,
                    Value = experiences,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving experiences: {Message}", ex.Message);

                return new ResultModel<IEnumerable<Experience>>
                {
                    Success = false,
                    Errors = ["An error occurred while retrieving experiences. Please try again or contact support"]
                };
            }
        }

        public async Task<ResultModel<Experience>> UpdateExperienceAsync(ExperienceUpdateRequestModel ExperienceUpdateRequestModel)
        {
            try
            {
                // get experience
                var selectedExperience = await _experienceRepository.GetByIdAsync(ExperienceUpdateRequestModel.Id);

                // check if exists
                if (selectedExperience == null)
                {
                    return new ResultModel<Experience>
                    {
                        Success = false,
                        Errors = [$"No experience found with id: {ExperienceUpdateRequestModel.Id}"],
                    };
                }

                // update experience
                selectedExperience.Location = ExperienceUpdateRequestModel.Location;
                selectedExperience.Company = ExperienceUpdateRequestModel.Company;
                selectedExperience.StartDate = ExperienceUpdateRequestModel.StartDate;
                selectedExperience.EndDate = ExperienceUpdateRequestModel.EndDate;
                selectedExperience.Position = ExperienceUpdateRequestModel.Position;
                selectedExperience.Information = ExperienceUpdateRequestModel.Information;

                await _experienceRepository.UpdateAsync(selectedExperience);

                return new ResultModel<Experience>
                {
                    Success = true,
                    Value = selectedExperience,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while updating experience with id {Id}: {Message}", ExperienceUpdateRequestModel.Id, ex.Message);
                return new ResultModel<Experience>
                {
                    Success = false,
                    Errors = [$"An error occurred while updating experience with id: {ExperienceUpdateRequestModel.Id}. Please try again or contact support"]
                };
            }
        }
    }
}
