namespace keepr.Models;

public abstract class RepoItem<T>
{
  public T Id { get; set; }
  public DateTime createdAt { get; set; }
  public DateTime updatedAt { get; set; }
}