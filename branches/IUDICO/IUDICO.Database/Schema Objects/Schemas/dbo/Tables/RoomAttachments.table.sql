CREATE TABLE [dbo].[RoomAttachments] (
    [RoomRef] INT					NOT NULL,
	[ComputerRef] NVARCHAR (20)  NOT NULL UNIQUE
);