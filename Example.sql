ALTER TRIGGER [dbo].[insert_trigger] on [dbo].[some_table]
AFTER INSERT
AS
BEGIN

	DECLARE @message XML

	SET @message = 
	(
		SELECT
		*
		FROM inserted
		FOR XML RAW('Message'), ROOT('Messages')
	)

	exec usp_BroadcastXml @message
END