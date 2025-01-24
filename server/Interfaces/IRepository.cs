namespace keepr.Interfaces;

public interface IRepository<T>
{
  public T GetById(int id);
  public List<T> GetAll(int id);
  public T Create(T rawData);
  public void Update(int id);
  public void Delete(int id);
}