CREATE TABLE [yugioh].[Attribute](
	[AttributeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[UpdatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [Attribute_pk] PRIMARY KEY CLUSTERED 
(
	[AttributeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Attribute_Name] UNIQUE ([Name]) -- Unique constraint to prevent duplicate names
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[Attribute] ADD DEFAULT (newid()) FOR [AttributeId]
GO

ALTER TABLE [yugioh].[Attribute] WITH CHECK ADD CONSTRAINT [Attribute_UserProfile_CreatedBy_fk] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO

ALTER TABLE [yugioh].[Attribute] CHECK CONSTRAINT [Attribute_UserProfile_CreatedBy_fk]
GO

ALTER TABLE [yugioh].[Attribute] WITH CHECK ADD CONSTRAINT [Attribute_UserProfile_UpdatedBy_fk] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO

ALTER TABLE [yugioh].[Attribute] CHECK CONSTRAINT [Attribute_UserProfile_UpdatedBy_fk]
GO