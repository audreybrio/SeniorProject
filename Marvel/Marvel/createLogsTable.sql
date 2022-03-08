USE [Marvel-dev]
GO

/****** Object:  Table [dbo].[logs]    Script Date: 15/12/2021 04:24:54 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[logs](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[timestamp] [datetime] NOT NULL,
	[category] [varchar](10) NOT NULL,
	[level] [varchar](10) NOT NULL,
	[username] [varchar](50) NOT NULL,
	[description] [varchar](512) NOT NULL,
 CONSTRAINT [PK_logs] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

