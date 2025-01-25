using System.ComponentModel.DataAnnotations;

namespace keepr.Models;

public class Kept : RepoItem<int>
{
  public int VaultKeepId { get; set; }
  [MaxLength(255)] public string Name { get; set; }
  [MaxLength(1000)] public string Description { get; set; }
  [MaxLength(1000)] public string Img { get; set; }
  public int Views { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
  public Profile Creator { get; set; }
}