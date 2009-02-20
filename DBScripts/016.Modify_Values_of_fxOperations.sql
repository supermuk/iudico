DELETE FROM fxStageOperations;
INSERT INTO fxStageOperations (Name, Description, CanBeDelegated) VALUES ('View', 'View the stage', 0);
INSERT INTO fxStageOperations (Name, Description, CanBeDelegated) VALUES ('Pass', 'Pass the stage', 0);

DELETE FROM fxThemeOperations;
INSERT INTO fxThemeOperations (Name, Description, CanBeDelegated) VALUES ('View', 'View the theme', 0);
INSERT INTO fxThemeOperations (Name, Description, CanBeDelegated) VALUES ('Pass', 'Pass the theme', 0);