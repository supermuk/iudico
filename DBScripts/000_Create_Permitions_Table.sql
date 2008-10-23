CREATE TABLE tblPermission
(
	ID int IDENTITY(1,1) UNIQUE NOT NULL,
	ParentPermitionRef int NULL,
	DateFrom datetime NULL,
	DateTo datetime NULL,
	CanBeDelagated bit NOT NULL,
	SampleBusinessObjectRef INT NULL,
	SampleBusinessObjectOperationRef INT NULL
)

CREATE TABLE tblSampleBusinesObject
(
	ID int IDENTITY(1,1) UNIQUE NOT NULL,
	Name nvarchar(max) NULL
)

CREATE TABLE fxSampleBusinesObjectOperation
(
	ID int IDENTITY(1,1) UNIQUE NOT NULL,
	Name nvarchar(50) NOT NULL,
	Description nvarchar(max) NULL,
	CanBeDelegated bit NOT NULL
)

ALTER TABLE tblPermission 
	ADD CONSTRAINT FK_PARENT_PERMITION
	FOREIGN KEY (ParentPermitionRef)
	REFERENCES tblPermission(ID)

ALTER TABLE tblPermission ADD CONSTRAINT FK_SAMPLE_BUSINESS_OBJECT 
	FOREIGN KEY (SampleBusinessObjectRef)
	REFERENCES tblSampleBusinesObject(ID)
	
ALTER TABLE tblPermission ADD CONSTRAINT FK_SAMPLE_BUSINESS_OBJECT_OPERATION
	FOREIGN KEY (SampleBusinessObjectOperationRef)
	REFERENCES fxSampleBusinesObjectOperation(ID)	
	
ALTER TABLE tblPermission WITH CHECK ADD CONSTRAINT FK_SAMPLE_BUSINESS_OBJECT_SUFFICIENT
	CHECK (	
		(SampleBusinessObjectRef IS NULL AND SampleBusinessObjectOperationRef IS NULL) 
		OR 
		(SampleBusinessObjectRef IS NOT NULL AND SampleBusinessObjectOperationRef IS NOT NULL) 		
	)
	
 