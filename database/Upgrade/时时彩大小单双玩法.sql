USE Ticket;
GO

--1, 添加彩种大类
DECLARE @bigId INT, @sort INT, @lttype INT = 1;

--猜大小
SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;
INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @ltType, N'猜大小', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'猜大小');

--猜单双
SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;
INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @ltType, N'猜单双', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'猜单双');

--2, 初使彩种小类: 猜大小
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'猜大小';

--猜大小, 万位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜大小', N'万位', 'P_DX_W', N'万位', N'投注的号码与开出的号码万位(第一球)一致即中奖', N'大: 5 6 7 8 9均中奖, 小: 0 1 2 3 4均中奖', N'投注的号码与开出的号码万位(第一球)一致即中奖, 1 3 5 7 9均中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DX_W' AND LotteryId=@lttype);

--猜大小, 千位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜大小', N'千位', 'P_DX_Q', N'千位', N'投注的号码与开出的号码千位(第二球)一致即中奖', N'大: 5 6 7 8 9均中奖, 小: 0 1 2 3 4均中奖', N'投注的号码与开出的号码千位(第二球)一致即中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DX_Q' AND LotteryId=@lttype);

--猜大小, 百位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜大小', N'百位', 'P_DX_B', N'百位', N'投注的号码与开出的号码百位(第三球)一致即中奖', N'大: 5 6 7 8 9均中奖, 小: 0 1 2 3 4均中奖', N'投注的号码与开出的号码百位(第三球)一致即中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DX_B' AND LotteryId=@lttype);

--猜大小, 十位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜大小', N'十位', 'P_DX_S', N'十位', N'投注的号码与开出的号码十位(第四球)一致即中奖', N'大: 5 6 7 8 9均中奖, 小: 0 1 2 3 4均中奖', N'投注的号码与开出的号码十位(第四球)一致即中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DX_S' AND LotteryId=@lttype);

--猜大小, 个位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜大小', N'个位', 'P_DX_G', N'个位', N'投注的号码与开出的号码个位(第五球)一致即中奖', N'大: 5 6 7 8 9均中奖, 小: 0 1 2 3 4均中奖', N'投注的号码与开出的号码个位(第五球)一致即中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DX_G' AND LotteryId=@lttype);

--3, 初使彩种小类: 猜单双
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'猜单双';

--猜单双, 万位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜单双', N'万位', 'P_DS_W', N'万位', N'投注的号码与开出的号码万位(第一球)一致即中奖', N'单：1 3 5 7 9均中奖, 双: 0 2 4 6 8均中奖', N'投注的号码与开出的号码万位(第一球)一致即中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DS_W' AND LotteryId=@lttype);

--猜单双, 千位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜单双', N'千位', 'P_DS_Q', N'千位', N'投注的号码与开出的号码千位(第二球)一致即中奖', N'单：1 3 5 7 9均中奖, 双: 0 2 4 6 8均中奖', N'投注的号码与开出的号码千位(第二球)一致即中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DS_Q' AND LotteryId=@lttype);

--猜单双, 百位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜单双', N'百位', 'P_DS_B', N'百位', N'投注的号码与开出的号码百位(第三球)一致即中奖', N'单：1 3 5 7 9均中奖, 双: 0 2 4 6 8均中奖', N'投注的号码与开出的号码百位(第三球)一致即中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DS_B' AND LotteryId=@lttype);

--猜单双, 十位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜单双', N'十位', 'P_DS_S', N'十位', N'投注的号码与开出的号码十位(第四球)一致即中奖', N'单：1 3 5 7 9均中奖, 双: 0 2 4 6 8均中奖', N'投注的号码与开出的号码十位(第四球)一致即中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DS_S' AND LotteryId=@lttype);

--猜单双, 个位
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'猜单双', N'个位', 'P_DS_G', N'个位', N'投注的号码与开出的号码个位(第五球)一致即中奖', N'单：1 3 5 7 9均中奖, 双: 0 2 4 6 8均中奖', N'投注的号码与开出的号码个位(第五球)一致即中奖', 19.56, 19.56, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P_DS_G' AND LotteryId=@lttype);

--更新字段类型Sys_PlaySmallType.MinBonus
IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'Sys_PlaySmallType') AND XType = N'U')
	AND EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'Sys_PlaySmallType') AND name = N'MinBonus')
	ALTER TABLE Sys_PlaySmallType ALTER COLUMN MinBonus decimal(18, 4);
GO

--更新字段类型Sys_PlaySmallType.MaxBonus
IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'Sys_PlaySmallType') AND XType = N'U')
	AND EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'Sys_PlaySmallType') AND name = N'MaxBonus')
	ALTER TABLE Sys_PlaySmallType ALTER COLUMN MaxBonus decimal(18, 4);
GO

--更新赔率，大小，单双
UPDATE Sys_PlaySmallType SET MinBonus = '3.96', MaxBonus = '3.96' WHERE Title2 LIKE 'P_DX_%' OR Title2 LIKE 'P_DS_%';
