USE [master]
GO

CREATE DATABASE [LaundryMiddlePlatform]
GO

USE [LaundryMiddlePlatform]
GO
/****** Object:  Table [dbo].[Customer]    Script Date: 18/10/2023 09:26:07 ******/
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
	[Email] [varchar](30) NOT NULL,
	[AvatarUrl] [varchar](200) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[IsBanned] [bit] NOT NULL,
 CONSTRAINT [PK_Customer] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Machine]    Script Date: 18/10/2023 09:26:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Machine](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NULL,
	[Brand] [nvarchar](100) NOT NULL,
	[Model] [nvarchar](100) NOT NULL,
	[Capacity] [real] NOT NULL,
	[IsAvailable] [bit] NOT NULL,
	[WashTimeInMinute] [real] NOT NULL,
	[StoreId] [int] NOT NULL,
 CONSTRAINT [PK_Machine] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MachineOrderAssignment]    Script Date: 18/10/2023 09:26:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MachineOrderAssignment](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MachineId] [int] NOT NULL,
	[OrderId] [varchar](20) NOT NULL,
	[AssignedStartAt] [datetime] NOT NULL,
	[AssignedEndAt] [datetime] NOT NULL,
 CONSTRAINT [PK_MachineOrderAssignment] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Order]    Script Date: 18/10/2023 09:26:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Order](
	[Id] [varchar](20) NOT NULL,
	[CustomerId] [int] NOT NULL,
	[ServiceId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[PickUpAt] [datetime] NULL,
	[DropOffAt] [datetime] NULL,
	[Weight] [real] NOT NULL,
	[TotalCost] [decimal](18, 2) NOT NULL,
	[Status] [int] NOT NULL,
	[Note] [nvarchar](200) NULL,
 CONSTRAINT [PK_Order] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Service]    Script Date: 18/10/2023 09:26:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Service](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[PricePerKg] [decimal](18, 2) NOT NULL,
	[ServiceTimeInHour] [real] NOT NULL,
	[StoreId] [int] NOT NULL,
 CONSTRAINT [PK_Service] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Store]    Script Date: 18/10/2023 09:26:08 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Store](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](100) NOT NULL,
	[Address] [nvarchar](200) NOT NULL,
	[Phone] [varchar](20) NOT NULL,
	[Email] [varchar](30) NOT NULL,
	[Description] [nvarchar](200) NOT NULL,
	[AvatarUrl] [varchar](200) NULL,
	[CoverUrl] [varchar](200) NULL,
	[FacebookUrl] [varchar](200) NULL,
	[Password] [varchar](100) NOT NULL,
	[OpenTime] [time](0) NOT NULL,
	[CloseTime] [time](0) NOT NULL,
	[IsOpening] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[IsBanned] [bit] NOT NULL,
 CONSTRAINT [PK_Store] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Customer] ADD  DEFAULT ((0)) FOR [IsBanned]
GO
ALTER TABLE [dbo].[Machine] ADD  DEFAULT ((1)) FOR [IsAvailable]
GO
ALTER TABLE [dbo].[Order] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Store] ADD  DEFAULT ((0)) FOR [IsOpening]
GO
ALTER TABLE [dbo].[Store] ADD  DEFAULT (getdate()) FOR [CreatedAt]
GO
ALTER TABLE [dbo].[Store] ADD  DEFAULT ((0)) FOR [IsBanned]
GO
ALTER TABLE [dbo].[Machine]  WITH CHECK ADD  CONSTRAINT [FK_Machine_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Machine] CHECK CONSTRAINT [FK_Machine_Store_StoreId]
GO
ALTER TABLE [dbo].[MachineOrderAssignment]  WITH CHECK ADD  CONSTRAINT [FK_MachineOrderAssignment_Machine_MachineId] FOREIGN KEY([MachineId])
REFERENCES [dbo].[Machine] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[MachineOrderAssignment] CHECK CONSTRAINT [FK_MachineOrderAssignment_Machine_MachineId]
GO
ALTER TABLE [dbo].[MachineOrderAssignment]  WITH CHECK ADD  CONSTRAINT [FK_MachineOrderAssignment_Order_OrderId] FOREIGN KEY([OrderId])
REFERENCES [dbo].[Order] ([Id])
GO
ALTER TABLE [dbo].[MachineOrderAssignment] CHECK CONSTRAINT [FK_MachineOrderAssignment_Order_OrderId]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Customer_CustomerId] FOREIGN KEY([CustomerId])
REFERENCES [dbo].[Customer] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Customer_CustomerId]
GO
ALTER TABLE [dbo].[Order]  WITH CHECK ADD  CONSTRAINT [FK_Order_Service_ServiceId] FOREIGN KEY([ServiceId])
REFERENCES [dbo].[Service] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Order] CHECK CONSTRAINT [FK_Order_Service_ServiceId]
GO
ALTER TABLE [dbo].[Service]  WITH CHECK ADD  CONSTRAINT [FK_Service_Store_StoreId] FOREIGN KEY([StoreId])
REFERENCES [dbo].[Store] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Service] CHECK CONSTRAINT [FK_Service_Store_StoreId]
GO
