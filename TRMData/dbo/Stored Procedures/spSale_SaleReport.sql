CREATE PROCEDURE dbo.spSale_SaleReport
	
AS
begin 

	set nocount on;

	select	s.SaleDate, s.SubTotal,	s.Tax, s.Total, u.Firstname, u.Lastname,
			u.EmailAddress

	from Sales s
	inner join [User] u on s.CasherID = u.ID

end 
	