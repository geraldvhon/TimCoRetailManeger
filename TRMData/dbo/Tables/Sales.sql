CREATE TABLE [dbo].[Sales]
(
	[Id] INT NOT NULL PRIMARY KEY Identity,
	[CasherID] nvarchar(128) NOT NULL,
	[SaleDate] DATETIME2 NOT NULL, 
    [SubTotal] MONEY NULL,
	[Tax] MONEY NULL,
	[Total] MONEY NULL, 
    CONSTRAINT [FK_Sales_ToUser] FOREIGN KEY (CasherID) REFERENCES [User](ID)
)
