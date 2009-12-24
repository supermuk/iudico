CREATE TABLE [fxAnswerType](
	[ID] [int] IDENTITY(1, 1) NOT NULL,
	[Name] [nvarchar](20) NULL,
        [sysState] [smallint] NOT NULL DEFAULT 0
 CONSTRAINT [PK_fxAnswerType] PRIMARY KEY  
(
	[ID] ASC
) 
)

INSERT INTO fxAnswerType (Name) VALUES ('UserAnswer');
INSERT INTO fxAnswerType (Name) VALUES ('EmptyAnswer');
INSERT INTO fxAnswerType (Name) VALUES ('NotIncludedAnswer');

ALTER TABLE tblUserAnswers ADD AnswerTypeRef int not null DEFAULT 1


ALTER TABLE tblUserAnswers WITH CHECK ADD CONSTRAINT FK_tblUserAnswers_AnswerTypeRef FOREIGN KEY (AnswerTypeRef) REFERENCES fxAnswerType (ID)
GO
ALTER TABLE tblUserAnswers CHECK CONSTRAINT FK_tblUserAnswers_AnswerTypeRef