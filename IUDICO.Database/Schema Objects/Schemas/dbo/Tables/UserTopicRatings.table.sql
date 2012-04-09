CREATE TABLE [dbo].[UserTopicRatings] (
    [UserId]  UNIQUEIDENTIFIER NOT NULL,
    [TopicId] INT              NOT NULL,
    [Rating]  INT              NOT NULL
);

