﻿CREATE TABLE [dbo].[User] (
    [Id]           UNIQUEIDENTIFIER NOT NULL,
    [Username]     NVARCHAR (100)   COLLATE Ukrainian_CI_AS NOT NULL,
    [Password]     NVARCHAR (50)    COLLATE Ukrainian_CI_AS NOT NULL,
    [Email]        NVARCHAR (100)   COLLATE Ukrainian_CI_AS NOT NULL,
    [OpenId]       NVARCHAR (200)   COLLATE Ukrainian_CI_AS NOT NULL,
    [Name]         NVARCHAR (200)   COLLATE Ukrainian_CI_AS NOT NULL,
    [IsApproved]   BIT              NOT NULL,
    [Deleted]      BIT              NOT NULL,
    [CreationDate] DATETIME         NOT NULL,
    [ApprovedBy]   UNIQUEIDENTIFIER NULL,
    [UserId]       NVARCHAR (100)   NULL
);