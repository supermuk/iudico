ALTER TABLE tblCompiledAnswers ADD CompiledQuestionsDataRef int not null DEFAULT 0


ALTER TABLE tblCompiledAnswers WITH CHECK ADD CONSTRAINT FK_tblCompiledAnswers_tblCompiledQuestionsData FOREIGN KEY (CompiledQuestionsDataRef) REFERENCES tblCompiledQuestionsData (ID)
GO
ALTER TABLE tblCompiledAnswers CHECK CONSTRAINT FK_tblCompiledAnswers_tblCompiledQuestionsData