/****** Object:  ForeignKey [FK_GROUP]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [FK_GROUP]
GO
/****** Object:  ForeignKey [FK_USER]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [FK_USER]
GO
/****** Object:  ForeignKey [FK_ROLE_ID]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [FK_ROLE_ID]
GO
/****** Object:  ForeignKey [FK_USER_ID]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [FK_USER_ID]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_fxdCompiledStatuses]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblCompiledQuestionsData]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblUserAnswers]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestions_fxdLanguages]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions] DROP CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestionsData_tblCompiledQuestions]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData] DROP CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblItems_tblItems]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblItems]
GO
/****** Object:  ForeignKey [FK_tblItems_tblOrganizations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblOrganizations]
GO
/****** Object:  ForeignKey [FK_tblItems_tblResources]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblResources]
GO
/****** Object:  ForeignKey [FK_tblItems_tblLearnerSessions]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] DROP CONSTRAINT [FK_tblItems_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblLearnerAttempts_tblLearnerSessions]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] DROP CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblOrganizations_tblCourses]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations] DROP CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
/****** Object:  ForeignKey [FK_Page_PageType]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages] DROP CONSTRAINT [FK_Page_PageType]
GO
/****** Object:  ForeignKey [FK_PARENT_PERMITION]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_PARENT_PERMITION]
GO
/****** Object:  ForeignKey [FK_Permissions_CourseOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_CourseOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Courses]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Courses]
GO
/****** Object:  ForeignKey [FK_Permissions_CurriculumOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Curriculums]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Curriculums]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupObjects]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_GroupObjects]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_GroupOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Groups]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Groups]
GO
/****** Object:  ForeignKey [FK_Permissions_Organizations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Organizations]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerGroup]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_OwnerGroup]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerUser]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_OwnerUser]
GO
/****** Object:  ForeignKey [FK_Permissions_PageOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_PageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Pages]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Pages]
GO
/****** Object:  ForeignKey [FK_Permissions_StageOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_StageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Stages]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Stages]
GO
/****** Object:  ForeignKey [FK_Permissions_ThemeOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_ThemeOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Themes]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Themes]
GO
/****** Object:  ForeignKey [FK_Permissions_UserObjects]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_UserObjects]
GO
/****** Object:  ForeignKey [FK_CorrectAnswer_Page]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [FK_CorrectAnswer_Page]
GO
/****** Object:  ForeignKey [FK_tblQuestions_tblCompiledQuestions]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblResources_tblCourses]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources] DROP CONSTRAINT [FK_tblResources_tblCourses]
GO
/****** Object:  ForeignKey [FK_Curriculums_Stages]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages] DROP CONSTRAINT [FK_Curriculums_Stages]
GO
/****** Object:  ForeignKey [FK_Organizations_Themes]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Organizations_Themes]
GO
/****** Object:  ForeignKey [FK_Stages_Themes]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Stages_Themes]
GO
/****** Object:  ForeignKey [FK_Themes_Course]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Themes_Course]
GO
/****** Object:  ForeignKey [FK_tblUserAnswers_AnswerTypeRef]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
/****** Object:  ForeignKey [FK_UserAnswer_CorrectAnswer]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
/****** Object:  ForeignKey [FK_UserAnswers_Users]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_UserAnswers_Users]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblComputers]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] DROP CONSTRAINT [FK_tblUsersSignIn_tblComputers]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblUsers]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] DROP CONSTRAINT [FK_tblUsersSignIn_tblUsers]
GO
/****** Object:  ForeignKey [FK_tblVars_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars] DROP CONSTRAINT [FK_tblVars_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses] DROP CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionObjectives_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives] DROP CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractions_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions] DROP CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsScore_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore] DROP CONSTRAINT [FK_tblVarsScore_tblLearnerSessions]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsTheme]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionTheme]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsStage]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionStage]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCourse]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCourse]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCurriculum]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCurriculum]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsGroup]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionGroup]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCurriculum]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsStage]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsGroup]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCurriculum]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCourse]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForGroup]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCourse]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForStage]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForTheme]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsTheme]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsTheme]
GO
/****** Object:  Table [dbo].[tblVarsScore]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsScore]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsScore]
GO
/****** Object:  Table [dbo].[tblVars]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVars]') AND type in (N'U'))
DROP TABLE [dbo].[tblVars]
GO
/****** Object:  Table [dbo].[tblVarsInteractionCorrectResponses]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsInteractionCorrectResponses]
GO
/****** Object:  Table [dbo].[tblVarsInteractionObjectives]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsInteractionObjectives]
GO
/****** Object:  Table [dbo].[tblVarsInteractions]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsInteractions]
GO
/****** Object:  Table [dbo].[tblLearnerSessions]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]') AND type in (N'U'))
DROP TABLE [dbo].[tblLearnerSessions]
GO
/****** Object:  Table [dbo].[tblItems]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblItems]') AND type in (N'U'))
DROP TABLE [dbo].[tblItems]
GO
/****** Object:  Table [dbo].[tblResources]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResources]') AND type in (N'U'))
DROP TABLE [dbo].[tblResources]
GO
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserGroups]') AND type in (N'U'))
DROP TABLE [dbo].[relUserGroups]
GO
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledAnswers]
GO
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledQuestionsData]
GO
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]') AND type in (N'U'))
DROP TABLE [dbo].[tblUserAnswers]
GO
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuestions]') AND type in (N'U'))
DROP TABLE [dbo].[tblQuestions]
GO
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledQuestions]
GO
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[relUserRoles]
GO
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPermissions]') AND type in (N'U'))
DROP TABLE [dbo].[tblPermissions]
GO
/****** Object:  Table [dbo].[tblThemes]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThemes]') AND type in (N'U'))
DROP TABLE [dbo].[tblThemes]
GO
/****** Object:  Table [dbo].[tblStages]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStages]') AND type in (N'U'))
DROP TABLE [dbo].[tblStages]
GO
/****** Object:  Table [dbo].[tblPages]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPages]') AND type in (N'U'))
DROP TABLE [dbo].[tblPages]
GO
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOrganizations]') AND type in (N'U'))
DROP TABLE [dbo].[tblOrganizations]
GO
/****** Object:  Table [dbo].[tblUsersSignIn]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]') AND type in (N'U'))
DROP TABLE [dbo].[tblUsersSignIn]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
DROP TABLE [dbo].[tblUsers]
GO
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]') AND type in (N'U'))
DROP TABLE [dbo].[tblSampleBusinesObject]
GO
/****** Object:  Table [dbo].[tblLearnerAttempts]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]') AND type in (N'U'))
DROP TABLE [dbo].[tblLearnerAttempts]
GO
/****** Object:  Table [dbo].[tblCourses]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCourses]') AND type in (N'U'))
DROP TABLE [dbo].[tblCourses]
GO
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCurriculums]') AND type in (N'U'))
DROP TABLE [dbo].[tblCurriculums]
GO
/****** Object:  Table [dbo].[tblGroups]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGroups]') AND type in (N'U'))
DROP TABLE [dbo].[tblGroups]
GO
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxAnswerType]') AND type in (N'U'))
DROP TABLE [dbo].[fxAnswerType]
GO
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]') AND type in (N'U'))
DROP TABLE [dbo].[fxCompiledStatuses]
GO
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxCourseOperations]
GO
/****** Object:  Table [dbo].[tblSettings]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
DROP TABLE [dbo].[tblSettings]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSecurityID]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSecurityID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetSecurityID]
GO
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxCurriculumOperations]
GO
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxGroupOperations]
GO
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxLanguages]') AND type in (N'U'))
DROP TABLE [dbo].[fxLanguages]
GO
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageOperations]
GO
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOrders]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageOrders]
GO
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageTypes]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageTypes]
GO
/****** Object:  Table [dbo].[fxRoles]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxRoles]') AND type in (N'U'))
DROP TABLE [dbo].[fxRoles]
GO
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]') AND type in (N'U'))
DROP TABLE [dbo].[fxSampleBusinesObjectOperation]
GO
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxStageOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxStageOperations]
GO
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxThemeOperations]
GO
/****** Object:  Table [dbo].[tblComputers]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblComputers]') AND type in (N'U'))
DROP TABLE [dbo].[tblComputers]
GO
/****** Object:  Table [dbo].[tblUserNotes]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserNotes]') AND type in (N'U'))
DROP TABLE [dbo].[tblUserNotes]
GO
/****** Object:  Default [DF__fxAnswerT__sysSt__0C85DE4D]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxAnswerT__sysSt__0C85DE4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxAnswerType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxAnswerT__sysSt__0C85DE4D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxAnswerType] DROP CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]
END


End
GO
/****** Object:  Default [DF__fxCompile__sysSt__73BA3083]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCompile__sysSt__73BA3083]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCompile__sysSt__73BA3083]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCompiledStatuses] DROP CONSTRAINT [DF__fxCompile__sysSt__73BA3083]
END


End
GO
/****** Object:  Default [DF__fxCourseO__sysSt__72C60C4A]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCourseO__sysSt__72C60C4A]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCourseO__sysSt__72C60C4A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCourseOperations] DROP CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]
END


End
GO
/****** Object:  Default [DF__fxCurricu__sysSt__71D1E811]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCurricu__sysSt__71D1E811]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCurricu__sysSt__71D1E811]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCurriculumOperations] DROP CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]
END


End
GO
/****** Object:  Default [DF__fxGroupOp__sysSt__03F0984C]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxGroupOp__sysSt__03F0984C]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxGroupOp__sysSt__03F0984C]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxGroupOperations] DROP CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]
END


End
GO
/****** Object:  Default [DF__fxLanguag__sysSt__70DDC3D8]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxLanguag__sysSt__70DDC3D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxLanguages]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxLanguag__sysSt__70DDC3D8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxLanguages] DROP CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]
END


End
GO
/****** Object:  Default [DF__fxPageOpe__sysSt__6FE99F9F]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOpe__sysSt__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOpe__sysSt__6FE99F9F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOperations] DROP CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]
END


End
GO
/****** Object:  Default [DF__fxPageOrd__sysSt__6EF57B66]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOrd__sysSt__6EF57B66]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOrders]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOrd__sysSt__6EF57B66]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOrders] DROP CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]
END


End
GO
/****** Object:  Default [DF__fxPageTyp__sysSt__6E01572D]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageTyp__sysSt__6E01572D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageTypes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageTyp__sysSt__6E01572D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageTypes] DROP CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]
END


End
GO
/****** Object:  Default [DF__fxRoles__sysStat__6D0D32F4]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxRoles__sysStat__6D0D32F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxRoles]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxRoles__sysStat__6D0D32F4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxRoles] DROP CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]
END


End
GO
/****** Object:  Default [DF__fxSampleB__sysSt__6A30C649]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxSampleB__sysSt__6A30C649]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxSampleB__sysSt__6A30C649]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxSampleBusinesObjectOperation] DROP CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]
END


End
GO
/****** Object:  Default [DF__fxStageOp__sysSt__6C190EBB]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxStageOp__sysSt__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxStageOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxStageOp__sysSt__6C190EBB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxStageOperations] DROP CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]
END


End
GO
/****** Object:  Default [DF__fxThemeOp__sysSt__6B24EA82]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxThemeOp__sysSt__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxThemeOp__sysSt__6B24EA82]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxThemeOperations] DROP CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]
END


End
GO
/****** Object:  Default [DF__relUserGr__sysSt__02FC7413]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserGr__sysSt__02FC7413]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserGr__sysSt__02FC7413]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [DF__relUserGr__sysSt__02FC7413]
END


End
GO
/****** Object:  Default [DF__relUserRo__sysSt__02084FDA]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserRo__sysSt__02084FDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserRo__sysSt__02084FDA]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [DF__relUserRo__sysSt__02084FDA]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__76969D2E]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__76969D2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__76969D2E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__sysSt__76969D2E]
END


End
GO
/****** Object:  Default [DF__tblCompil__UserA__04E4BC85]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__UserA__04E4BC85]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__UserA__04E4BC85]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__UserA__04E4BC85]
END


End
GO
/****** Object:  Default [DF__tblCompil__Compi__08B54D69]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__Compi__08B54D69]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__Compi__08B54D69]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__Compi__08B54D69]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__778AC167]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__778AC167]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__778AC167]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestions] DROP CONSTRAINT [DF__tblCompil__sysSt__778AC167]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__7D439ABD]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__7D439ABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__7D439ABD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestionsData] DROP CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]
END


End
GO
/****** Object:  Default [DF__tblCourse__sysSt__75A278F5]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCourse__sysSt__75A278F5]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCourses]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCourse__sysSt__75A278F5]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCourses] DROP CONSTRAINT [DF__tblCourse__sysSt__75A278F5]
END


End
GO
/****** Object:  Default [DF__tblCurric__sysSt__74AE54BC]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCurric__sysSt__74AE54BC]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCurriculums]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCurric__sysSt__74AE54BC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCurriculums] DROP CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]
END


End
GO
/****** Object:  Default [DF__tblGroups__sysSt__693CA210]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblGroups__sysSt__693CA210]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblGroups]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblGroups__sysSt__693CA210]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblGroups] DROP CONSTRAINT [DF__tblGroups__sysSt__693CA210]
END


End
GO
/****** Object:  Default [DF_tblItems_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblItems_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblItems_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [DF_tblItems_sysState]
END


End
GO
/****** Object:  Default [DF_tblLearnerAttempts_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblLearnerAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblLearnerAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerAttempts] DROP CONSTRAINT [DF_tblLearnerAttempts_sysState]
END


End
GO
/****** Object:  Default [DF_tblAttempts_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerSessions] DROP CONSTRAINT [DF_tblAttempts_sysState]
END


End
GO
/****** Object:  Default [DF_tblOrganizations_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblOrganizations_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblOrganizations_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblOrganizations] DROP CONSTRAINT [DF_tblOrganizations_sysState]
END


End
GO
/****** Object:  Default [DF__tblPages__sysSta__7C4F7684]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPages__sysSta__7C4F7684]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPages__sysSta__7C4F7684]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPages] DROP CONSTRAINT [DF__tblPages__sysSta__7C4F7684]
END


End
GO
/****** Object:  Default [DF__tblPermis__sysSt__7B5B524B]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPermis__sysSt__7B5B524B]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPermis__sysSt__7B5B524B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]
END


End
GO
/****** Object:  Default [DF__tblQuesti__sysSt__7E37BEF6]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblQuesti__sysSt__7E37BEF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblQuesti__sysSt__7E37BEF6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]
END


End
GO
/****** Object:  Default [DF_tblResources_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblResources_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblResources_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblResources] DROP CONSTRAINT [DF_tblResources_sysState]
END


End
GO
/****** Object:  Default [DF__tblSample__sysSt__68487DD7]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblSample__sysSt__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblSample__sysSt__68487DD7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSampleBusinesObject] DROP CONSTRAINT [DF__tblSample__sysSt__68487DD7]
END


End
GO
/****** Object:  Default [DF_tblSettings_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] DROP CONSTRAINT [DF_tblSettings_sysState]
END


End
GO
/****** Object:  Default [DF__tblStages__sysSt__787EE5A0]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblStages__sysSt__787EE5A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblStages__sysSt__787EE5A0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblStages] DROP CONSTRAINT [DF__tblStages__sysSt__787EE5A0]
END


End
GO
/****** Object:  Default [DF__tblThemes__sysSt__797309D9]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__sysSt__797309D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__sysSt__797309D9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__sysSt__797309D9]
END


End
GO
/****** Object:  Default [DF__tblThemes__PageC__06CD04F7]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__PageC__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__PageC__06CD04F7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__PageC__06CD04F7]
END


End
GO
/****** Object:  Default [DF__tblThemes__MaxCo__07C12930]    Script Date: 04/19/2010 01:42:31 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__MaxCo__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__MaxCo__07C12930]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__MaxCo__07C12930]
END


End
GO
/****** Object:  Default [DF__tblUserAn__sysSt__01142BA1]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__sysSt__01142BA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__sysSt__01142BA1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]
END


End
GO
/****** Object:  Default [DF__tblUserAn__Answe__0D7A0286]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__Answe__0D7A0286]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__Answe__0D7A0286]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]
END


End
GO
/****** Object:  Default [DF__tblUsers__sysSta__6754599E]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUsers__sysSta__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUsers__sysSta__6754599E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUsers] DROP CONSTRAINT [DF__tblUsers__sysSta__6754599E]
END


End
GO
/****** Object:  Default [DF_tblAttemptsVars_sysState]    Script Date: 04/19/2010 01:42:32 ******/
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttemptsVars_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttemptsVars_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblVars] DROP CONSTRAINT [DF_tblAttemptsVars_sysState]
END


