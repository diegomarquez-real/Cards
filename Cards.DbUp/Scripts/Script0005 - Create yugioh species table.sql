CREATE TABLE [yugioh].[Species](
	[SpeciesId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[UpdatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [Species_pk] PRIMARY KEY CLUSTERED 
(
	[SpeciesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Species_Name] UNIQUE ([Name]) -- Unique constraint to prevent duplicate names
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[Species] ADD DEFAULT (newid()) FOR [SpeciesId]
GO

ALTER TABLE [yugioh].[Species] WITH CHECK ADD CONSTRAINT [Species_UserProfile_CreatedBy_fk] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO

ALTER TABLE [yugioh].[Species] CHECK CONSTRAINT [Species_UserProfile_CreatedBy_fk]
GO

ALTER TABLE [yugioh].[Species] WITH CHECK ADD CONSTRAINT [Species_UserProfile_UpdatedBy_fk] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO

ALTER TABLE [yugioh].[Species] CHECK CONSTRAINT [Species_UserProfile_UpdatedBy_fk]
GO