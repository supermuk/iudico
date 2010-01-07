/****** Object:  ForeignKey [FK_relResourcesDependency_tblResources_Dependant]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesDependency_tblResources_Dependant]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]'))
ALTER TABLE [dbo].[relResourcesDependency] DROP CONSTRAINT [FK_relResourcesDependency_tblResources_Dependant]
GO
/****** Object:  ForeignKey [FK_relResourcesDependency_tblResources_Dependency]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesDependency_tblResources_Dependency]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]'))
ALTER TABLE [dbo].[relResourcesDependency] DROP CONSTRAINT [FK_relResourcesDependency_tblResources_Dependency]
GO
/****** Object:  ForeignKey [FK_relResourcesFiles_tblFiles]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesFiles_tblFiles]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]'))
ALTER TABLE [dbo].[relResourcesFiles] DROP CONSTRAINT [FK_relResourcesFiles_tblFiles]
GO
/****** Object:  ForeignKey [FK_relResourcesFiles_tblResources]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesFiles_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]'))
ALTER TABLE [dbo].[relResourcesFiles] DROP CONSTRAINT [FK_relResourcesFiles_tblResources]
GO
/****** Object:  ForeignKey [FK_GROUP]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [FK_GROUP]
GO
/****** Object:  ForeignKey [FK_USER]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [FK_USER]
GO
/****** Object:  ForeignKey [FK_ROLE_ID]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [FK_ROLE_ID]
GO
/****** Object:  ForeignKey [FK_USER_ID]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [FK_USER_ID]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_fxdCompiledStatuses]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblCompiledQuestionsData]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblUserAnswers]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestions_fxdLanguages]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions] DROP CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestionsData_tblCompiledQuestions]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData] DROP CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblItems_tblItems]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblItems]
GO
/****** Object:  ForeignKey [FK_tblItems_tblOrganizations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblOrganizations]
GO
/****** Object:  ForeignKey [FK_tblItems_tblResources]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblResources]
GO
/****** Object:  ForeignKey [FK_tblOrganizations_tblCourses]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations] DROP CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
/****** Object:  ForeignKey [FK_Page_PageType]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages] DROP CONSTRAINT [FK_Page_PageType]
GO
/****** Object:  ForeignKey [FK_PARENT_PERMITION]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_PARENT_PERMITION]
GO
/****** Object:  ForeignKey [FK_Permissions_CourseOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_CourseOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Courses]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Courses]
GO
/****** Object:  ForeignKey [FK_Permissions_CurriculumOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Curriculums]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Curriculums]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupObjects]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_GroupObjects]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_GroupOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Groups]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Groups]
GO
/****** Object:  ForeignKey [FK_Permissions_Organizations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Organizations]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerGroup]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_OwnerGroup]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerUser]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_OwnerUser]
GO
/****** Object:  ForeignKey [FK_Permissions_PageOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_PageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Pages]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Pages]
GO
/****** Object:  ForeignKey [FK_Permissions_StageOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_StageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Stages]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Stages]
GO
/****** Object:  ForeignKey [FK_Permissions_ThemeOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_ThemeOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Themes]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Themes]
GO
/****** Object:  ForeignKey [FK_Permissions_UserObjects]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_UserObjects]
GO
/****** Object:  ForeignKey [FK_CorrectAnswer_Page]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [FK_CorrectAnswer_Page]
GO
/****** Object:  ForeignKey [FK_tblQuestions_tblCompiledQuestions]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblResources_tblCourses]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources] DROP CONSTRAINT [FK_tblResources_tblCourses]
GO
/****** Object:  ForeignKey [FK_Curriculums_Stages]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages] DROP CONSTRAINT [FK_Curriculums_Stages]
GO
/****** Object:  ForeignKey [FK_Organizations_Themes]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Organizations_Themes]
GO
/****** Object:  ForeignKey [FK_Stages_Themes]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Stages_Themes]
GO
/****** Object:  ForeignKey [FK_Themes_Course]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Themes_Course]
GO
/****** Object:  ForeignKey [FK_tblUserAnswers_AnswerTypeRef]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
/****** Object:  ForeignKey [FK_UserAnswer_CorrectAnswer]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
/****** Object:  ForeignKey [FK_UserAnswers_Users]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_UserAnswers_Users]
GO
/****** Object:  Default [DF__fxAnswerT__sysSt__0C85DE4D]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxAnswerT__sysSt__0C85DE4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxAnswerType]'))
Begin
ALTER TABLE [dbo].[fxAnswerType] DROP CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]

End
GO
/****** Object:  Default [DF__fxCompile__sysSt__73BA3083]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCompile__sysSt__73BA3083]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]'))
Begin
ALTER TABLE [dbo].[fxCompiledStatuses] DROP CONSTRAINT [DF__fxCompile__sysSt__73BA3083]

End
GO
/****** Object:  Default [DF__fxCourseO__sysSt__72C60C4A]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCourseO__sysSt__72C60C4A]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]'))
Begin
ALTER TABLE [dbo].[fxCourseOperations] DROP CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]

End
GO
/****** Object:  Default [DF__fxCurricu__sysSt__71D1E811]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCurricu__sysSt__71D1E811]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]'))
Begin
ALTER TABLE [dbo].[fxCurriculumOperations] DROP CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]

End
GO
/****** Object:  Default [DF__fxGroupOp__sysSt__03F0984C]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxGroupOp__sysSt__03F0984C]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]'))
Begin
ALTER TABLE [dbo].[fxGroupOperations] DROP CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]

End
GO
/****** Object:  Default [DF__fxLanguag__sysSt__70DDC3D8]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxLanguag__sysSt__70DDC3D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxLanguages]'))
Begin
ALTER TABLE [dbo].[fxLanguages] DROP CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]

End
GO
/****** Object:  Default [DF__fxPageOpe__sysSt__6FE99F9F]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOpe__sysSt__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOperations]'))
Begin
ALTER TABLE [dbo].[fxPageOperations] DROP CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]

End
GO
/****** Object:  Default [DF__fxPageOrd__sysSt__6EF57B66]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOrd__sysSt__6EF57B66]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOrders]'))
Begin
ALTER TABLE [dbo].[fxPageOrders] DROP CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]

End
GO
/****** Object:  Default [DF__fxPageTyp__sysSt__6E01572D]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageTyp__sysSt__6E01572D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageTypes]'))
Begin
ALTER TABLE [dbo].[fxPageTypes] DROP CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]

End
GO
/****** Object:  Default [DF__fxRoles__sysStat__6D0D32F4]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxRoles__sysStat__6D0D32F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxRoles]'))
Begin
ALTER TABLE [dbo].[fxRoles] DROP CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]

End
GO
/****** Object:  Default [DF__fxSampleB__sysSt__6A30C649]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxSampleB__sysSt__6A30C649]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]'))
Begin
ALTER TABLE [dbo].[fxSampleBusinesObjectOperation] DROP CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]

End
GO
/****** Object:  Default [DF__fxStageOp__sysSt__6C190EBB]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxStageOp__sysSt__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxStageOperations]'))
Begin
ALTER TABLE [dbo].[fxStageOperations] DROP CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]

End
GO
/****** Object:  Default [DF__fxThemeOp__sysSt__6B24EA82]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxThemeOp__sysSt__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]'))
Begin
ALTER TABLE [dbo].[fxThemeOperations] DROP CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]

End
GO
/****** Object:  Default [DF_relResourcesDependency_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_relResourcesDependency_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]'))
Begin
ALTER TABLE [dbo].[relResourcesDependency] DROP CONSTRAINT [DF_relResourcesDependency_sysState]

End
GO
/****** Object:  Default [DF_relResourcesFiles_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_relResourcesFiles_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]'))
Begin
ALTER TABLE [dbo].[relResourcesFiles] DROP CONSTRAINT [DF_relResourcesFiles_sysState]

End
GO
/****** Object:  Default [DF__relUserGr__sysSt__02FC7413]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserGr__sysSt__02FC7413]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
Begin
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [DF__relUserGr__sysSt__02FC7413]

End
GO
/****** Object:  Default [DF__relUserRo__sysSt__02084FDA]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserRo__sysSt__02084FDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
Begin
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [DF__relUserRo__sysSt__02084FDA]

End
GO
/****** Object:  Default [DF__tblCompil__sysSt__76969D2E]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__76969D2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__sysSt__76969D2E]

End
GO
/****** Object:  Default [DF__tblCompil__UserA__04E4BC85]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__UserA__04E4BC85]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__UserA__04E4BC85]

End
GO
/****** Object:  Default [DF__tblCompil__Compi__08B54D69]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__Compi__08B54D69]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__Compi__08B54D69]

End
GO
/****** Object:  Default [DF__tblCompil__sysSt__778AC167]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__778AC167]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
Begin
ALTER TABLE [dbo].[tblCompiledQuestions] DROP CONSTRAINT [DF__tblCompil__sysSt__778AC167]

End
GO
/****** Object:  Default [DF__tblCompil__sysSt__7D439ABD]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__7D439ABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
Begin
ALTER TABLE [dbo].[tblCompiledQuestionsData] DROP CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]

End
GO
/****** Object:  Default [DF__tblCourse__sysSt__75A278F5]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCourse__sysSt__75A278F5]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCourses]'))
Begin
ALTER TABLE [dbo].[tblCourses] DROP CONSTRAINT [DF__tblCourse__sysSt__75A278F5]

End
GO
/****** Object:  Default [DF__tblCurric__sysSt__74AE54BC]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCurric__sysSt__74AE54BC]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCurriculums]'))
Begin
ALTER TABLE [dbo].[tblCurriculums] DROP CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]

End
GO
/****** Object:  Default [DF_tblFiles_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblFiles_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblFiles]'))
Begin
ALTER TABLE [dbo].[tblFiles] DROP CONSTRAINT [DF_tblFiles_sysState]

End
GO
/****** Object:  Default [DF__tblGroups__sysSt__693CA210]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblGroups__sysSt__693CA210]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblGroups]'))
Begin
ALTER TABLE [dbo].[tblGroups] DROP CONSTRAINT [DF__tblGroups__sysSt__693CA210]

End
GO
/****** Object:  Default [DF_tblItems_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblItems_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
Begin
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [DF_tblItems_sysState]

End
GO
/****** Object:  Default [DF_tblOrganizations_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblOrganizations_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
Begin
ALTER TABLE [dbo].[tblOrganizations] DROP CONSTRAINT [DF_tblOrganizations_sysState]

End
GO
/****** Object:  Default [DF__tblPages__sysSta__7C4F7684]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPages__sysSta__7C4F7684]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
Begin
ALTER TABLE [dbo].[tblPages] DROP CONSTRAINT [DF__tblPages__sysSta__7C4F7684]

End
GO
/****** Object:  Default [DF__tblPermis__sysSt__7B5B524B]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPermis__sysSt__7B5B524B]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
Begin
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]

End
GO
/****** Object:  Default [DF__tblQuesti__sysSt__7E37BEF6]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblQuesti__sysSt__7E37BEF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
Begin
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]

End
GO
/****** Object:  Default [DF_tblResources_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblResources_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
Begin
ALTER TABLE [dbo].[tblResources] DROP CONSTRAINT [DF_tblResources_sysState]

End
GO
/****** Object:  Default [DF__tblSample__sysSt__68487DD7]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblSample__sysSt__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]'))
Begin
ALTER TABLE [dbo].[tblSampleBusinesObject] DROP CONSTRAINT [DF__tblSample__sysSt__68487DD7]

End
GO
/****** Object:  Default [DF__tblStages__sysSt__787EE5A0]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblStages__sysSt__787EE5A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
Begin
ALTER TABLE [dbo].[tblStages] DROP CONSTRAINT [DF__tblStages__sysSt__787EE5A0]

End
GO
/****** Object:  Default [DF__tblThemes__sysSt__797309D9]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__sysSt__797309D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__sysSt__797309D9]

End
GO
/****** Object:  Default [DF__tblThemes__PageC__06CD04F7]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__PageC__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__PageC__06CD04F7]

End
GO
/****** Object:  Default [DF__tblThemes__MaxCo__07C12930]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__MaxCo__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__MaxCo__07C12930]

End
GO
/****** Object:  Default [DF__tblUserAn__sysSt__01142BA1]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__sysSt__01142BA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]

End
GO
/****** Object:  Default [DF__tblUserAn__Answe__0D7A0286]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__Answe__0D7A0286]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]

End
GO
/****** Object:  Default [DF__tblUsers__sysSta__6754599E]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUsers__sysSta__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
Begin
ALTER TABLE [dbo].[tblUsers] DROP CONSTRAINT [DF__tblUsers__sysSta__6754599E]

End
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCourse]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCourse]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForTheme]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCourse]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionTheme]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCourse]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsTheme]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForStage]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsTheme]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForGroup]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsStage]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsStage]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsGroup]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCurriculum]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCurriculum]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsGroup]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionGroup]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionStage]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCurriculum]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCurriculum]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionCurriculum]
GO
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserGroups]') AND type in (N'U'))
DROP TABLE [dbo].[relUserGroups]
GO
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[relUserRoles]
GO
/****** Object:  Table [dbo].[tblItems]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblItems]') AND type in (N'U'))
DROP TABLE [dbo].[tblItems]
GO
/****** Object:  Table [dbo].[relResourcesFiles]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]') AND type in (N'U'))
DROP TABLE [dbo].[relResourcesFiles]
GO
/****** Object:  Table [dbo].[relResourcesDependency]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]') AND type in (N'U'))
DROP TABLE [dbo].[relResourcesDependency]
GO
/****** Object:  Table [dbo].[tblResources]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResources]') AND type in (N'U'))
DROP TABLE [dbo].[tblResources]
GO
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledAnswers]
GO
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledQuestionsData]
GO
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPermissions]') AND type in (N'U'))
DROP TABLE [dbo].[tblPermissions]
GO
/****** Object:  Table [dbo].[tblThemes]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThemes]') AND type in (N'U'))
DROP TABLE [dbo].[tblThemes]
GO
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOrganizations]') AND type in (N'U'))
DROP TABLE [dbo].[tblOrganizations]
GO
/****** Object:  Table [dbo].[tblStages]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStages]') AND type in (N'U'))
DROP TABLE [dbo].[tblStages]
GO
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]') AND type in (N'U'))
DROP TABLE [dbo].[tblUserAnswers]
GO
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuestions]') AND type in (N'U'))
DROP TABLE [dbo].[tblQuestions]
GO
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledQuestions]
GO
/****** Object:  Table [dbo].[tblPages]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPages]') AND type in (N'U'))
DROP TABLE [dbo].[tblPages]
GO
/****** Object:  Table [dbo].[fxRoles]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxRoles]') AND type in (N'U'))
DROP TABLE [dbo].[fxRoles]
GO
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxThemeOperations]
GO
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]') AND type in (N'U'))
DROP TABLE [dbo].[fxSampleBusinesObjectOperation]
GO
/****** Object:  Table [dbo].[tblGroups]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGroups]') AND type in (N'U'))
DROP TABLE [dbo].[tblGroups]
GO
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]') AND type in (N'U'))
DROP TABLE [dbo].[tblSampleBusinesObject]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
DROP TABLE [dbo].[tblUsers]
GO
/****** Object:  Table [dbo].[tblFiles]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblFiles]') AND type in (N'U'))
DROP TABLE [dbo].[tblFiles]
GO
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxGroupOperations]
GO
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxStageOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxStageOperations]
GO
/****** Object:  Table [dbo].[tblCourses]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCourses]') AND type in (N'U'))
DROP TABLE [dbo].[tblCourses]
GO
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCurriculums]') AND type in (N'U'))
DROP TABLE [dbo].[tblCurriculums]
GO
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]') AND type in (N'U'))
DROP TABLE [dbo].[fxCompiledStatuses]
GO
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxCourseOperations]
GO
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxLanguages]') AND type in (N'U'))
DROP TABLE [dbo].[fxLanguages]
GO
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxCurriculumOperations]
GO
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageOperations]
GO
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOrders]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageOrders]
GO
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxAnswerType]') AND type in (N'U'))
DROP TABLE [dbo].[fxAnswerType]
GO
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageTypes]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageTypes]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSecurityID]    Script Date: 01/07/2010 02:55:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSecurityID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetSecurityID]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSecurityID]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSecurityID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
BEGIN
execute dbo.sp_executesql @statement = N'CREATE function [dbo].[GetSecurityID]()
RETURNS uniqueidentifier
AS
BEGIN
	RETURN ''02805d74-ddd9-4b9e-b53e-86ebd55bbfe6'';
END' 
END
GO
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](10) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_PageType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxPageTypes] ON
INSERT [dbo].[fxPageTypes] ([ID], [Type], [sysState]) VALUES (1, N'Theory', 0)
INSERT [dbo].[fxPageTypes] ([ID], [Type], [sysState]) VALUES (2, N'Practice', 0)
SET IDENTITY_INSERT [dbo].[fxPageTypes] OFF
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxAnswerType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxAnswerType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxAnswerType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxAnswerType] ON
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (1, N'UserAnswer', 0)
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (2, N'EmptyAnswer', 0)
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (3, N'NotIncludedAnswer', 0)
SET IDENTITY_INSERT [dbo].[fxAnswerType] OFF
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOrders]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdPageOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxPageOrders] ON
INSERT [dbo].[fxPageOrders] ([ID], [Name], [sysState]) VALUES (1, N'Straight', 0)
INSERT [dbo].[fxPageOrders] ([ID], [Name], [sysState]) VALUES (2, N'Random', 0)
SET IDENTITY_INSERT [dbo].[fxPageOrders] OFF
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_PageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxPageOperations] ON
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (1, N'Add', N'Add new Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (2, N'Edit', N'Edit Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (3, N'View', N'View Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (4, N'Delete', N'Delete Page', 1, 0)
SET IDENTITY_INSERT [dbo].[fxPageOperations] OFF
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCurriculumOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_CurriculumOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxCurriculumOperations] ON
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'Modify', N'Modify curriculum by teacher', 1, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Use', N'Use curriculum by teacher', 1, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (7, N'View', N'View the curriculum', 0, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (8, N'Pass', N'Pass the curriculum', 0, 0)
SET IDENTITY_INSERT [dbo].[fxCurriculumOperations] OFF
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxLanguages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxLanguages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdLanguages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxLanguages] ON
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (17, N'Vs6CPlusPlus', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (18, N'Vs8CPlusPlus', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (19, N'DotNet2', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (20, N'DotNet3', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (21, N'Java6', 0)
INSERT [dbo].[fxLanguages] ([ID], [Name], [sysState]) VALUES (22, N'Delphi7', 0)
SET IDENTITY_INSERT [dbo].[fxLanguages] OFF
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCourseOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_CourseOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxCourseOperations] ON
INSERT [dbo].[fxCourseOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'Modify', N'Modify course by teacher', 1, 0)
INSERT [dbo].[fxCourseOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Use', N'Use course by teacher', 1, 0)
SET IDENTITY_INSERT [dbo].[fxCourseOperations] OFF
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCompiledStatuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdCompiledStatuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxCompiledStatuses] ON
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (1, N'WrongAnswer', N'Program was compiled, it passed time and memory limits,but it returns wrong output', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (2, N'Accepted', N'Program was compiled, it passed time and memory limits, and it returns correct output.', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (3, N'TimeLimit', N'Program was compiled, but it takes too much time to run.', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (4, N'MemoryLimit', N'Program was compiled, but it takes too much memory during run', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (5, N'CompilationError', N'Program wasnt compiled succesfully', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (6, N'Running', N'Program was compiled, and it is running right now', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (7, N'Enqueued', N'Program was received, and it is waiting too proceed', 0)
INSERT [dbo].[fxCompiledStatuses] ([ID], [Name], [Description], [sysState]) VALUES (8, N'Crashed', N'Program was compiled, but it crashed during execution', 0)
SET IDENTITY_INSERT [dbo].[fxCompiledStatuses] OFF
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCurriculums]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCurriculums](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_SdudyCourses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblCurriculums] ON
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (1, N'test', N'123', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (2, N'qwe', N'qwe', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (3, N'DB_test', N'', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (4, N'DB_test', N'DB_test_desc', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (5, N'C++', N'C++', 0)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (6, N'C++', N'', 1)
INSERT [dbo].[tblCurriculums] ([ID], [Name], [Description], [sysState]) VALUES (7, N'C++', N'', 0)
SET IDENTITY_INSERT [dbo].[tblCurriculums] OFF
/****** Object:  Table [dbo].[tblCourses]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCourses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCourses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[UploadDate] [datetime] NULL,
	[Version] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblCourses] ON
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (69, N'C++_first', N'', CAST(0x00009CF501522DA3 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (70, N'C++_first', N'', CAST(0x00009CF501569FD2 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (71, N'C++_first', N'', CAST(0x00009CF50169FA8A AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (72, N'C++_first', N'', CAST(0x00009CF600CAEE20 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (73, N'C++_first', N'', CAST(0x00009CF600CD3AB9 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (74, N'C++_first', N'', CAST(0x00009CF600CE10CC AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (75, N'C++_first', N'', CAST(0x00009CF600CEC489 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (76, N'C++_first', N'', CAST(0x00009CF600D1A405 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (77, N'DB_test3', N'', CAST(0x00009CF6015B0F5B AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (78, N'DB_test3', N'', CAST(0x00009CF6015BB1F7 AS DateTime), 1, 1)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (79, N'DB_test3', N'', CAST(0x00009CF70019D425 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (80, N'DB_test3', N'', CAST(0x00009CF7001A57E2 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (81, N'DB_test3', N'', CAST(0x00009CF7001D5525 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (82, N'DB_test3', N'', CAST(0x00009CF7001D9FD5 AS DateTime), 1, 0)
INSERT [dbo].[tblCourses] ([ID], [Name], [Description], [UploadDate], [Version], [sysState]) VALUES (83, N'DB_test3', N'', CAST(0x00009CF7001EF8E4 AS DateTime), 1, 0)
SET IDENTITY_INSERT [dbo].[tblCourses] OFF
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxStageOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxStageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_StageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxStageOperations] ON
INSERT [dbo].[fxStageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'View', N'View the stage', 0, 0)
INSERT [dbo].[fxStageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Pass', N'Pass the stage', 0, 0)
SET IDENTITY_INSERT [dbo].[fxStageOperations] OFF
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxGroupOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxGroupOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [UK_fxGroupOperations_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxGroupOperations] ON
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (1, N'View', NULL, 1, 0)
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (2, N'Rename', NULL, 1, 0)
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (3, N'ChangeMembers', NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[fxGroupOperations] OFF
/****** Object:  Table [dbo].[tblFiles]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblFiles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblFiles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Path] [nvarchar](300) COLLATE Ukrainian_CI_AS NOT NULL,
	[sysState] [int] NOT NULL,
 CONSTRAINT [PK_tblFiles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblFiles] ON
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (1, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (2, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (3, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (4, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (5, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (6, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (7, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (8, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (9, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (10, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (11, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (12, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (13, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (14, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (15, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (16, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (17, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (18, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (19, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (20, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (21, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (22, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (23, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (24, N'New_Examination_23.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (25, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (26, N'SCOObj.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (27, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (28, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (29, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (30, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (31, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (32, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (33, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (34, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (35, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (36, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (37, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (38, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (39, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (40, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (41, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (42, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (43, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (44, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (45, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (46, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (47, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (48, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (49, N'New_Examination_23.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (50, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (51, N'SCOObj.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (52, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (53, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (54, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (55, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (56, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (57, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (58, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (59, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (60, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (61, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (62, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (63, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (64, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (65, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (66, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (67, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (68, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (69, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (70, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (71, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (72, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (73, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (74, N'New_Examination_23.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (75, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (76, N'SCOObj.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (77, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (78, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (79, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (80, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (81, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (82, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (83, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (84, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (85, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (86, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (87, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (88, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (89, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (90, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (91, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (92, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (93, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (94, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (95, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (96, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (97, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (98, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (99, N'New_Examination_23.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (100, N'New_Examination.html', 0)
GO
print 'Processed 100 total records'
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (101, N'SCOObj.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (102, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (103, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (104, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (105, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (106, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (107, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (108, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (109, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (110, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (111, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (112, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (113, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (114, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (115, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (116, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (117, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (118, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (119, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (120, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (121, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (122, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (123, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (124, N'New_Examination_23.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (125, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (126, N'SCOObj.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (127, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (128, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (129, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (130, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (131, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (132, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (133, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (134, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (135, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (136, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (137, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (138, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (139, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (140, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (141, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (142, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (143, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (144, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (145, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (146, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (147, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (148, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (149, N'New_Examination_23.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (150, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (151, N'SCOObj.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (152, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (153, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (154, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (155, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (156, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (157, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (158, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (159, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (160, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (161, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (162, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (163, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (164, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (165, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (166, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (167, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (168, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (169, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (170, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (171, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (172, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (173, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (174, N'New_Examination_23.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (175, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (176, N'SCOObj.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (177, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (178, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (179, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (180, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (181, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (182, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (183, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (184, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (185, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (186, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (187, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (188, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (189, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (190, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (191, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (192, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (193, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (194, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (195, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (196, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (197, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (198, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (199, N'New_Examination_23.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (200, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (201, N'SCOObj.js', 0)
GO
print 'Processed 200 total records'
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (202, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (203, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (204, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (205, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (206, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (207, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (208, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (209, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (210, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (211, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (212, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (213, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (214, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (215, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (216, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (217, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (218, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (219, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (220, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (221, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (222, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (223, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (224, N'New_Examination_23.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (225, N'New_Theory.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (226, N'New_Theory_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (227, N'New_Theory_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (228, N'New_Theory.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (229, N'New_Theory.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (230, N'New_Theory.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (231, N'New_Theory.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (232, N'New_Theory.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (233, N'New_Theory_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (234, N'New_Theory_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (235, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (236, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (237, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (238, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (239, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (240, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (241, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (242, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (243, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (244, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (245, N'New_Theory.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (246, N'New_Theory_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (247, N'New_Theory_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (248, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (249, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (250, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (251, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (252, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (253, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (254, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (255, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (256, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (257, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (258, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (259, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (260, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (261, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (262, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (263, N'New_Examination_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (264, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (265, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (266, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (267, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (268, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (269, N'New_Examination_8.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (270, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (271, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (272, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (273, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (274, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (275, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (276, N'New_Examination_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (277, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (278, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (279, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (280, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (281, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (282, N'New_Examination_8.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (283, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (284, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (285, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (286, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (287, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (288, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (289, N'New_Examination_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (290, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (291, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (292, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (293, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (294, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (295, N'New_Examination_8.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (296, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (297, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (298, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (299, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (300, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (301, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (302, N'New_Examination_2.html', 1)
GO
print 'Processed 300 total records'
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (303, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (304, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (305, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (306, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (307, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (308, N'New_Examination_8.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (309, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (310, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (311, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (312, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (313, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (314, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (315, N'New_Examination_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (316, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (317, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (318, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (319, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (320, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (321, N'New_Examination_8.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (322, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (323, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (324, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (325, N'New_Examination_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (326, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (327, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (328, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (329, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (330, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (331, N'New_Examination_8.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (332, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (333, N'New_Examination_10.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (334, N'New_Examination_11.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (335, N'New_Examination_12.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (336, N'New_Examination_13.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (337, N'New_Examination_14.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (338, N'New_Examination_15.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (339, N'New_Examination_16.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (340, N'New_Examination_17.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (341, N'New_Examination_18.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (342, N'New_Examination_19.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (343, N'New_Examination_20.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (344, N'New_Examination_21.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (345, N'New_Examination_22.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (346, N'New_Examination_23.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (347, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (348, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (349, N'New_Examination_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (350, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (351, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (352, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (353, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (354, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (355, N'New_Examination_8.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (356, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (357, N'New_Examination_10.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (358, N'New_Examination_11.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (359, N'New_Examination_12.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (360, N'New_Examination_13.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (361, N'New_Examination_14.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (362, N'New_Examination_15.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (363, N'New_Examination_16.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (364, N'New_Examination_17.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (365, N'New_Examination_18.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (366, N'New_Examination_19.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (367, N'New_Examination_20.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (368, N'New_Examination_21.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (369, N'New_Examination_22.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (370, N'New_Examination_23.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (371, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (372, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (373, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (374, N'New_Theory_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (375, N'New_Theory_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (376, N'New_Theory_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (377, N'New_Theory_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (378, N'New_Theory_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (379, N'New_Theory.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (380, N'New_Theory_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (381, N'New_Theory_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (382, N'New_Theory_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (383, N'New_Theory_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (384, N'New_Theory_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (385, N'New_Theory_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (386, N'New_Theory_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (387, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (388, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (389, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (390, N'New_Theory_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (391, N'New_Theory_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (392, N'New_Theory_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (393, N'New_Theory_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (394, N'New_Theory_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (395, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (396, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (397, N'New_Examination_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (398, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (399, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (400, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (401, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (402, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (403, N'New_Examination_8.html', 1)
GO
print 'Processed 400 total records'
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (404, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (405, N'New_Examination_10.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (406, N'New_Examination_11.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (407, N'New_Examination_12.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (408, N'New_Examination_13.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (409, N'New_Examination_14.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (410, N'New_Examination_15.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (411, N'New_Examination_16.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (412, N'New_Examination_17.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (413, N'New_Examination_18.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (414, N'New_Examination_19.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (415, N'New_Examination_20.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (416, N'New_Examination_21.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (417, N'New_Examination_22.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (418, N'New_Examination_23.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (419, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (420, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (421, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (422, N'New_Theory_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (423, N'New_Theory_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (424, N'New_Theory_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (425, N'New_Theory_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (426, N'New_Theory_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (427, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (428, N'New_Theory_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (429, N'New_Theory_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (430, N'New_Theory_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (431, N'New_Theory_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (432, N'New_Theory_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (433, N'New_Theory_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (434, N'New_Theory_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (435, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (436, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (437, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (438, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (439, N'New_Theory_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (440, N'New_Theory_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (441, N'New_Theory_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (442, N'New_Theory_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (443, N'New_Theory_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (444, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (445, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (446, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (447, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (448, N'New_Theory_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (449, N'New_Theory_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (450, N'New_Theory_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (451, N'New_Theory_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (452, N'New_Theory_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (453, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (454, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (455, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (456, N'New_Theory_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (457, N'New_Theory_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (458, N'New_Theory_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (459, N'New_Theory_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (460, N'New_Theory_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (461, N'New_Theory.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (462, N'New_Theory_files/image001.png', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (463, N'New_Theory_files/image002.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (464, N'New_Theory_files/image003.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (465, N'New_Theory_files/image005.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (466, N'New_Theory_files/image006.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (467, N'New_Theory_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (468, N'New_Theory_1_files/image001.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (469, N'New_Theory_1_files/image006.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (470, N'New_Theory_1_files/image007.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (471, N'New_Theory_1_files/image008.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (472, N'New_Theory_1_files/image009.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (473, N'New_Theory_1_files/image010.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (474, N'New_Theory_1_files/image011.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (475, N'New_Theory_1_files/image012.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (476, N'New_Theory_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (477, N'New_Theory_2_files/image001.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (478, N'New_Theory_2_files/image002.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (479, N'New_Theory_2_files/image003.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (480, N'New_Theory_2_files/image004.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (481, N'New_Theory_2_files/image005.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (482, N'New_Theory_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (483, N'New_Theory_3_files/image001.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (484, N'New_Theory_3_files/image002.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (485, N'New_Theory_3_files/image003.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (486, N'New_Theory_3_files/image004.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (487, N'New_Theory_3_files/image005.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (488, N'New_Theory_3_files/image006.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (489, N'New_Theory_3_files/image007.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (490, N'New_Theory_3_files/image008.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (491, N'New_Theory_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (492, N'New_Theory_4_files/image001.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (493, N'New_Theory_4_files/image002.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (494, N'New_Theory_4_files/image003.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (495, N'New_Theory_4_files/image004.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (496, N'New_Theory_4_files/image005.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (497, N'New_Theory_4_files/image006.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (498, N'New_Theory_4_files/image007.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (499, N'New_Theory_4_files/image008.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (500, N'New_Theory_4_files/image009.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (501, N'New_Theory_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (502, N'New_Theory_5_files/image001.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (503, N'New_Theory_5_files/image002.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (504, N'New_Theory_5_files/image003.gif', 0)
GO
print 'Processed 500 total records'
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (505, N'New_Theory_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (506, N'New_Theory_6_files/image001.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (507, N'New_Theory_6_files/image002.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (508, N'New_Theory_6_files/image003.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (509, N'New_Theory_6_files/image004.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (510, N'New_Theory_6_files/image005.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (511, N'New_Theory_6_files/image006.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (512, N'New_Theory_6_files/image007.gif', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (513, N'New_Theory_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (514, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (515, N'New_Theory_files/image001.png', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (516, N'New_Theory_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (517, N'New_Theory_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (518, N'New_Theory_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (519, N'New_Theory_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (520, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (521, N'New_Theory_1_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (522, N'New_Theory_1_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (523, N'New_Theory_1_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (524, N'New_Theory_1_files/image008.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (525, N'New_Theory_1_files/image009.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (526, N'New_Theory_1_files/image010.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (527, N'New_Theory_1_files/image011.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (528, N'New_Theory_1_files/image012.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (529, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (530, N'New_Theory_2_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (531, N'New_Theory_2_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (532, N'New_Theory_2_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (533, N'New_Theory_2_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (534, N'New_Theory_2_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (535, N'New_Theory_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (536, N'New_Theory_3_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (537, N'New_Theory_3_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (538, N'New_Theory_3_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (539, N'New_Theory_3_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (540, N'New_Theory_3_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (541, N'New_Theory_3_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (542, N'New_Theory_3_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (543, N'New_Theory_3_files/image008.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (544, N'New_Theory_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (545, N'New_Theory_4_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (546, N'New_Theory_4_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (547, N'New_Theory_4_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (548, N'New_Theory_4_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (549, N'New_Theory_4_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (550, N'New_Theory_4_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (551, N'New_Theory_4_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (552, N'New_Theory_4_files/image008.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (553, N'New_Theory_4_files/image009.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (554, N'New_Theory_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (555, N'New_Theory_5_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (556, N'New_Theory_5_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (557, N'New_Theory_5_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (558, N'New_Theory_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (559, N'New_Theory_6_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (560, N'New_Theory_6_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (561, N'New_Theory_6_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (562, N'New_Theory_6_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (563, N'New_Theory_6_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (564, N'New_Theory_6_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (565, N'New_Theory_6_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (566, N'New_Theory_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (567, N'New_Theory_2_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (568, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (569, N'New_Theory_files/image001.png', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (570, N'New_Theory_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (571, N'New_Theory_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (572, N'New_Theory_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (573, N'New_Theory_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (574, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (575, N'New_Theory_1_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (576, N'New_Theory_1_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (577, N'New_Theory_1_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (578, N'New_Theory_1_files/image008.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (579, N'New_Theory_1_files/image009.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (580, N'New_Theory_1_files/image010.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (581, N'New_Theory_1_files/image011.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (582, N'New_Theory_1_files/image012.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (583, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (584, N'New_Theory_2_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (585, N'New_Theory_2_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (586, N'New_Theory_2_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (587, N'New_Theory_2_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (588, N'New_Theory_2_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (589, N'New_Theory_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (590, N'New_Theory_3_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (591, N'New_Theory_3_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (592, N'New_Theory_3_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (593, N'New_Theory_3_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (594, N'New_Theory_3_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (595, N'New_Theory_3_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (596, N'New_Theory_3_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (597, N'New_Theory_3_files/image008.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (598, N'New_Theory_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (599, N'New_Theory_4_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (600, N'New_Theory_4_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (601, N'New_Theory_4_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (602, N'New_Theory_4_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (603, N'New_Theory_4_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (604, N'New_Theory_4_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (605, N'New_Theory_4_files/image007.gif', 1)
GO
print 'Processed 600 total records'
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (606, N'New_Theory_4_files/image008.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (607, N'New_Theory_4_files/image009.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (608, N'New_Theory_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (609, N'New_Theory_5_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (610, N'New_Theory_5_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (611, N'New_Theory_5_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (612, N'New_Theory_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (613, N'New_Theory_6_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (614, N'New_Theory_6_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (615, N'New_Theory_6_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (616, N'New_Theory_6_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (617, N'New_Theory_6_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (618, N'New_Theory_6_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (619, N'New_Theory_6_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (620, N'New_Theory_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (621, N'New_Theory_7_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (622, N'New_Theory.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (623, N'New_Theory_files/image001.png', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (624, N'New_Theory_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (625, N'New_Theory_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (626, N'New_Theory_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (627, N'New_Theory_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (628, N'New_Theory_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (629, N'New_Theory_1_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (630, N'New_Theory_1_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (631, N'New_Theory_1_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (632, N'New_Theory_1_files/image008.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (633, N'New_Theory_1_files/image009.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (634, N'New_Theory_1_files/image010.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (635, N'New_Theory_1_files/image011.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (636, N'New_Theory_1_files/image012.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (637, N'New_Theory_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (638, N'New_Theory_2_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (639, N'New_Theory_2_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (640, N'New_Theory_2_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (641, N'New_Theory_2_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (642, N'New_Theory_2_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (643, N'New_Theory_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (644, N'New_Theory_3_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (645, N'New_Theory_3_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (646, N'New_Theory_3_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (647, N'New_Theory_3_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (648, N'New_Theory_3_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (649, N'New_Theory_3_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (650, N'New_Theory_3_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (651, N'New_Theory_3_files/image008.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (652, N'New_Theory_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (653, N'New_Theory_4_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (654, N'New_Theory_4_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (655, N'New_Theory_4_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (656, N'New_Theory_4_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (657, N'New_Theory_4_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (658, N'New_Theory_4_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (659, N'New_Theory_4_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (660, N'New_Theory_4_files/image008.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (661, N'New_Theory_4_files/image009.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (662, N'New_Theory_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (663, N'New_Theory_5_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (664, N'New_Theory_5_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (665, N'New_Theory_5_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (666, N'New_Theory_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (667, N'New_Theory_6_files/image001.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (668, N'New_Theory_6_files/image002.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (669, N'New_Theory_6_files/image003.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (670, N'New_Theory_6_files/image004.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (671, N'New_Theory_6_files/image005.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (672, N'New_Theory_6_files/image006.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (673, N'New_Theory_6_files/image007.gif', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (674, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (675, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (676, N'New_Examination_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (677, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (678, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (679, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (680, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (681, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (682, N'New_Examination_8.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (683, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (684, N'New_Examination_10.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (685, N'New_Examination_11.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (686, N'New_Examination_12.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (687, N'New_Examination_13.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (688, N'New_Examination_14.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (689, N'New_Examination_15.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (690, N'New_Examination_16.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (691, N'New_Examination_17.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (692, N'New_Examination_18.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (693, N'New_Examination_19.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (694, N'New_Examination_20.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (695, N'New_Examination_21.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (696, N'New_Examination_22.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (697, N'New_Examination_23.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (698, N'New_Examination.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (699, N'New_Examination_1.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (700, N'New_Examination_2.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (701, N'New_Examination_3.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (702, N'New_Examination_4.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (703, N'New_Examination_5.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (704, N'New_Examination_6.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (705, N'New_Examination_7.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (706, N'New_Examination_8.html', 1)
GO
print 'Processed 700 total records'
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (707, N'New_Examination_9.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (708, N'New_Examination_10.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (709, N'New_Examination_11.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (710, N'New_Examination_12.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (711, N'New_Examination_13.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (712, N'New_Examination_14.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (713, N'New_Examination_15.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (714, N'New_Examination_16.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (715, N'New_Examination_17.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (716, N'New_Examination_18.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (717, N'New_Examination_19.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (718, N'New_Examination_20.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (719, N'New_Examination_21.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (720, N'New_Examination_22.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (721, N'New_Examination_23.html', 1)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (722, N'New_Examination.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (723, N'SCOObj.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (724, N'LMSIntf.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (725, N'LMSDebugger.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (726, N'help.js', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (727, N'New_Examination_1.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (728, N'New_Examination_2.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (729, N'New_Examination_3.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (730, N'New_Examination_4.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (731, N'New_Examination_5.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (732, N'New_Examination_6.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (733, N'New_Examination_7.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (734, N'New_Examination_8.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (735, N'New_Examination_9.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (736, N'New_Examination_10.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (737, N'New_Examination_11.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (738, N'New_Examination_12.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (739, N'New_Examination_13.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (740, N'New_Examination_14.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (741, N'New_Examination_15.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (742, N'New_Examination_16.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (743, N'New_Examination_17.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (744, N'New_Examination_18.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (745, N'New_Examination_19.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (746, N'New_Examination_20.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (747, N'New_Examination_21.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (748, N'New_Examination_22.html', 0)
INSERT [dbo].[tblFiles] ([ID], [Path], [sysState]) VALUES (749, N'New_Examination_23.html', 0)
SET IDENTITY_INSERT [dbo].[tblFiles] OFF
/****** Object:  Table [dbo].[tblUsers]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[LastName] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Login] [nvarchar](32) COLLATE Ukrainian_CI_AS NOT NULL,
	[PasswordHash] [char](32) COLLATE Ukrainian_CI_AS NOT NULL,
	[Email] [char](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [UK_EMAIL] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON),
 CONSTRAINT [UK_Login] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblUsers] ON
INSERT [dbo].[tblUsers] ([ID], [FirstName], [LastName], [Login], [PasswordHash], [Email], [sysState]) VALUES (1, N'Volodymyr', N'Shtenovych', N'lex', N'B067B3D3054D8868C950E1946300A3F4', N'ShVolodya@gmail.com                               ', 0)
INSERT [dbo].[tblUsers] ([ID], [FirstName], [LastName], [Login], [PasswordHash], [Email], [sysState]) VALUES (3, N'V', N'P', N'vladykx', N'123                             ', N'1                                                 ', 0)
SET IDENTITY_INSERT [dbo].[tblUsers] OFF
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSampleBusinesObject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [UQ__tblSampleBusines__7E6CC920] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblGroups]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblGroups] ON
INSERT [dbo].[tblGroups] ([ID], [Name], [sysState]) VALUES (1, N'123', 0)
SET IDENTITY_INSERT [dbo].[tblGroups] OFF
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxSampleBusinesObjectOperation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [UQ__fxSampleBusinesO__023D5A04] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxThemeOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NOT NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_ThemeOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxThemeOperations] ON
INSERT [dbo].[fxThemeOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'View', N'View the theme', 0, 0)
INSERT [dbo].[fxThemeOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Pass', N'Pass the theme', 0, 0)
SET IDENTITY_INSERT [dbo].[fxThemeOperations] OFF
/****** Object:  Table [dbo].[fxRoles]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) COLLATE Ukrainian_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[fxRoles] ON
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (1, N'STUDENT', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (2, N'LECTOR', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (3, N'TRAINER', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (4, N'ADMIN', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (5, N'SUPER_ADMIN', NULL, 0)
SET IDENTITY_INSERT [dbo].[fxRoles] OFF
/****** Object:  Table [dbo].[tblPages]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblPages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeRef] [int] NULL,
	[PageTypeRef] [int] NULL,
	[PageRank] [int] NULL,
	[PageName] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[PageFile] [varchar](250) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCompiledQuestions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LanguageRef] [int] NOT NULL,
	[TimeLimit] [int] NULL,
	[MemoryLimit] [int] NULL,
	[OutputLimit] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblCompiledQuestions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuestions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblQuestions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageRef] [int] NULL,
	[TestName] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[CorrectAnswer] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[Rank] [int] NULL,
	[IsCompiled] [bit] NOT NULL,
	[CompiledQuestionRef] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_CorrectAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUserAnswers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRef] [int] NULL,
	[QuestionRef] [int] NULL,
	[UserAnswer] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[Date] [datetime] NULL,
	[IsCompiledAnswer] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
	[AnswerTypeRef] [int] NOT NULL,
 CONSTRAINT [PK_UserAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblStages]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblStages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[CurriculumRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Stages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblStages] ON
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (1, N'', N'', 1, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (2, N'', N'', 1, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (3, N'123', N'a', 1, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (4, N'DB_test', N'', 3, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (5, N'DB_test2', N'', 3, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (6, N'DB_test_stage', N'DB_test_stage_desc', 4, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (7, N'Semester 1', N'Semester 1', 5, 0)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (8, N'C++', N'', 6, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (9, N'asdf', N'', 6, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (10, N'sdfg', N'', 6, 1)
INSERT [dbo].[tblStages] ([ID], [Name], [Description], [CurriculumRef], [sysState]) VALUES (11, N'Semester 1', N'', 7, 0)
SET IDENTITY_INSERT [dbo].[tblStages] OFF
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOrganizations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblOrganizations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseRef] [int] NOT NULL,
	[Title] [nvarchar](200) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblOrganizations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblOrganizations] ON
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (17, 69, N'C++_first', 0)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (18, 70, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (19, 71, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (20, 72, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (21, 74, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (22, 75, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (23, 76, N'C++_first', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (24, 77, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (25, 78, N'DB_test3', 1)
INSERT [dbo].[tblOrganizations] ([ID], [CourseRef], [Title], [sysState]) VALUES (26, 83, N'DB_test3', 0)
SET IDENTITY_INSERT [dbo].[tblOrganizations] OFF
/****** Object:  Table [dbo].[tblThemes]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThemes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblThemes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
	[CourseRef] [int] NOT NULL,
	[OrganizationRef] [int] NOT NULL,
	[StageRef] [int] NOT NULL,
	[IsControl] [bit] NOT NULL,
	[PageOrderRef] [int] NULL,
	[sysState] [smallint] NOT NULL,
	[PageCountToShow] [int] NULL,
	[MaxCountToSubmit] [int] NULL,
 CONSTRAINT [PK_Chapter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblThemes] ON
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (2, N'C++_first', 71, 19, 8, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (3, N'C++_first', 71, 19, 9, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (4, N'C++_first', 71, 19, 9, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (5, N'C++_first', 71, 19, 10, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (6, N'C++_first', 71, 19, 8, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (7, N'C++_first', 75, 22, 11, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (8, N'C++_first', 76, 23, 11, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (9, N'DB_test3', 77, 24, 11, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (10, N'DB_test3', 78, 25, 11, 0, NULL, 1, NULL, NULL)
INSERT [dbo].[tblThemes] ([ID], [Name], [CourseRef], [OrganizationRef], [StageRef], [IsControl], [PageOrderRef], [sysState], [PageCountToShow], [MaxCountToSubmit]) VALUES (11, N'DB_test3', 83, 26, 11, 0, NULL, 0, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblThemes] OFF
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPermissions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblPermissions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ParentPermitionRef] [int] NULL,
	[DateSince] [datetime] NULL,
	[DateTill] [datetime] NULL,
	[OwnerUserRef] [int] NULL,
	[OwnerGroupRef] [int] NULL,
	[CanBeDelagated] [bit] NOT NULL,
	[CourseRef] [int] NULL,
	[CourseOperationRef] [int] NULL,
	[CurriculumRef] [int] NULL,
	[CurriculumOperationRef] [int] NULL,
	[StageRef] [int] NULL,
	[StageOperationRef] [int] NULL,
	[ThemeRef] [int] NULL,
	[ThemeOperationRef] [int] NULL,
	[PageRef] [int] NULL,
	[PageOperationRef] [int] NULL,
	[UserObjectRef] [int] NULL,
	[GroupObjectRef] [int] NULL,
	[GroupRef] [int] NULL,
	[GroupOperationRef] [int] NULL,
	[OrganizationRef] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblPermissions] ON
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (113, NULL, NULL, NULL, 1, NULL, 1, 70, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (114, NULL, NULL, NULL, 1, NULL, 1, 70, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (115, NULL, NULL, NULL, 1, NULL, 1, 71, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (116, NULL, NULL, NULL, 1, NULL, 1, 71, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (117, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 6, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (118, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 6, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (119, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 6, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (120, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 6, 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (121, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 8, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (122, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 8, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (123, NULL, NULL, NULL, 1, NULL, 1, 72, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (124, NULL, NULL, NULL, 1, NULL, 1, 72, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (125, NULL, NULL, NULL, 1, NULL, 1, 74, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (126, NULL, NULL, NULL, 1, NULL, 1, 74, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (127, NULL, NULL, NULL, 1, NULL, 1, 75, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (128, NULL, NULL, NULL, 1, NULL, 1, 75, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (129, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 7, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (130, NULL, NULL, NULL, 1, NULL, 1, NULL, NULL, 7, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (131, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 7, 7, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (132, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, 7, 8, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (133, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 11, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (134, NULL, NULL, NULL, NULL, 1, 1, NULL, NULL, NULL, NULL, 11, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (135, NULL, NULL, NULL, 1, NULL, 1, 76, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (136, NULL, NULL, NULL, 1, NULL, 1, 76, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (137, NULL, NULL, NULL, 1, NULL, 1, 77, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (138, NULL, NULL, NULL, 1, NULL, 1, 77, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (139, NULL, NULL, NULL, 1, NULL, 1, 78, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (140, NULL, NULL, NULL, 1, NULL, 1, 78, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 1)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (141, NULL, NULL, NULL, 1, NULL, 1, 83, 6, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
INSERT [dbo].[tblPermissions] ([ID], [ParentPermitionRef], [DateSince], [DateTill], [OwnerUserRef], [OwnerGroupRef], [CanBeDelagated], [CourseRef], [CourseOperationRef], [CurriculumRef], [CurriculumOperationRef], [StageRef], [StageOperationRef], [ThemeRef], [ThemeOperationRef], [PageRef], [PageOperationRef], [UserObjectRef], [GroupObjectRef], [GroupRef], [GroupOperationRef], [OrganizationRef], [sysState]) VALUES (142, NULL, NULL, NULL, 1, NULL, 1, 83, 5, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL, 0)
SET IDENTITY_INSERT [dbo].[tblPermissions] OFF
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCompiledQuestionsData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompiledQuestionRef] [int] NOT NULL,
	[Input] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[Output] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblCompiledQuestionsData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCompiledAnswers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[TimeUsed] [int] NULL,
	[MemoryUsed] [int] NULL,
	[StatusRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
	[UserAnswerRef] [int] NOT NULL,
	[Output] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[CompiledQuestionsDataRef] [int] NOT NULL,
 CONSTRAINT [PK_tblCompiledAnswers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblResources]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResources]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblResources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseRef] [int] NOT NULL,
	[Identifier] [nvarchar](300) COLLATE Ukrainian_CI_AS NOT NULL,
	[Type] [nvarchar](100) COLLATE Ukrainian_CI_AS NOT NULL,
	[Href] [nvarchar](300) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblResources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblResources] ON
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (214, 70, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (215, 70, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (216, 70, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (217, 70, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (218, 70, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (219, 70, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (220, 70, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (221, 70, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (222, 70, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (223, 71, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (224, 71, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (225, 71, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (226, 71, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (227, 71, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (228, 71, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (229, 71, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (230, 71, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (231, 72, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (232, 72, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (233, 72, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (234, 72, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (235, 72, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (236, 72, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (237, 72, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (238, 72, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (239, 73, N'New_Theory', N'asset', N'New_Theory.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (240, 73, N'New_Theory_1', N'asset', N'New_Theory_1.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (241, 73, N'New_Theory_2', N'asset', N'New_Theory_2.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (242, 73, N'New_Theory_3', N'asset', N'New_Theory_3.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (243, 73, N'New_Theory_4', N'asset', N'New_Theory_4.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (244, 73, N'New_Theory_5', N'asset', N'New_Theory_5.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (245, 73, N'New_Theory_6', N'asset', N'New_Theory_6.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (246, 73, N'New_Theory_7', N'asset', N'New_Theory_7.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (247, 74, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (248, 74, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (249, 74, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (250, 74, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (251, 74, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (252, 74, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (253, 74, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (254, 74, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (255, 75, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (256, 75, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (257, 75, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (258, 75, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (259, 75, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (260, 75, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (261, 75, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (262, 75, N'New_Theory_7', N'asset', N'New_Theory_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (263, 76, N'New_Theory', N'asset', N'New_Theory.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (264, 76, N'New_Theory_1', N'asset', N'New_Theory_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (265, 76, N'New_Theory_2', N'asset', N'New_Theory_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (266, 76, N'New_Theory_3', N'asset', N'New_Theory_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (267, 76, N'New_Theory_4', N'asset', N'New_Theory_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (268, 76, N'New_Theory_5', N'asset', N'New_Theory_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (269, 76, N'New_Theory_6', N'asset', N'New_Theory_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (270, 77, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (271, 77, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (272, 77, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (273, 77, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (274, 77, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (275, 77, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (276, 77, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (277, 77, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (278, 77, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (279, 77, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (280, 77, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (281, 77, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (282, 77, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (283, 77, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (284, 77, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (285, 77, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (286, 77, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (287, 77, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (288, 77, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (289, 77, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (290, 77, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (291, 77, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (292, 77, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (293, 77, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (294, 78, N'New_Examination', N'sco', N'New_Examination.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (295, 78, N'New_Examination_1', N'sco', N'New_Examination_1.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (296, 78, N'New_Examination_2', N'sco', N'New_Examination_2.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (297, 78, N'New_Examination_3', N'sco', N'New_Examination_3.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (298, 78, N'New_Examination_4', N'sco', N'New_Examination_4.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (299, 78, N'New_Examination_5', N'sco', N'New_Examination_5.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (300, 78, N'New_Examination_6', N'sco', N'New_Examination_6.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (301, 78, N'New_Examination_7', N'sco', N'New_Examination_7.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (302, 78, N'New_Examination_8', N'sco', N'New_Examination_8.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (303, 78, N'New_Examination_9', N'sco', N'New_Examination_9.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (304, 78, N'New_Examination_10', N'sco', N'New_Examination_10.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (305, 78, N'New_Examination_11', N'sco', N'New_Examination_11.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (306, 78, N'New_Examination_12', N'sco', N'New_Examination_12.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (307, 78, N'New_Examination_13', N'sco', N'New_Examination_13.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (308, 78, N'New_Examination_14', N'sco', N'New_Examination_14.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (309, 78, N'New_Examination_15', N'sco', N'New_Examination_15.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (310, 78, N'New_Examination_16', N'sco', N'New_Examination_16.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (311, 78, N'New_Examination_17', N'sco', N'New_Examination_17.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (312, 78, N'New_Examination_18', N'sco', N'New_Examination_18.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (313, 78, N'New_Examination_19', N'sco', N'New_Examination_19.html', 1)
GO
print 'Processed 100 total records'
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (314, 78, N'New_Examination_20', N'sco', N'New_Examination_20.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (315, 78, N'New_Examination_21', N'sco', N'New_Examination_21.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (316, 78, N'New_Examination_22', N'sco', N'New_Examination_22.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (317, 78, N'New_Examination_23', N'sco', N'New_Examination_23.html', 1)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (318, 79, N'New_Examination', N'sco', N'New_Examination.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (319, 80, N'New_Examination', N'sco', N'New_Examination.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (320, 81, N'New_Examination', N'sco', N'New_Examination.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (321, 82, N'New_Examination_1', N'sco', N'New_Examination_1.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (322, 83, N'New_Examination', N'sco', N'New_Examination.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (323, 83, N'New_Examination_1', N'sco', N'New_Examination_1.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (324, 83, N'New_Examination_2', N'sco', N'New_Examination_2.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (325, 83, N'New_Examination_3', N'sco', N'New_Examination_3.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (326, 83, N'New_Examination_4', N'sco', N'New_Examination_4.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (327, 83, N'New_Examination_5', N'sco', N'New_Examination_5.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (328, 83, N'New_Examination_6', N'sco', N'New_Examination_6.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (329, 83, N'New_Examination_7', N'sco', N'New_Examination_7.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (330, 83, N'New_Examination_8', N'sco', N'New_Examination_8.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (331, 83, N'New_Examination_9', N'sco', N'New_Examination_9.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (332, 83, N'New_Examination_10', N'sco', N'New_Examination_10.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (333, 83, N'New_Examination_11', N'sco', N'New_Examination_11.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (334, 83, N'New_Examination_12', N'sco', N'New_Examination_12.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (335, 83, N'New_Examination_13', N'sco', N'New_Examination_13.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (336, 83, N'New_Examination_14', N'sco', N'New_Examination_14.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (337, 83, N'New_Examination_15', N'sco', N'New_Examination_15.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (338, 83, N'New_Examination_16', N'sco', N'New_Examination_16.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (339, 83, N'New_Examination_17', N'sco', N'New_Examination_17.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (340, 83, N'New_Examination_18', N'sco', N'New_Examination_18.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (341, 83, N'New_Examination_19', N'sco', N'New_Examination_19.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (342, 83, N'New_Examination_20', N'sco', N'New_Examination_20.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (343, 83, N'New_Examination_21', N'sco', N'New_Examination_21.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (344, 83, N'New_Examination_22', N'sco', N'New_Examination_22.html', 0)
INSERT [dbo].[tblResources] ([ID], [CourseRef], [Identifier], [Type], [Href], [sysState]) VALUES (345, 83, N'New_Examination_23', N'sco', N'New_Examination_23.html', 0)
SET IDENTITY_INSERT [dbo].[tblResources] OFF
/****** Object:  Table [dbo].[relResourcesDependency]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relResourcesDependency](
	[DependantRef] [int] NOT NULL,
	[DependencyRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_relResourcesDependency] PRIMARY KEY CLUSTERED 
(
	[DependantRef] ASC,
	[DependencyRef] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[relResourcesFiles]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relResourcesFiles](
	[ResourceRef] [int] NOT NULL,
	[FileRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_relResourcesFiles] PRIMARY KEY CLUSTERED 
(
	[ResourceRef] ASC,
	[FileRef] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (214, 436, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (215, 437, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (216, 438, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (217, 439, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (218, 440, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (219, 441, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (220, 442, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (221, 443, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (222, 444, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (223, 445, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (224, 446, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (225, 447, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (226, 448, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (227, 449, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (228, 450, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (229, 451, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (230, 452, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (231, 453, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (232, 454, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (233, 455, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (234, 456, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (235, 457, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (236, 458, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (237, 459, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (238, 460, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (239, 461, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (239, 462, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (239, 463, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (239, 464, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (239, 465, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (239, 466, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (240, 467, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (240, 468, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (240, 469, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (240, 470, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (240, 471, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (240, 472, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (240, 473, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (240, 474, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (240, 475, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (241, 476, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (241, 477, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (241, 478, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (241, 479, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (241, 480, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (241, 481, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (242, 482, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (242, 483, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (242, 484, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (242, 485, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (242, 486, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (242, 487, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (242, 488, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (242, 489, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (242, 490, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 491, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 492, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 493, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 494, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 495, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 496, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 497, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 498, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 499, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (243, 500, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (244, 501, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (244, 502, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (244, 503, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (244, 504, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (245, 505, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (245, 506, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (245, 507, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (245, 508, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (245, 509, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (245, 510, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (245, 511, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (245, 512, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (246, 513, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (247, 514, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (247, 515, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (247, 516, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (247, 517, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (247, 518, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (247, 519, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (248, 520, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (248, 521, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (248, 522, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (248, 523, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (248, 524, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (248, 525, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (248, 526, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (248, 527, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (248, 528, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (249, 529, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (249, 530, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (249, 531, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (249, 532, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (249, 533, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (249, 534, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (250, 535, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (250, 536, 1)
GO
print 'Processed 100 total records'
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (250, 537, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (250, 538, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (250, 539, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (250, 540, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (250, 541, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (250, 542, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (250, 543, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 544, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 545, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 546, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 547, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 548, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 549, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 550, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 551, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 552, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (251, 553, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (252, 554, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (252, 555, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (252, 556, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (252, 557, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (253, 558, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (253, 559, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (253, 560, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (253, 561, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (253, 562, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (253, 563, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (253, 564, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (253, 565, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (254, 566, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (254, 567, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (255, 568, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (255, 569, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (255, 570, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (255, 571, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (255, 572, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (255, 573, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (256, 574, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (256, 575, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (256, 576, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (256, 577, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (256, 578, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (256, 579, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (256, 580, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (256, 581, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (256, 582, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (257, 583, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (257, 584, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (257, 585, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (257, 586, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (257, 587, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (257, 588, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (258, 589, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (258, 590, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (258, 591, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (258, 592, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (258, 593, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (258, 594, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (258, 595, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (258, 596, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (258, 597, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 598, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 599, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 600, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 601, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 602, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 603, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 604, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 605, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 606, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (259, 607, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (260, 608, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (260, 609, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (260, 610, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (260, 611, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (261, 612, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (261, 613, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (261, 614, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (261, 615, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (261, 616, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (261, 617, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (261, 618, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (261, 619, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (262, 620, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (262, 621, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (263, 622, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (263, 623, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (263, 624, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (263, 625, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (263, 626, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (263, 627, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (264, 628, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (264, 629, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (264, 630, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (264, 631, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (264, 632, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (264, 633, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (264, 634, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (264, 635, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (264, 636, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (265, 637, 1)
GO
print 'Processed 200 total records'
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (265, 638, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (265, 639, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (265, 640, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (265, 641, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (265, 642, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (266, 643, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (266, 644, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (266, 645, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (266, 646, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (266, 647, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (266, 648, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (266, 649, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (266, 650, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (266, 651, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 652, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 653, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 654, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 655, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 656, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 657, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 658, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 659, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 660, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (267, 661, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (268, 662, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (268, 663, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (268, 664, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (268, 665, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (269, 666, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (269, 667, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (269, 668, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (269, 669, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (269, 670, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (269, 671, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (269, 672, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (269, 673, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (270, 674, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (271, 675, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (272, 676, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (273, 677, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (274, 678, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (275, 679, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (276, 680, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (277, 681, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (278, 682, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (279, 683, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (280, 684, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (281, 685, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (282, 686, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (283, 687, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (284, 688, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (285, 689, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (286, 690, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (287, 691, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (288, 692, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (289, 693, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (290, 694, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (291, 695, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (292, 696, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (293, 697, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (294, 698, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (295, 699, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (296, 700, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (297, 701, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (298, 702, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (299, 703, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (300, 704, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (301, 705, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (302, 706, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (303, 707, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (304, 708, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (305, 709, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (306, 710, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (307, 711, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (308, 712, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (309, 713, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (310, 714, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (311, 715, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (312, 716, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (313, 717, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (314, 718, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (315, 719, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (316, 720, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (317, 721, 1)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (322, 722, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (322, 723, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (322, 724, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (322, 725, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (322, 726, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (323, 727, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (324, 728, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (325, 729, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (326, 730, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (327, 731, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (328, 732, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (329, 733, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (330, 734, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (331, 735, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (332, 736, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (333, 737, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (334, 738, 0)
GO
print 'Processed 300 total records'
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (335, 739, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (336, 740, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (337, 741, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (338, 742, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (339, 743, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (340, 744, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (341, 745, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (342, 746, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (343, 747, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (344, 748, 0)
INSERT [dbo].[relResourcesFiles] ([ResourceRef], [FileRef], [sysState]) VALUES (345, 749, 0)
/****** Object:  Table [dbo].[tblItems]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblItems]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblItems](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PID] [int] NULL,
	[OrganizationRef] [int] NOT NULL,
	[ResourceRef] [int] NULL,
	[Title] [nvarchar](200) COLLATE Ukrainian_CI_AS NULL,
	[IsLeaf] [bit] NOT NULL,
	[sysState] [int] NOT NULL,
 CONSTRAINT [PK_tblItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblItems] ON
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (176, NULL, 18, 214, N'C++_first', 0, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (177, 176, 18, 214, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (178, 176, 18, 215, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (179, 176, 18, 219, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (180, 176, 18, 218, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (181, NULL, 19, 223, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (182, NULL, 19, 224, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (183, NULL, 19, 228, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (184, NULL, 19, 229, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (185, NULL, 19, 230, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (186, NULL, 20, 231, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (187, NULL, 20, 232, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (188, NULL, 20, 236, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (189, NULL, 20, 237, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (190, NULL, 20, 238, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (191, NULL, 21, 247, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (192, NULL, 21, 248, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (193, NULL, 21, 252, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (194, NULL, 21, 253, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (195, NULL, 21, 254, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (196, NULL, 22, 255, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (197, NULL, 22, 256, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (198, NULL, 22, 260, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (199, NULL, 22, 261, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (200, NULL, 22, 262, N'New Theory', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (201, NULL, 23, 263, N'Проста С++ програма', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (202, NULL, 23, 264, N'Вирази з даними вбудованих типів', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (203, NULL, 23, 268, N'Вказівники - низькорівневий засіб С++', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (204, NULL, 23, 269, N'Функції', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (205, NULL, 24, NULL, N'DBColoquium1General+SQL', 0, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (206, 205, 24, 270, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (207, 205, 24, 271, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (208, 205, 24, 272, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (209, 205, 24, 273, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (210, 205, 24, 274, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (211, 205, 24, 275, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (212, 205, 24, 276, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (213, 205, 24, 277, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (214, 205, 24, 278, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (215, 205, 24, 279, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (216, 205, 24, 280, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (217, 205, 24, 281, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (218, 205, 24, 282, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (219, 205, 24, 283, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (220, 205, 24, 284, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (221, 205, 24, 285, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (222, 205, 24, 286, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (223, 205, 24, 287, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (224, 205, 24, 288, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (225, 205, 24, 289, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (226, 205, 24, 290, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (227, 205, 24, 291, N'test 22', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (228, 205, 24, 292, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (229, 205, 24, 293, N'test 24', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (230, NULL, 25, 294, N'test 1', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (231, NULL, 25, 295, N'test 2', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (232, NULL, 25, 296, N'test 3', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (233, NULL, 25, 297, N'test 4', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (234, NULL, 25, 298, N'test 5', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (235, NULL, 25, 299, N'test 6', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (236, NULL, 25, 300, N'test 7', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (237, NULL, 25, 301, N'test 8', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (238, NULL, 25, 302, N'test 9', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (239, NULL, 25, 303, N'test 10', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (240, NULL, 25, 304, N'test 11', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (241, NULL, 25, 305, N'test 12', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (242, NULL, 25, 306, N'test 13', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (243, NULL, 25, 307, N'test 14', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (244, NULL, 25, 308, N'test 15', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (245, NULL, 25, 309, N'test 16', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (246, NULL, 25, 310, N'test 17', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (247, NULL, 25, 311, N'test 18', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (248, NULL, 25, 312, N'test 19', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (249, NULL, 25, 313, N'test 20', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (250, NULL, 25, 314, N'test 21', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (251, NULL, 25, 315, N'test 22', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (252, NULL, 25, 316, N'test 23', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (253, NULL, 25, 317, N'test 24', 1, 1)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (254, NULL, 26, 322, N'test 1', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (255, NULL, 26, 323, N'test 2', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (256, NULL, 26, 324, N'test 3', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (257, NULL, 26, 325, N'test 4', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (258, NULL, 26, 326, N'test 5', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (259, NULL, 26, 327, N'test 6', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (260, NULL, 26, 328, N'test 7', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (261, NULL, 26, 329, N'test 8', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (262, NULL, 26, 330, N'test 9', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (263, NULL, 26, 331, N'test 10', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (264, NULL, 26, 332, N'test 11', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (265, NULL, 26, 333, N'test 12', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (266, NULL, 26, 334, N'test 13', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (267, NULL, 26, 335, N'test 14', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (268, NULL, 26, 336, N'test 15', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (269, NULL, 26, 337, N'test 16', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (270, NULL, 26, 338, N'test 17', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (271, NULL, 26, 339, N'test 18', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (272, NULL, 26, 340, N'test 19', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (273, NULL, 26, 341, N'test 20', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (274, NULL, 26, 342, N'test 21', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (275, NULL, 26, 343, N'test 22', 1, 0)
GO
print 'Processed 100 total records'
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (276, NULL, 26, 344, N'test 23', 1, 0)
INSERT [dbo].[tblItems] ([ID], [PID], [OrganizationRef], [ResourceRef], [Title], [IsLeaf], [sysState]) VALUES (277, NULL, 26, 345, N'test 24', 1, 0)
SET IDENTITY_INSERT [dbo].[tblItems] OFF
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relUserRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_USER_ROLE] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 1, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 2, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 3, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 4, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 5, 0)
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relUserGroups](
	[UserRef] [int] NOT NULL,
	[GroupRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_relUserGroups_KEY] PRIMARY KEY CLUSTERED 
(
	[UserRef] ASC,
	[GroupRef] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
INSERT [dbo].[relUserGroups] ([UserRef], [GroupRef], [sysState]) VALUES (1, 1, 0)
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCurriculum]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCurriculum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionCurriculum]
	@UserID int,
	@CurriculumOperationID int,   
    @CurriculumID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@CurriculumID = CurriculumRef AND
		@CurriculumOperationID = CurriculumOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCurriculum]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCurriculum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsCurriculum]
    @GroupID int,
    @CurriculumOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@CurriculumOperationID IS NULL) OR (@CurriculumOperationID = CurriculumOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CurriculumRef],parent_prms.[CurriculumOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionStage]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionStage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionStage]
	@UserID int,
	@StageOperationID int,   
    @StageID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@StageID = StageRef AND
		@StageOperationID = StageOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionGroup]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionGroup]
	@UserID int,
	@GroupOperationID int,   
    @GroupID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@GroupID = GroupRef AND
		@GroupOperationID = GroupOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsGroup]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsGroup]
    @GroupID int,
    @GroupOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@GroupOperationID IS NULL) OR (@GroupOperationID = GroupOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[GroupRef],parent_prms.[GroupOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCurriculum]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCurriculum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForCurriculum]
	@UserID int,
	@CurriculumID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CurriculumRef],parent_prms.[CurriculumOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT CurriculumOperationRef from FlatPermissionList		
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCurriculum]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCurriculum]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsCurriculum]
	@UserID int,
	@CurriculumOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CurriculumRef],[CurriculumOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@CurriculumOperationID IS NULL) OR (@CurriculumOperationID = CurriculumOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CurriculumRef],parent_prms.[CurriculumOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsGroup]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsGroup]
	@UserID int,
	@GroupOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@GroupOperationID IS NULL) OR (@GroupOperationID = GroupOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[GroupRef],parent_prms.[GroupOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsStage]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsStage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsStage]
	@UserID int,
	@StageOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@StageOperationID IS NULL) OR (@StageOperationID = StageOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[StageRef],parent_prms.[StageOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsStage]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsStage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsStage]
    @GroupID int,
    @StageOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@StageOperationID IS NULL) OR (@StageOperationID = StageOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[StageRef],parent_prms.[StageOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForGroup]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForGroup]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForGroup]
	@UserID int,
	@GroupID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[GroupRef],[GroupOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[GroupRef],parent_prms.[GroupOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT GroupOperationRef from FlatPermissionList		
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsTheme]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsTheme]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsTheme]
    @GroupID int,
    @ThemeOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@ThemeOperationID IS NULL) OR (@ThemeOperationID = ThemeOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[ThemeRef],parent_prms.[ThemeOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForStage]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForStage]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForStage]
	@UserID int,
	@StageID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[StageRef],[StageOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[StageRef],parent_prms.[StageOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT StageOperationRef from FlatPermissionList		
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsTheme]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsTheme]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsTheme]
	@UserID int,
	@ThemeOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@ThemeOperationID IS NULL) OR (@ThemeOperationID = ThemeOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[ThemeRef],parent_prms.[ThemeOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCourse]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCourse]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionCourse]
	@UserID int,
	@CourseOperationID int,   
    @CourseID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@CourseID = CourseRef AND
		@CourseOperationID = CourseOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionTheme]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionTheme]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_CheckPermissionTheme]
	@UserID int,
	@ThemeOperationID int,   
    @ThemeID int,
	@TargetDate datetime = NULL
AS
BEGIN    
	IF @TargetDate IS NULL
		SET @TargetDate = GETDATE();

	IF	(NOT EXISTS (SELECT ID FROM tblPermissions WHERE 
		@UserID = OwnerUserRef AND
        sysState = 0 AND
		@ThemeID = ThemeRef AND
		@ThemeOperationID = ThemeOperationRef AND
		((DateSince IS NULL) OR (DateSince <= @TargetDate)) AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
	)) RAISERROR (''Not enough permission to perform this operation'', 16, 16);
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCourse]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCourse]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetGroupPermissionsCourse]
    @GroupID int,
    @CourseOperationID int = NULL,
    @TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL
        SET @TargetDate = GETDATE();
    

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]
		FROM tblPermissions 
		WHERE (@GroupID = OwnerGroupRef) AND 
            (sysState = 0) AND 
            ((@CourseOperationID IS NULL) OR (@CourseOperationID = CourseOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CourseRef],parent_prms.[CourseOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForTheme]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForTheme]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForTheme]
	@UserID int,
	@ThemeID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[ThemeRef],[ThemeOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[ThemeRef],parent_prms.[ThemeOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT ThemeOperationRef from FlatPermissionList		
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCourse]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCourse]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetPermissionsCourse]
	@UserID int,
	@CourseOperationID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
    IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 
    
	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((@CourseOperationID IS NULL) OR (@CourseOperationID = CourseOperationRef)) AND
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CourseRef],parent_prms.[CourseOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT * from FlatPermissionList
END' 
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCourse]    Script Date: 01/07/2010 02:55:54 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCourse]') AND type in (N'P', N'PC'))
BEGIN
EXEC dbo.sp_executesql @statement = N'CREATE PROCEDURE [dbo].[Security_GetOperationsForCourse]
	@UserID int,
	@CourseID int = NULL,
	@TargetDate datetime = NULL
AS
BEGIN
	IF @TargetDate IS NULL 
		SET @TargetDate = GETDATE(); 

	WITH FlatPermissionList ([ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]) AS
	(
		SELECT [ID],[ParentPermitionRef],[DateSince],[DateTill],[OwnerUserRef],[OwnerGroupRef],[CanBeDelagated],[CourseRef],[CourseOperationRef]
		FROM tblPermissions 
		WHERE ((@UserID = OwnerUserRef) OR (EXISTS (SELECT * FROM relUserGroups WHERE @UserID = UserRef AND OwnerGroupRef = relUserGroups.GroupRef ))) AND 
            (sysState = 0) AND 
            ((DateSince IS NULL) OR (DateSince <= @TargetDate)) 
            AND ((DateTill IS NULL) OR (DateTill >= @TargetDate))
		
		UNION ALL
		
		SELECT p.[ID],p.[ParentPermitionRef],p.[DateSince],p.[DateTill],p.[OwnerUserRef],p.[OwnerGroupRef],p.[CanBeDelagated],parent_prms.[CourseRef],parent_prms.[CourseOperationRef]
		FROM tblPermissions p
		INNER JOIN FlatPermissionList parent_prms ON p.ParentPermitionRef = parent_prms.ID AND
            (parent_prms.[CanBeDelagated] = 1) AND
            (p.sysState = 0) AND 
            ((p.DateSince IS NULL) OR (p.DateSince <= @TargetDate)) 
            AND ((p.DateTill IS NULL) OR (p.DateTill >= @TargetDate))
	)

    SELECT DISTINCT CourseOperationRef from FlatPermissionList		
END' 
END
GO
/****** Object:  Default [DF__fxAnswerT__sysSt__0C85DE4D]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxAnswerT__sysSt__0C85DE4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxAnswerType]'))
Begin
ALTER TABLE [dbo].[fxAnswerType] ADD  CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxCompile__sysSt__73BA3083]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCompile__sysSt__73BA3083]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]'))
Begin
ALTER TABLE [dbo].[fxCompiledStatuses] ADD  CONSTRAINT [DF__fxCompile__sysSt__73BA3083]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxCourseO__sysSt__72C60C4A]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCourseO__sysSt__72C60C4A]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]'))
Begin
ALTER TABLE [dbo].[fxCourseOperations] ADD  CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxCurricu__sysSt__71D1E811]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCurricu__sysSt__71D1E811]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]'))
Begin
ALTER TABLE [dbo].[fxCurriculumOperations] ADD  CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxGroupOp__sysSt__03F0984C]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxGroupOp__sysSt__03F0984C]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]'))
Begin
ALTER TABLE [dbo].[fxGroupOperations] ADD  CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxLanguag__sysSt__70DDC3D8]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxLanguag__sysSt__70DDC3D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxLanguages]'))
Begin
ALTER TABLE [dbo].[fxLanguages] ADD  CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxPageOpe__sysSt__6FE99F9F]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOpe__sysSt__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOperations]'))
Begin
ALTER TABLE [dbo].[fxPageOperations] ADD  CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxPageOrd__sysSt__6EF57B66]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOrd__sysSt__6EF57B66]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOrders]'))
Begin
ALTER TABLE [dbo].[fxPageOrders] ADD  CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxPageTyp__sysSt__6E01572D]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageTyp__sysSt__6E01572D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageTypes]'))
Begin
ALTER TABLE [dbo].[fxPageTypes] ADD  CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxRoles__sysStat__6D0D32F4]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxRoles__sysStat__6D0D32F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxRoles]'))
Begin
ALTER TABLE [dbo].[fxRoles] ADD  CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxSampleB__sysSt__6A30C649]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxSampleB__sysSt__6A30C649]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]'))
Begin
ALTER TABLE [dbo].[fxSampleBusinesObjectOperation] ADD  CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxStageOp__sysSt__6C190EBB]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxStageOp__sysSt__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxStageOperations]'))
Begin
ALTER TABLE [dbo].[fxStageOperations] ADD  CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__fxThemeOp__sysSt__6B24EA82]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxThemeOp__sysSt__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]'))
Begin
ALTER TABLE [dbo].[fxThemeOperations] ADD  CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF_relResourcesDependency_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_relResourcesDependency_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]'))
Begin
ALTER TABLE [dbo].[relResourcesDependency] ADD  CONSTRAINT [DF_relResourcesDependency_sysState]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF_relResourcesFiles_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_relResourcesFiles_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]'))
Begin
ALTER TABLE [dbo].[relResourcesFiles] ADD  CONSTRAINT [DF_relResourcesFiles_sysState]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__relUserGr__sysSt__02FC7413]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserGr__sysSt__02FC7413]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
Begin
ALTER TABLE [dbo].[relUserGroups] ADD  CONSTRAINT [DF__relUserGr__sysSt__02FC7413]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__relUserRo__sysSt__02084FDA]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserRo__sysSt__02084FDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
Begin
ALTER TABLE [dbo].[relUserRoles] ADD  CONSTRAINT [DF__relUserRo__sysSt__02084FDA]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblCompil__sysSt__76969D2E]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__76969D2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__sysSt__76969D2E]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblCompil__UserA__04E4BC85]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__UserA__04E4BC85]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__UserA__04E4BC85]  DEFAULT ((0)) FOR [UserAnswerRef]

End
GO
/****** Object:  Default [DF__tblCompil__Compi__08B54D69]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__Compi__08B54D69]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__Compi__08B54D69]  DEFAULT ((0)) FOR [CompiledQuestionsDataRef]

End
GO
/****** Object:  Default [DF__tblCompil__sysSt__778AC167]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__778AC167]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
Begin
ALTER TABLE [dbo].[tblCompiledQuestions] ADD  CONSTRAINT [DF__tblCompil__sysSt__778AC167]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblCompil__sysSt__7D439ABD]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__7D439ABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
Begin
ALTER TABLE [dbo].[tblCompiledQuestionsData] ADD  CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblCourse__sysSt__75A278F5]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCourse__sysSt__75A278F5]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCourses]'))
Begin
ALTER TABLE [dbo].[tblCourses] ADD  CONSTRAINT [DF__tblCourse__sysSt__75A278F5]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblCurric__sysSt__74AE54BC]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCurric__sysSt__74AE54BC]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCurriculums]'))
Begin
ALTER TABLE [dbo].[tblCurriculums] ADD  CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF_tblFiles_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblFiles_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblFiles]'))
Begin
ALTER TABLE [dbo].[tblFiles] ADD  CONSTRAINT [DF_tblFiles_sysState]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblGroups__sysSt__693CA210]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblGroups__sysSt__693CA210]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblGroups]'))
Begin
ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF__tblGroups__sysSt__693CA210]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF_tblItems_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblItems_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
Begin
ALTER TABLE [dbo].[tblItems] ADD  CONSTRAINT [DF_tblItems_sysState]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF_tblOrganizations_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblOrganizations_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
Begin
ALTER TABLE [dbo].[tblOrganizations] ADD  CONSTRAINT [DF_tblOrganizations_sysState]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblPages__sysSta__7C4F7684]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPages__sysSta__7C4F7684]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
Begin
ALTER TABLE [dbo].[tblPages] ADD  CONSTRAINT [DF__tblPages__sysSta__7C4F7684]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblPermis__sysSt__7B5B524B]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPermis__sysSt__7B5B524B]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
Begin
ALTER TABLE [dbo].[tblPermissions] ADD  CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblQuesti__sysSt__7E37BEF6]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblQuesti__sysSt__7E37BEF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
Begin
ALTER TABLE [dbo].[tblQuestions] ADD  CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF_tblResources_sysState]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblResources_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
Begin
ALTER TABLE [dbo].[tblResources] ADD  CONSTRAINT [DF_tblResources_sysState]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblSample__sysSt__68487DD7]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblSample__sysSt__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]'))
Begin
ALTER TABLE [dbo].[tblSampleBusinesObject] ADD  CONSTRAINT [DF__tblSample__sysSt__68487DD7]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblStages__sysSt__787EE5A0]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblStages__sysSt__787EE5A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
Begin
ALTER TABLE [dbo].[tblStages] ADD  CONSTRAINT [DF__tblStages__sysSt__787EE5A0]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblThemes__sysSt__797309D9]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__sysSt__797309D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__sysSt__797309D9]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblThemes__PageC__06CD04F7]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__PageC__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__PageC__06CD04F7]  DEFAULT (NULL) FOR [PageCountToShow]

End
GO
/****** Object:  Default [DF__tblThemes__MaxCo__07C12930]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__MaxCo__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__MaxCo__07C12930]  DEFAULT (NULL) FOR [MaxCountToSubmit]

End
GO
/****** Object:  Default [DF__tblUserAn__sysSt__01142BA1]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__sysSt__01142BA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  Default [DF__tblUserAn__Answe__0D7A0286]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__Answe__0D7A0286]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]  DEFAULT ((1)) FOR [AnswerTypeRef]

End
GO
/****** Object:  Default [DF__tblUsers__sysSta__6754599E]    Script Date: 01/07/2010 02:55:54 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUsers__sysSta__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
Begin
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF__tblUsers__sysSta__6754599E]  DEFAULT ((0)) FOR [sysState]

End
GO
/****** Object:  ForeignKey [FK_relResourcesDependency_tblResources_Dependant]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesDependency_tblResources_Dependant]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]'))
ALTER TABLE [dbo].[relResourcesDependency]  WITH CHECK ADD  CONSTRAINT [FK_relResourcesDependency_tblResources_Dependant] FOREIGN KEY([DependantRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[relResourcesDependency] CHECK CONSTRAINT [FK_relResourcesDependency_tblResources_Dependant]
GO
/****** Object:  ForeignKey [FK_relResourcesDependency_tblResources_Dependency]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesDependency_tblResources_Dependency]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesDependency]'))
ALTER TABLE [dbo].[relResourcesDependency]  WITH CHECK ADD  CONSTRAINT [FK_relResourcesDependency_tblResources_Dependency] FOREIGN KEY([DependencyRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[relResourcesDependency] CHECK CONSTRAINT [FK_relResourcesDependency_tblResources_Dependency]
GO
/****** Object:  ForeignKey [FK_relResourcesFiles_tblFiles]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesFiles_tblFiles]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]'))
ALTER TABLE [dbo].[relResourcesFiles]  WITH CHECK ADD  CONSTRAINT [FK_relResourcesFiles_tblFiles] FOREIGN KEY([FileRef])
REFERENCES [dbo].[tblFiles] ([ID])
GO
ALTER TABLE [dbo].[relResourcesFiles] CHECK CONSTRAINT [FK_relResourcesFiles_tblFiles]
GO
/****** Object:  ForeignKey [FK_relResourcesFiles_tblResources]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_relResourcesFiles_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[relResourcesFiles]'))
ALTER TABLE [dbo].[relResourcesFiles]  WITH CHECK ADD  CONSTRAINT [FK_relResourcesFiles_tblResources] FOREIGN KEY([ResourceRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[relResourcesFiles] CHECK CONSTRAINT [FK_relResourcesFiles_tblResources]
GO
/****** Object:  ForeignKey [FK_GROUP]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_GROUP] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_GROUP]
GO
/****** Object:  ForeignKey [FK_USER]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_USER] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_USER]
GO
/****** Object:  ForeignKey [FK_ROLE_ID]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_ID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[fxRoles] ([ID])
GO
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_ROLE_ID]
GO
/****** Object:  ForeignKey [FK_USER_ID]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_USER_ID] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_USER_ID]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_fxdCompiledStatuses]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses] FOREIGN KEY([StatusRef])
REFERENCES [dbo].[fxCompiledStatuses] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblCompiledQuestionsData]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData] FOREIGN KEY([CompiledQuestionsDataRef])
REFERENCES [dbo].[tblCompiledQuestionsData] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblUserAnswers]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers] FOREIGN KEY([UserAnswerRef])
REFERENCES [dbo].[tblUserAnswers] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestions_fxdLanguages]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages] FOREIGN KEY([LanguageRef])
REFERENCES [dbo].[fxLanguages] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledQuestions] CHECK CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestionsData_tblCompiledQuestions]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledQuestionsData] CHECK CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblItems_tblItems]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblItems] FOREIGN KEY([PID])
REFERENCES [dbo].[tblItems] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblItems]
GO
/****** Object:  ForeignKey [FK_tblItems_tblOrganizations]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblOrganizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblOrganizations]
GO
/****** Object:  ForeignKey [FK_tblItems_tblResources]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblResources] FOREIGN KEY([ResourceRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblResources]
GO
/****** Object:  ForeignKey [FK_tblOrganizations_tblCourses]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_tblOrganizations_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblOrganizations] CHECK CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
/****** Object:  ForeignKey [FK_Page_PageType]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages]  WITH CHECK ADD  CONSTRAINT [FK_Page_PageType] FOREIGN KEY([PageTypeRef])
REFERENCES [dbo].[fxPageTypes] ([ID])
GO
ALTER TABLE [dbo].[tblPages] CHECK CONSTRAINT [FK_Page_PageType]
GO
/****** Object:  ForeignKey [FK_PARENT_PERMITION]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_PARENT_PERMITION] FOREIGN KEY([ParentPermitionRef])
REFERENCES [dbo].[tblPermissions] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_PARENT_PERMITION]
GO
/****** Object:  ForeignKey [FK_Permissions_CourseOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CourseOperations] FOREIGN KEY([CourseOperationRef])
REFERENCES [dbo].[fxCourseOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CourseOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Courses]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Courses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Courses]
GO
/****** Object:  ForeignKey [FK_Permissions_CurriculumOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CurriculumOperations] FOREIGN KEY([CurriculumOperationRef])
REFERENCES [dbo].[fxCurriculumOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Curriculums]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Curriculums] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Curriculums]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupObjects]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupObjects] FOREIGN KEY([GroupObjectRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupObjects]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupOperations] FOREIGN KEY([GroupOperationRef])
REFERENCES [dbo].[fxGroupOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Groups]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Groups] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Groups]
GO
/****** Object:  ForeignKey [FK_Permissions_Organizations]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Organizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Organizations]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerGroup]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerGroup] FOREIGN KEY([OwnerGroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerGroup]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerUser]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerUser] FOREIGN KEY([OwnerUserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerUser]
GO
/****** Object:  ForeignKey [FK_Permissions_PageOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PageOperations] FOREIGN KEY([PageOperationRef])
REFERENCES [dbo].[fxPageOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_PageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Pages]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Pages] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Pages]
GO
/****** Object:  ForeignKey [FK_Permissions_StageOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_StageOperations] FOREIGN KEY([StageOperationRef])
REFERENCES [dbo].[fxStageOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_StageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Stages]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Stages] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Stages]
GO
/****** Object:  ForeignKey [FK_Permissions_ThemeOperations]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_ThemeOperations] FOREIGN KEY([ThemeOperationRef])
REFERENCES [dbo].[fxThemeOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_ThemeOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Themes]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Themes] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[tblThemes] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Themes]
GO
/****** Object:  ForeignKey [FK_Permissions_UserObjects]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_UserObjects] FOREIGN KEY([UserObjectRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_UserObjects]
GO
/****** Object:  ForeignKey [FK_CorrectAnswer_Page]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_CorrectAnswer_Page] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_CorrectAnswer_Page]
GO
/****** Object:  ForeignKey [FK_tblQuestions_tblCompiledQuestions]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestions_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblResources_tblCourses]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources]  WITH CHECK ADD  CONSTRAINT [FK_tblResources_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblResources] CHECK CONSTRAINT [FK_tblResources_tblCourses]
GO
/****** Object:  ForeignKey [FK_Curriculums_Stages]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages]  WITH CHECK ADD  CONSTRAINT [FK_Curriculums_Stages] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
ALTER TABLE [dbo].[tblStages] CHECK CONSTRAINT [FK_Curriculums_Stages]
GO
/****** Object:  ForeignKey [FK_Organizations_Themes]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Themes] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Organizations_Themes]
GO
/****** Object:  ForeignKey [FK_Stages_Themes]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Stages_Themes] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Stages_Themes]
GO
/****** Object:  ForeignKey [FK_Themes_Course]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Themes_Course] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Themes_Course]
GO
/****** Object:  ForeignKey [FK_tblUserAnswers_AnswerTypeRef]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef] FOREIGN KEY([AnswerTypeRef])
REFERENCES [dbo].[fxAnswerType] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
/****** Object:  ForeignKey [FK_UserAnswer_CorrectAnswer]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswer_CorrectAnswer] FOREIGN KEY([QuestionRef])
REFERENCES [dbo].[tblQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
/****** Object:  ForeignKey [FK_UserAnswers_Users]    Script Date: 01/07/2010 02:55:54 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswers_Users] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswers_Users]
GO
