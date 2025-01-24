using System.ComponentModel.DataAnnotations;

namespace keepr.Models;

public class Keep : RepoItem<int>
{
  [MaxLength(255)] public string Name { get; set; }
  [MaxLength(100)] public string Description { get; set; }
  [Url, MaxLength(1000)] public string Img { get; set; }
  public int Views { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
}