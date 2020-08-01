SELECT
	T.tablename AS "Name",
	'T' AS "TypeName",
	obj_description ( C.oid ) AS "Description" 
FROM
	pg_tables T
	LEFT JOIN pg_class C ON C.relname = T.tablename 
	AND C.relkind = 'r' 
WHERE
	schemaname = @DbSchema 
UNION ALL
SELECT
	V.viewname AS "Name",
	'V' AS "TypeName",
	obj_description ( C.oid ) AS "Description" 
FROM
	pg_views V
	LEFT JOIN pg_class C ON C.relname = V.viewname 
	AND C.relkind = 'v' 
WHERE
	schemaname = @DbSchema