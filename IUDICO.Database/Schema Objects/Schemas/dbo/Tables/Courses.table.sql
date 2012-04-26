CREATE TABLE [dbo].[Courses] (
    [Id]         INT           IDENTITY (1, 1) NOT NULL,
    [Name]       NVARCHAR (50) COLLATE Ukrainian_CI_AS NOT NULL,
    [Owner]      NVARCHAR (50) COLLATE Ukrainian_CI_AS NULL,
    [UpdatedBy]  NVARCHAR (50) COLLATE Ukrainian_CI_AS NULL,
    [Created]    DATETIME      NOT NULL,
    [Updated]    DATETIME      NOT NULL,
    [Deleted]    BIT           NOT NULL,
    [Locked]     BIT           NULL,
    [Sequencing] XML           NULL
);

