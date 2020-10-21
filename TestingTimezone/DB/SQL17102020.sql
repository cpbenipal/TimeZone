USE [DbTestTime]
GO
/****** Object:  Table [dbo].[tblScheduleTest]    Script Date: 16-10-2020 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblScheduleTest](
	[ScheduleId] [int] IDENTITY(1,1) NOT NULL,
	[TopicId] [int] NULL,
	[StartDateTime] [datetime] NULL,
	[Duration] [nvarchar](6) NULL,
	[TeacherTZ] [nvarchar](50) NULL,
	[CreatedOn] [datetime] NULL,
 CONSTRAINT [PK_tblScheduleTest] PRIMARY KEY CLUSTERED 
(
	[ScheduleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblTopic]    Script Date: 16-10-2020 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblTopic](
	[TopicId] [int] IDENTITY(1,1) NOT NULL,
	[Topic] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblTopic] PRIMARY KEY CLUSTERED 
(
	[TopicId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblTopic] ON 
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (1, N'Testing TZ')
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (2, N'Math Test')
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (3, N'English Vocab Test')
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (4, N'Nutrition Test')
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (5, N'Skill Test')
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (6, N'C# Test')
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (7, N'VB Test')
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (8, N'.Net Framework Test')
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (9, N'Fun Test')
GO
INSERT [dbo].[tblTopic] ([TopicId], [Topic]) VALUES (10, N'Blal Blah Test')
GO
SET IDENTITY_INSERT [dbo].[tblTopic] OFF
GO
ALTER TABLE [dbo].[tblScheduleTest] ADD  CONSTRAINT [DF_tblScheduleTest_CreatedOn]  DEFAULT (getutcdate()) FOR [CreatedOn]
GO
ALTER TABLE [dbo].[tblScheduleTest]  WITH CHECK ADD  CONSTRAINT [FK_tblScheduleTest_tblTopic] FOREIGN KEY([TopicId])
REFERENCES [dbo].[tblTopic] ([TopicId])
GO
ALTER TABLE [dbo].[tblScheduleTest] CHECK CONSTRAINT [FK_tblScheduleTest_tblTopic]
GO
/****** Object:  StoredProcedure [dbo].[spCreateTest]    Script Date: 16-10-2020 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spCreateTest] 
@TopicId int, 
@StartDateTime Datetime, 
@Duration nvarchar(6), 
@TeacherTZ nvarchar(50)	 
AS
BEGIN 
	BEGIN TRY 
		BEGIN TRAN 
		If not exists(select top 1 1 from tblScheduleTest where TopicId =@TopicId)
		Begin
				INSERT INTO tblScheduleTest
				(TopicId, StartDateTime, Duration, TeacherTZ)
				VALUES   (@TopicId, @StartDateTime, @Duration, @TeacherTZ)
				SELECT 1
		 End
		 Else
				SELECT 0
		 COMMIT TRAN
	END TRY  
	BEGIN CATCH  
		IF @@TRANCOUNT > 0 
			ROLLBACK TRAN
		    PRINT 'ROLLBACK'
			DECLARE  @ERRMSG NVARCHAR(1000) = ERROR_MESSAGE() + 
			', Error on Line- ' + CONVERT(NVARCHAR, ERROR_LINE())  
			SELECT @ERRMSG    			
		RETURN  
	END CATCH 
END
GO
/****** Object:  StoredProcedure [dbo].[spGetAllTopics]    Script Date: 16-10-2020 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spGetAllTopics] 
	-- Add the parameters for the stored procedure here
	 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT   TopicId, Topic FROM tblTopic

END
GO
/****** Object:  StoredProcedure [dbo].[spGetUpcomingTest]    Script Date: 16-10-2020 23:47:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Beni
-- Create date: 13/10/2020
-- Description:	Get all upcoming test
-- =============================================
CREATE PROCEDURE [dbo].[spGetUpcomingTest] 
 	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	  
	Select T.TopicId, T.Topic, S.StartDateTime, S.Duration from tblScheduleTest S
	inner join tblTopic T on S.TopicId = T.TopicId order by S.StartDateTime desc 
END
GO
