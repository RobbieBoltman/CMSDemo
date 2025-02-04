USE [CMSDemo_2]
GO

/****** Object:  Table [dbo].[StockItems]    Script Date: 2025/02/04 22:03:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[StockItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[RegNo] [varchar](10) NOT NULL,
	[Make] [varchar](255) NOT NULL,
	[Model] [varchar](255) NOT NULL,
	[ModelYear] [int] NOT NULL,
	[KMS] [int] NOT NULL,
	[Colour] [varchar](16) NOT NULL,
	[VIN] [char](17) NOT NULL,
	[RetailPrice] [decimal](18, 0) NOT NULL,
	[CostPrice] [decimal](18, 0) NOT NULL,
	[DTCreated] [datetime] NOT NULL,
	[DTUpdated] [datetime] NOT NULL,
 CONSTRAINT [PK_StockItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

CREATE TABLE [dbo].[StockAccessories](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](255) NOT NULL,
	[StockItemId] [int] NOT NULL,
 CONSTRAINT [PK_StockAccessories] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[StockAccessories]  WITH CHECK ADD  CONSTRAINT [FK_StockAccessories_StockAccessories] FOREIGN KEY([StockItemId])
REFERENCES [dbo].[StockItems] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[StockAccessories] CHECK CONSTRAINT [FK_StockAccessories_StockAccessories]
GO


CREATE TABLE [dbo].[Images](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](255) NOT NULL,
	[ImageBinary] [varbinary](max) NOT NULL,
	[StockItemId] [int] NOT NULL,
 CONSTRAINT [PK_Images] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Images]  WITH CHECK ADD  CONSTRAINT [FK_Images_StockItems] FOREIGN KEY([StockItemId])
REFERENCES [dbo].[StockItems] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[Images] CHECK CONSTRAINT [FK_Images_StockItems]
GO


