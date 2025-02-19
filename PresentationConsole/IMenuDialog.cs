using Business.Models;
using System.Threading.Tasks;

namespace PresentationConsole
{
    public interface IMenuDialog
    {
        Task ShowProjects();
        Task CreateProjectAsync();
        Task MainMenu();
        Task UpdateProject();
        Task<Project?> ShowAndSelectProject();
    }
}