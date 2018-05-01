USE Ticket;
GO

--�ر�Ȥζ�淨
UPDATE Sys_PlayBigType SET IsOpen = 1, IsOpenIphone = 1 WHERE Title LIKE '%Ȥζ%';

UPDATE Sys_PlaySmallType SET IsOpen = 1, IsOpenIphone = 1 WHERE Title0 LIKE '%Ȥζ%' OR Title LIKE '%Ȥζ%' OR TitleName LIKE '%Ȥζ%';