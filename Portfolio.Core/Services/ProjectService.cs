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

        public ProjectService(IProjectRepository projectRepository)
        {
            _projectRepository = projectRepository;
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
                return new ResultModel<Project>
                {
                    Success = false,
                    Errors = [$"An error occurred while retrieving project: {ex.Message}"]
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
                return new ResultModel<Project>
                {
                    Success = false,
                    Errors = [$"An error occured while creating new project : {ex.Message}"]
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
                return new ResultModel<Project>
                {
                    Success = false,
                    Errors = [$"An error occured while deleting project: {ex.Message}"],
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

        public Task<ResultModel<Project>> UpdateProjectAsync(ProjectUpdateRequestModel ProjectUpdateRequestModel)
        {
            throw new NotImplementedException();
        }
    }
}
