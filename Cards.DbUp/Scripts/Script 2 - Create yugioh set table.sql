CREATE TABLE [yugioh].[Set](
	[SetId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](255) NOT NULL,
	[ReleaseDate] [date] NOT NULL,
 CONSTRAINT [Set_pk] PRIMARY KEY CLUSTERED 
(
	[SetId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[Set] ADD  DEFAULT (newid()) FOR [SetId]
GO