End
GO
/****** Object:  Table [dbo].[tblUserNotes]    Script Date: 04/19/2010 01:42:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserNotes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUserNotes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRef] [int] NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[Date] [datetime] NULL,
	[SysState] [smallint] NOT NULL,
 CONSTRAINT [PK_UserNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblComputers]    Script Date: 04/19/2010 01:42:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblComputers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblComputers](
	[ID] [int] IDENTITY(10000,1) NOT NULL,
	[ComputerName] [nvarchar](100) COLLATE Ukrainian_CI_AS NULL,
	[IP] [nvarchar](15) COLLATE Ukrainian_CI_AS NULL,
	[LectureRoom] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
 CONSTRAINT [PK_tblComputers_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
SET IDENTITY_INSERT [dbo].[tblComputers] ON
INSERT [dbo].[tblComputers] ([ID], [ComputerName], [IP], [LectureRoom]) VALUES (10000, N'', N'127.0.0.1', N'')
SET IDENTITY_INSERT [dbo].[tblComputers] OFF
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxRoles]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  UserDefinedFunction [dbo].[GetSecurityID]    Script Date: 04/19/2010 01:42:32 ******/
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
	RETURN ''847928d1-591b-47ef-8f3e-ab7de9ce69cd'';
END' 
END
GO
/****** Object:  Table [dbo].[tblSettings]    Script Date: 04/19/2010 01:42:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSettings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](250) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL
)
END
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND name = N'PK_Settings')
CREATE UNIQUE CLUSTERED INDEX [PK_Settings] ON [dbo].[tblSettings] 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND name = N'UN_Settings')
CREATE UNIQUE NONCLUSTERED INDEX [UN_Settings] ON [dbo].[tblSettings] 
(
	[Name] ASC,
	[sysState] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
GO
SET IDENTITY_INSERT [dbo].[tblSettings] ON
INSERT [dbo].[tblSettings] ([ID], [Name], [Value], [sysState]) VALUES (4, N'compile_service_url', N'http://localhost:8080/Compile.asp', 1)
INSERT [dbo].[tblSettings] ([ID], [Name], [Value], [sysState]) VALUES (8, N'compile_service_url', N'http://localhost:49440/Service1.asmx/Compile', 0)
SET IDENTITY_INSERT [dbo].[tblSettings] OFF
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblGroups]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblCourses]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblLearnerAttempts]    Script Date: 04/19/2010 01:42:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLearnerAttempts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeRef] [int] NOT NULL,
	[UserRef] [int] NOT NULL,
	[Started] [datetime] NULL,
	[Finished] [datetime] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblLearnerAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblUsers]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  Table [dbo].[tblUsersSignIn]    Script Date: 04/19/2010 01:42:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUsersSignIn](
	[UserId] [int] NOT NULL,
	[ComputerId] [int] NOT NULL,
	[LastLogin] [datetime] NULL,
 CONSTRAINT [PK_tblUsersSignIn_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblPages]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblStages]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblThemes]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblResources]    Script Date: 04/19/2010 01:42:31 ******/
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
/****** Object:  Table [dbo].[tblItems]    Script Date: 04/19/2010 01:42:31 ******/
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
	[Rank] [int] NULL,
 CONSTRAINT [PK_tblItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblLearnerSessions]    Script Date: 04/19/2010 01:42:31 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLearnerSessions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerAttemptRef] [int] NOT NULL,
	[ItemRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblVarsInteractions]    Script Date: 04/19/2010 01:42:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsInteractions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblVarsInteractionObjectives]    Script Date: 04/19/2010 01:42:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractionObjectives](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblVarsInteractionCorrectResponses]    Script Date: 04/19/2010 01:42:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractionCorrectResponses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblVars]    Script Date: 04/19/2010 01:42:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVars]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblAttemptsVars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  Table [dbo].[tblVarsScore]    Script Date: 04/19/2010 01:42:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsScore]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsScore](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsScore] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON)
)
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsTheme]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForTheme]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForStage]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCourse]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForGroup]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCourse]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCurriculum]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsGroup]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsStage]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCurriculum]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionGroup]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsGroup]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCurriculum]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCurriculum]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCourse]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCourse]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionStage]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsStage]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionTheme]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsTheme]    Script Date: 04/19/2010 01:42:32 ******/
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
/****** Object:  Default [DF__fxAnswerT__sysSt__0C85DE4D]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxAnswerT__sysSt__0C85DE4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxAnswerType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxAnswerT__sysSt__0C85DE4D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxAnswerType] ADD  CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxCompile__sysSt__73BA3083]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCompile__sysSt__73BA3083]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCompile__sysSt__73BA3083]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCompiledStatuses] ADD  CONSTRAINT [DF__fxCompile__sysSt__73BA3083]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxCourseO__sysSt__72C60C4A]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCourseO__sysSt__72C60C4A]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCourseO__sysSt__72C60C4A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCourseOperations] ADD  CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxCurricu__sysSt__71D1E811]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCurricu__sysSt__71D1E811]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCurricu__sysSt__71D1E811]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCurriculumOperations] ADD  CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxGroupOp__sysSt__03F0984C]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxGroupOp__sysSt__03F0984C]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxGroupOp__sysSt__03F0984C]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxGroupOperations] ADD  CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxLanguag__sysSt__70DDC3D8]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxLanguag__sysSt__70DDC3D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxLanguages]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxLanguag__sysSt__70DDC3D8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxLanguages] ADD  CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxPageOpe__sysSt__6FE99F9F]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOpe__sysSt__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOpe__sysSt__6FE99F9F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOperations] ADD  CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxPageOrd__sysSt__6EF57B66]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOrd__sysSt__6EF57B66]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOrders]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOrd__sysSt__6EF57B66]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOrders] ADD  CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxPageTyp__sysSt__6E01572D]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageTyp__sysSt__6E01572D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageTypes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageTyp__sysSt__6E01572D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageTypes] ADD  CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxRoles__sysStat__6D0D32F4]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxRoles__sysStat__6D0D32F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxRoles]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxRoles__sysStat__6D0D32F4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxRoles] ADD  CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxSampleB__sysSt__6A30C649]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxSampleB__sysSt__6A30C649]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxSampleB__sysSt__6A30C649]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxSampleBusinesObjectOperation] ADD  CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxStageOp__sysSt__6C190EBB]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxStageOp__sysSt__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxStageOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxStageOp__sysSt__6C190EBB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxStageOperations] ADD  CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxThemeOp__sysSt__6B24EA82]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxThemeOp__sysSt__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxThemeOp__sysSt__6B24EA82]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxThemeOperations] ADD  CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__relUserGr__sysSt__02FC7413]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserGr__sysSt__02FC7413]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserGr__sysSt__02FC7413]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserGroups] ADD  CONSTRAINT [DF__relUserGr__sysSt__02FC7413]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__relUserRo__sysSt__02084FDA]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserRo__sysSt__02084FDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserRo__sysSt__02084FDA]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserRoles] ADD  CONSTRAINT [DF__relUserRo__sysSt__02084FDA]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__76969D2E]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__76969D2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__76969D2E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__sysSt__76969D2E]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCompil__UserA__04E4BC85]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__UserA__04E4BC85]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__UserA__04E4BC85]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__UserA__04E4BC85]  DEFAULT ((0)) FOR [UserAnswerRef]
END


End
GO
/****** Object:  Default [DF__tblCompil__Compi__08B54D69]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__Compi__08B54D69]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__Compi__08B54D69]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__Compi__08B54D69]  DEFAULT ((0)) FOR [CompiledQuestionsDataRef]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__778AC167]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__778AC167]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__778AC167]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestions] ADD  CONSTRAINT [DF__tblCompil__sysSt__778AC167]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__7D439ABD]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__7D439ABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__7D439ABD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestionsData] ADD  CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCourse__sysSt__75A278F5]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCourse__sysSt__75A278F5]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCourses]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCourse__sysSt__75A278F5]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCourses] ADD  CONSTRAINT [DF__tblCourse__sysSt__75A278F5]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCurric__sysSt__74AE54BC]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCurric__sysSt__74AE54BC]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCurriculums]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCurric__sysSt__74AE54BC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCurriculums] ADD  CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblGroups__sysSt__693CA210]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblGroups__sysSt__693CA210]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblGroups]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblGroups__sysSt__693CA210]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF__tblGroups__sysSt__693CA210]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblItems_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblItems_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblItems_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblItems] ADD  CONSTRAINT [DF_tblItems_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblLearnerAttempts_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblLearnerAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblLearnerAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerAttempts] ADD  CONSTRAINT [DF_tblLearnerAttempts_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblAttempts_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerSessions] ADD  CONSTRAINT [DF_tblAttempts_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblOrganizations_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblOrganizations_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblOrganizations_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblOrganizations] ADD  CONSTRAINT [DF_tblOrganizations_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblPages__sysSta__7C4F7684]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPages__sysSta__7C4F7684]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPages__sysSta__7C4F7684]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPages] ADD  CONSTRAINT [DF__tblPages__sysSta__7C4F7684]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblPermis__sysSt__7B5B524B]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPermis__sysSt__7B5B524B]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPermis__sysSt__7B5B524B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPermissions] ADD  CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblQuesti__sysSt__7E37BEF6]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblQuesti__sysSt__7E37BEF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblQuesti__sysSt__7E37BEF6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblQuestions] ADD  CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblResources_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblResources_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblResources_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblResources] ADD  CONSTRAINT [DF_tblResources_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblSample__sysSt__68487DD7]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblSample__sysSt__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblSample__sysSt__68487DD7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSampleBusinesObject] ADD  CONSTRAINT [DF__tblSample__sysSt__68487DD7]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblSettings_sysState]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] ADD  CONSTRAINT [DF_tblSettings_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblStages__sysSt__787EE5A0]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblStages__sysSt__787EE5A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblStages__sysSt__787EE5A0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblStages] ADD  CONSTRAINT [DF__tblStages__sysSt__787EE5A0]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblThemes__sysSt__797309D9]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__sysSt__797309D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__sysSt__797309D9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__sysSt__797309D9]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblThemes__PageC__06CD04F7]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__PageC__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__PageC__06CD04F7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__PageC__06CD04F7]  DEFAULT (NULL) FOR [PageCountToShow]
END


End
GO
/****** Object:  Default [DF__tblThemes__MaxCo__07C12930]    Script Date: 04/19/2010 01:42:31 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__MaxCo__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__MaxCo__07C12930]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__MaxCo__07C12930]  DEFAULT (NULL) FOR [MaxCountToSubmit]
END


End
GO
/****** Object:  Default [DF__tblUserAn__sysSt__01142BA1]    Script Date: 04/19/2010 01:42:32 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__sysSt__01142BA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__sysSt__01142BA1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblUserAn__Answe__0D7A0286]    Script Date: 04/19/2010 01:42:32 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__Answe__0D7A0286]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__Answe__0D7A0286]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]  DEFAULT ((1)) FOR [AnswerTypeRef]
END


End
GO
/****** Object:  Default [DF__tblUsers__sysSta__6754599E]    Script Date: 04/19/2010 01:42:32 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUsers__sysSta__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUsers__sysSta__6754599E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF__tblUsers__sysSta__6754599E]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblAttemptsVars_sysState]    Script Date: 04/19/2010 01:42:32 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttemptsVars_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttemptsVars_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblVars] ADD  CONSTRAINT [DF_tblAttemptsVars_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  ForeignKey [FK_GROUP]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_GROUP] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_GROUP]
GO
/****** Object:  ForeignKey [FK_USER]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_USER] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_USER]
GO
/****** Object:  ForeignKey [FK_ROLE_ID]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_ID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[fxRoles] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_ROLE_ID]
GO
/****** Object:  ForeignKey [FK_USER_ID]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_USER_ID] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_USER_ID]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_fxdCompiledStatuses]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses] FOREIGN KEY([StatusRef])
REFERENCES [dbo].[fxCompiledStatuses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblCompiledQuestionsData]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData] FOREIGN KEY([CompiledQuestionsDataRef])
REFERENCES [dbo].[tblCompiledQuestionsData] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblUserAnswers]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers] FOREIGN KEY([UserAnswerRef])
REFERENCES [dbo].[tblUserAnswers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestions_fxdLanguages]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages] FOREIGN KEY([LanguageRef])
REFERENCES [dbo].[fxLanguages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions] CHECK CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestionsData_tblCompiledQuestions]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData] CHECK CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblItems_tblItems]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblItems] FOREIGN KEY([PID])
REFERENCES [dbo].[tblItems] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblItems]
GO
/****** Object:  ForeignKey [FK_tblItems_tblOrganizations]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblOrganizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblOrganizations]
GO
/****** Object:  ForeignKey [FK_tblItems_tblResources]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblResources] FOREIGN KEY([ResourceRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblResources]
GO
/****** Object:  ForeignKey [FK_tblItems_tblLearnerSessions]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblLearnerSessions] FOREIGN KEY([ItemRef])
REFERENCES [dbo].[tblItems] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblItems_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblLearnerAttempts_tblLearnerSessions]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions] FOREIGN KEY([LearnerAttemptRef])
REFERENCES [dbo].[tblLearnerAttempts] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblOrganizations_tblCourses]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_tblOrganizations_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations] CHECK CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
/****** Object:  ForeignKey [FK_Page_PageType]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages]  WITH CHECK ADD  CONSTRAINT [FK_Page_PageType] FOREIGN KEY([PageTypeRef])
REFERENCES [dbo].[fxPageTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages] CHECK CONSTRAINT [FK_Page_PageType]
GO
/****** Object:  ForeignKey [FK_PARENT_PERMITION]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_PARENT_PERMITION] FOREIGN KEY([ParentPermitionRef])
REFERENCES [dbo].[tblPermissions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_PARENT_PERMITION]
GO
/****** Object:  ForeignKey [FK_Permissions_CourseOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CourseOperations] FOREIGN KEY([CourseOperationRef])
REFERENCES [dbo].[fxCourseOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CourseOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Courses]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Courses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Courses]
GO
/****** Object:  ForeignKey [FK_Permissions_CurriculumOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CurriculumOperations] FOREIGN KEY([CurriculumOperationRef])
REFERENCES [dbo].[fxCurriculumOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Curriculums]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Curriculums] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Curriculums]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupObjects]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupObjects] FOREIGN KEY([GroupObjectRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupObjects]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupOperations] FOREIGN KEY([GroupOperationRef])
REFERENCES [dbo].[fxGroupOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Groups]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Groups] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Groups]
GO
/****** Object:  ForeignKey [FK_Permissions_Organizations]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Organizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Organizations]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerGroup]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerGroup] FOREIGN KEY([OwnerGroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerGroup]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerUser]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerUser] FOREIGN KEY([OwnerUserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerUser]
GO
/****** Object:  ForeignKey [FK_Permissions_PageOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PageOperations] FOREIGN KEY([PageOperationRef])
REFERENCES [dbo].[fxPageOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_PageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Pages]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Pages] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Pages]
GO
/****** Object:  ForeignKey [FK_Permissions_StageOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_StageOperations] FOREIGN KEY([StageOperationRef])
REFERENCES [dbo].[fxStageOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_StageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Stages]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Stages] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Stages]
GO
/****** Object:  ForeignKey [FK_Permissions_ThemeOperations]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_ThemeOperations] FOREIGN KEY([ThemeOperationRef])
REFERENCES [dbo].[fxThemeOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_ThemeOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Themes]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Themes] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[tblThemes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Themes]
GO
/****** Object:  ForeignKey [FK_Permissions_UserObjects]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_UserObjects] FOREIGN KEY([UserObjectRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_UserObjects]
GO
/****** Object:  ForeignKey [FK_CorrectAnswer_Page]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_CorrectAnswer_Page] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_CorrectAnswer_Page]
GO
/****** Object:  ForeignKey [FK_tblQuestions_tblCompiledQuestions]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestions_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblResources_tblCourses]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources]  WITH CHECK ADD  CONSTRAINT [FK_tblResources_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources] CHECK CONSTRAINT [FK_tblResources_tblCourses]
GO
/****** Object:  ForeignKey [FK_Curriculums_Stages]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages]  WITH CHECK ADD  CONSTRAINT [FK_Curriculums_Stages] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages] CHECK CONSTRAINT [FK_Curriculums_Stages]
GO
/****** Object:  ForeignKey [FK_Organizations_Themes]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Themes] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Organizations_Themes]
GO
/****** Object:  ForeignKey [FK_Stages_Themes]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Stages_Themes] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Stages_Themes]
GO
/****** Object:  ForeignKey [FK_Themes_Course]    Script Date: 04/19/2010 01:42:31 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Themes_Course] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Themes_Course]
GO
/****** Object:  ForeignKey [FK_tblUserAnswers_AnswerTypeRef]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef] FOREIGN KEY([AnswerTypeRef])
REFERENCES [dbo].[fxAnswerType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
/****** Object:  ForeignKey [FK_UserAnswer_CorrectAnswer]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswer_CorrectAnswer] FOREIGN KEY([QuestionRef])
REFERENCES [dbo].[tblQuestions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
/****** Object:  ForeignKey [FK_UserAnswers_Users]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswers_Users] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswers_Users]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblComputers]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn]  WITH CHECK ADD  CONSTRAINT [FK_tblUsersSignIn_tblComputers] FOREIGN KEY([ComputerId])
REFERENCES [dbo].[tblComputers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] CHECK CONSTRAINT [FK_tblUsersSignIn_tblComputers]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblUsers]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn]  WITH CHECK ADD  CONSTRAINT [FK_tblUsersSignIn_tblUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] CHECK CONSTRAINT [FK_tblUsersSignIn_tblUsers]
GO
/****** Object:  ForeignKey [FK_tblVars_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars]  WITH CHECK ADD  CONSTRAINT [FK_tblVars_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars] CHECK CONSTRAINT [FK_tblVars_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses] CHECK CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionObjectives_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives] CHECK CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractions_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions] CHECK CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsScore_tblLearnerSessions]    Script Date: 04/19/2010 01:42:32 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsScore_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore] CHECK CONSTRAINT [FK_tblVarsScore_tblLearnerSessions]
GO
USE [IUDICO]
GO
/****** Object:  Table [dbo].[tblUserNotes]    Script Date: 04/22/2010 12:24:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserNotes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUserNotes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRef] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[SysState] [smallint] NOT NULL,
 CONSTRAINT [PK_UserNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblComputers]    Script Date: 04/22/2010 12:24:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblComputers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblComputers](
	[ID] [int] IDENTITY(10000,1) NOT NULL,
	[ComputerName] [nvarchar](100) NULL,
	[IP] [nvarchar](15) NULL,
	[LectureRoom] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblComputers_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 04/22/2010 12:24:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxThemeOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]  DEFAULT ((0)),
 CONSTRAINT [PK_ThemeOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 04/22/2010 12:24:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxStageOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxStageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]  DEFAULT ((0)),
 CONSTRAINT [PK_StageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 04/22/2010 12:24:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxSampleBusinesObjectOperation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]  DEFAULT ((0)),
 CONSTRAINT [UQ__fxSampleBusinesO__023D5A04] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxRoles]    Script Date: 04/22/2010 12:24:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]  DEFAULT ((0)),
 CONSTRAINT [PK_fxdRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 04/22/2010 12:24:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](10) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]  DEFAULT ((0)),
 CONSTRAINT [PK_PageType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 04/22/2010 12:24:17 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOrders]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]  DEFAULT ((0)),
 CONSTRAINT [PK_fxdPageOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]  DEFAULT ((0)),
 CONSTRAINT [PK_PageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxLanguages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxLanguages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]  DEFAULT ((0)),
 CONSTRAINT [PK_fxdLanguages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxGroupOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]  DEFAULT ((0)),
 CONSTRAINT [PK_fxGroupOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_fxGroupOperations_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCurriculumOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]  DEFAULT ((0)),
 CONSTRAINT [PK_CurriculumOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblSettings]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSettings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Value] [varchar](250) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_tblSettings_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblSettings] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO

/****** Object:  Index [PK_Settings]    Script Date: 04/22/2010 12:24:18 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND name = N'PK_Settings')
CREATE UNIQUE CLUSTERED INDEX [PK_Settings] ON [dbo].[tblSettings] 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO

/****** Object:  Index [UN_Settings]    Script Date: 04/22/2010 12:24:18 ******/
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND name = N'UN_Settings')
CREATE UNIQUE NONCLUSTERED INDEX [UN_Settings] ON [dbo].[tblSettings] 
(
	[Name] ASC,
	[sysState] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCourseOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]  DEFAULT ((0)),
 CONSTRAINT [PK_CourseOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCompiledStatuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxCompile__sysSt__73BA3083]  DEFAULT ((0)),
 CONSTRAINT [PK_fxdCompiledStatuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxAnswerType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxAnswerType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]  DEFAULT ((0)),
 CONSTRAINT [PK_fxAnswerType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblGroups]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblGroups__sysSt__693CA210]  DEFAULT ((0)),
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCurriculums]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCurriculums](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]  DEFAULT ((0)),
 CONSTRAINT [PK_SdudyCourses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblCourses]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCourses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCourses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UploadDate] [datetime] NULL,
	[Version] [int] NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblCourse__sysSt__75A278F5]  DEFAULT ((0)),
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblLearnerAttempts]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLearnerAttempts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeRef] [int] NOT NULL,
	[UserRef] [int] NOT NULL,
	[Started] [datetime] NULL,
	[Finished] [datetime] NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_tblLearnerAttempts_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblLearnerAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSampleBusinesObject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblSample__sysSt__68487DD7]  DEFAULT ((0)),
 CONSTRAINT [UQ__tblSampleBusines__7E6CC920] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](32) NOT NULL,
	[PasswordHash] [char](32) NOT NULL,
	[Email] [char](50) NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblUsers__sysSta__6754599E]  DEFAULT ((0)),
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_EMAIL] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY],
 CONSTRAINT [UK_Login] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblUsersSignIn]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUsersSignIn](
	[UserId] [int] NOT NULL,
	[ComputerId] [int] NOT NULL,
	[LastLogin] [datetime] NULL,
 CONSTRAINT [PK_tblUsersSignIn_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 04/22/2010 12:24:18 ******/
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
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]  DEFAULT ((0)),
 CONSTRAINT [PK_Permissions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relUserRoles](
	[UserID] [int] NOT NULL,
	[RoleID] [int] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__relUserRo__sysSt__02084FDA]  DEFAULT ((0)),
 CONSTRAINT [PK_USER_ROLE] PRIMARY KEY CLUSTERED 
(
	[UserID] ASC,
	[RoleID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblPages]    Script Date: 04/22/2010 12:24:18 ******/
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
	[PageName] [nvarchar](50) NULL,
	[PageFile] [varchar](250) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblPages__sysSta__7C4F7684]  DEFAULT ((0)),
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 04/22/2010 12:24:18 ******/
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
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblCompil__sysSt__778AC167]  DEFAULT ((0)),
 CONSTRAINT [PK_tblCompiledQuestions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 04/22/2010 12:24:18 ******/
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
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblCompil__sysSt__76969D2E]  DEFAULT ((0)),
	[UserAnswerRef] [int] NOT NULL CONSTRAINT [DF__tblCompil__UserA__04E4BC85]  DEFAULT ((0)),
	[Output] [nvarchar](max) NULL,
	[CompiledQuestionsDataRef] [int] NOT NULL CONSTRAINT [DF__tblCompil__Compi__08B54D69]  DEFAULT ((0)),
 CONSTRAINT [PK_tblCompiledAnswers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 04/22/2010 12:24:18 ******/
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
	[UserAnswer] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[IsCompiledAnswer] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]  DEFAULT ((0)),
	[AnswerTypeRef] [int] NOT NULL CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]  DEFAULT ((1)),
 CONSTRAINT [PK_UserAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[relUserGroups](
	[UserRef] [int] NOT NULL,
	[GroupRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__relUserGr__sysSt__02FC7413]  DEFAULT ((0)),
 CONSTRAINT [PK_relUserGroups_KEY] PRIMARY KEY CLUSTERED 
(
	[UserRef] ASC,
	[GroupRef] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblStages]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblStages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CurriculumRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblStages__sysSt__787EE5A0]  DEFAULT ((0)),
 CONSTRAINT [PK_Stages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblThemes]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThemes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblThemes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[CourseRef] [int] NOT NULL,
	[OrganizationRef] [int] NOT NULL,
	[StageRef] [int] NOT NULL,
	[IsControl] [bit] NOT NULL,
	[PageOrderRef] [int] NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblThemes__sysSt__797309D9]  DEFAULT ((0)),
	[PageCountToShow] [int] NULL CONSTRAINT [DF__tblThemes__PageC__06CD04F7]  DEFAULT (NULL),
	[MaxCountToSubmit] [int] NULL CONSTRAINT [DF__tblThemes__MaxCo__07C12930]  DEFAULT (NULL),
 CONSTRAINT [PK_Chapter] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOrganizations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblOrganizations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseRef] [int] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_tblOrganizations_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblOrganizations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblResources]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResources]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblResources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseRef] [int] NOT NULL,
	[Identifier] [nvarchar](300) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Href] [nvarchar](300) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_tblResources_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblResources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblLearnerSessions]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLearnerSessions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerAttemptRef] [int] NOT NULL,
	[ItemRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_tblAttempts_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblItems]    Script Date: 04/22/2010 12:24:18 ******/
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
	[Title] [nvarchar](200) NULL,
	[IsLeaf] [bit] NOT NULL,
	[sysState] [int] NOT NULL CONSTRAINT [DF_tblItems_sysState]  DEFAULT ((0)),
	[Rank] [int] NULL,
 CONSTRAINT [PK_tblItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuestions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblQuestions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageRef] [int] NULL,
	[TestName] [nvarchar](50) NULL,
	[CorrectAnswer] [nvarchar](max) NULL,
	[Rank] [int] NULL,
	[IsCompiled] [bit] NOT NULL,
	[CompiledQuestionRef] [int] NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]  DEFAULT ((0)),
 CONSTRAINT [PK_CorrectAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCompiledQuestionsData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompiledQuestionRef] [int] NOT NULL,
	[Input] [nvarchar](max) NULL,
	[Output] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]  DEFAULT ((0)),
 CONSTRAINT [PK_tblCompiledQuestionsData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblVarsInteractions]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsInteractions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblVarsInteractionObjectives]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractionObjectives](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblVarsInteractionCorrectResponses]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractionCorrectResponses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblVars]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVars]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[sysState] [smallint] NOT NULL CONSTRAINT [DF_tblAttemptsVars_sysState]  DEFAULT ((0)),
 CONSTRAINT [PK_tblAttemptsVars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblVarsScore]    Script Date: 04/22/2010 12:24:18 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsScore]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsScore](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsScore] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, IGNORE_DUP_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
