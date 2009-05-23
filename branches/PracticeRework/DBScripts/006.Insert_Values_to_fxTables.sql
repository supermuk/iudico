INSERT INTO fxPageTypes (Type) VALUES ('Theory');
INSERT INTO fxPageTypes (Type) VALUES ('Practice');

INSERT INTO fxLanguages (Name) VALUES ('Axapta');
INSERT INTO fxLanguages (Name) VALUES ('Cpp');
INSERT INTO fxLanguages (Name) VALUES ('Delphi');
INSERT INTO fxLanguages (Name) VALUES ('HTML');
INSERT INTO fxLanguages (Name) VALUES ('Java');
INSERT INTO fxLanguages (Name) VALUES ('JavaScript');
INSERT INTO fxLanguages (Name) VALUES ('Perl');
INSERT INTO fxLanguages (Name) VALUES ('PHP');
INSERT INTO fxLanguages (Name) VALUES ('Python');
INSERT INTO fxLanguages (Name) VALUES ('RIB');
INSERT INTO fxLanguages (Name) VALUES ('RSL');
INSERT INTO fxLanguages (Name) VALUES ('Ruby');
INSERT INTO fxLanguages (Name) VALUES ('Smalltalk');
INSERT INTO fxLanguages (Name) VALUES ('SQL');
INSERT INTO fxLanguages (Name) VALUES ('VBScript');


INSERT INTO fxCompiledStatuses (Name, Description) VALUES ('WrongAnswer', 'Program was compiled, it passed time and memory limits,but it returns wrong output');
INSERT INTO fxCompiledStatuses (Name, Description) VALUES ('Accepted', 'Program was compiled, it passed time and memory limits, and it returns correct output.');
INSERT INTO fxCompiledStatuses (Name, Description) VALUES ('TimeLimit', 'Program was compiled, but it takes too much time to run.');
INSERT INTO fxCompiledStatuses (Name, Description) VALUES ('MemoryLimit', 'Program was compiled, but it takes too much memory during run');
INSERT INTO fxCompiledStatuses (Name, Description) VALUES ('CompilationError', 'Program wasnt compiled succesfully');
INSERT INTO fxCompiledStatuses (Name, Description) VALUES ('Running', 'Program was compiled, and it is running right now');
INSERT INTO fxCompiledStatuses (Name, Description) VALUES ('Enqueued', 'Program was received, and it is waiting too proceed');
INSERT INTO fxCompiledStatuses (Name, Description) VALUES ('Crashed', 'Program was compiled, but it crashed during execution');