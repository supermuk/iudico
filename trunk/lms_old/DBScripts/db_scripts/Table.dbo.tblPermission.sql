SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[tblPermission]') AND type in (N'U'))
BEGIN
CREATE TABLE [tblPermission](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentPermitionRef] [int] NULL,
	[DateFrom] [datetime] NULL,
	[DateTo] [datetime] NULL,
	[CanBeDelagated] [bit] NOT NULL,
	[SampleBusinessObjectRef] [int] NULL,
	[SampleBusinessObjectOperationRef] [int] NULL,
UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[tblPermission]'))
ALTER TABLE [tblPermission]  WITH CHECK ADD  CONSTRAINT [FK_PARENT_PERMITION] FOREIGN KEY([ParentPermitionRef])
REFERENCES [dbo].[tblPermission] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SAMPLE_BUSINESS_OBJECT]') AND parent_object_id = OBJECT_ID(N'[tblPermission]'))
ALTER TABLE [tblPermission]  WITH CHECK ADD  CONSTRAINT [FK_SAMPLE_BUSINESS_OBJECT] FOREIGN KEY([SampleBusinessObjectRef])
REFERENCES [dbo].[tblSampleBusinesObject] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[FK_SAMPLE_BUSINESS_OBJECT_OPERATION]') AND parent_object_id = OBJECT_ID(N'[tblPermission]'))
ALTER TABLE [tblPermission]  WITH CHECK ADD  CONSTRAINT [FK_SAMPLE_BUSINESS_OBJECT_OPERATION] FOREIGN KEY([SampleBusinessObjectOperationRef])
REFERENCES [dbo].[fxSampleBusinesObjectOperation] ([ID])
GO
IF NOT EXISTS (SELECT * FROM sys.check_constraints WHERE object_id = OBJECT_ID(N'[dbo].[FK_SAMPLE_BUSINESS_OBJECT_SUFFICIENT]') AND parent_object_id = OBJECT_ID(N'[tblPermission]'))
ALTER TABLE [tblPermission]  WITH CHECK ADD  CONSTRAINT [FK_SAMPLE_BUSINESS_OBJECT_SUFFICIENT] CHECK  (([SampleBusinessObjectRef] IS NULL AND [SampleBusinessObjectOperationRef] IS NULL OR [SampleBusinessObjectRef] IS NOT NULL AND [SampleBusinessObjectOperationRef] IS NOT NULL))
GO
