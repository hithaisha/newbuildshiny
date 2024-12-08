BEGIN TRANSACTION;
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Product]') AND [c].[name] = N'LoyaltyPoints');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Product] DROP CONSTRAINT [' + @var0 + '];');
UPDATE [Product] SET [LoyaltyPoints] = 0 WHERE [LoyaltyPoints] IS NULL;
ALTER TABLE [Product] ALTER COLUMN [LoyaltyPoints] int NOT NULL;
ALTER TABLE [Product] ADD DEFAULT 0 FOR [LoyaltyPoints];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231101183137_MORR00009', N'7.0.9');
GO

COMMIT;
GO

