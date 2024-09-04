CREATE TABLE [yugioh].[CardSetAssociation](
	[CardSetAssociationId] [uniqueidentifier] NOT NULL,
	[CardId] [uniqueidentifier] NOT NULL,
	[SetId] [uniqueidentifier] NOT NULL,
 CONSTRAINT [CardSetAssociation_pk] PRIMARY KEY CLUSTERED 
(
	[CardSetAssociationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_CardId_SetId] UNIQUE ([CardId], [SetId])  -- Unique constraint to prevent duplicates
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[CardSetAssociation] ADD  DEFAULT (newid()) FOR [CardSetAssociationId]
GO

ALTER TABLE [yugioh].[CardSetAssociation]  WITH CHECK ADD  CONSTRAINT [CardSetAssociation_Card_CardId_fk] FOREIGN KEY([CardId])
REFERENCES [yugioh].[Card] ([CardId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [yugioh].[CardSetAssociation] CHECK CONSTRAINT [CardSetAssociation_Card_CardId_fk]
GO

ALTER TABLE [yugioh].[CardSetAssociation]  WITH CHECK ADD  CONSTRAINT [CardSetAssociation_Set_SetId_fk] FOREIGN KEY([SetId])
REFERENCES [yugioh].[Set] ([SetId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [yugioh].[CardSetAssociation] CHECK CONSTRAINT [CardSetAssociation_Set_SetId_fk]
GO