CREATE TABLE [yugioh].[Power](
	[PowerId] [uniqueidentifier] NOT NULL,
	[CardId] [uniqueidentifier] NOT NULL,
	[Level] [int] NOT NULL,
	[Attack] [int] NOT NULL,
	[Defense] [int] NOT NULL,
 CONSTRAINT [Power_pk] PRIMARY KEY CLUSTERED 
(
	[PowerId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_Power_CardId] UNIQUE ([CardId]) -- Unique constraint to prevent duplicate card ids
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[Power] ADD  DEFAULT (newid()) FOR [PowerId]
GO

ALTER TABLE [yugioh].[Power]  WITH CHECK ADD  CONSTRAINT [Power_Card_CardId_fk] FOREIGN KEY([CardId])
REFERENCES [yugioh].[Card] ([CardId])
ON UPDATE CASCADE
ON DELETE CASCADE
GO

ALTER TABLE [yugioh].[Power] CHECK CONSTRAINT [Power_Card_CardId_fk]
GO