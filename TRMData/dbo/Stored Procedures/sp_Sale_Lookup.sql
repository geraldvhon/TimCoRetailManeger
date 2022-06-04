CREATE PROCEDURE [dbo].[sp_Sale_Lookup]
	@CasherID nvarchar(128),
	@SaleDate datetime2
AS
begin 
	set nocount on;

	select Id
	from Sales
	where CasherID = @CasherID and SaleDate = @SaleDate;
end 