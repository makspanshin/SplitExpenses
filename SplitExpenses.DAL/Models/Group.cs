using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using SplitExpensesCalculation.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Models;

public partial class Group
{
    [Key]
    [Column("ID")]
    public int Id { get; set; }

    public string? Name { get; set; }

    [Column(TypeName = "jsonb")]
    public string? Members { get; set; }

    [Column(TypeName = "jsonb")]
    public string? Transactions { get; set; }

    [Column("OwnerID")]
    public int? OwnerId { get; set; }

    [ForeignKey("OwnerId")]
    [InverseProperty("Groups")]
    public virtual UsersTg? Owner { get; set; }

    public SplitExpensesCalculation.Models.Group ToDomainGroup()
    {
        var group = new SplitExpensesCalculation.Models.Group(this.Name);

        if (Members != null) group.Members = JsonConvert.DeserializeObject<List<Member>>(Members);

        if (Transactions != null) group.Transactions = JsonConvert.DeserializeObject<List<Transaction>>(Transactions);

        return group;
    }
}
