CREATE TABLE [dbo].[SalesDetail]
(
	[Id] INT NOT NULL PRIMARY KEY Identity, 
    [SaleId] INT NOT NULL, 
    [ProductId] INT NOT NULL, 
    [Quatity] INT NOT NULL DEFAULT 1, 
    [PurchasePrice] MONEY NOT NULL, 
    [Tax] MONEY NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_SalesDetail_ToSales] FOREIGN KEY (SaleId) REFERENCES Sales(Id), 
    CONSTRAINT [FK_SalesDetail_ToProduct] FOREIGN KEY (ProductId) REFERENCES Product(Id) 
)
