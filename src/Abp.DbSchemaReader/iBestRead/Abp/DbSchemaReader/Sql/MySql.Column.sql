SELECT
	Col.Column_Name AS `Name`,
	Col.Data_Type AS DbType,
	(CASE
		WHEN Col.Numeric_Precision IS NOT NULL THEN
			( Col.Numeric_Precision + 1 ) 
		WHEN Col.Character_Maximum_Length IS NOT NULL THEN
			Col.Character_Maximum_Length ELSE NULL 
		END 
	) AS DataLength,
	( CASE 
		WHEN Col.Is_Nullable = 'NO' THEN 0 
		ELSE 1 
		END 
	) AS IsNullable,
	( CASE 
	    WHEN Col.Column_Key = 'PRI' THEN 1 
	    ELSE 0 
	    END 
	) AS IsPrimaryKey,
	( CASE 
	    WHEN Col.Extra = 'auto_increment' THEN 1 
	    ELSE 0 
	    END 
	) AS AutoIncrement,
	Col.Column_Comment AS `Description` 
FROM
    Information_Schema.COLUMNS AS Col 
WHERE
    Table_Schema =?DbName 
	AND Table_Name =?TableName 
ORDER BY
	Col.ORDINAL_POSITION ASC;