END
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn]  WITH CHECK ADD  CONSTRAINT [FK_tblUsersSignIn_tblComputers] FOREIGN KEY([ComputerId])
REFERENCES [dbo].[tblComputers] ([ID])
GO
ALTER TABLE [dbo].[tblUsersSignIn] CHECK CONSTRAINT [FK_tblUsersSignIn_tblComputers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn]  WITH CHECK ADD  CONSTRAINT [FK_tblUsersSignIn_tblUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblUsersSignIn] CHECK CONSTRAINT [FK_tblUsersSignIn_tblUsers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_PARENT_PERMITION] FOREIGN KEY([ParentPermitionRef])
REFERENCES [dbo].[tblPermissions] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_PARENT_PERMITION]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CourseOperations] FOREIGN KEY([CourseOperationRef])
REFERENCES [dbo].[fxCourseOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CourseOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Courses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Courses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CurriculumOperations] FOREIGN KEY([CurriculumOperationRef])
REFERENCES [dbo].[fxCurriculumOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Curriculums] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Curriculums]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupObjects] FOREIGN KEY([GroupObjectRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupObjects]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupOperations] FOREIGN KEY([GroupOperationRef])
REFERENCES [dbo].[fxGroupOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Groups] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Groups]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Organizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Organizations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerGroup] FOREIGN KEY([OwnerGroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerGroup]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerUser] FOREIGN KEY([OwnerUserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerUser]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PageOperations] FOREIGN KEY([PageOperationRef])
REFERENCES [dbo].[fxPageOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_PageOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Pages] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Pages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_StageOperations] FOREIGN KEY([StageOperationRef])
REFERENCES [dbo].[fxStageOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_StageOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Stages] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Stages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_ThemeOperations] FOREIGN KEY([ThemeOperationRef])
REFERENCES [dbo].[fxThemeOperations] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_ThemeOperations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Themes] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[tblThemes] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Themes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_UserObjects] FOREIGN KEY([UserObjectRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_UserObjects]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_ID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[fxRoles] ([ID])
GO
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_ROLE_ID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_USER_ID] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_USER_ID]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages]  WITH CHECK ADD  CONSTRAINT [FK_Page_PageType] FOREIGN KEY([PageTypeRef])
REFERENCES [dbo].[fxPageTypes] ([ID])
GO
ALTER TABLE [dbo].[tblPages] CHECK CONSTRAINT [FK_Page_PageType]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages] FOREIGN KEY([LanguageRef])
REFERENCES [dbo].[fxLanguages] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledQuestions] CHECK CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses] FOREIGN KEY([StatusRef])
REFERENCES [dbo].[fxCompiledStatuses] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData] FOREIGN KEY([CompiledQuestionsDataRef])
REFERENCES [dbo].[tblCompiledQuestionsData] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers] FOREIGN KEY([UserAnswerRef])
REFERENCES [dbo].[tblUserAnswers] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef] FOREIGN KEY([AnswerTypeRef])
REFERENCES [dbo].[fxAnswerType] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswer_CorrectAnswer] FOREIGN KEY([QuestionRef])
REFERENCES [dbo].[tblQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswers_Users] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswers_Users]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_GROUP] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_GROUP]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_USER] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_USER]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages]  WITH CHECK ADD  CONSTRAINT [FK_Curriculums_Stages] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
ALTER TABLE [dbo].[tblStages] CHECK CONSTRAINT [FK_Curriculums_Stages]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Themes] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Organizations_Themes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Stages_Themes] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Stages_Themes]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Themes_Course] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Themes_Course]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_tblOrganizations_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblOrganizations] CHECK CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources]  WITH CHECK ADD  CONSTRAINT [FK_tblResources_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
ALTER TABLE [dbo].[tblResources] CHECK CONSTRAINT [FK_tblResources_tblCourses]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblLearnerSessions] FOREIGN KEY([ItemRef])
REFERENCES [dbo].[tblItems] ([ID])
GO
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblItems_tblLearnerSessions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions] FOREIGN KEY([LearnerAttemptRef])
REFERENCES [dbo].[tblLearnerAttempts] ([ID])
GO
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblItems] FOREIGN KEY([PID])
REFERENCES [dbo].[tblItems] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblItems]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblOrganizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblOrganizations]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblResources] FOREIGN KEY([ResourceRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblResources]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_CorrectAnswer_Page] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_CorrectAnswer_Page]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestions_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
ALTER TABLE [dbo].[tblCompiledQuestionsData] CHECK CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
ALTER TABLE [dbo].[tblVarsInteractions] CHECK CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
ALTER TABLE [dbo].[tblVarsInteractionObjectives] CHECK CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses] CHECK CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars]  WITH CHECK ADD  CONSTRAINT [FK_tblVars_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
ALTER TABLE [dbo].[tblVars] CHECK CONSTRAINT [FK_tblVars_tblLearnerSessions]
GO
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsScore_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
ALTER TABLE [dbo].[tblVarsScore] CHECK CONSTRAINT [FK_tblVarsScore_tblLearnerSessions]
USE [IUDICO]
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxAnswerT__sysSt__0C85DE4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxAnswerType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxAnswerT__sysSt__0C85DE4D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxAnswerType] DROP CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCompile__sysSt__73BA3083]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCompile__sysSt__73BA3083]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCompiledStatuses] DROP CONSTRAINT [DF__fxCompile__sysSt__73BA3083]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCourseO__sysSt__72C60C4A]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCourseO__sysSt__72C60C4A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCourseOperations] DROP CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCurricu__sysSt__71D1E811]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCurricu__sysSt__71D1E811]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCurriculumOperations] DROP CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxGroupOp__sysSt__03F0984C]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxGroupOp__sysSt__03F0984C]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxGroupOperations] DROP CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxLanguag__sysSt__70DDC3D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxLanguages]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxLanguag__sysSt__70DDC3D8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxLanguages] DROP CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOpe__sysSt__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOpe__sysSt__6FE99F9F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOperations] DROP CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOrd__sysSt__6EF57B66]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOrders]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOrd__sysSt__6EF57B66]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOrders] DROP CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageTyp__sysSt__6E01572D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageTypes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageTyp__sysSt__6E01572D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageTypes] DROP CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxRoles__sysStat__6D0D32F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxRoles]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxRoles__sysStat__6D0D32F4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxRoles] DROP CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxSampleB__sysSt__6A30C649]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxSampleB__sysSt__6A30C649]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxSampleBusinesObjectOperation] DROP CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxStageOp__sysSt__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxStageOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxStageOp__sysSt__6C190EBB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxStageOperations] DROP CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxThemeOp__sysSt__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxThemeOp__sysSt__6B24EA82]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxThemeOperations] DROP CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserGr__sysSt__02FC7413]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserGr__sysSt__02FC7413]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [DF__relUserGr__sysSt__02FC7413]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserRo__sysSt__02084FDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserRo__sysSt__02084FDA]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [DF__relUserRo__sysSt__02084FDA]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__76969D2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__76969D2E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__sysSt__76969D2E]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__UserA__04E4BC85]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__UserA__04E4BC85]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__UserA__04E4BC85]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__Compi__08B54D69]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__Compi__08B54D69]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__Compi__08B54D69]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__778AC167]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__778AC167]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestions] DROP CONSTRAINT [DF__tblCompil__sysSt__778AC167]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__7D439ABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__7D439ABD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestionsData] DROP CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCourse__sysSt__75A278F5]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCourses]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCourse__sysSt__75A278F5]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCourses] DROP CONSTRAINT [DF__tblCourse__sysSt__75A278F5]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCurric__sysSt__74AE54BC]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCurriculums]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCurric__sysSt__74AE54BC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCurriculums] DROP CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblGroups__sysSt__693CA210]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblGroups]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblGroups__sysSt__693CA210]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblGroups] DROP CONSTRAINT [DF__tblGroups__sysSt__693CA210]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblItems_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblItems_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [DF_tblItems_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblLearnerAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblLearnerAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerAttempts] DROP CONSTRAINT [DF_tblLearnerAttempts_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerSessions] DROP CONSTRAINT [DF_tblAttempts_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblOrganizations_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblOrganizations_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblOrganizations] DROP CONSTRAINT [DF_tblOrganizations_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPages__sysSta__7C4F7684]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPages__sysSta__7C4F7684]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPages] DROP CONSTRAINT [DF__tblPages__sysSta__7C4F7684]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPermis__sysSt__7B5B524B]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPermis__sysSt__7B5B524B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblQuesti__sysSt__7E37BEF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblQuesti__sysSt__7E37BEF6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblResources_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblResources_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblResources] DROP CONSTRAINT [DF_tblResources_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblSample__sysSt__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblSample__sysSt__68487DD7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSampleBusinesObject] DROP CONSTRAINT [DF__tblSample__sysSt__68487DD7]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] DROP CONSTRAINT [DF_tblSettings_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblStages__sysSt__787EE5A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblStages__sysSt__787EE5A0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblStages] DROP CONSTRAINT [DF__tblStages__sysSt__787EE5A0]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__sysSt__797309D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__sysSt__797309D9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__sysSt__797309D9]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__PageC__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__PageC__06CD04F7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__PageC__06CD04F7]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__MaxCo__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__MaxCo__07C12930]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__MaxCo__07C12930]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__sysSt__01142BA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__sysSt__01142BA1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__Answe__0D7A0286]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__Answe__0D7A0286]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUsers__sysSta__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUsers__sysSta__6754599E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUsers] DROP CONSTRAINT [DF__tblUsers__sysSta__6754599E]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttemptsVars_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttemptsVars_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblVars] DROP CONSTRAINT [DF_tblAttemptsVars_sysState]
END


