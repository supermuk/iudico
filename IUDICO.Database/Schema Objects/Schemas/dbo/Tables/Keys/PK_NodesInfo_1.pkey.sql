﻿ALTER TABLE [dbo].[NodesInfo]
    ADD CONSTRAINT [PK_NodesInfo_1] PRIMARY KEY CLUSTERED ([NodeId] ASC) WITH (ALLOW_PAGE_LOCKS = ON, ALLOW_ROW_LOCKS = ON, PAD_INDEX = OFF, IGNORE_DUP_KEY = OFF, STATISTICS_NORECOMPUTE = OFF);
