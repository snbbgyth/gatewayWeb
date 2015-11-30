USE [GateWay]
GO

/****** Object:  Table [dbo].[Account]    Script Date: 2015/11/26 17:29:38 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE TABLE [dbo].[LogMessage](
	[Id] [uniqueidentifier] NOT NULL,
	[ClassType] [nvarchar](500) NULL,
	[MessageType] [nvarchar](200) NULL,
	[Text] [text] NULL,
	[CreateDateTime] [datetime] NULL,
	[MethodName] [nvarchar](200) NULL,
 CONSTRAINT [PK_LogMessage] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

CREATE TABLE [dbo].[Account](
	[Id] [uniqueidentifier] NOT NULL,
	[CreateDateTime] [datetime] NULL,
	[BirthDay] [datetime] NULL,
	[IsDelete] [bit] NULL,
	[Name] [nvarchar](200) NULL,
	[NickName] [nvarchar](200) NULL,
	[Password] [nvarchar](200) NULL,
	[Phone] [nvarchar](200) NULL,
	[Sex] [nvarchar](200) NULL,
	[Email] [nvarchar](200) NULL,
	[Salt] [nvarchar](200) NULL,
 CONSTRAINT [PK_Account] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

 CREATE TABLE [dbo].[EnglishReadArticle](
	[Id] [uniqueidentifier] NOT NULL,
	[PublishDateTime] [datetime] NULL,
	[ArticleTypeId] [uniqueidentifier] NULL,
	[Author] [nvarchar](200) NULL,
	[Content] [nvarchar](max) NULL,
	[CreateDateTime] [datetime] NULL,
	[From] [nvarchar](200) NULL,
	[FromUrl] [nvarchar](500) NULL,
	[IsDelete] [bit] NULL,
	[Summary] [nvarchar](500) NULL,
	[Title] [nvarchar](200) NULL,
 CONSTRAINT [PK_EnglishReadArticle] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


CREATE TABLE [dbo].[ArticleType](
	[Id] [uniqueidentifier] NOT NULL,
	[IsDelete] [bit] NULL,
	[CreateDateTime] [datetime] NULL,
	[Name] [nvarchar](200) NULL,
	[Type] [int] NULL,
 CONSTRAINT [PK_ArticleType] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO




