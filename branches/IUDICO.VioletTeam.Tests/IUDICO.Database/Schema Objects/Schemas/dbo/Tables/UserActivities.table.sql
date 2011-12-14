CREATE TABLE [dbo].[UserActivities]
(
    [Id]                int IDENTITY (1, 1) NOT NULL,
    [UserRef]           uniqueidentifier NULL,
    [RequestStartTime]  datetime NOT NULL,
    [RequestEndTime]    datetime NOT NULL,
    [RequestLength]     int NOT NULL,
    [ResponseLength]    int NOT NULL,
    [Request]           nvarchar(max) NOT NULL
)
