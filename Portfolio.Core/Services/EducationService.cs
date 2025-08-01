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
    public class EducationService : IEducationService
    {
        private readonly IEducationRepository _educationRepository;
        private readonly ILogger<EducationService> _logger;

        public EducationService(IEducationRepository educationRepository, ILogger<EducationService> logger)
        {
            _educationRepository = educationRepository;
            _logger = logger;
        }

        public async Task<ResultModel<Education>> GetByIdAsync(string id)
        {
            try
            {
                //get education
                var education = await _educationRepository.GetByIdAsync(id);

                //check if exists
                if (education == null) 
                {
                    return new ResultModel<Education>
                    {
                        Success = false,
                        Errors = [$"No education found with id: {id}"],
                    };
                }

                //if exists
                return new ResultModel<Education>
                {
                    Success = true,
                    Value = education,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving education with id {Id} : {Message}", id, ex.Message);

                return new ResultModel<Education>
                {
                    Success = false,
                    Errors = [$"An error occured while retrieving education with id: {id}"]
                };
            }
        }

        public Task<ResultModel<Education>> CreateEducationAsync(EducationCreateRequestModel EducationCreateRequestModel)
        {
            throw new NotImplementedException();
        }

        public async Task<ResultModel<Education>> DeleteEducationAsync(string id)
        {
            try
            {
                //check if exists
                var selectedEducation = await _educationRepository.GetByIdAsync(id);
                if (selectedEducation == null)
                {
                    return new ResultModel<Education>
                    {
                        Success = false,
                        Errors = [$"Education with id: {id}, does not exist!"]
                    };
                }

                //delete education
                await _educationRepository.DeleteAsync(selectedEducation);

                return new ResultModel<Education> { Success = true };
            }
            catch (Exception ex)
            {
                return new ResultModel<Education>
                {
                    Success = false,
                    Errors = [$"An error occured while deleting education : {ex.Message}"]
                };
            }
        }

        public async Task<ResultModel<IEnumerable<Education>>> GetAllAsync()
        {
            try
            {
                //get the educations
                var educations = await _educationRepository.GetAllAsync();

                //check if exists
                if (!educations.Any())
                {
                    return new ResultModel<IEnumerable<Education>>
                    {
                        Success = false,
                        Errors = ["No educations found!"],
                    };
                }

                //if exists
                return new ResultModel<IEnumerable<Education>>
                {
                    Success = true,
                    Value = educations,
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<IEnumerable<Education>>
                {
                    Success = false,
                    Errors = [$"An error occured while retrieving educations : {ex.Message}"]
                };
            }
        }

        public Task<ResultModel<Education>> UpdateEducationAsync(EducationtUpdateRequestModel EducationUpdateRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
