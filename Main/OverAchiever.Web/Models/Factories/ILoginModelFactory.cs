namespace OverAchiever.Web.Models.Factories
{
    public interface ILoginModelFactory
    {
        ILoginModel Create();

        void Dispose(ILoginModel model);
    }
}