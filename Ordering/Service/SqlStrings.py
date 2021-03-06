SqlSelectUser = '''
SELECT [ID]
      ,[Name]
      ,[Email]
      ,[Permission]
FROM [Ordering].[dbo].[Users]
'''

SqlInsertUser = '''
INSERT INTO [Ordering].[dbo].[Users] (Name, Email, Permission)
VALUES '''

SqlCleanUsers = '''
DELETE
FROM [Ordering].[dbo].[Users]
WHERE [Permission] = 1'''

SqlGetCatalog = '''
SELECT [ID]
      ,[Name]
      ,[Category]
      ,[Price]
      ,[ImgUrl]
FROM [Ordering].[dbo].[Products]
'''

SqlGetAddresses = '''
SELECT TOP (1000) [ID]
      ,[CustomerID]
      ,[DeliverTo]
      ,[Phone]
      ,[ZIP]
      ,[City]
      ,[TheRestOfIt]
FROM [Ordering].[dbo].[Addresses]
'''

SqlAddAddress = '''
INSERT INTO [Ordering].[dbo].[Addresses] (CustomerID, DeliverTo, Phone, ZIP, City, TheRestOfIt)
VALUES
'''