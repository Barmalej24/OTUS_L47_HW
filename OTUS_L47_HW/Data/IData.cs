namespace OTUS_L47_HW.Data
{
    public interface IData<T>
    {
        Task Delete(int id);
        Task<T?> Get(int id);
        Task<List<T>> GetAll();
        Task Add(T obj);
        Task Update(T obj);
    }
}
