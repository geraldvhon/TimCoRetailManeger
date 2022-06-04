CREATE TABLE [dbo].[User]
(
	[ID] NVARCHAR(128) NOT NULL PRIMARY KEY,
	[Lastname] NVARCHAR(50) NOT NULL,
	[Firstname] NVARCHAR(50) NOT NULL,
	[EmailAddress] NVARCHAR(256) NOT NULL,
	[DateCreated]  datetime2  NOT NULL DEFAULT getutcdate()
)
