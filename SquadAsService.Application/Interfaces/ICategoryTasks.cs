namespace Fiker.Application.Interfaces
{
    public interface ICategoryTasks
    {
        Task SendNewArea(string name);
        Task SendNewMarket(string name);
        Task SendNewTechnology(string name);
        Task SendNewProfile(string name);
    }
}
