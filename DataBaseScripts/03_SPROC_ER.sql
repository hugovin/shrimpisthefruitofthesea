USE [ER]
GO

/****** Object:  StoredProcedure [dbo].[Get_Torch_Skus]    Script Date: 10/15/2011 16:01:47 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		Hugo Vindas
-- Create date: 10/13/2010
-- Description:	Get All the skus and prices for torch product
-- =============================================
CREATE PROCEDURE [dbo].[Get_Torch_Skus]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	execute PublicPortal..stp_Get_Torch_SKUs
END

GO

/****-------------------------------------****/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		Hugo Vindas
-- Create date: 14/10/2011
-- Description:	Get Torch configuration and ID
-- =============================================
CREATE PROCEDURE Get_Torch_Configuration 
	-- Add the parameters for the stored procedure here
	@carid int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	execute PublicPortal..stp_Get_Torch
	@carid
END
GO

/****-------------------------------------****/
USE [ER]
GO

/****** Object:  StoredProcedure [dbo].[sp_add_torch_configurations]    Script Date: 10/15/2011 16:13:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Create date: Oct - 12 - 2011,
-- Description:	This store procedure perform the insert into torch configuration table,
-- =============================================
CREATE PROCEDURE [dbo].[sp_add_torch_configurations] 
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
	execute PublicPortal..sp_add_torch_configurations
           @cartid, 
           @state,	
           @sku,	
           @spanish,
           @whiteboard, 
           @repsystem,
           @description  
           
END
GO



