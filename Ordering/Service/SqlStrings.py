SqlSelectUser = '''SELECT [ID]
      ,[Name]
      ,[Email]
      ,[Permission]
  FROM [Ordering].[dbo].[Users]
  '''

SqlInsertUser = '''INSERT INTO [Ordering].[dbo].[Users] (Name, Email, Permission)
    VALUES '''

SqlCleanUsers = '''DELETE
  FROM [Ordering].[dbo].[Users]
  WHERE [Permission] = 1'''
