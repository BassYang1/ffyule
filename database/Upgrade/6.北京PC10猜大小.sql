USE Ticket
GO

SELECT TOP 30 * From Sys_PlaySmallType with(nolock)  Where 1=1 and LotteryId=4 ORDER BY Id ASC;

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, 
MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, 
flag, STime)
SELECT 4, 4005, (SELECT MAX(Sort) + 1 FROM Sys_PlaySmallType), 1, N'猜大小', N'第四名', 'PK10_DXFour', N'猜大小第四名', NULL, NULL, NULL, 19.60, 
19.60, 0.002000, NULL, 0, NULL, 0, 0, 999999, 0, 0, 0, 2, 0, 1, 
0, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlaySmallType WHERE LotteryId=4 AND Title2 = 'PK10_DXFour')
GO

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, 
MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, 
flag, STime)
SELECT 4, 4005, (SELECT MAX(Sort) + 1 FROM Sys_PlaySmallType), 1, N'猜大小', N'第五名', 'PK10_DXFive', N'猜大小第五名', NULL, NULL, NULL, 19.60, 
19.60, 0.002000, NULL, 0, NULL, 0, 0, 999999, 0, 0, 0, 2, 0, 1, 
0, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlaySmallType WHERE LotteryId=4 AND Title2 = 'PK10_DXFive')
GO

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, 
MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, 
flag, STime)
SELECT 4, 4005, (SELECT MAX(Sort) + 1 FROM Sys_PlaySmallType), 1, N'猜大小', N'第六名', 'PK10_DXSix', N'猜大小第六名', NULL, NULL, NULL, 19.60, 
19.60, 0.002000, NULL, 0, NULL, 0, 0, 999999, 0, 0, 0, 2, 0, 1, 
0, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlaySmallType WHERE LotteryId=4 AND Title2 = 'PK10_DXSix')
GO

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, 
MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, 
flag, STime)
SELECT 4, 4005, (SELECT MAX(Sort) + 1 FROM Sys_PlaySmallType), 1, N'猜大小', N'第七名', 'PK10_DXSeven', N'猜大小第七名', NULL, NULL, NULL, 19.60, 
19.60, 0.002000, NULL, 0, NULL, 0, 0, 999999, 0, 0, 0, 2, 0, 1, 
0, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlaySmallType WHERE LotteryId=4 AND Title2 = 'PK10_DXSeven')
GO

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, 
MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, 
flag, STime)
SELECT 4, 4005, (SELECT MAX(Sort) + 1 FROM Sys_PlaySmallType), 1, N'猜大小', N'第八名', 'PK10_DXEight', N'猜大小第八名', NULL, NULL, NULL, 19.60, 
19.60, 0.002000, NULL, 0, NULL, 0, 0, 999999, 0, 0, 0, 2, 0, 1, 
0, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlaySmallType WHERE LotteryId=4 AND Title2 = 'PK10_DXEight')
GO

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, 
MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, 
flag, STime)
SELECT 4, 4005, (SELECT MAX(Sort) + 1 FROM Sys_PlaySmallType), 1, N'猜大小', N'第九名', 'PK10_DXNine', N'猜大小第九名', NULL, NULL, NULL, 19.60, 
19.60, 0.002000, NULL, 0, NULL, 0, 0, 999999, 0, 0, 0, 2, 0, 1, 
0, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlaySmallType WHERE LotteryId=4 AND Title2 = 'PK10_DXNine')
GO

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, 
MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, 
flag, STime)
SELECT 4, 4005, (SELECT MAX(Sort) + 1 FROM Sys_PlaySmallType), 1, N'猜大小', N'第十名', 'PK10_DXTen', N'猜大小第十名', NULL, NULL, NULL, 19.60, 
19.60, 0.002000, NULL, 0, NULL, 0, 0, 999999, 0, 0, 0, 2, 0, 1, 
0, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlaySmallType WHERE LotteryId=4 AND Title2 = 'PK10_DXTen')
GO



