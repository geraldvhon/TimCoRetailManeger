CREATE PROCEDURE [dbo].[sp_Product_GetAll]
	
AS
Begin
	set nocount on;
	Select Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable
	from Product
	order by ProductName
end 
