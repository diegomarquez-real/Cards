CREATE TABLE [yugioh].[Set](
	[SetId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ReleaseDate] [date] NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[UpdatedOn] [datetimeoffset](7) NULL,
 CONSTRAINT [Set_pk] PRIMARY KEY CLUSTERED 
(
	[SetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Set_Name] UNIQUE ([Name]) -- Unique constraint to prevent duplicate names
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[Set] ADD  DEFAULT (newid()) FOR [SetId]
GO