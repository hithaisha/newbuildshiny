BEGIN TRANSACTION;
GO

EXEC sp_rename N'[Product].[LoyalityPoints]', N'LoyaltyPoints', N'COLUMN';
GO

ALTER TABLE [User] ADD [LoyaltyPoints] decimal(18,2) NOT NULL DEFAULT 0.0;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231101131025_MORR00007', N'7.0.9');
GO

COMMIT;
GO

