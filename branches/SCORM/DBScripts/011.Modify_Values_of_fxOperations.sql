DELETE FROM fxCourseOperations
WHERE id > -1;

INSERT INTO fxCourseOperations (Name, Description, CanBeDelegated) VALUES ('Modify', 'Modify course by teacher', 1);

INSERT INTO fxCourseOperations (Name, Description, CanBeDelegated) VALUES ('Use', 'Use course by teacher', 1);

DELETE FROM fxCurriculumOperations
WHERE id > -1;

INSERT INTO fxCurriculumOperations (Name, Description, CanBeDelegated) VALUES ('Modify', 'Modify curriculum by teacher', 1);

INSERT INTO fxCurriculumOperations (Name, Description, CanBeDelegated) VALUES ('Use', 'Use curriculum by teacher', 1);
INSERT INTO fxCurriculumOperations (Name, Description, CanBeDelegated) VALUES ('View', 'View the curriculum', 0);
INSERT INTO fxCurriculumOperations (Name, Description, CanBeDelegated) VALUES ('Pass', 'Pass the curriculum', 0);
