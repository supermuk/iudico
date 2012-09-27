CREATE TABLE [dbo].[CurriculumChapterTopics] (
    [Id]                       INT      IDENTITY (1, 1) NOT NULL,
    [CurriculumChapterRef]     INT      NOT NULL,
    [TopicRef]                 INT      NOT NULL,
    [TestStartDate]            DATETIME NULL,
    [TestEndDate]              DATETIME NULL,
    [TheoryStartDate]          DATETIME NULL,
    [TheoryEndDate]            DATETIME NULL,
    [ThresholdOfSuccess]       INT      DEFAULT '50' NOT NULL,
    [BlockTopicAtTesting]      BIT      NOT NULL,
    [BlockCurriculumAtTesting] BIT      NOT NULL,
    [IsDeleted]                BIT      NOT NULL
);

