USE [ASDB]
GO

/****** Object:  Table [dbo].[AS]    Script Date: 3/1/2022 11:38:11 AM ******/

GO

/****** Object:  Table [dbo].[AS]    Script Date: 3/1/2022 11:38:11 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[AS](
	[View_name] [varchar](255) NOT NULL,
	[No_view] [int] NOT NULL,
	[Avg_duration] [int] NOT NULL,
	[Univ_name] [varchar](255) NOT NULL,
	[No_Ulogin] [int] NOT NULL,
	[Date] [date] NOT NULL,
	[No_login] [int] NOT NULL,
	[No_reg] [int] NOT NULL,
	[No_matched] [int] NOT NULL,
) ON [PRIMARY]
GO
Go

INSERT [dbo].[AS] ([View_name], [No_view], [Avg_duration], [Univ_name], [No_Ulogin], [Date], [No_login], [No_reg], [No_matched]) 
VALUES ('View1',100,5,'Univ1',3000,'4/1/2022',5000,120,2000),
('View2',170,6,'Univ2',1200,'4/2/2022',8000,230,1900),
('View3',152,7,'Univ3',2000,'4/3/2022',7800,310,1600),
('View4',340,2,'Univ4',2500,'4/4/2022',7500,120,2000),
('View5',280,1,'Univ5',1700,'4/5/2022',6900,230,1500),
('View6',250,4,'Univ6',1500,'4/6/2022',9000,310,2300),
('View7',60,3,'Univ7',1600,'4/7/2022',8800,310,3000);

Go
