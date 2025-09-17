using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Models;

[Table("UsersTg")]
public partial class UsersTg
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public string? Nickname { get; set; }

    [InverseProperty("Owner")]
    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
