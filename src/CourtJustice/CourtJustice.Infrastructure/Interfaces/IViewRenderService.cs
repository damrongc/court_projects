namespace CourtJustice.Infrastructure.Interfaces
{
    public interface IViewRenderService
    {
        Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model);
    }
}
