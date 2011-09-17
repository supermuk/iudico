ALTER TABLE [dbo].[Group]
    ADD CONSTRAINT [DF_Group_Deleted] DEFAULT ((0)) FOR [Deleted];

