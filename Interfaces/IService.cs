namespace CodigoDoFuturoApi.Interfaces
{
    public interface IService<T>
    {
        Task<List<T>> TodosAsync();
        Task AdicionarAsync(T obj);

        Task<T> AtualizarAsync(T obj);

        Task ApagarAsync(T obj);

    }
}
