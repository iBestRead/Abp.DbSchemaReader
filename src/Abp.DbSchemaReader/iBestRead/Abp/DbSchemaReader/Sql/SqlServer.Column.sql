SELECT
	Col.column_id AS Id,
	Col.object_id AS TableId,
	Col.name AS Name,
	Tp.name AS DbType,
	Col.max_length AS DataLength,
	EPCol.value AS Description,
	Col.is_nullable AS IsNullable,
	Col.is_identity AS AutoIncrement,
	IsNull( ( SELECT TOP 1 1 FROM INFORMATION_SCHEMA.KEY_COLUMN_USAGE WHERE Table_Name =@TableName AND Col.name= Column_Name ), 0 ) IsPrimaryKey 
FROM
	Sys.Columns Col
	LEFT JOIN Sys.Extended_Properties EPCol ON EPCol.major_id= Col.object_id 
	AND EPCol.minor_id= Col.column_id
	LEFT JOIN Sys.Types Tp ON Tp.system_type_id= Col.system_type_id 
WHERE
	TP.name != 'sysname' 
	AND Col.object_id = @TableId 
ORDER BY
	Col.column_id ASC