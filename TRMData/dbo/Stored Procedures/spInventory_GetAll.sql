CREATE PROCEDURE [dbo].[spInventory_GetAll]
	
AS
begin 
	set nocount on;

	Select [ProductId], [Quantity], [PurchasePrice], [PurchaseDate]
	from Inventory;

end 
