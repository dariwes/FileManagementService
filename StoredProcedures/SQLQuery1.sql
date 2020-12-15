-- ================================================
-- Template generated from Template Explorer using:
-- Create Procedure (New Menu).SQL
--
-- Use the Specify Values for Template Parameters 
-- command (Ctrl-Shift-M) to fill in the parameter 
-- values below.
--
-- This block of comments will not be included in
-- the definition of the procedure.
-- ================================================
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GetPersonByID]
	
	@ID int = NOTNULLL
AS
BEGIN
	SET NOCOUNT ON;

    SELECT [Person].[BusinessEntityID]
		  ,[PersonType]
	      ,[FirstName]
          ,[MiddleName]
		  ,[LastName]
		  ,[EmailPromotion]
		  ,[EmailAddress]
		  ,[PhoneNumber]
		  ,[PasswordSalt]
		  ,[City]
	FROM [AdventureWorks].[Person].[Person]
	LEFT JOIN [AdventureWorks].[Person].[PersonPhone]
	ON [AdventureWorks].[Person].[Person].[BusinessEntityID] = [AdventureWorks].[Person].[PersonPhone].[BusinessEntityID]
	LEFT JOIN [AdventureWorks].[Person].[EmailAddress]
	ON [AdventureWorks].[Person].[PersonPhone].[BusinessEntityID] = [AdventureWorks].[Person].[EmailAddress].[BusinessEntityID]
	LEFT JOIN [AdventureWorks].[Person].[Password]
	ON [AdventureWorks].[Person].[EmailAddress].[BusinessEntityID] = [AdventureWorks].[Person].[Password].[BusinessEntityID]
	LEFT JOIN [AdventureWorks].[Person].[BusinessEntityAddress]
	ON [AdventureWorks].[Person].[Password].[BusinessEntityID] = [AdventureWorks].[Person].[BusinessEntityAddress].[BusinessEntityID]
	LEFT JOIN [AdventureWorks].[Person].[Address]
	ON [AdventureWorks].[Person].[BusinessEntityAddress].[AddressID] = [AdventureWorks].[Person].[Address].[AddressID]
	WHERE [AdventureWorks].[Person].[Person].[BusinessEntityID] = @ID
	SET NOCOUNT OFF;
END
