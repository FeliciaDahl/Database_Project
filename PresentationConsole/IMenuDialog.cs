using System.Threading.Tasks;

namespace PresentationConsole
{
    public interface IMenuDialog
    {
        Task ShowProjects();
        Task CreateProjectAsync();
    }
}