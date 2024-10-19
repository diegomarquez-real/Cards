CREATE TABLE [yugioh].[EffectType](
	[EffectTypeId] [uniqueidentifier] NOT NULL,
	[Name] [nvarchar](25) NOT NULL,
	[CreatedOn] [datetimeoffset](7) NOT NULL,
	[UpdatedOn] [datetimeoffset](7) NULL,
 CONSTRAINT [EffectType_pk] PRIMARY KEY CLUSTERED 
(
	[EffectTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY],
CONSTRAINT [UQ_EffectType_Name] UNIQUE ([Name]) -- Unique constraint to prevent duplicate names
) ON [PRIMARY]
GO

ALTER TABLE [yugioh].[EffectType] ADD  DEFAULT (newid()) FOR [EffectTypeId]
GO