End
GO
/****** Object:  ForeignKey [FK_GROUP]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [FK_GROUP]
GO
/****** Object:  ForeignKey [FK_USER]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [FK_USER]
GO
/****** Object:  ForeignKey [FK_ROLE_ID]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [FK_ROLE_ID]
GO
/****** Object:  ForeignKey [FK_USER_ID]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [FK_USER_ID]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_fxdCompiledStatuses]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblCompiledQuestionsData]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblUserAnswers]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestions_fxdLanguages]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions] DROP CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestionsData_tblCompiledQuestions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData] DROP CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblItems_tblItems]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblItems]
GO
/****** Object:  ForeignKey [FK_tblItems_tblOrganizations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblOrganizations]
GO
/****** Object:  ForeignKey [FK_tblItems_tblResources]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblResources]
GO
/****** Object:  ForeignKey [FK_tblItems_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] DROP CONSTRAINT [FK_tblItems_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblLearnerAttempts_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] DROP CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblOrganizations_tblCourses]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations] DROP CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
/****** Object:  ForeignKey [FK_Page_PageType]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages] DROP CONSTRAINT [FK_Page_PageType]
GO
/****** Object:  ForeignKey [FK_PARENT_PERMITION]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_PARENT_PERMITION]
GO
/****** Object:  ForeignKey [FK_Permissions_CourseOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_CourseOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Courses]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Courses]
GO
/****** Object:  ForeignKey [FK_Permissions_CurriculumOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Curriculums]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Curriculums]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupObjects]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_GroupObjects]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_GroupOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Groups]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Groups]
GO
/****** Object:  ForeignKey [FK_Permissions_Organizations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Organizations]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerGroup]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_OwnerGroup]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerUser]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_OwnerUser]
GO
/****** Object:  ForeignKey [FK_Permissions_PageOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_PageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Pages]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Pages]
GO
/****** Object:  ForeignKey [FK_Permissions_StageOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_StageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Stages]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Stages]
GO
/****** Object:  ForeignKey [FK_Permissions_ThemeOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_ThemeOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Themes]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Themes]
GO
/****** Object:  ForeignKey [FK_Permissions_UserObjects]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_UserObjects]
GO
/****** Object:  ForeignKey [FK_CorrectAnswer_Page]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [FK_CorrectAnswer_Page]
GO
/****** Object:  ForeignKey [FK_tblQuestions_tblCompiledQuestions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblResources_tblCourses]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources] DROP CONSTRAINT [FK_tblResources_tblCourses]
GO
/****** Object:  ForeignKey [FK_Curriculums_Stages]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages] DROP CONSTRAINT [FK_Curriculums_Stages]
GO
/****** Object:  ForeignKey [FK_Organizations_Themes]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Organizations_Themes]
GO
/****** Object:  ForeignKey [FK_Stages_Themes]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Stages_Themes]
GO
/****** Object:  ForeignKey [FK_Themes_Course]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Themes_Course]
GO
/****** Object:  ForeignKey [FK_tblUserAnswers_AnswerTypeRef]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
/****** Object:  ForeignKey [FK_UserAnswer_CorrectAnswer]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
/****** Object:  ForeignKey [FK_UserAnswers_Users]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_UserAnswers_Users]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblComputers]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] DROP CONSTRAINT [FK_tblUsersSignIn_tblComputers]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblUsers]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] DROP CONSTRAINT [FK_tblUsersSignIn_tblUsers]
GO
/****** Object:  ForeignKey [FK_tblVars_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars] DROP CONSTRAINT [FK_tblVars_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses] DROP CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionObjectives_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives] DROP CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractions_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions] DROP CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsScore_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore] DROP CONSTRAINT [FK_tblVarsScore_tblLearnerSessions]
GO
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledAnswers]
GO
/****** Object:  Table [dbo].[tblVars]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVars]') AND type in (N'U'))
DROP TABLE [dbo].[tblVars]
GO
/****** Object:  Table [dbo].[tblVarsInteractionCorrectResponses]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsInteractionCorrectResponses]
GO
/****** Object:  Table [dbo].[tblVarsInteractionObjectives]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsInteractionObjectives]
GO
/****** Object:  Table [dbo].[tblVarsInteractions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsInteractions]
GO
/****** Object:  Table [dbo].[tblVarsScore]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsScore]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsScore]
GO
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]') AND type in (N'U'))
DROP TABLE [dbo].[tblUserAnswers]
GO
/****** Object:  Table [dbo].[tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]') AND type in (N'U'))
DROP TABLE [dbo].[tblLearnerSessions]
GO
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPermissions]') AND type in (N'U'))
DROP TABLE [dbo].[tblPermissions]
GO
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuestions]') AND type in (N'U'))
DROP TABLE [dbo].[tblQuestions]
GO
/****** Object:  Table [dbo].[tblThemes]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThemes]') AND type in (N'U'))
DROP TABLE [dbo].[tblThemes]
GO
/****** Object:  Table [dbo].[tblItems]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblItems]') AND type in (N'U'))
DROP TABLE [dbo].[tblItems]
GO
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledQuestionsData]
GO
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledQuestions]
GO
/****** Object:  Table [dbo].[tblUsersSignIn]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]') AND type in (N'U'))
DROP TABLE [dbo].[tblUsersSignIn]
GO
/****** Object:  Table [dbo].[tblResources]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResources]') AND type in (N'U'))
DROP TABLE [dbo].[tblResources]
GO
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserGroups]') AND type in (N'U'))
DROP TABLE [dbo].[relUserGroups]
GO
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[relUserRoles]
GO
/****** Object:  Table [dbo].[tblStages]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStages]') AND type in (N'U'))
DROP TABLE [dbo].[tblStages]
GO
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOrganizations]') AND type in (N'U'))
DROP TABLE [dbo].[tblOrganizations]
GO
/****** Object:  Table [dbo].[tblPages]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPages]') AND type in (N'U'))
DROP TABLE [dbo].[tblPages]
GO
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]') AND type in (N'U'))
DROP TABLE [dbo].[tblSampleBusinesObject]
GO
/****** Object:  Table [dbo].[tblSettings]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
DROP TABLE [dbo].[tblSettings]
GO
/****** Object:  Table [dbo].[tblUserNotes]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserNotes]') AND type in (N'U'))
DROP TABLE [dbo].[tblUserNotes]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
DROP TABLE [dbo].[tblUsers]
GO
/****** Object:  Table [dbo].[tblLearnerAttempts]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]') AND type in (N'U'))
DROP TABLE [dbo].[tblLearnerAttempts]
GO
/****** Object:  Table [dbo].[tblComputers]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblComputers]') AND type in (N'U'))
DROP TABLE [dbo].[tblComputers]
GO
/****** Object:  Table [dbo].[tblCourses]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCourses]') AND type in (N'U'))
DROP TABLE [dbo].[tblCourses]
GO
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCurriculums]') AND type in (N'U'))
DROP TABLE [dbo].[tblCurriculums]
GO
/****** Object:  Table [dbo].[tblGroups]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGroups]') AND type in (N'U'))
DROP TABLE [dbo].[tblGroups]
GO
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxAnswerType]') AND type in (N'U'))
DROP TABLE [dbo].[fxAnswerType]
GO
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]') AND type in (N'U'))
DROP TABLE [dbo].[fxCompiledStatuses]
GO
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxCourseOperations]
GO
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxCurriculumOperations]
GO
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxGroupOperations]
GO
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxLanguages]') AND type in (N'U'))
DROP TABLE [dbo].[fxLanguages]
GO
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageOperations]
GO
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOrders]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageOrders]
GO
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageTypes]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageTypes]
GO
/****** Object:  Table [dbo].[fxRoles]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxRoles]') AND type in (N'U'))
DROP TABLE [dbo].[fxRoles]
GO
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]') AND type in (N'U'))
DROP TABLE [dbo].[fxSampleBusinesObjectOperation]
GO
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxStageOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxStageOperations]
GO
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxThemeOperations]
GO
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxThemeOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_ThemeOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxThemeOperations] ON
INSERT [dbo].[fxThemeOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'View', N'View the theme', 0, 0)
INSERT [dbo].[fxThemeOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Pass', N'Pass the theme', 0, 0)
SET IDENTITY_INSERT [dbo].[fxThemeOperations] OFF
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxStageOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxStageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_StageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxStageOperations] ON
INSERT [dbo].[fxStageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'View', N'View the stage', 0, 0)
INSERT [dbo].[fxStageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Pass', N'Pass the stage', 0, 0)
SET IDENTITY_INSERT [dbo].[fxStageOperations] OFF
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxSampleBusinesObjectOperation](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [UQ__fxSampleBusinesO__023D5A04] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxRoles]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxRoles]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxRoles](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdRoles] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxRoles] ON
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (1, N'STUDENT', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (2, N'LECTOR', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (3, N'TRAINER', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (4, N'ADMIN', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (5, N'SUPER_ADMIN', NULL, 0)
SET IDENTITY_INSERT [dbo].[fxRoles] OFF
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageTypes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageTypes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Type] [nvarchar](10) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_PageType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxPageTypes] ON
INSERT [dbo].[fxPageTypes] ([ID], [Type], [sysState]) VALUES (1, N'Theory', 0)
INSERT [dbo].[fxPageTypes] ([ID], [Type], [sysState]) VALUES (2, N'Practice', 0)
SET IDENTITY_INSERT [dbo].[fxPageTypes] OFF
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOrders]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageOrders](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdPageOrders] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxPageOrders] ON
INSERT [dbo].[fxPageOrders] ([ID], [Name], [sysState]) VALUES (1, N'Straight', 0)
INSERT [dbo].[fxPageOrders] ([ID], [Name], [sysState]) VALUES (2, N'Random', 0)
SET IDENTITY_INSERT [dbo].[fxPageOrders] OFF
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxPageOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_PageOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxPageOperations] ON
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (1, N'Add', N'Add new Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (2, N'Edit', N'Edit Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (3, N'View', N'View Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (4, N'Delete', N'Delete Page', 1, 0)
SET IDENTITY_INSERT [dbo].[fxPageOperations] OFF
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxLanguages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxLanguages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdLanguages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxGroupOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxGroupOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_fxGroupOperations_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxGroupOperations] ON
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (1, N'View', NULL, 1, 0)
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (2, N'Rename', NULL, 1, 0)
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (3, N'ChangeMembers', NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[fxGroupOperations] OFF
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCurriculumOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_CurriculumOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxCurriculumOperations] ON
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'Modify', N'Modify curriculum by teacher', 1, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Use', N'Use curriculum by teacher', 1, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (7, N'View', N'View the curriculum', 0, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (8, N'Pass', N'Pass the curriculum', 0, 0)
SET IDENTITY_INSERT [dbo].[fxCurriculumOperations] OFF
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCourseOperations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Description] [nvarchar](max) NULL,
	[CanBeDelegated] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_CourseOperations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxCourseOperations] ON
INSERT [dbo].[fxCourseOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'Modify', N'Modify course by teacher', 1, 0)
INSERT [dbo].[fxCourseOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Use', N'Use course by teacher', 1, 0)
SET IDENTITY_INSERT [dbo].[fxCourseOperations] OFF
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxCompiledStatuses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxdCompiledStatuses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxAnswerType]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[fxAnswerType](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](20) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_fxAnswerType] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxAnswerType] ON
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (1, N'UserAnswer', 0)
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (2, N'EmptyAnswer', 0)
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (3, N'NotIncludedAnswer', 0)
SET IDENTITY_INSERT [dbo].[fxAnswerType] OFF
/****** Object:  Table [dbo].[tblGroups]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGroups]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblGroups](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Groups] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[tblGroups] ON
INSERT [dbo].[tblGroups] ([ID], [Name], [sysState]) VALUES (1, N'123', 0)
SET IDENTITY_INSERT [dbo].[tblGroups] OFF
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCurriculums]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCurriculums](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_SdudyCourses] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblCourses]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCourses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCourses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[UploadDate] [datetime] NULL,
	[Version] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Course] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblComputers]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblComputers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblComputers](
	[ID] [int] IDENTITY(10000,1) NOT NULL,
	[ComputerName] [nvarchar](100) NULL,
	[IP] [nvarchar](15) NULL,
	[LectureRoom] [nvarchar](50) NULL,
 CONSTRAINT [PK_tblComputers_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[tblComputers] ON
INSERT [dbo].[tblComputers] ([ID], [ComputerName], [IP], [LectureRoom]) VALUES (10000, N'', N'127.0.0.1', N'')
INSERT [dbo].[tblComputers] ([ID], [ComputerName], [IP], [LectureRoom]) VALUES (10001, N'', N'::1', N'')
SET IDENTITY_INSERT [dbo].[tblComputers] OFF
/****** Object:  Table [dbo].[tblLearnerAttempts]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLearnerAttempts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeRef] [int] NOT NULL,
	[UserRef] [int] NOT NULL,
	[Started] [datetime] NULL,
	[Finished] [datetime] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblLearnerAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUsers](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[Login] [nvarchar](32) NOT NULL,
	[PasswordHash] [char](32) NOT NULL,
	[Email] [char](50) NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_EMAIL] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_Login] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblUsers] ON
INSERT [dbo].[tblUsers] ([ID], [FirstName], [LastName], [Login], [PasswordHash], [Email], [sysState]) VALUES (1, N'Volodymyr', N'Shtenovych', N'lex', N'B067B3D3054D8868C950E1946300A3F4', N'ShVolodya@gmail.com                               ', 0)
INSERT [dbo].[tblUsers] ([ID], [FirstName], [LastName], [Login], [PasswordHash], [Email], [sysState]) VALUES (3, N'V', N'P', N'vladykx', N'123                             ', N'1                                                 ', 0)
SET IDENTITY_INSERT [dbo].[tblUsers] OFF
/****** Object:  Table [dbo].[tblUserNotes]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserNotes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUserNotes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRef] [int] NULL,
	[Description] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[SysState] [smallint] NOT NULL,
 CONSTRAINT [PK_UserNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblSettings]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSettings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Value] [varchar](250) NULL,
	[sysState] [smallint] NOT NULL
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND name = N'PK_Settings')
CREATE UNIQUE CLUSTERED INDEX [PK_Settings] ON [dbo].[tblSettings] 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND name = N'UN_Settings')
CREATE UNIQUE NONCLUSTERED INDEX [UN_Settings] ON [dbo].[tblSettings] 
(
	[Name] ASC,
	[sysState] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblSettings] ON
INSERT [dbo].[tblSettings] ([ID], [Name], [Value], [sysState]) VALUES (4, N'compile_service_url', N'http://localhost:8080/Compile.asp', 1)
INSERT [dbo].[tblSettings] ([ID], [Name], [Value], [sysState]) VALUES (8, N'compile_service_url', N'http://localhost:49440/Service1.asmx/Compile', 0)
SET IDENTITY_INSERT [dbo].[tblSettings] OFF
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSampleBusinesObject](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [UQ__tblSampleBusines__7E6CC920] UNIQUE NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblPages]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblPages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeRef] [int] NULL,
	[PageTypeRef] [int] NULL,
	[PageRank] [int] NULL,
	[PageName] [nvarchar](50) NULL,
	[PageFile] [varchar](250) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Page] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOrganizations]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblOrganizations](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseRef] [int] NOT NULL,
	[Title] [nvarchar](200) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblOrganizations] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblStages]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStages]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblStages](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
	[Description] [nvarchar](max) NULL,
	[CurriculumRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Stages] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 04/25/2010 01:06:24 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 1, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 2, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 3, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 4, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 5, 0)
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 04/25/2010 01:06:24 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[relUserGroups] ([UserRef], [GroupRef], [sysState]) VALUES (1, 1, 0)
/****** Object:  Table [dbo].[tblResources]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResources]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblResources](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CourseRef] [int] NOT NULL,
	[Identifier] [nvarchar](300) NOT NULL,
	[Type] [nvarchar](100) NOT NULL,
	[Href] [nvarchar](300) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblResources] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblUsersSignIn]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUsersSignIn](
	[UserId] [int] NOT NULL,
	[ComputerId] [int] NOT NULL,
	[LastLogin] [datetime] NULL,
 CONSTRAINT [PK_tblUsersSignIn_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[tblUsersSignIn] ([UserId], [ComputerId], [LastLogin]) VALUES (1, 10001, CAST(0x00009D63000AE9A4 AS DateTime))
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 04/25/2010 01:06:24 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblCompiledQuestionsData](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[CompiledQuestionRef] [int] NOT NULL,
	[Input] [nvarchar](max) NULL,
	[Output] [nvarchar](max) NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblCompiledQuestionsData] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblItems]    Script Date: 04/25/2010 01:06:24 ******/
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
	[Title] [nvarchar](200) NULL,
	[IsLeaf] [bit] NOT NULL,
	[sysState] [int] NOT NULL,
	[Rank] [int] NULL,
 CONSTRAINT [PK_tblItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblThemes]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThemes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblThemes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](50) NULL,
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuestions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblQuestions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[PageRef] [int] NULL,
	[TestName] [nvarchar](50) NULL,
	[CorrectAnswer] [nvarchar](max) NULL,
	[Rank] [int] NULL,
	[IsCompiled] [bit] NOT NULL,
	[CompiledQuestionRef] [int] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_CorrectAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 04/25/2010 01:06:24 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLearnerSessions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerAttemptRef] [int] NOT NULL,
	[ItemRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 04/25/2010 01:06:24 ******/
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
	[UserAnswer] [nvarchar](max) NULL,
	[Date] [datetime] NULL,
	[IsCompiledAnswer] [bit] NOT NULL,
	[sysState] [smallint] NOT NULL,
	[AnswerTypeRef] [int] NOT NULL,
 CONSTRAINT [PK_UserAnswer] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblVarsScore]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsScore]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsScore](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsScore] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVarsInteractions]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsInteractions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVarsInteractionObjectives]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractionObjectives](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVarsInteractionCorrectResponses]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractionCorrectResponses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVars]    Script Date: 04/25/2010 01:06:24 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVars]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) NOT NULL,
	[Value] [varchar](max) NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblAttemptsVars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 04/25/2010 01:06:24 ******/
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
	[Output] [nvarchar](max) NULL,
	[CompiledQuestionsDataRef] [int] NOT NULL,
 CONSTRAINT [PK_tblCompiledAnswers] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Default [DF__fxAnswerT__sysSt__0C85DE4D]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxAnswerT__sysSt__0C85DE4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxAnswerType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxAnswerT__sysSt__0C85DE4D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxAnswerType] ADD  CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxCompile__sysSt__73BA3083]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCompile__sysSt__73BA3083]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCompile__sysSt__73BA3083]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCompiledStatuses] ADD  CONSTRAINT [DF__fxCompile__sysSt__73BA3083]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxCourseO__sysSt__72C60C4A]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCourseO__sysSt__72C60C4A]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCourseO__sysSt__72C60C4A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCourseOperations] ADD  CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxCurricu__sysSt__71D1E811]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCurricu__sysSt__71D1E811]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCurricu__sysSt__71D1E811]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCurriculumOperations] ADD  CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxGroupOp__sysSt__03F0984C]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxGroupOp__sysSt__03F0984C]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxGroupOp__sysSt__03F0984C]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxGroupOperations] ADD  CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxLanguag__sysSt__70DDC3D8]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxLanguag__sysSt__70DDC3D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxLanguages]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxLanguag__sysSt__70DDC3D8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxLanguages] ADD  CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxPageOpe__sysSt__6FE99F9F]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOpe__sysSt__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOpe__sysSt__6FE99F9F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOperations] ADD  CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxPageOrd__sysSt__6EF57B66]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOrd__sysSt__6EF57B66]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOrders]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOrd__sysSt__6EF57B66]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOrders] ADD  CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxPageTyp__sysSt__6E01572D]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageTyp__sysSt__6E01572D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageTypes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageTyp__sysSt__6E01572D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageTypes] ADD  CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxRoles__sysStat__6D0D32F4]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxRoles__sysStat__6D0D32F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxRoles]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxRoles__sysStat__6D0D32F4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxRoles] ADD  CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxSampleB__sysSt__6A30C649]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxSampleB__sysSt__6A30C649]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxSampleB__sysSt__6A30C649]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxSampleBusinesObjectOperation] ADD  CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxStageOp__sysSt__6C190EBB]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxStageOp__sysSt__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxStageOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxStageOp__sysSt__6C190EBB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxStageOperations] ADD  CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxThemeOp__sysSt__6B24EA82]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxThemeOp__sysSt__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxThemeOp__sysSt__6B24EA82]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxThemeOperations] ADD  CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__relUserGr__sysSt__02FC7413]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserGr__sysSt__02FC7413]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserGr__sysSt__02FC7413]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserGroups] ADD  CONSTRAINT [DF__relUserGr__sysSt__02FC7413]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__relUserRo__sysSt__02084FDA]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserRo__sysSt__02084FDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserRo__sysSt__02084FDA]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserRoles] ADD  CONSTRAINT [DF__relUserRo__sysSt__02084FDA]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__76969D2E]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__76969D2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__76969D2E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__sysSt__76969D2E]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCompil__UserA__04E4BC85]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__UserA__04E4BC85]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__UserA__04E4BC85]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__UserA__04E4BC85]  DEFAULT ((0)) FOR [UserAnswerRef]
