DROP TABLE tblCompiledAnswersData

ALTER TABLE tblUserAnswers DROP CONSTRAINT FK_tblUserAnswers_tblCompiledAnswers

ALTER TABLE tblUserAnswers DROP COLUMN CompiledAnswerRef


ALTER TABLE tblCompiledAnswers ADD UserAnswerRef int not null DEFAULT 0 

ALTER TABLE tblCompiledAnswers ADD Output nvarchar(MAX) 

ALTER TABLE tblCompiledAnswers WITH CHECK ADD CONSTRAINT FK_tblCompiledAnswers_tblUserAnswers FOREIGN KEY (UserAnswerRef) REFERENCES tblUserAnswers (ID)
GO
ALTER TABLE tblCompiledAnswers CHECK CONSTRAINT FK_tblCompiledAnswers_tblUserAnswers
