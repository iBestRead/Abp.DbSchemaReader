SELECT
	a.attname AS "Name",
	col.udt_name AS "DbType",
	COALESCE ( col.character_maximum_length, col.numeric_precision,- 1 ) AS "DataLength",
	col.numeric_scale AS "Scale",
	( CASE a.attnotnull WHEN 't' THEN 0 ELSE 1 END ) AS "IsNullable",
	( CASE a.attnum WHEN cs.conkey [ 1 ] THEN 1 ELSE 0 END ) AS "IsPrimaryKey",
	( CASE WHEN position( 'nextval' IN col.column_default ) > 0 THEN 1 ELSE 0 END ) AS "AutoIncrement",
	col_description ( a.attrelid, a.attnum ) AS "Description" 
FROM
	pg_attribute a
	LEFT JOIN pg_class c ON a.attrelid = c.oid
	LEFT JOIN pg_constraint cs ON cs.conrelid = c.oid 
	AND cs.contype = 'p'
	LEFT JOIN pg_namespace n ON n.oid = c.relnamespace
	LEFT JOIN information_schema.COLUMNS col ON col.table_schema = n.nspname 
	AND col.table_name = c.relname 
	AND col.column_name = a.attname 
WHERE
	a.attnum > 0 
	AND col.udt_name IS NOT NULL 
	AND c.relname = @TableName 
	AND n.nspname = @DbSchema 
ORDER BY
	a.attnum ASC