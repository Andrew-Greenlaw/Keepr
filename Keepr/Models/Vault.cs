namespace Keepr.Models;

using System.ComponentModel.DataAnnotations;
using Keepr.Interfaces;
public class Vault : IHasCreator
{
  public int Id { get; set; }
  public DateTime CreatedAt { get; set; }
  public DateTime UpdatedAt { get; set; }
  public string CreatorId { get; set; }
  public Profile Creator { get; set; }
  [Required]
  public string Name { get; set; }
  public string Description { get; set; }
  public string Img { get; set; }
  public bool IsPrivate { get; set; } = false;
}