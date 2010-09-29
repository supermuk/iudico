/****** Add column PageRef with FOREIGN KEY******/
ALTER TABLE [tblPermissions] ADD PageRef int NULL;
GO
ALTER TABLE [tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_Pages] FOREIGN KEY([PageRef])
REFERENCES [tblPages] ([ID])
GO
ALTER TABLE [tblPermissions] CHECK CONSTRAINT [FK_Permissions_Pages]
GO


/****** Add column PageOperationRef with FOREIGN KEY******/
ALTER TABLE [tblPermissions] ADD PageOperationRef int NULL;
GO
ALTER TABLE [tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_PageOperations] FOREIGN KEY([PageOperationRef])
REFERENCES [fxPageOperations] ([ID])
GO
ALTER TABLE [tblPermissions] CHECK CONSTRAINT [FK_Permissions_PageOperations]
GO


/****** Add column UserObjectRef with FOREIGN KEY******/
ALTER TABLE [tblPermissions] ADD UserObjectRef int NULL;
GO
ALTER TABLE [tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_UserObjects] FOREIGN KEY([UserObjectRef])
REFERENCES [tblUsers] ([ID])
GO
ALTER TABLE [tblPermissions] CHECK CONSTRAINT [FK_Permissions_UserObjects]
GO


/****** Add column GroupObjectRef with FOREIGN KEY******/
ALTER TABLE [tblPermissions] ADD GroupObjectRef int NULL;
GO
ALTER TABLE [tblPermissions]  WITH CHECK ADD  CONSTRAINT [FK_Permissions_GroupObjects] FOREIGN KEY([GroupObjectRef])
REFERENCES [tblGroups] ([ID])
GO
ALTER TABLE [tblPermissions] CHECK CONSTRAINT [FK_Permissions_GroupObjects]
GO


