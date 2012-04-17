SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		HUgo Vindas
-- Create date: 10/14/2011
-- Description:	Get torch configutration desciption and ID
-- =============================================
CREATE PROCEDURE stp_Get_Torch
	-- Add the parameters for the stored procedure here
	@cartid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
        ,[description]
	FROM [PublicPortal].[dbo].[torch_configurations]
	WHERE cartid = @cartid
END
GO

/*****------------------------------*****/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Create date: Oct - 12 - 2011,
-- Description:	This store procedure perform the insert into torch configuration table,
-- =============================================
Create PROCEDURE [dbo].[sp_add_torch_configurations] 
   @cartid int,
   @state varchar(50),
   @sku varchar(50),
   @spanish int,
   @whiteboard varchar(50),
   @repsystem varchar(50),
   @description varchar(2000)
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO [torch_configurations]
           ([cartid]
           ,[state]
           ,[sku]
           ,[spanish]
           ,[whiteboard]
           ,[repsystem]
           ,[description]
           ,[datecreated]
           )
     VALUES
           (@cartid, 
           @state,	
           @sku,	
           @spanish,
           @whiteboard, 
           @repsystem,
           @description,  
           getdate())
END

/*****------------------------------*****/

/****** Object:  StoredProcedure [dbo].[stp_Get_Torch_SKUs]    Script Date: 10/15/2011 16:11:58 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hugo Vindas
-- Create date: 10/13/2011
-- Description:	Get All the prices and Skus
-- =============================================
CREATE PROCEDURE [dbo].[stp_Get_Torch_SKUs] 
	-- Add the parameters for the stored procedure here
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT [id]
      ,[subjectid]
      ,[gradeid]
      ,[price]
      ,[SKU]
	FROM [PublicPortal].[dbo].[Torch_sku]
END

GO



