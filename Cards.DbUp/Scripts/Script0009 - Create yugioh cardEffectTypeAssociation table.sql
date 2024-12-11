CREATE TABLE [yugioh].[CardEffectTypeAssociation](
	[CardEffectTypeAssociationId] [uniqueidentifier] NOT NULL,
	[CardId] [uniqueidentifier] NOT NULL,
	[EffectTypeId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [CardEffectTypeAssociation_pk] PRIMARY KEY CLUSTERED 
(
	[CardEffectTypeAssociationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_CardId_EffectTypeId] UNIQUE ([CardId], [EffectTypeId])  -- Unique constraint to prevent duplicates
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[CardEffectTypeAssociation] ADD  DEFAULT (newid()) FOR [CardEffectTypeAssociationId]
GO

ALTER TABLE [yugioh].[CardEffectTypeAssociation]  WITH CHECK ADD  CONSTRAINT [CardEffectTypeAssociation_Card_CardId_fk] FOREIGN KEY([CardId])
REFERENCES [yugioh].[Card] ([CardId])
GO

ALTER TABLE [yugioh].[CardEffectTypeAssociation] CHECK CONSTRAINT [CardEffectTypeAssociation_Card_CardId_fk]
GO

ALTER TABLE [yugioh].[CardEffectTypeAssociation]  WITH CHECK ADD  CONSTRAINT [CardEffectTypeAssociation_EffectType_EffectTypeId_fk] FOREIGN KEY([EffectTypeId])
REFERENCES [yugioh].[EffectType] ([EffectTypeId])
GO

ALTER TABLE [yugioh].[CardEffectTypeAssociation] CHECK CONSTRAINT [CardEffectTypeAssociation_EffectType_EffectTypeId_fk]
GO