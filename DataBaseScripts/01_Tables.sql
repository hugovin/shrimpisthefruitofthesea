USE [PublicPortal]
GO

/****** Object:  Table [dbo].[Torch_subjects]    Script Date: 10/15/2011 17:21:03 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Torch_subjects](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](500) NULL,
 CONSTRAINT [PK_torch_subjects] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



USE [PublicPortal]
GO

/****** Object:  Table [dbo].[Torch_grades]    Script Date: 10/15/2011 17:21:54 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Torch_grades](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[description] [varchar](50) NULL,
 CONSTRAINT [PK_torch_grades] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO



USE [PublicPortal]
GO

/****** Object:  Table [dbo].[Torch_sku]    Script Date: 10/15/2011 17:22:19 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[Torch_sku](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[subjectid] [int] NULL,
	[gradeid] [int] NULL,
	[price] [decimal](18, 2) NULL,
	[SKU] [varchar](50) NULL,
 CONSTRAINT [PK_torch_sku] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

USE [PublicPortal]
GO

/****** Object:  Table [dbo].[torch_configurations]    Script Date: 10/15/2011 17:22:34 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[torch_configurations](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[cartid] [int] NULL,
	[state] [varchar](50) NULL,
	[sku] [varchar](50) NULL,
	[spanish] [int] NULL,
	[whiteboard] [varchar](50) NULL,
	[repsystem] [varchar](50) NULL,
	[description] [varchar](2000) NULL,
	[datecreated] [datetime] NULL,
 CONSTRAINT [PK_torch_configurations] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[torch_configurations] ADD  CONSTRAINT [DF_torch_configurations_datecreated]  DEFAULT (getdate()) FOR [datecreated]
GO




  INSERT INTO [PublicPortal].[dbo].[Torch_grades]([description])VALUES('SingleGrade')
GO
  INSERT INTO [PublicPortal].[dbo].[Torch_grades]([description])VALUES('4-8 Grade')
GO

  INSERT INTO [PublicPortal].[dbo].[Torch_subjects]([description])VALUES('Single')
GO
  INSERT INTO [PublicPortal].[dbo].[Torch_subjects]([description])VALUES('All')
GO
  INSERT INTO [PublicPortal].[dbo].[Torch_subjects]([description])VALUES('SingleELL')
GO
  INSERT INTO [PublicPortal].[dbo].[Torch_subjects]([description])VALUES('AllELL')
GO



INSERT INTO [PublicPortal].[dbo].[Torch_sku]([subjectid],[gradeid],[price],[SKU])VALUES(1,1,299.95,'4574518')
GO
INSERT INTO [PublicPortal].[dbo].[Torch_sku]([subjectid],[gradeid],[price],[SKU])VALUES(3,1,549.95,'4574519')
GO
INSERT INTO [PublicPortal].[dbo].[Torch_sku]([subjectid],[gradeid],[price],[SKU])VALUES(1,2,549.95,'4574520')
GO
INSERT INTO [PublicPortal].[dbo].[Torch_sku]([subjectid],[gradeid],[price],[SKU])VALUES(3,2,799.95,'4574521')
GO
INSERT INTO [PublicPortal].[dbo].[Torch_sku]([subjectid],[gradeid],[price],[SKU])VALUES(2,2,1249.95,'4574522')
GO
INSERT INTO [PublicPortal].[dbo].[Torch_sku]([subjectid],[gradeid],[price],[SKU])VALUES(4,2,1499.95,'4574523')
GO

