CREATE PROCEDURE [dbo].[sp_Sale_Insert]
	@Id int output,
	@CasherID nvarchar(128),
	@SaleDate datetime2,
	@SubTotal money,
	@Tax money,
	@Total money

AS
begin
	set nocount on

	insert into Sales( CasherID , SaleDate, SubTotal, Tax, Total)
	values (@CasherID , @SaleDate, @SubTotal, @Tax, @Total);

	Select @Id = Scope_IDENTITY();
end
