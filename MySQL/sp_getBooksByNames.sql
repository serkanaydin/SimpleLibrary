CREATE PROCEDURE `sp_getBooksByNames`(filterWord char(255))
begin
	drop temporary table if exists filterTemp;
	create temporary table filterTemp(val char(255));
	set @sql = concat("insert into filterTemp(val) values ('",replace((filterWord),":","'),('"),"');");
	prepare stmt1 from @sql;
	execute stmt1;
	

	(Select b.*
	From Book b
	inner join filterTemp t on b.Name like CONCAT('%',t.val,'%')
	Group by b.Id
	);	
end