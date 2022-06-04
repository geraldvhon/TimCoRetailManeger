CREATE PROCEDURE [dbo].[spInventory_Insert]
	--[ProductId], [Quantity], [PurchasePrice], [PurchaseDate]
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@PurchaseDate datetime2
AS
begin 
	set nocount on;

	insert into Inventory (ProductId, Quantity, PurchasePrice, PurchaseDate)
	values (@ProductId, @Quantity, @PurchasePrice, @PurchaseDate);

end 