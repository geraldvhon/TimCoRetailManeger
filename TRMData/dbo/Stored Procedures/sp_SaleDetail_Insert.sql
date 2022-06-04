CREATE PROCEDURE [dbo].[sp_SaleDetail_Insert]
	@SaleId int,
	@ProductId int,
	@Quantity int,
	@PurchasePrice money,
	@Tax money
AS
begin
	set nocount on;

	insert into SalesDetail( SaleId, ProductId,	Quatity,	PurchasePrice, Tax)
	values ( @SaleId, @ProductId, @Quantity, @PurchasePrice, @Tax)
end 