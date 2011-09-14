/*
ALTER DATABASE [$(DatabaseName)]
	ADD FILE (NAME = [IUDICO], FILENAME = '$(DefaultDataPath)$(DatabaseName).mdf', SIZE = 2048 KB, FILEGROWTH = 1024 KB) TO FILEGROUP [PRIMARY];

*/