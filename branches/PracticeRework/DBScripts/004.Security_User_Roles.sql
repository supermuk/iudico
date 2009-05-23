CREATE TABLE relUserGroups
(
	UserRef INT NOT NULL,
	GroupRef INT NOT NULL
)
GO

ALTER TABLE relUserGroups 
	ADD CONSTRAINT PK_relUserGroups_KEY
	PRIMARY KEY (UserRef, GroupRef)
	
ALTER TABLE relUserGroups
	ADD CONSTRAINT FK_USER
	FOREIGN KEY (UserRef)
	REFERENCES tblUsers(ID)
	
ALTER TABLE relUserGroups
	ADD CONSTRAINT FK_GROUP
	FOREIGN KEY (GroupRef)
	REFERENCES tblGroups(ID)
	
