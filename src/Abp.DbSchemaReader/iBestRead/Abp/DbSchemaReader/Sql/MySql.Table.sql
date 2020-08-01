SELECT
	T.TABLE_NAME NAME,
	( CASE WHEN T.TABLE_TYPE = 'VIEW' THEN 'V' ELSE 'T' END ) AS TypeName,
	T.TABLE_COMMENT AS `Description` 
FROM
	Information_Schema.TABLES AS T 
WHERE
	T.Table_Schema = ?DbName;