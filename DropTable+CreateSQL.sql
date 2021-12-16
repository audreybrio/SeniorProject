USE [Users]
GO

/****** Object:  Table [dbo].[UserTable]    Script Date: 12/14/2021 8:45:14 PM ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserTable]') AND type in (N'U'))
DROP TABLE [dbo].[UserTable]
GO

/****** Object:  Table [dbo].[UserTable]    Script Date: 12/14/2021 8:45:14 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[UserTable](
	[id] [int] NOT NULL,
	[name] [varchar](50) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[password] [varchar](50) NOT NULL,
	[email] [varchar](50) NOT NULL,
	[passcode] [varchar](50) NOT NULL,
	[role] [varchar](50) NOT NULL,
	[active_status] [bit] NOT NULL
) ON [PRIMARY]
GO


