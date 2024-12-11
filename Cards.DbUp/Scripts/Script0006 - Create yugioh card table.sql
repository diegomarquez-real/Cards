CREATE TABLE [yugioh].[Card](
	[CardId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[Description] [nvarchar](max) NOT NULL,
	[AttributeId] [uniqueidentifier] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[UpdatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [Card_pk] PRIMARY KEY CLUSTERED 
(
	[CardId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Card_Name] UNIQUE ([Name]) -- Unique constraint to prevent duplicate names
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[Card] ADD DEFAULT (newid()) FOR [CardId]
GO

ALTER TABLE [yugioh].[Card] WITH CHECK ADD CONSTRAINT [Card_Attribute_AttributeId_fk] FOREIGN KEY([AttributeId])
REFERENCES [yugioh].[Attribute] ([AttributeId])
GO

ALTER TABLE [yugioh].[Card] CHECK CONSTRAINT [Card_Attribute_AttributeId_fk]
GO

ALTER TABLE [yugioh].[Card] WITH CHECK ADD CONSTRAINT [Card_UserProfile_CreatedBy_fk] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO

ALTER TABLE [yugioh].[Card] CHECK CONSTRAINT [Card_UserProfile_CreatedBy_fk]
GO

ALTER TABLE [yugioh].[Card] WITH CHECK ADD CONSTRAINT [Card_UserProfile_UpdatedBy_fk] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO

ALTER TABLE [yugioh].[Card] CHECK CONSTRAINT [Card_UserProfile_UpdatedBy_fk]
GO