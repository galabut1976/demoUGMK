DECLARE @Flowers TABLE(Name nvarchar(20))
INSERT INTO @Flowers VALUES 
('Rose'),
('Tulip'),
('Daisy'),
('Forget-me-not'),
('Lilac'),
('Narcissus'),
('Camomile'),
('Lily of the valley')

select
	f1.name, 
	f2.name,
	f3.name,
	f4.name,
	f5.name
from 
	@flowers f1 
	join  @flowers f2 on f1.name < f2.name
	join @flowers f3 on f2.name < f3.name
	join @flowers f4 on f3.name < f4.name
	join @flowers f5 on f4.name < f5.name
	
	
--еще можно так
select
	f1.name, 
	f2.name,
	f3.name,
	f4.name,
	f5.name
from 
	@flowers f1, @flowers f2, @flowers f3, @flowers f4, @flowers f5
where
	f1.name < f2.name and
	f2.name < f3.name and
	f3.name < f4.name and
	f4.name < f5.name