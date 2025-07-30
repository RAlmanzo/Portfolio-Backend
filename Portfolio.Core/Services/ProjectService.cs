using Microsoft.Extensions.Logging;
using Portfolio.Core.Entities;
using Portfolio.Core.Interfaces.Repositories;
using Portfolio.Core.Interfaces.Services;
using Portfolio.Core.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Portfolio.Core.Services
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly ILogger<ProjectService> _logger;

        public ProjectService(IProjectRepository projectRepository, ILogger<ProjectService> logger)
        {
            _projectRepository = projectRepository;
            _logger = logger;
        }

        public async Task<ResultModel<Project>> GetByIdAsync(string id)
        {
            try
            {
                var project = await _projectRepository.GetByIdAsync(id);

                if (project == null)
                {
                    return new ResultModel<Project>
                    {
                        Success = false,
                        Errors = ["No project found!"]
                    };
                }

                return new ResultModel<Project>
                {
                    Success = true,
                    Value = project
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while retrieving project with id {Id} : {Message}", id, ex.Message);

                return new ResultModel<Project>
                {
                    Success = false,
                    Errors = ["An error occurred while retrieving project. Please try again or contact support"]
                };
            }
        }

        public async Task<ResultModel<Project>> CreateProjectAsync(ProjectCreateRequestModel ProjectCreateRequestModel)
        {
            try
            {
                //TODO: Research Store images in wwwroot or external db???

                //create new project
                var newProject = new Project
                {
                    Name = ProjectCreateRequestModel.Name,
                    Description = ProjectCreateRequestModel.Description,
                    FrontendTechStack = ProjectCreateRequestModel.FrontendTechStack?.ToList() ?? [],
                    BackendTechStack = ProjectCreateRequestModel.BackendTechStack?.ToList() ?? [],
                    FrontendGitHubUrl = ProjectCreateRequestModel.FrontendGitHubUrl,
                    BackendGitHubUrl = ProjectCreateRequestModel.BackendGitHubUrl,
                    ImagesPath = ProjectCreateRequestModel.ImagesPath?.ToList() ?? []
                };

                //call projectsrepo to add in db
                var result = await _projectRepository.AddAsync(newProject);

                if (!result)
                {
                    return new ResultModel<Project>
                    {
                        Success = false,
                        Errors = ["Could not create new project"],
                    };
                }

                return new ResultModel<Project>
                {
                    Success = true,
                    Value = newProject,
                };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while creating project {Name} : {Message}", ProjectCreateRequestModel.Name, ex.Message);

                return new ResultModel<Project>
                {
                    Success = false,
                    Errors = ["An error occured while creating new project. Please try again or contact support"]
                };
            }
        }

        public async Task<ResultModel<Project>> DeleteProjectAsync(string id)
        {
            try
            {
                //check if project exists
                var selectedProject = await _projectRepository.GetByIdAsync(id);
                if (selectedProject == null)
                {
                    return new ResultModel<Project>
                    {
                        Success = false,
                        Errors = ["Project does not exist!"],
                    };
                }

                //TODO: delete images from db or wwwroot?

                //delete project
                await _projectRepository.DeleteAsync(selectedProject);

                return new ResultModel<Project> { Success = true, };
            }
            catch (Exception ex)
            {
                _logger.LogError("An error occurred while deleting project with id {Id} : {Message}", id, ex.Message);

                return new ResultModel<Project>
                {
                    Success = false,
                    Errors = [$"An error occured while deleting project with id: {id}"],
                };
            } 
        }

        public async Task<ResultModel<IEnumerable<Project>>> GetAllAsync()
        {
            try
            {
                //get the projects
                var projects = await _projectRepository.GetAllAsync();

                //check if exists
                if (!projects.Any()) 
                {
                    return new ResultModel<IEnumerable<Project>>
                    {
                        Success = false,
                        Errors = ["No projects found"],
                    };
                }

                //if exists
                return new ResultModel<IEnumerable<Project>> 
                { 
                    Success = true,
                    Value = projects,
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<IEnumerable<Project>>
                {
                    Success = false,
                    Errors = [$"An error occured while retrieving all projects: {ex.Message}"],
                };
            }
        }

        public async Task<ResultModel<Project>> UpdateProjectAsync(ProjectUpdateRequestModel ProjectUpdateRequestModel)
        {
            try
            {
                //get the project
                var selectedProject = await _projectRepository.GetByIdAsync(ProjectUpdateRequestModel.Id);

                //check if exists
                if (selectedProject == null)
                {
                    return new ResultModel<Project>
                    {
                        Success = false,
                        Errors = [$"Project with id: {ProjectUpdateRequestModel.Id} not found!"],
                    };
                }

                //TODO: delete old images and add new images to db or wwwroot!!!!

                //update project fields
                selectedProject.Id = ProjectUpdateRequestModel.Id;
                selectedProject.Name = ProjectUpdateRequestModel.Name;
                selectedProject.Description = ProjectUpdateRequestModel.Description;
                selectedProject.FrontendTechStack = ProjectUpdateRequestModel.FrontendTechStack.ToList();
                selectedProject.BackendTechStack = ProjectUpdateRequestModel.BackendTechStack.ToList();
                selectedProject.FrontendGitHubUrl = ProjectUpdateRequestModel.FrontendGitHubUrl;
                selectedProject.BackendGitHubUrl = ProjectUpdateRequestModel.BackendGitHubUrl;
                selectedProject.ImagesPath = ProjectUpdateRequestModel.ImagesPath.ToList();

                //update db
                await _projectRepository.UpdateAsync(selectedProject);

                return new ResultModel<Project>
                {
                    Success = true,
                    Value = selectedProject,
                };
            }
            catch (Exception ex)
            {
                return new ResultModel<Project>
                {
                    Success = false,
                    Errors = [$"An error occured while updating project : {ex.Message}"]
                };
            }
        }
    }
}
