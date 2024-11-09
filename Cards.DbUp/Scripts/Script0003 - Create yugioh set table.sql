CREATE TABLE [yugioh].[Set](
	[SetId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ReleaseDate] [date] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[UpdatedOn] [datetimeoffset](7) NULL,
	[CreatedBy] [uniqueidentifier] NOT NULL,
	[UpdatedBy] [uniqueidentifier] NULL,
 CONSTRAINT [Set_pk] PRIMARY KEY CLUSTERED 
(
	[SetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Set_Name] UNIQUE ([Name]) -- Unique constraint to prevent duplicate names
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[Set] ADD DEFAULT (newid()) FOR [SetId]
GO

ALTER TABLE [yugioh].[Set] WITH CHECK ADD CONSTRAINT [Set_UserProfile_CreatedBy_fk] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO

ALTER TABLE [yugioh].[Set] CHECK CONSTRAINT [Set_UserProfile_CreatedBy_fk]
GO

ALTER TABLE [yugioh].[Set] WITH CHECK ADD CONSTRAINT [Set_UserProfile_UpdatedBy_fk] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[UserProfile] ([UserProfileId])
GO

ALTER TABLE [yugioh].[Set] CHECK CONSTRAINT [Set_UserProfile_UpdatedBy_fk]
GO