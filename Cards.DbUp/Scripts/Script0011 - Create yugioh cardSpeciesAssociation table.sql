CREATE TABLE [yugioh].[CardSpeciesAssociation](
	[CardSpeciesAssociationId] [uniqueidentifier] NOT NULL,
	[CardId] [uniqueidentifier] NOT NULL,
	[SpeciesId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [CardSpeciesAssociation_pk] PRIMARY KEY CLUSTERED 
(
	[CardSpeciesAssociationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UQ_CardId_SpeciesId] UNIQUE NONCLUSTERED 
(
	[CardId] ASC,
	[SpeciesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[CardSpeciesAssociation] ADD  DEFAULT (newid()) FOR [CardSpeciesAssociationId]
GO

ALTER TABLE [yugioh].[CardSpeciesAssociation]  WITH CHECK ADD  CONSTRAINT [CardSpeciesAssociation_Card_CardId_fk] FOREIGN KEY([CardId])
REFERENCES [yugioh].[Card] ([CardId])
GO

ALTER TABLE [yugioh].[CardSpeciesAssociation] CHECK CONSTRAINT [CardSpeciesAssociation_Card_CardId_fk]
GO

ALTER TABLE [yugioh].[CardSpeciesAssociation]  WITH CHECK ADD  CONSTRAINT [CardSpeciesAssociation_Species_SpeciesId_fk] FOREIGN KEY([SpeciesId])
REFERENCES [yugioh].[Species] ([SpeciesId])
GO

ALTER TABLE [yugioh].[CardSpeciesAssociation] CHECK CONSTRAINT [CardSpeciesAssociation_Species_SpeciesId_fk]
GO