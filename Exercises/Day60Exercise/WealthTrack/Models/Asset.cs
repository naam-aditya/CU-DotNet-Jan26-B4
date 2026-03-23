using System.ComponentModel.DataAnnotations;

namespace WealthTrack.Models;

public class Asset
{
    public int Id { get; set; }

    [Required]
    [Display(Name = "Asset Name")]
    public required string AssetName { get; set; }
    [Required]
    public double Price { get; set; }
}