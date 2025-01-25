
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Entities;

public class CostumerEntity
{
    [Key]
    public int Id { get; set; }

    [Required]
    [Column(TypeName ="nvarchar(50)")]
    public string CostumerName { get; set; } = null!;

    public ICollection<ProjectEntity> Projects { get; set; } = [];
}

