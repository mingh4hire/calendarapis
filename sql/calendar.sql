create table calendarEvent(
	startDate DateTime, endDate DateTime, eventTitle varchar(1000),
	eventDescription varchar(1000),
		creator varchar(500) foreign key references users,
		createDate DateTime default getdate(), eventType varchar(250) references eventType);
 
 create table eventType (color varchar(10), name varchar(250) primary key , createDate DateTime, creator varchar(500) foreign key references users);
 create table users (name varchar(500) primary key, fname varchar(100), lname varchar(100), createDate DateTime default getDate(), creator varchar(100),
password varchar(5000))

delete  from users
insert into users values ('dan', 'dan', 'smith', getdate(), 'dan', hashbytes('sha1', 'mustbe8bytes'))


select * from users
 drop table calendarEvent
 drop table eventType
 drop table users
 
 select * from calendarEvent c left join eventType e on c.eventtype = e.name


 insert into calendarEvent values (getdate(), getdate() + 5, 'work on calendar', 'working on my own calendar app', 'dan', getdate(), 'important')

 insert into calendarEvent values (getdate(), getdate() + 5, 'study chinese', 'study chinese 10 words every day', 'dan', getdate(), 'unimportant')
 insert into calendarEvent values (getdate(), getdate() + 5, 'horse stance', 'do horse stance 10 minutes a day straight', 'dan', getdate(), 'cool' )
 insert into calendarEvent values (getdate()+5, getdate() + 15, 'no junk', 'don"t eat too much potato chips and ice cream', 'dan', getdate(), 'bad' )

 insert into eventType values ('#ababab', 'important', getdate(), 'dan')
 insert into eventType values ('#cacaca', 'unimportant', getdate(), 'dan')

insert into eventType values ('#dfdfdf', 'cool', getdate(), 'dan')
insert into eventType values ('#3f3f3f', 'bad', getdate(), 'dan')
insert into eventType values ('#dfdfdf', 'cool', getdate(), 'dan')

select * from calendarevent
select * from eventType
select * from users