BEGIN TRANSACTION;
GO

CREATE TABLE [Order] (
    [Id] int NOT NULL IDENTITY,
    [InvoiceNumber] nvarchar(max) NOT NULL,
    [OrderId] int NOT NULL,
    [TotalPrice] decimal(18,5) NOT NULL,
    [CreatedDate] datetime2 NOT NULL,
    [CreatedByUserId] int NOT NULL,
    [UpdateDate] datetime2 NULL,
    [UpdatedByUserId] int NOT NULL,
    [IsActive] bit NOT NULL,
    CONSTRAINT [PK_Order] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_Order_User_CreatedByUserId] FOREIGN KEY ([CreatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Order_User_UpdatedByUserId] FOREIGN KEY ([UpdatedByUserId]) REFERENCES [User] ([Id]) ON DELETE NO ACTION
);
GO

CREATE TABLE [OrderItem] (
    [Id] int NOT NULL IDENTITY,
    [OrderId] int NOT NULL,
    [ProductId] int NOT NULL,
    [Quantity] int NOT NULL,
    [TotalPrice] decimal(18,5) NOT NULL,
    CONSTRAINT [PK_OrderItem] PRIMARY KEY ([Id]),
    CONSTRAINT [FK_OrderItem_Order_OrderId] FOREIGN KEY ([OrderId]) REFERENCES [Order] ([Id]) ON DELETE NO ACTION,
    CONSTRAINT [FK_OrderItem_Product_ProductId] FOREIGN KEY ([ProductId]) REFERENCES [Product] ([Id]) ON DELETE NO ACTION
);
GO

CREATE INDEX [IX_Order_CreatedByUserId] ON [Order] ([CreatedByUserId]);
GO

CREATE INDEX [IX_Order_UpdatedByUserId] ON [Order] ([UpdatedByUserId]);
GO

CREATE INDEX [IX_OrderItem_OrderId] ON [OrderItem] ([OrderId]);
GO

CREATE INDEX [IX_OrderItem_ProductId] ON [OrderItem] ([ProductId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20231022063802_MORR00006', N'7.0.9');
GO

COMMIT;
GO

