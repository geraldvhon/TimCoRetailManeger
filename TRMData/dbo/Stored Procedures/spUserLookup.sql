CREATE PROCEDURE [dbo].[spUserLookup]
	@Id nvarchar(128)
AS
begin
	set nocount on;

	SELECT ID, Firstname, Lastname, EmailAddress, DateCreated
	from [dbo].[User] 
	where Id like @Id;

end
