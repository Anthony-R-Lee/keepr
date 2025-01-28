namespace keepr.Services;

public class KeepsService
{

  public KeepsService(KeepsRepository repository)
  {
    _repository = repository;
  }
  private readonly KeepsRepository _repository;

  internal Keep CreateKeep(Keep keepData)
  {
    Keep keep = _repository.Create(keepData);
    return keep;
  }

  internal List<Keep> GetAllKeeps()
  {
    List<Keep> keeps = _repository.GetAll();
    return keeps;
  }

  internal Keep GetKeepById(int keepId)
  {
    Keep keep = _repository.GetById(keepId);

    if (keep == null) throw new Exception($"Invalid keep id: {keepId}");

    return keep;
  }

  internal Keep GetKeepById(int keepId, string userId)
  {
    Keep keep = _repository.GetById(keepId);

    if (keep == null) throw new Exception($"Invalid keep id: {keepId}");

    return keep;
  }

  internal Keep UpdateKeep(int keepId, string userId, Keep updateData)
  {
    Keep keep = GetKeepById(keepId);

    if (keep.CreatorId != userId) throw new Exception("This is not your keep to edit");

    keep.Name = updateData.Name ?? keep.Name;
    keep.Description = updateData.Description ?? keep.Description;

    _repository.Update(keep);

    return keep;
  }

  internal string DeleteKeep(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId);

    if (keep.CreatorId != userId) throw new Exception("This is not your keep to delete");

    _repository.Delete(keepId);

    return $"Deleted {keep.Name}";
  }

  internal Keep IncrementVisits(int keepId, string userId)
  {
    Keep keep = GetKeepById(keepId, userId);
    if (keep.CreatorId != userId)
    {
      keep.Views++;

      _repository.IncrementVisits(keep);
    }
    return keep;
  }
}