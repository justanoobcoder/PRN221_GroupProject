USE [master]
GO

CREATE DATABASE [LaundryMiddlePlatform]
GO

USE [LaundryMiddlePlatform]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 08/10/2023 21:46:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Customer](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FullName] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Password] [varchar](100) NOT NULL,
	[Email] [varchar](30) NULL,
	[AvatarUrl] [varchar](200) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[IsBanned] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ItemType]    Script Date: 08/10/2023 21:46:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ItemType](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_ItemType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 08/10/2023 21:46:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [varchar](20) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
	[ServicePriceId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[FinishedAt] [datetime] NULL,
	[TakenAt] [datetime] NULL,
	[Weight] [decimal](18, 2) NOT NULL,
	[TotalPrice] [decimal](18, 2) NOT NULL,
	[Status] [varchar](20) NOT NULL,
	[Note] [nvarchar](200) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 08/10/2023 21:46:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[ItemTypeId] [int] NOT NULL,
	[StoreId] [int] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ServicePrice]    Script Date: 08/10/2023 21:46:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ServicePrice](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MinWeight] [decimal](18, 2) NOT NULL,
	[MaxWeight] [decimal](18, 2) NOT NULL,
	[Price] [decimal](18, 2) NULL,
	[PricePerUnit] [decimal](18, 2) NULL,
	[WashTimeInMinute] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
 CONSTRAINT [PK_ServicePrice] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 08/10/2023 21:46:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Email] [varchar](30) NULL,
	[Description] [nvarchar](200) NOT NULL,
	[LogoUrl] [varchar](200) NULL,
	[FacebookUrl] [varchar](200) NULL,
	[Password] [varchar](100) NOT NULL,
	[OwnerName] [nvarchar](100) NOT NULL,
	[OpenTime] [time](0) NOT NULL,
	[CloseTime] [time](0) NOT NULL,
	[IsOpened] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[IsBanned] [bit] NOT NULL,
	[DeletedAt] [datetime] NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[WashingMachine]    Script Date: 08/10/2023 21:46:04 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[WashingMachine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Brand] [nvarchar](100) NOT NULL,
	[Model] [nvarchar](100) NOT NULL,
	[MaxWeight] [decimal](18, 2) NOT NULL,
	[IsAvailable] [bit] NOT NULL,
	[StoreId] [int] NOT NULL,
 CONSTRAINT [PK_WashingMachine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[ItemType] ON 

INSERT [dbo].[ItemType] ([Id], [Name], [Description]) VALUES (1, N'Áo quần', N'Áo quần là những sản phẩm được may từ vải, dùng để che phủ cơ thể và bảo vệ sức khỏe.')
INSERT [dbo].[ItemType] ([Id], [Name], [Description]) VALUES (2, N'Gấu bông', N'Gấu bông là đồ chơi được làm từ vải và được nhồi bông.')
INSERT [dbo].[ItemType] ([Id], [Name], [Description]) VALUES (3, N'Chăn, ga, rèm', N'Chăn, ga, rèm là những sản phẩm được dùng để trang trí và bảo vệ giường, cửa sổ.')
SET IDENTITY_INSERT [dbo].[ItemType] OFF
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0)) FOR [IsBanned]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Store] ADD  DEFAULT ((0)) FOR [IsOpened]
GO
ALTER TABLE [dbo].[Store] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Store] ADD  DEFAULT ((0)) FOR [IsBanned]
GO
ALTER TABLE [dbo].[WashingMachine] ADD  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer_CustomerId]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_ServicePrice_ServicePriceId] FOREIGN KEY([ServicePriceId])
REFERENCES [dbo].[ServicePrice] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_ServicePrice_ServicePriceId]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Store_StoreId]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_ItemType_ItemTypeId] FOREIGN KEY([ItemTypeId])
REFERENCES [dbo].[ItemType] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_ItemType_ItemTypeId]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Store_StoreId]
GO
ALTER TABLE [dbo].[ServicePrice]  WITH CHECK ADD  CONSTRAINT [FK_ServicePrice_Service_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ServicePrice] CHECK CONSTRAINT [FK_ServicePrice_Service_ServiceId]
GO
ALTER TABLE [dbo].[WashingMachine]  WITH CHECK ADD  CONSTRAINT [FK_WashingMachine_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[WashingMachine] CHECK CONSTRAINT [FK_WashingMachine_Store_StoreId]
GO