END


End
GO
/****** Object:  Default [DF__tblCompil__Compi__08B54D69]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__Compi__08B54D69]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__Compi__08B54D69]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__Compi__08B54D69]  DEFAULT ((0)) FOR [CompiledQuestionsDataRef]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__778AC167]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__778AC167]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__778AC167]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestions] ADD  CONSTRAINT [DF__tblCompil__sysSt__778AC167]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__7D439ABD]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__7D439ABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__7D439ABD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestionsData] ADD  CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCourse__sysSt__75A278F5]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCourse__sysSt__75A278F5]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCourses]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCourse__sysSt__75A278F5]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCourses] ADD  CONSTRAINT [DF__tblCourse__sysSt__75A278F5]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCurric__sysSt__74AE54BC]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCurric__sysSt__74AE54BC]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCurriculums]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCurric__sysSt__74AE54BC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCurriculums] ADD  CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblGroups__sysSt__693CA210]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblGroups__sysSt__693CA210]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblGroups]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblGroups__sysSt__693CA210]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF__tblGroups__sysSt__693CA210]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblItems_sysState]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblItems_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblItems_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblItems] ADD  CONSTRAINT [DF_tblItems_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblLearnerAttempts_sysState]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblLearnerAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblLearnerAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerAttempts] ADD  CONSTRAINT [DF_tblLearnerAttempts_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblAttempts_sysState]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerSessions] ADD  CONSTRAINT [DF_tblAttempts_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblOrganizations_sysState]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblOrganizations_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblOrganizations_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblOrganizations] ADD  CONSTRAINT [DF_tblOrganizations_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblPages__sysSta__7C4F7684]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPages__sysSta__7C4F7684]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPages__sysSta__7C4F7684]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPages] ADD  CONSTRAINT [DF__tblPages__sysSta__7C4F7684]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblPermis__sysSt__7B5B524B]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPermis__sysSt__7B5B524B]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPermis__sysSt__7B5B524B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPermissions] ADD  CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblQuesti__sysSt__7E37BEF6]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblQuesti__sysSt__7E37BEF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblQuesti__sysSt__7E37BEF6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblQuestions] ADD  CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblResources_sysState]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblResources_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblResources_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblResources] ADD  CONSTRAINT [DF_tblResources_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblSample__sysSt__68487DD7]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblSample__sysSt__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblSample__sysSt__68487DD7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSampleBusinesObject] ADD  CONSTRAINT [DF__tblSample__sysSt__68487DD7]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblSettings_sysState]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] ADD  CONSTRAINT [DF_tblSettings_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblStages__sysSt__787EE5A0]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblStages__sysSt__787EE5A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblStages__sysSt__787EE5A0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblStages] ADD  CONSTRAINT [DF__tblStages__sysSt__787EE5A0]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblThemes__sysSt__797309D9]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__sysSt__797309D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__sysSt__797309D9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__sysSt__797309D9]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblThemes__PageC__06CD04F7]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__PageC__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__PageC__06CD04F7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__PageC__06CD04F7]  DEFAULT (NULL) FOR [PageCountToShow]
END


End
GO
/****** Object:  Default [DF__tblThemes__MaxCo__07C12930]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__MaxCo__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__MaxCo__07C12930]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__MaxCo__07C12930]  DEFAULT (NULL) FOR [MaxCountToSubmit]
END


End
GO
/****** Object:  Default [DF__tblUserAn__sysSt__01142BA1]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__sysSt__01142BA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__sysSt__01142BA1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblUserAn__Answe__0D7A0286]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__Answe__0D7A0286]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__Answe__0D7A0286]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]  DEFAULT ((1)) FOR [AnswerTypeRef]
END


End
GO
/****** Object:  Default [DF__tblUsers__sysSta__6754599E]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUsers__sysSta__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUsers__sysSta__6754599E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF__tblUsers__sysSta__6754599E]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblAttemptsVars_sysState]    Script Date: 04/25/2010 01:06:24 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttemptsVars_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttemptsVars_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblVars] ADD  CONSTRAINT [DF_tblAttemptsVars_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  ForeignKey [FK_GROUP]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_GROUP] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_GROUP]
GO
/****** Object:  ForeignKey [FK_USER]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_USER] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_USER]
GO
/****** Object:  ForeignKey [FK_ROLE_ID]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_ID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[fxRoles] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_ROLE_ID]
GO
/****** Object:  ForeignKey [FK_USER_ID]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_USER_ID] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_USER_ID]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_fxdCompiledStatuses]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses] FOREIGN KEY([StatusRef])
REFERENCES [dbo].[fxCompiledStatuses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblCompiledQuestionsData]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData] FOREIGN KEY([CompiledQuestionsDataRef])
REFERENCES [dbo].[tblCompiledQuestionsData] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblUserAnswers]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers] FOREIGN KEY([UserAnswerRef])
REFERENCES [dbo].[tblUserAnswers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestions_fxdLanguages]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages] FOREIGN KEY([LanguageRef])
REFERENCES [dbo].[fxLanguages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions] CHECK CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestionsData_tblCompiledQuestions]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData] CHECK CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblItems_tblItems]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblItems] FOREIGN KEY([PID])
REFERENCES [dbo].[tblItems] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblItems]
GO
/****** Object:  ForeignKey [FK_tblItems_tblOrganizations]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblOrganizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblOrganizations]
GO
/****** Object:  ForeignKey [FK_tblItems_tblResources]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblResources] FOREIGN KEY([ResourceRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblResources]
GO
/****** Object:  ForeignKey [FK_tblItems_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblLearnerSessions] FOREIGN KEY([ItemRef])
REFERENCES [dbo].[tblItems] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblItems_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblLearnerAttempts_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions] FOREIGN KEY([LearnerAttemptRef])
REFERENCES [dbo].[tblLearnerAttempts] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblOrganizations_tblCourses]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_tblOrganizations_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations] CHECK CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
/****** Object:  ForeignKey [FK_Page_PageType]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages]  WITH CHECK ADD  CONSTRAINT [FK_Page_PageType] FOREIGN KEY([PageTypeRef])
REFERENCES [dbo].[fxPageTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages] CHECK CONSTRAINT [FK_Page_PageType]
GO
/****** Object:  ForeignKey [FK_PARENT_PERMITION]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_PARENT_PERMITION] FOREIGN KEY([ParentPermitionRef])
REFERENCES [dbo].[tblPermissions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_PARENT_PERMITION]
GO
/****** Object:  ForeignKey [FK_Permissions_CourseOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CourseOperations] FOREIGN KEY([CourseOperationRef])
REFERENCES [dbo].[fxCourseOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CourseOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Courses]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Courses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Courses]
GO
/****** Object:  ForeignKey [FK_Permissions_CurriculumOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CurriculumOperations] FOREIGN KEY([CurriculumOperationRef])
REFERENCES [dbo].[fxCurriculumOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Curriculums]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Curriculums] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Curriculums]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupObjects]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupObjects] FOREIGN KEY([GroupObjectRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupObjects]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupOperations] FOREIGN KEY([GroupOperationRef])
REFERENCES [dbo].[fxGroupOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Groups]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Groups] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Groups]
GO
/****** Object:  ForeignKey [FK_Permissions_Organizations]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Organizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Organizations]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerGroup]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerGroup] FOREIGN KEY([OwnerGroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerGroup]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerUser]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerUser] FOREIGN KEY([OwnerUserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerUser]
GO
/****** Object:  ForeignKey [FK_Permissions_PageOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PageOperations] FOREIGN KEY([PageOperationRef])
REFERENCES [dbo].[fxPageOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_PageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Pages]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Pages] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Pages]
GO
/****** Object:  ForeignKey [FK_Permissions_StageOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_StageOperations] FOREIGN KEY([StageOperationRef])
REFERENCES [dbo].[fxStageOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_StageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Stages]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Stages] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Stages]
GO
/****** Object:  ForeignKey [FK_Permissions_ThemeOperations]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_ThemeOperations] FOREIGN KEY([ThemeOperationRef])
REFERENCES [dbo].[fxThemeOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_ThemeOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Themes]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Themes] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[tblThemes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Themes]
GO
/****** Object:  ForeignKey [FK_Permissions_UserObjects]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_UserObjects] FOREIGN KEY([UserObjectRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_UserObjects]
GO
/****** Object:  ForeignKey [FK_CorrectAnswer_Page]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_CorrectAnswer_Page] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_CorrectAnswer_Page]
GO
/****** Object:  ForeignKey [FK_tblQuestions_tblCompiledQuestions]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestions_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblResources_tblCourses]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources]  WITH CHECK ADD  CONSTRAINT [FK_tblResources_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources] CHECK CONSTRAINT [FK_tblResources_tblCourses]
GO
/****** Object:  ForeignKey [FK_Curriculums_Stages]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages]  WITH CHECK ADD  CONSTRAINT [FK_Curriculums_Stages] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages] CHECK CONSTRAINT [FK_Curriculums_Stages]
GO
/****** Object:  ForeignKey [FK_Organizations_Themes]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Themes] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Organizations_Themes]
GO
/****** Object:  ForeignKey [FK_Stages_Themes]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Stages_Themes] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Stages_Themes]
GO
/****** Object:  ForeignKey [FK_Themes_Course]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Themes_Course] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Themes_Course]
GO
/****** Object:  ForeignKey [FK_tblUserAnswers_AnswerTypeRef]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef] FOREIGN KEY([AnswerTypeRef])
REFERENCES [dbo].[fxAnswerType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
/****** Object:  ForeignKey [FK_UserAnswer_CorrectAnswer]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswer_CorrectAnswer] FOREIGN KEY([QuestionRef])
REFERENCES [dbo].[tblQuestions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
/****** Object:  ForeignKey [FK_UserAnswers_Users]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswers_Users] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswers_Users]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblComputers]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn]  WITH CHECK ADD  CONSTRAINT [FK_tblUsersSignIn_tblComputers] FOREIGN KEY([ComputerId])
REFERENCES [dbo].[tblComputers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] CHECK CONSTRAINT [FK_tblUsersSignIn_tblComputers]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblUsers]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn]  WITH CHECK ADD  CONSTRAINT [FK_tblUsersSignIn_tblUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] CHECK CONSTRAINT [FK_tblUsersSignIn_tblUsers]
GO
/****** Object:  ForeignKey [FK_tblVars_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars]  WITH CHECK ADD  CONSTRAINT [FK_tblVars_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars] CHECK CONSTRAINT [FK_tblVars_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses] CHECK CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionObjectives_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives] CHECK CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractions_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions] CHECK CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsScore_tblLearnerSessions]    Script Date: 04/25/2010 01:06:24 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsScore_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore] CHECK CONSTRAINT [FK_tblVarsScore_tblLearnerSessions]
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxAnswerT__sysSt__0C85DE4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxAnswerType]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxAnswerT__sysSt__0C85DE4D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxAnswerType] DROP CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCompile__sysSt__73BA3083]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCompile__sysSt__73BA3083]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCompiledStatuses] DROP CONSTRAINT [DF__fxCompile__sysSt__73BA3083]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCourseO__sysSt__72C60C4A]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCourseO__sysSt__72C60C4A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCourseOperations] DROP CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCurricu__sysSt__71D1E811]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCurricu__sysSt__71D1E811]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCurriculumOperations] DROP CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxGroupOp__sysSt__03F0984C]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxGroupOp__sysSt__03F0984C]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxGroupOperations] DROP CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxLanguag__sysSt__70DDC3D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxLanguages]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxLanguag__sysSt__70DDC3D8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxLanguages] DROP CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOpe__sysSt__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOpe__sysSt__6FE99F9F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOperations] DROP CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOrd__sysSt__6EF57B66]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOrders]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOrd__sysSt__6EF57B66]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOrders] DROP CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageTyp__sysSt__6E01572D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageTypes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageTyp__sysSt__6E01572D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageTypes] DROP CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxRoles__sysStat__6D0D32F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxRoles]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxRoles__sysStat__6D0D32F4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxRoles] DROP CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxSampleB__sysSt__6A30C649]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxSampleB__sysSt__6A30C649]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxSampleBusinesObjectOperation] DROP CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxStageOp__sysSt__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxStageOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxStageOp__sysSt__6C190EBB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxStageOperations] DROP CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxThemeOp__sysSt__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxThemeOp__sysSt__6B24EA82]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxThemeOperations] DROP CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserGr__sysSt__02FC7413]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserGr__sysSt__02FC7413]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [DF__relUserGr__sysSt__02FC7413]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserRo__sysSt__02084FDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserRo__sysSt__02084FDA]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [DF__relUserRo__sysSt__02084FDA]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__76969D2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__76969D2E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__sysSt__76969D2E]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__UserA__04E4BC85]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__UserA__04E4BC85]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__UserA__04E4BC85]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__Compi__08B54D69]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__Compi__08B54D69]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [DF__tblCompil__Compi__08B54D69]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__778AC167]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__778AC167]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestions] DROP CONSTRAINT [DF__tblCompil__sysSt__778AC167]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__7D439ABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__7D439ABD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestionsData] DROP CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCourse__sysSt__75A278F5]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCourses]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCourse__sysSt__75A278F5]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCourses] DROP CONSTRAINT [DF__tblCourse__sysSt__75A278F5]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCurric__sysSt__74AE54BC]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCurriculums]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCurric__sysSt__74AE54BC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCurriculums] DROP CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblGroups__sysSt__693CA210]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblGroups]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblGroups__sysSt__693CA210]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblGroups] DROP CONSTRAINT [DF__tblGroups__sysSt__693CA210]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblItems_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblItems_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [DF_tblItems_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblLearnerAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblLearnerAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerAttempts] DROP CONSTRAINT [DF_tblLearnerAttempts_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerSessions] DROP CONSTRAINT [DF_tblAttempts_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblOrganizations_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblOrganizations_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblOrganizations] DROP CONSTRAINT [DF_tblOrganizations_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPages__sysSta__7C4F7684]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPages__sysSta__7C4F7684]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPages] DROP CONSTRAINT [DF__tblPages__sysSta__7C4F7684]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPermis__sysSt__7B5B524B]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPermis__sysSt__7B5B524B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblQuesti__sysSt__7E37BEF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblQuesti__sysSt__7E37BEF6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblResources_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblResources_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblResources] DROP CONSTRAINT [DF_tblResources_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblSample__sysSt__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblSample__sysSt__68487DD7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSampleBusinesObject] DROP CONSTRAINT [DF__tblSample__sysSt__68487DD7]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] DROP CONSTRAINT [DF_tblSettings_sysState]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblStages__sysSt__787EE5A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblStages__sysSt__787EE5A0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblStages] DROP CONSTRAINT [DF__tblStages__sysSt__787EE5A0]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__sysSt__797309D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__sysSt__797309D9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__sysSt__797309D9]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__PageC__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__PageC__06CD04F7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__PageC__06CD04F7]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__MaxCo__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__MaxCo__07C12930]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [DF__tblThemes__MaxCo__07C12930]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__sysSt__01142BA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__sysSt__01142BA1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__Answe__0D7A0286]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__Answe__0D7A0286]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUsers__sysSta__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUsers__sysSta__6754599E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUsers] DROP CONSTRAINT [DF__tblUsers__sysSta__6754599E]
END


End
GO
IF  EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttemptsVars_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
Begin
IF  EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttemptsVars_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblVars] DROP CONSTRAINT [DF_tblAttemptsVars_sysState]
END


End
GO
/****** Object:  ForeignKey [FK_GROUP]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [FK_GROUP]
GO
/****** Object:  ForeignKey [FK_USER]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] DROP CONSTRAINT [FK_USER]
GO
/****** Object:  ForeignKey [FK_ROLE_ID]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [FK_ROLE_ID]
GO
/****** Object:  ForeignKey [FK_USER_ID]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] DROP CONSTRAINT [FK_USER_ID]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_fxdCompiledStatuses]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblCompiledQuestionsData]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblUserAnswers]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] DROP CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestions_fxdLanguages]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions] DROP CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestionsData_tblCompiledQuestions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData] DROP CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblItems_tblItems]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblItems]
GO
/****** Object:  ForeignKey [FK_tblItems_tblOrganizations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblOrganizations]
GO
/****** Object:  ForeignKey [FK_tblItems_tblResources]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] DROP CONSTRAINT [FK_tblItems_tblResources]
GO
/****** Object:  ForeignKey [FK_tblItems_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] DROP CONSTRAINT [FK_tblItems_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblLearnerAttempts_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] DROP CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblOrganizations_tblCourses]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations] DROP CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
/****** Object:  ForeignKey [FK_Page_PageType]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages] DROP CONSTRAINT [FK_Page_PageType]
GO
/****** Object:  ForeignKey [FK_PARENT_PERMITION]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_PARENT_PERMITION]
GO
/****** Object:  ForeignKey [FK_Permissions_CourseOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_CourseOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Courses]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Courses]
GO
/****** Object:  ForeignKey [FK_Permissions_CurriculumOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Curriculums]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Curriculums]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupObjects]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_GroupObjects]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_GroupOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Groups]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Groups]
GO
/****** Object:  ForeignKey [FK_Permissions_Organizations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Organizations]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerGroup]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_OwnerGroup]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerUser]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_OwnerUser]
GO
/****** Object:  ForeignKey [FK_Permissions_PageOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_PageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Pages]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Pages]
GO
/****** Object:  ForeignKey [FK_Permissions_StageOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_StageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Stages]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Stages]
GO
/****** Object:  ForeignKey [FK_Permissions_ThemeOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_ThemeOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Themes]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_Themes]
GO
/****** Object:  ForeignKey [FK_Permissions_UserObjects]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] DROP CONSTRAINT [FK_Permissions_UserObjects]
GO
/****** Object:  ForeignKey [FK_CorrectAnswer_Page]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [FK_CorrectAnswer_Page]
GO
/****** Object:  ForeignKey [FK_tblQuestions_tblCompiledQuestions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] DROP CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblResources_tblCourses]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources] DROP CONSTRAINT [FK_tblResources_tblCourses]
GO
/****** Object:  ForeignKey [FK_Curriculums_Stages]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages] DROP CONSTRAINT [FK_Curriculums_Stages]
GO
/****** Object:  ForeignKey [FK_Organizations_Themes]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Organizations_Themes]
GO
/****** Object:  ForeignKey [FK_Stages_Themes]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Stages_Themes]
GO
/****** Object:  ForeignKey [FK_Themes_Course]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] DROP CONSTRAINT [FK_Themes_Course]
GO
/****** Object:  ForeignKey [FK_tblUserAnswers_AnswerTypeRef]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
/****** Object:  ForeignKey [FK_UserAnswer_CorrectAnswer]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
/****** Object:  ForeignKey [FK_UserAnswers_Users]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] DROP CONSTRAINT [FK_UserAnswers_Users]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblComputers]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] DROP CONSTRAINT [FK_tblUsersSignIn_tblComputers]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblUsers]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] DROP CONSTRAINT [FK_tblUsersSignIn_tblUsers]
GO
/****** Object:  ForeignKey [FK_tblVars_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars] DROP CONSTRAINT [FK_tblVars_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses] DROP CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionObjectives_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives] DROP CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractions_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions] DROP CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsScore_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore] DROP CONSTRAINT [FK_tblVarsScore_tblLearnerSessions]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCourse]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCurriculum]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionGroup]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionStage]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionTheme]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_CheckPermissionTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_CheckPermissionTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCourse]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCurriculum]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsGroup]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsStage]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsTheme]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetGroupPermissionsTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetGroupPermissionsTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCourse]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCurriculum]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForGroup]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForStage]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForTheme]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetOperationsForTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetOperationsForTheme]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCourse]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCourse]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsCourse]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCurriculum]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsCurriculum]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsCurriculum]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsGroup]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsGroup]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsGroup]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsStage]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsStage]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsStage]
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsTheme]    Script Date: 04/25/2010 14:34:54 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Security_GetPermissionsTheme]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[Security_GetPermissionsTheme]
GO
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledAnswers]
GO
/****** Object:  Table [dbo].[tblVars]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVars]') AND type in (N'U'))
DROP TABLE [dbo].[tblVars]
GO
/****** Object:  Table [dbo].[tblVarsInteractionCorrectResponses]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsInteractionCorrectResponses]
GO
/****** Object:  Table [dbo].[tblVarsInteractionObjectives]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsInteractionObjectives]
GO
/****** Object:  Table [dbo].[tblVarsInteractions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsInteractions]
GO
/****** Object:  Table [dbo].[tblVarsScore]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsScore]') AND type in (N'U'))
DROP TABLE [dbo].[tblVarsScore]
GO
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]') AND type in (N'U'))
DROP TABLE [dbo].[tblUserAnswers]
GO
/****** Object:  Table [dbo].[tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]') AND type in (N'U'))
DROP TABLE [dbo].[tblLearnerSessions]
GO
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPermissions]') AND type in (N'U'))
DROP TABLE [dbo].[tblPermissions]
GO
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblQuestions]') AND type in (N'U'))
DROP TABLE [dbo].[tblQuestions]
GO
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledQuestionsData]
GO
/****** Object:  Table [dbo].[tblItems]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblItems]') AND type in (N'U'))
DROP TABLE [dbo].[tblItems]
GO
/****** Object:  Table [dbo].[tblThemes]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblThemes]') AND type in (N'U'))
DROP TABLE [dbo].[tblThemes]
GO
/****** Object:  Table [dbo].[tblStages]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblStages]') AND type in (N'U'))
DROP TABLE [dbo].[tblStages]
GO
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblOrganizations]') AND type in (N'U'))
DROP TABLE [dbo].[tblOrganizations]
GO
/****** Object:  Table [dbo].[tblPages]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblPages]') AND type in (N'U'))
DROP TABLE [dbo].[tblPages]
GO
/****** Object:  Table [dbo].[tblUsersSignIn]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]') AND type in (N'U'))
DROP TABLE [dbo].[tblUsersSignIn]
GO
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserGroups]') AND type in (N'U'))
DROP TABLE [dbo].[relUserGroups]
GO
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[relUserRoles]') AND type in (N'U'))
DROP TABLE [dbo].[relUserRoles]
GO
/****** Object:  Table [dbo].[tblResources]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblResources]') AND type in (N'U'))
DROP TABLE [dbo].[tblResources]
GO
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]') AND type in (N'U'))
DROP TABLE [dbo].[tblCompiledQuestions]
GO
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxAnswerType]') AND type in (N'U'))
DROP TABLE [dbo].[fxAnswerType]
GO
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]') AND type in (N'U'))
DROP TABLE [dbo].[fxCompiledStatuses]
GO
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxCourseOperations]
GO
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxCurriculumOperations]
GO
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxGroupOperations]
GO
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxLanguages]') AND type in (N'U'))
DROP TABLE [dbo].[fxLanguages]
GO
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageOperations]
GO
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageOrders]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageOrders]
GO
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxPageTypes]') AND type in (N'U'))
DROP TABLE [dbo].[fxPageTypes]
GO
/****** Object:  Table [dbo].[fxRoles]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxRoles]') AND type in (N'U'))
DROP TABLE [dbo].[fxRoles]
GO
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]') AND type in (N'U'))
DROP TABLE [dbo].[fxSampleBusinesObjectOperation]
GO
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxStageOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxStageOperations]
GO
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]') AND type in (N'U'))
DROP TABLE [dbo].[fxThemeOperations]
GO
/****** Object:  UserDefinedFunction [dbo].[GetSecurityID]    Script Date: 04/25/2010 14:34:59 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSecurityID]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[GetSecurityID]
GO
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]') AND type in (N'U'))
DROP TABLE [dbo].[tblSampleBusinesObject]
GO
/****** Object:  Table [dbo].[tblSettings]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
DROP TABLE [dbo].[tblSettings]
GO
/****** Object:  Table [dbo].[tblComputers]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblComputers]') AND type in (N'U'))
DROP TABLE [dbo].[tblComputers]
GO
/****** Object:  Table [dbo].[tblCourses]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCourses]') AND type in (N'U'))
DROP TABLE [dbo].[tblCourses]
GO
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblCurriculums]') AND type in (N'U'))
DROP TABLE [dbo].[tblCurriculums]
GO
/****** Object:  Table [dbo].[tblGroups]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblGroups]') AND type in (N'U'))
DROP TABLE [dbo].[tblGroups]
GO
/****** Object:  Table [dbo].[tblUserNotes]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserNotes]') AND type in (N'U'))
DROP TABLE [dbo].[tblUserNotes]
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsers]') AND type in (N'U'))
DROP TABLE [dbo].[tblUsers]
GO
/****** Object:  Table [dbo].[tblLearnerAttempts]    Script Date: 04/25/2010 14:34:57 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]') AND type in (N'U'))
DROP TABLE [dbo].[tblLearnerAttempts]
GO
/****** Object:  Table [dbo].[tblLearnerAttempts]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLearnerAttempts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[ThemeRef] [int] NOT NULL,
	[UserRef] [int] NOT NULL,
	[Started] [datetime] NULL,
	[Finished] [datetime] NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblLearnerAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblUsers]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_EMAIL] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_Login] UNIQUE NONCLUSTERED 
(
	[Login] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
SET IDENTITY_INSERT [dbo].[tblUsers] ON
INSERT [dbo].[tblUsers] ([ID], [FirstName], [LastName], [Login], [PasswordHash], [Email], [sysState]) VALUES (1, N'Volodymyr', N'Shtenovych', N'lex', N'B067B3D3054D8868C950E1946300A3F4', N'ShVolodya@gmail.com                               ', 0)
INSERT [dbo].[tblUsers] ([ID], [FirstName], [LastName], [Login], [PasswordHash], [Email], [sysState]) VALUES (3, N'V', N'P', N'vladykx', N'123                             ', N'1                                                 ', 0)
SET IDENTITY_INSERT [dbo].[tblUsers] OFF
/****** Object:  Table [dbo].[tblUserNotes]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUserNotes]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUserNotes](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserRef] [int] NULL,
	[Description] [nvarchar](max) COLLATE Ukrainian_CI_AS NULL,
	[Date] [datetime] NULL,
	[SysState] [smallint] NOT NULL,
 CONSTRAINT [PK_UserNotes] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblGroups]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[tblGroups] ON
INSERT [dbo].[tblGroups] ([ID], [Name], [sysState]) VALUES (1, N'123', 0)
SET IDENTITY_INSERT [dbo].[tblGroups] OFF
/****** Object:  Table [dbo].[tblCurriculums]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblCourses]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblComputers]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblComputers]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblComputers](
	[ID] [int] IDENTITY(10000,1) NOT NULL,
	[ComputerName] [nvarchar](100) COLLATE Ukrainian_CI_AS NULL,
	[IP] [nvarchar](15) COLLATE Ukrainian_CI_AS NULL,
	[LectureRoom] [nvarchar](50) COLLATE Ukrainian_CI_AS NULL,
 CONSTRAINT [PK_tblComputers_1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[tblComputers] ON
INSERT [dbo].[tblComputers] ([ID], [ComputerName], [IP], [LectureRoom]) VALUES (10000, N'', N'127.0.0.1', N'')
SET IDENTITY_INSERT [dbo].[tblComputers] OFF
/****** Object:  Table [dbo].[tblSettings]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblSettings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](250) COLLATE Ukrainian_CI_AS NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblSettings] PRIMARY KEY NONCLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND name = N'PK_Settings')
CREATE UNIQUE CLUSTERED INDEX [PK_Settings] ON [dbo].[tblSettings] 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
IF NOT EXISTS (SELECT * FROM sys.indexes WHERE object_id = OBJECT_ID(N'[dbo].[tblSettings]') AND name = N'UN_Settings')
CREATE UNIQUE NONCLUSTERED INDEX [UN_Settings] ON [dbo].[tblSettings] 
(
	[Name] ASC,
	[sysState] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, DROP_EXISTING = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblSettings] ON
INSERT [dbo].[tblSettings] ([ID], [Name], [Value], [sysState]) VALUES (4, N'compile_service_url', N'http://localhost:8080/Compile.asp', 1)
INSERT [dbo].[tblSettings] ([ID], [Name], [Value], [sysState]) VALUES (8, N'compile_service_url', N'http://localhost:49440/Service1.asmx/Compile', 0)
SET IDENTITY_INSERT [dbo].[tblSettings] OFF
/****** Object:  Table [dbo].[tblSampleBusinesObject]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  UserDefinedFunction [dbo].[GetSecurityID]    Script Date: 04/25/2010 14:34:59 ******/
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
	RETURN ''847928d1-591b-47ef-8f3e-ab7de9ce69cd'';
