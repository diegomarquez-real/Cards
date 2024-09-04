CREATE TABLE [yugioh].[Attribute](
	[AttributeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
 CONSTRAINT [Attribute_pk] PRIMARY KEY CLUSTERED 
(
	[AttributeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Attribute_Name] UNIQUE ([Name]) -- Unique constraint to prevent duplicate names
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[Attribute] ADD  DEFAULT (newid()) FOR [AttributeId]
GO