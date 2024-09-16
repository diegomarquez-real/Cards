CREATE TABLE [yugioh].[Species](
	[SpeciesId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[UpdatedOn] [datetimeoffset](7) NULL,
 CONSTRAINT [Species_pk] PRIMARY KEY CLUSTERED 
(
	[SpeciesId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Species_Name] UNIQUE ([Name]) -- Unique constraint to prevent duplicate names
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[Species] ADD  DEFAULT (newid()) FOR [SpeciesId]
GO