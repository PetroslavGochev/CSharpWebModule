namespace SIS.MvcFramework
{
    public partial class ViewEngine
    {
        public interface IView
        {
            string GetHtml(object model, string user);
        }
    }
}
