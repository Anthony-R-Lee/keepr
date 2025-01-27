using System.ComponentModel.DataAnnotations;

namespace keepr.Models;

public class VaultKeep : RepoItem<int>
{
  public int KeepId { get; set; }
  public int VaultId { get; set; }
  [MaxLength(255)] public string CreatorId { get; set; }
  // public Profile Creator { get; set; }

}