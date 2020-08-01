SELECT
	Obj.object_id AS Id,
	Obj.name AS Name,
	( CASE Obj.type WHEN 'V' THEN 'V' ELSE 'T' END ) AS TypeName,
	ObjSchema.name AS DbSchema,
	EPObj.value AS [Description] 
FROM
	Sys.Objects Obj
	LEFT JOIN Sys.Extended_Properties EPObj ON EPObj.major_id= Obj.object_id 
	AND EPObj.minor_id= 0 
	AND EPObj.name= 'MS_Description' 
	LEFT JOIN Sys.schemas ObjSchema ON ObjSchema.schema_id= Obj.schema_id 
WHERE
	Obj.type IN ( 'U', 'V' ) 
	AND Obj.is_ms_shipped=0