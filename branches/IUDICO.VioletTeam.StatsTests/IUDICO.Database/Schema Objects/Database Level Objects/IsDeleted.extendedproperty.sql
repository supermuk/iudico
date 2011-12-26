EXEC sp_addextendedproperty @name='IsDeleted', 
    @value ='property value', 
    @level0type = 'SCHEMA',
    @level0name = 'dbo', 
    @level1type = 'TABLE',
    @level1name = 'ThemeAssignments', 
    @level2type = 'COLUMN',
    @level2name = 'Id'