
// This file was automatically generated by the PetaPoco T4 Template
// Do not make changes directly to this file - edit the template instead
// 
// The following connection settings were used to generate this file
// 
//     Connection String Name: ``
//     Provider:               `System.Data.SqlClient`
//     Connection String:      `Server=(local);Database=Cards;Trusted_Connection=True;MultipleActiveResultSets=true`
//     Schema:                 `yugioh`
//     Include Views:          `True`

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using PetaPoco;

namespace Cards.Data.Models.Yugioh
{
	

    
	[TableName("[yugioh].[Attribute]")]
	[PrimaryKey("AttributeId")]
	[ExplicitColumns]
    public partial class Attribute  
    {
		[Key]
		[Column] public Guid AttributeId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public DateTimeOffset CreatedOn { get; set; }
		[Column] public DateTimeOffset? UpdatedOn { get; set; }
		[Column] public Guid CreatedBy { get; set; }
		[Column] public Guid? UpdatedBy { get; set; }
	}
    
	[TableName("[yugioh].[Card]")]
	[PrimaryKey("CardId")]
	[ExplicitColumns]
    public partial class Card  
    {
		[Key]
		[Column] public Guid CardId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public string Description { get; set; }
		[Column] public Guid AttributeId { get; set; }
		[Column] public DateTimeOffset CreatedOn { get; set; }
		[Column] public DateTimeOffset? UpdatedOn { get; set; }
		[Column] public Guid CreatedBy { get; set; }
		[Column] public Guid? UpdatedBy { get; set; }
	}
    
	[TableName("[yugioh].[CardEffectTypeAssociation]")]
	[PrimaryKey("CardEffectTypeAssociationId")]
	[ExplicitColumns]
    public partial class CardEffectTypeAssociation  
    {
		[Key]
		[Column] public Guid CardEffectTypeAssociationId { get; set; }
		[Column] public Guid CardId { get; set; }
		[Column] public Guid EffectTypeId { get; set; }
	}
    
	[TableName("[yugioh].[CardSetAssociation]")]
	[PrimaryKey("CardSetAssociationId")]
	[ExplicitColumns]
    public partial class CardSetAssociation  
    {
		[Key]
		[Column] public Guid CardSetAssociationId { get; set; }
		[Column] public Guid CardId { get; set; }
		[Column] public Guid SetId { get; set; }
	}
    
	[TableName("[yugioh].[CardSpeciesAssociation]")]
	[PrimaryKey("CardSpeciesAssociationId")]
	[ExplicitColumns]
    public partial class CardSpeciesAssociation  
    {
		[Key]
		[Column] public Guid CardSpeciesAssociationId { get; set; }
		[Column] public Guid CardId { get; set; }
		[Column] public Guid SpeciesId { get; set; }
	}
    
	[TableName("[yugioh].[EffectType]")]
	[PrimaryKey("EffectTypeId")]
	[ExplicitColumns]
    public partial class EffectType  
    {
		[Key]
		[Column] public Guid EffectTypeId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public DateTimeOffset CreatedOn { get; set; }
		[Column] public DateTimeOffset? UpdatedOn { get; set; }
		[Column] public Guid CreatedBy { get; set; }
		[Column] public Guid? UpdatedBy { get; set; }
	}
    
	[TableName("[yugioh].[Power]")]
	[PrimaryKey("PowerId")]
	[ExplicitColumns]
    public partial class Power  
    {
		[Key]
		[Column] public Guid PowerId { get; set; }
		[Column] public Guid CardId { get; set; }
		[Column] public int? Level { get; set; }
		[Column] public int? Rank { get; set; }
		[Column] public int? Link { get; set; }
		[Column] public int? PScale { get; set; }
		[Column] public int? Attack { get; set; }
		[Column] public int? Defense { get; set; }
		[Column] public DateTimeOffset CreatedOn { get; set; }
		[Column] public DateTimeOffset? UpdatedOn { get; set; }
		[Column] public Guid CreatedBy { get; set; }
		[Column] public Guid? UpdatedBy { get; set; }
	}
    
	[TableName("[yugioh].[Set]")]
	[PrimaryKey("SetId")]
	[ExplicitColumns]
    public partial class Set  
    {
		[Key]
		[Column] public Guid SetId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public DateTime ReleaseDate { get; set; }
		[Column] public DateTimeOffset CreatedOn { get; set; }
		[Column] public DateTimeOffset? UpdatedOn { get; set; }
		[Column] public Guid CreatedBy { get; set; }
		[Column] public Guid? UpdatedBy { get; set; }
	}
    
	[TableName("[yugioh].[Species]")]
	[PrimaryKey("SpeciesId")]
	[ExplicitColumns]
    public partial class Species  
    {
		[Key]
		[Column] public Guid SpeciesId { get; set; }
		[Column] public string Name { get; set; }
		[Column] public DateTimeOffset CreatedOn { get; set; }
		[Column] public DateTimeOffset? UpdatedOn { get; set; }
		[Column] public Guid CreatedBy { get; set; }
		[Column] public Guid? UpdatedBy { get; set; }
	}
}