END' 
END
GO
/****** Object:  Table [dbo].[fxThemeOperations]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxThemeOperations] ON
INSERT [dbo].[fxThemeOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'View', N'View the theme', 0, 0)
INSERT [dbo].[fxThemeOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Pass', N'Pass the theme', 0, 0)
SET IDENTITY_INSERT [dbo].[fxThemeOperations] OFF
/****** Object:  Table [dbo].[fxStageOperations]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxStageOperations] ON
INSERT [dbo].[fxStageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'View', N'View the stage', 0, 0)
INSERT [dbo].[fxStageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Pass', N'Pass the stage', 0, 0)
SET IDENTITY_INSERT [dbo].[fxStageOperations] OFF
/****** Object:  Table [dbo].[fxSampleBusinesObjectOperation]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[fxRoles]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxRoles] ON
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (1, N'STUDENT', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (2, N'LECTOR', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (3, N'TRAINER', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (4, N'ADMIN', NULL, 0)
INSERT [dbo].[fxRoles] ([ID], [Name], [Description], [sysState]) VALUES (5, N'SUPER_ADMIN', NULL, 0)
SET IDENTITY_INSERT [dbo].[fxRoles] OFF
/****** Object:  Table [dbo].[fxPageTypes]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxPageTypes] ON
INSERT [dbo].[fxPageTypes] ([ID], [Type], [sysState]) VALUES (1, N'Theory', 0)
INSERT [dbo].[fxPageTypes] ([ID], [Type], [sysState]) VALUES (2, N'Practice', 0)
SET IDENTITY_INSERT [dbo].[fxPageTypes] OFF
/****** Object:  Table [dbo].[fxPageOrders]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxPageOrders] ON
INSERT [dbo].[fxPageOrders] ([ID], [Name], [sysState]) VALUES (1, N'Straight', 0)
INSERT [dbo].[fxPageOrders] ([ID], [Name], [sysState]) VALUES (2, N'Random', 0)
SET IDENTITY_INSERT [dbo].[fxPageOrders] OFF
/****** Object:  Table [dbo].[fxPageOperations]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxPageOperations] ON
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (1, N'Add', N'Add new Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (2, N'Edit', N'Edit Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (3, N'View', N'View Page', 1, 0)
INSERT [dbo].[fxPageOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (4, N'Delete', N'Delete Page', 1, 0)
SET IDENTITY_INSERT [dbo].[fxPageOperations] OFF
/****** Object:  Table [dbo].[fxLanguages]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
/****** Object:  Table [dbo].[fxGroupOperations]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UK_fxGroupOperations_Name] UNIQUE NONCLUSTERED 
(
	[Name] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxGroupOperations] ON
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (1, N'View', NULL, 1, 0)
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (2, N'Rename', NULL, 1, 0)
INSERT [dbo].[fxGroupOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (3, N'ChangeMembers', NULL, 1, 0)
SET IDENTITY_INSERT [dbo].[fxGroupOperations] OFF
/****** Object:  Table [dbo].[fxCurriculumOperations]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxCurriculumOperations] ON
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'Modify', N'Modify curriculum by teacher', 1, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Use', N'Use curriculum by teacher', 1, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (7, N'View', N'View the curriculum', 0, 0)
INSERT [dbo].[fxCurriculumOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (8, N'Pass', N'Pass the curriculum', 0, 0)
SET IDENTITY_INSERT [dbo].[fxCurriculumOperations] OFF
/****** Object:  Table [dbo].[fxCourseOperations]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxCourseOperations] ON
INSERT [dbo].[fxCourseOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (5, N'Modify', N'Modify course by teacher', 1, 0)
INSERT [dbo].[fxCourseOperations] ([ID], [Name], [Description], [CanBeDelegated], [sysState]) VALUES (6, N'Use', N'Use course by teacher', 1, 0)
SET IDENTITY_INSERT [dbo].[fxCourseOperations] OFF
/****** Object:  Table [dbo].[fxCompiledStatuses]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
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
/****** Object:  Table [dbo].[fxAnswerType]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET IDENTITY_INSERT [dbo].[fxAnswerType] ON
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (1, N'UserAnswer', 0)
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (2, N'EmptyAnswer', 0)
INSERT [dbo].[fxAnswerType] ([ID], [Name], [sysState]) VALUES (3, N'NotIncludedAnswer', 0)
SET IDENTITY_INSERT [dbo].[fxAnswerType] OFF
/****** Object:  Table [dbo].[tblCompiledQuestions]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblResources]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[relUserRoles]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 1, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 2, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 3, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 4, 0)
INSERT [dbo].[relUserRoles] ([UserID], [RoleID], [sysState]) VALUES (1, 5, 0)
/****** Object:  Table [dbo].[relUserGroups]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
INSERT [dbo].[relUserGroups] ([UserRef], [GroupRef], [sysState]) VALUES (1, 1, 0)
/****** Object:  Table [dbo].[tblUsersSignIn]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblUsersSignIn](
	[UserId] [int] NOT NULL,
	[ComputerId] [int] NOT NULL,
	[LastLogin] [datetime] NULL,
 CONSTRAINT [PK_tblUsersSignIn_1] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblPages]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblOrganizations]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblStages]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblThemes]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblItems]    Script Date: 04/25/2010 14:34:57 ******/
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
	[Rank] [int] NULL,
 CONSTRAINT [PK_tblItems] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblCompiledQuestionsData]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblQuestions]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblPermissions]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblLearnerSessions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerAttemptRef] [int] NOT NULL,
	[ItemRef] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblAttempts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblUserAnswers]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  Table [dbo].[tblVarsScore]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsScore]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsScore](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsScore] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVarsInteractions]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractions](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblVarsInteractions] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVarsInteractionObjectives]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractionObjectives](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob1] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVarsInteractionCorrectResponses]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVarsInteractionCorrectResponses](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[InteractionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[Number] [int] NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_Bob2] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblVars]    Script Date: 04/25/2010 14:34:57 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tblVars]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[tblVars](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[LearnerSessionRef] [int] NOT NULL,
	[Name] [varchar](200) COLLATE Ukrainian_CI_AS NOT NULL,
	[Value] [varchar](max) COLLATE Ukrainian_CI_AS NOT NULL,
	[sysState] [smallint] NOT NULL,
 CONSTRAINT [PK_tblAttemptsVars] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblCompiledAnswers]    Script Date: 04/25/2010 14:34:57 ******/
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
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsTheme]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsStage]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsGroup]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCurriculum]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetPermissionsCourse]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForTheme]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForStage]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForGroup]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCurriculum]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetOperationsForCourse]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsTheme]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsStage]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsGroup]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCurriculum]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_GetGroupPermissionsCourse]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionTheme]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionStage]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionGroup]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCurriculum]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  StoredProcedure [dbo].[Security_CheckPermissionCourse]    Script Date: 04/25/2010 14:34:54 ******/
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
/****** Object:  Default [DF__fxAnswerT__sysSt__0C85DE4D]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxAnswerT__sysSt__0C85DE4D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxAnswerType]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxAnswerT__sysSt__0C85DE4D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxAnswerType] ADD  CONSTRAINT [DF__fxAnswerT__sysSt__0C85DE4D]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxCompile__sysSt__73BA3083]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCompile__sysSt__73BA3083]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCompiledStatuses]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCompile__sysSt__73BA3083]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCompiledStatuses] ADD  CONSTRAINT [DF__fxCompile__sysSt__73BA3083]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxCourseO__sysSt__72C60C4A]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCourseO__sysSt__72C60C4A]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCourseOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCourseO__sysSt__72C60C4A]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCourseOperations] ADD  CONSTRAINT [DF__fxCourseO__sysSt__72C60C4A]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxCurricu__sysSt__71D1E811]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxCurricu__sysSt__71D1E811]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxCurriculumOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxCurricu__sysSt__71D1E811]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxCurriculumOperations] ADD  CONSTRAINT [DF__fxCurricu__sysSt__71D1E811]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxGroupOp__sysSt__03F0984C]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxGroupOp__sysSt__03F0984C]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxGroupOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxGroupOp__sysSt__03F0984C]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxGroupOperations] ADD  CONSTRAINT [DF__fxGroupOp__sysSt__03F0984C]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxLanguag__sysSt__70DDC3D8]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxLanguag__sysSt__70DDC3D8]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxLanguages]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxLanguag__sysSt__70DDC3D8]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxLanguages] ADD  CONSTRAINT [DF__fxLanguag__sysSt__70DDC3D8]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxPageOpe__sysSt__6FE99F9F]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOpe__sysSt__6FE99F9F]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOpe__sysSt__6FE99F9F]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOperations] ADD  CONSTRAINT [DF__fxPageOpe__sysSt__6FE99F9F]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxPageOrd__sysSt__6EF57B66]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageOrd__sysSt__6EF57B66]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageOrders]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageOrd__sysSt__6EF57B66]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageOrders] ADD  CONSTRAINT [DF__fxPageOrd__sysSt__6EF57B66]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxPageTyp__sysSt__6E01572D]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxPageTyp__sysSt__6E01572D]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxPageTypes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxPageTyp__sysSt__6E01572D]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxPageTypes] ADD  CONSTRAINT [DF__fxPageTyp__sysSt__6E01572D]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxRoles__sysStat__6D0D32F4]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxRoles__sysStat__6D0D32F4]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxRoles]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxRoles__sysStat__6D0D32F4]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxRoles] ADD  CONSTRAINT [DF__fxRoles__sysStat__6D0D32F4]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxSampleB__sysSt__6A30C649]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxSampleB__sysSt__6A30C649]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxSampleBusinesObjectOperation]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxSampleB__sysSt__6A30C649]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxSampleBusinesObjectOperation] ADD  CONSTRAINT [DF__fxSampleB__sysSt__6A30C649]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxStageOp__sysSt__6C190EBB]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxStageOp__sysSt__6C190EBB]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxStageOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxStageOp__sysSt__6C190EBB]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxStageOperations] ADD  CONSTRAINT [DF__fxStageOp__sysSt__6C190EBB]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__fxThemeOp__sysSt__6B24EA82]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__fxThemeOp__sysSt__6B24EA82]') AND parent_object_id = OBJECT_ID(N'[dbo].[fxThemeOperations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__fxThemeOp__sysSt__6B24EA82]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[fxThemeOperations] ADD  CONSTRAINT [DF__fxThemeOp__sysSt__6B24EA82]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__relUserGr__sysSt__02FC7413]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserGr__sysSt__02FC7413]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserGr__sysSt__02FC7413]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserGroups] ADD  CONSTRAINT [DF__relUserGr__sysSt__02FC7413]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__relUserRo__sysSt__02084FDA]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__relUserRo__sysSt__02084FDA]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__relUserRo__sysSt__02084FDA]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[relUserRoles] ADD  CONSTRAINT [DF__relUserRo__sysSt__02084FDA]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__76969D2E]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__76969D2E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__76969D2E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__sysSt__76969D2E]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCompil__UserA__04E4BC85]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__UserA__04E4BC85]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__UserA__04E4BC85]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__UserA__04E4BC85]  DEFAULT ((0)) FOR [UserAnswerRef]
END


End
GO
/****** Object:  Default [DF__tblCompil__Compi__08B54D69]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__Compi__08B54D69]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__Compi__08B54D69]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledAnswers] ADD  CONSTRAINT [DF__tblCompil__Compi__08B54D69]  DEFAULT ((0)) FOR [CompiledQuestionsDataRef]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__778AC167]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__778AC167]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__778AC167]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestions] ADD  CONSTRAINT [DF__tblCompil__sysSt__778AC167]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCompil__sysSt__7D439ABD]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCompil__sysSt__7D439ABD]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCompil__sysSt__7D439ABD]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCompiledQuestionsData] ADD  CONSTRAINT [DF__tblCompil__sysSt__7D439ABD]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCourse__sysSt__75A278F5]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCourse__sysSt__75A278F5]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCourses]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCourse__sysSt__75A278F5]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCourses] ADD  CONSTRAINT [DF__tblCourse__sysSt__75A278F5]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblCurric__sysSt__74AE54BC]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblCurric__sysSt__74AE54BC]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCurriculums]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblCurric__sysSt__74AE54BC]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblCurriculums] ADD  CONSTRAINT [DF__tblCurric__sysSt__74AE54BC]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblGroups__sysSt__693CA210]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblGroups__sysSt__693CA210]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblGroups]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblGroups__sysSt__693CA210]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblGroups] ADD  CONSTRAINT [DF__tblGroups__sysSt__693CA210]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblItems_sysState]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblItems_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblItems_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblItems] ADD  CONSTRAINT [DF_tblItems_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblLearnerAttempts_sysState]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblLearnerAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerAttempts]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblLearnerAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerAttempts] ADD  CONSTRAINT [DF_tblLearnerAttempts_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblAttempts_sysState]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttempts_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttempts_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblLearnerSessions] ADD  CONSTRAINT [DF_tblAttempts_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblOrganizations_sysState]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblOrganizations_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblOrganizations_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblOrganizations] ADD  CONSTRAINT [DF_tblOrganizations_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblPages__sysSta__7C4F7684]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPages__sysSta__7C4F7684]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPages__sysSta__7C4F7684]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPages] ADD  CONSTRAINT [DF__tblPages__sysSta__7C4F7684]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblPermis__sysSt__7B5B524B]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblPermis__sysSt__7B5B524B]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblPermis__sysSt__7B5B524B]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblPermissions] ADD  CONSTRAINT [DF__tblPermis__sysSt__7B5B524B]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblQuesti__sysSt__7E37BEF6]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblQuesti__sysSt__7E37BEF6]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblQuesti__sysSt__7E37BEF6]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblQuestions] ADD  CONSTRAINT [DF__tblQuesti__sysSt__7E37BEF6]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblResources_sysState]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblResources_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblResources_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblResources] ADD  CONSTRAINT [DF_tblResources_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblSample__sysSt__68487DD7]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblSample__sysSt__68487DD7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSampleBusinesObject]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblSample__sysSt__68487DD7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSampleBusinesObject] ADD  CONSTRAINT [DF__tblSample__sysSt__68487DD7]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblSettings_sysState]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblSettings_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblSettings]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblSettings_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblSettings] ADD  CONSTRAINT [DF_tblSettings_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblStages__sysSt__787EE5A0]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblStages__sysSt__787EE5A0]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblStages__sysSt__787EE5A0]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblStages] ADD  CONSTRAINT [DF__tblStages__sysSt__787EE5A0]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblThemes__sysSt__797309D9]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__sysSt__797309D9]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__sysSt__797309D9]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__sysSt__797309D9]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblThemes__PageC__06CD04F7]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__PageC__06CD04F7]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__PageC__06CD04F7]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__PageC__06CD04F7]  DEFAULT (NULL) FOR [PageCountToShow]
END


End
GO
/****** Object:  Default [DF__tblThemes__MaxCo__07C12930]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblThemes__MaxCo__07C12930]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblThemes__MaxCo__07C12930]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblThemes] ADD  CONSTRAINT [DF__tblThemes__MaxCo__07C12930]  DEFAULT (NULL) FOR [MaxCountToSubmit]
END


End
GO
/****** Object:  Default [DF__tblUserAn__sysSt__01142BA1]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__sysSt__01142BA1]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__sysSt__01142BA1]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__sysSt__01142BA1]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF__tblUserAn__Answe__0D7A0286]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUserAn__Answe__0D7A0286]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUserAn__Answe__0D7A0286]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUserAnswers] ADD  CONSTRAINT [DF__tblUserAn__Answe__0D7A0286]  DEFAULT ((1)) FOR [AnswerTypeRef]
END


End
GO
/****** Object:  Default [DF__tblUsers__sysSta__6754599E]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF__tblUsers__sysSta__6754599E]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsers]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF__tblUsers__sysSta__6754599E]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblUsers] ADD  CONSTRAINT [DF__tblUsers__sysSta__6754599E]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  Default [DF_tblAttemptsVars_sysState]    Script Date: 04/25/2010 14:34:57 ******/
IF Not EXISTS (SELECT * FROM sys.default_constraints WHERE object_id = OBJECT_ID(N'[dbo].[DF_tblAttemptsVars_sysState]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
Begin
IF NOT EXISTS (SELECT * FROM dbo.sysobjects WHERE id = OBJECT_ID(N'[DF_tblAttemptsVars_sysState]') AND type = 'D')
BEGIN
ALTER TABLE [dbo].[tblVars] ADD  CONSTRAINT [DF_tblAttemptsVars_sysState]  DEFAULT ((0)) FOR [sysState]
END


End
GO
/****** Object:  ForeignKey [FK_GROUP]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_GROUP] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_GROUP]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_GROUP]
GO
/****** Object:  ForeignKey [FK_USER]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups]  WITH CHECK ADD  CONSTRAINT [FK_USER] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserGroups]'))
ALTER TABLE [dbo].[relUserGroups] CHECK CONSTRAINT [FK_USER]
GO
/****** Object:  ForeignKey [FK_ROLE_ID]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_ROLE_ID] FOREIGN KEY([RoleID])
REFERENCES [dbo].[fxRoles] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_ROLE_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_ROLE_ID]
GO
/****** Object:  ForeignKey [FK_USER_ID]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles]  WITH CHECK ADD  CONSTRAINT [FK_USER_ID] FOREIGN KEY([UserID])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_USER_ID]') AND parent_object_id = OBJECT_ID(N'[dbo].[relUserRoles]'))
ALTER TABLE [dbo].[relUserRoles] CHECK CONSTRAINT [FK_USER_ID]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_fxdCompiledStatuses]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses] FOREIGN KEY([StatusRef])
REFERENCES [dbo].[fxCompiledStatuses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_fxdCompiledStatuses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_fxdCompiledStatuses]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblCompiledQuestionsData]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData] FOREIGN KEY([CompiledQuestionsDataRef])
REFERENCES [dbo].[tblCompiledQuestionsData] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblCompiledQuestionsData]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblCompiledQuestionsData]
GO
/****** Object:  ForeignKey [FK_tblCompiledAnswers_tblUserAnswers]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers] FOREIGN KEY([UserAnswerRef])
REFERENCES [dbo].[tblUserAnswers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledAnswers_tblUserAnswers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledAnswers]'))
ALTER TABLE [dbo].[tblCompiledAnswers] CHECK CONSTRAINT [FK_tblCompiledAnswers_tblUserAnswers]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestions_fxdLanguages]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages] FOREIGN KEY([LanguageRef])
REFERENCES [dbo].[fxLanguages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestions_fxdLanguages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestions]'))
ALTER TABLE [dbo].[tblCompiledQuestions] CHECK CONSTRAINT [FK_tblCompiledQuestions_fxdLanguages]
GO
/****** Object:  ForeignKey [FK_tblCompiledQuestionsData_tblCompiledQuestions]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData]  WITH CHECK ADD  CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblCompiledQuestionsData_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblCompiledQuestionsData]'))
ALTER TABLE [dbo].[tblCompiledQuestionsData] CHECK CONSTRAINT [FK_tblCompiledQuestionsData_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblItems_tblItems]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblItems] FOREIGN KEY([PID])
REFERENCES [dbo].[tblItems] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblItems]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblItems]
GO
/****** Object:  ForeignKey [FK_tblItems_tblOrganizations]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblOrganizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblOrganizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblOrganizations]
GO
/****** Object:  ForeignKey [FK_tblItems_tblResources]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblResources] FOREIGN KEY([ResourceRef])
REFERENCES [dbo].[tblResources] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblResources]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblItems]'))
ALTER TABLE [dbo].[tblItems] CHECK CONSTRAINT [FK_tblItems_tblResources]
GO
/****** Object:  ForeignKey [FK_tblItems_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblItems_tblLearnerSessions] FOREIGN KEY([ItemRef])
REFERENCES [dbo].[tblItems] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblItems_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblItems_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblLearnerAttempts_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions]  WITH CHECK ADD  CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions] FOREIGN KEY([LearnerAttemptRef])
REFERENCES [dbo].[tblLearnerAttempts] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblLearnerAttempts_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblLearnerSessions]'))
ALTER TABLE [dbo].[tblLearnerSessions] CHECK CONSTRAINT [FK_tblLearnerAttempts_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblOrganizations_tblCourses]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations]  WITH CHECK ADD  CONSTRAINT [FK_tblOrganizations_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblOrganizations_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblOrganizations]'))
ALTER TABLE [dbo].[tblOrganizations] CHECK CONSTRAINT [FK_tblOrganizations_tblCourses]
GO
/****** Object:  ForeignKey [FK_Page_PageType]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages]  WITH CHECK ADD  CONSTRAINT [FK_Page_PageType] FOREIGN KEY([PageTypeRef])
REFERENCES [dbo].[fxPageTypes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Page_PageType]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPages]'))
ALTER TABLE [dbo].[tblPages] CHECK CONSTRAINT [FK_Page_PageType]
GO
/****** Object:  ForeignKey [FK_PARENT_PERMITION]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_PARENT_PERMITION] FOREIGN KEY([ParentPermitionRef])
REFERENCES [dbo].[tblPermissions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_PARENT_PERMITION]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_PARENT_PERMITION]
GO
/****** Object:  ForeignKey [FK_Permissions_CourseOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CourseOperations] FOREIGN KEY([CourseOperationRef])
REFERENCES [dbo].[fxCourseOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CourseOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CourseOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Courses]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Courses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Courses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Courses]
GO
/****** Object:  ForeignKey [FK_Permissions_CurriculumOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_CurriculumOperations] FOREIGN KEY([CurriculumOperationRef])
REFERENCES [dbo].[fxCurriculumOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_CurriculumOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_CurriculumOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Curriculums]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Curriculums] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Curriculums]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Curriculums]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupObjects]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupObjects] FOREIGN KEY([GroupObjectRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupObjects]
GO
/****** Object:  ForeignKey [FK_Permissions_GroupOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupOperations] FOREIGN KEY([GroupOperationRef])
REFERENCES [dbo].[fxGroupOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_GroupOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Groups]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Groups] FOREIGN KEY([GroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Groups]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Groups]
GO
/****** Object:  ForeignKey [FK_Permissions_Organizations]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Organizations] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Organizations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Organizations]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerGroup]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerGroup] FOREIGN KEY([OwnerGroupRef])
REFERENCES [dbo].[tblGroups] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerGroup]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerGroup]
GO
/****** Object:  ForeignKey [FK_Permissions_OwnerUser]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_OwnerUser] FOREIGN KEY([OwnerUserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_OwnerUser]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_OwnerUser]
GO
/****** Object:  ForeignKey [FK_Permissions_PageOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PageOperations] FOREIGN KEY([PageOperationRef])
REFERENCES [dbo].[fxPageOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_PageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_PageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Pages]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Pages] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Pages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Pages]
GO
/****** Object:  ForeignKey [FK_Permissions_StageOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_StageOperations] FOREIGN KEY([StageOperationRef])
REFERENCES [dbo].[fxStageOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_StageOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_StageOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Stages]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Stages] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Stages]
GO
/****** Object:  ForeignKey [FK_Permissions_ThemeOperations]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_ThemeOperations] FOREIGN KEY([ThemeOperationRef])
REFERENCES [dbo].[fxThemeOperations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_ThemeOperations]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_ThemeOperations]
GO
/****** Object:  ForeignKey [FK_Permissions_Themes]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Themes] FOREIGN KEY([ThemeRef])
REFERENCES [dbo].[tblThemes] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_Themes]
GO
/****** Object:  ForeignKey [FK_Permissions_UserObjects]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_UserObjects] FOREIGN KEY([UserObjectRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Permissions_UserObjects]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblPermissions]'))
ALTER TABLE [dbo].[tblPermissions] CHECK CONSTRAINT [FK_Permissions_UserObjects]
GO
/****** Object:  ForeignKey [FK_CorrectAnswer_Page]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_CorrectAnswer_Page] FOREIGN KEY([PageRef])
REFERENCES [dbo].[tblPages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_CorrectAnswer_Page]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_CorrectAnswer_Page]
GO
/****** Object:  ForeignKey [FK_tblQuestions_tblCompiledQuestions]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestions_tblCompiledQuestions] FOREIGN KEY([CompiledQuestionRef])
REFERENCES [dbo].[tblCompiledQuestions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblQuestions_tblCompiledQuestions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblQuestions]'))
ALTER TABLE [dbo].[tblQuestions] CHECK CONSTRAINT [FK_tblQuestions_tblCompiledQuestions]
GO
/****** Object:  ForeignKey [FK_tblResources_tblCourses]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources]  WITH CHECK ADD  CONSTRAINT [FK_tblResources_tblCourses] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblResources_tblCourses]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblResources]'))
ALTER TABLE [dbo].[tblResources] CHECK CONSTRAINT [FK_tblResources_tblCourses]
GO
/****** Object:  ForeignKey [FK_Curriculums_Stages]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages]  WITH CHECK ADD  CONSTRAINT [FK_Curriculums_Stages] FOREIGN KEY([CurriculumRef])
REFERENCES [dbo].[tblCurriculums] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Curriculums_Stages]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblStages]'))
ALTER TABLE [dbo].[tblStages] CHECK CONSTRAINT [FK_Curriculums_Stages]
GO
/****** Object:  ForeignKey [FK_Organizations_Themes]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Organizations_Themes] FOREIGN KEY([OrganizationRef])
REFERENCES [dbo].[tblOrganizations] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Organizations_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Organizations_Themes]
GO
/****** Object:  ForeignKey [FK_Stages_Themes]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Stages_Themes] FOREIGN KEY([StageRef])
REFERENCES [dbo].[tblStages] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Stages_Themes]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Stages_Themes]
GO
/****** Object:  ForeignKey [FK_Themes_Course]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes]  WITH CHECK ADD  CONSTRAINT [FK_Themes_Course] FOREIGN KEY([CourseRef])
REFERENCES [dbo].[tblCourses] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Themes_Course]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblThemes]'))
ALTER TABLE [dbo].[tblThemes] CHECK CONSTRAINT [FK_Themes_Course]
GO
/****** Object:  ForeignKey [FK_tblUserAnswers_AnswerTypeRef]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef] FOREIGN KEY([AnswerTypeRef])
REFERENCES [dbo].[fxAnswerType] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUserAnswers_AnswerTypeRef]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_tblUserAnswers_AnswerTypeRef]
GO
/****** Object:  ForeignKey [FK_UserAnswer_CorrectAnswer]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswer_CorrectAnswer] FOREIGN KEY([QuestionRef])
REFERENCES [dbo].[tblQuestions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswer_CorrectAnswer]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswer_CorrectAnswer]
GO
/****** Object:  ForeignKey [FK_UserAnswers_Users]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers]  WITH CHECK ADD  CONSTRAINT [FK_UserAnswers_Users] FOREIGN KEY([UserRef])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_UserAnswers_Users]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUserAnswers]'))
ALTER TABLE [dbo].[tblUserAnswers] CHECK CONSTRAINT [FK_UserAnswers_Users]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblComputers]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn]  WITH CHECK ADD  CONSTRAINT [FK_tblUsersSignIn_tblComputers] FOREIGN KEY([ComputerId])
REFERENCES [dbo].[tblComputers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblComputers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] CHECK CONSTRAINT [FK_tblUsersSignIn_tblComputers]
GO
/****** Object:  ForeignKey [FK_tblUsersSignIn_tblUsers]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn]  WITH CHECK ADD  CONSTRAINT [FK_tblUsersSignIn_tblUsers] FOREIGN KEY([UserId])
REFERENCES [dbo].[tblUsers] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblUsersSignIn_tblUsers]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblUsersSignIn]'))
ALTER TABLE [dbo].[tblUsersSignIn] CHECK CONSTRAINT [FK_tblUsersSignIn_tblUsers]
GO
/****** Object:  ForeignKey [FK_tblVars_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars]  WITH CHECK ADD  CONSTRAINT [FK_tblVars_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVars_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVars]'))
ALTER TABLE [dbo].[tblVars] CHECK CONSTRAINT [FK_tblVars_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionCorrectResponses]'))
ALTER TABLE [dbo].[tblVarsInteractionCorrectResponses] CHECK CONSTRAINT [FK_tblVarsInteractionCorrectResponses_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractionObjectives_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractionObjectives_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractionObjectives]'))
ALTER TABLE [dbo].[tblVarsInteractionObjectives] CHECK CONSTRAINT [FK_tblVarsInteractionObjectives_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsInteractions_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsInteractions_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsInteractions]'))
ALTER TABLE [dbo].[tblVarsInteractions] CHECK CONSTRAINT [FK_tblVarsInteractions_tblLearnerSessions]
GO
/****** Object:  ForeignKey [FK_tblVarsScore_tblLearnerSessions]    Script Date: 04/25/2010 14:34:57 ******/
IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore]  WITH CHECK ADD  CONSTRAINT [FK_tblVarsScore_tblLearnerSessions] FOREIGN KEY([LearnerSessionRef])
REFERENCES [dbo].[tblLearnerSessions] ([ID])
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_tblVarsScore_tblLearnerSessions]') AND parent_object_id = OBJECT_ID(N'[dbo].[tblVarsScore]'))
ALTER TABLE [dbo].[tblVarsScore] CHECK CONSTRAINT [FK_tblVarsScore_tblLearnerSessions]
GO
