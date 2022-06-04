CREATE PROCEDURE [dbo].[sp_Product_GetById]
	@Id int
AS
begin 
	
	set nocount on ;
	Select Id, ProductName, [Description], RetailPrice, QuantityInStock, IsTaxable
	from Product
	where Id = @Id

end 