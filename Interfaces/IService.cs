namespace CodigoDoFuturoApi.Interfaces
{
    public interface IService
    {
        Task<List<Object>> TodosAsync();
        Task AdicionarAsync(Object obj);

        Task<Object> AtualizarAsync(Object obj);

        Task ApagarAsync(Object obj);

    }
}
