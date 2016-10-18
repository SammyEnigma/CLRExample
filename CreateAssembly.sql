CREATE ASSEMBLY [SimpleClr]
FROM C:\SimpleClr.dll
WITH PERMISSION_SET = UNSAFE
GO

CREATE PROCEDURE [dbo].[usp_BroadcastXml]
	@message [xml]
WITH EXECUTE AS CALLER
AS
EXTERNAL NAME [SimpleClr].[SimpleClr.SimpleClr].[BroadcastXml]
--             ^ assembly  ^ namespace.class     ^ method name
GO