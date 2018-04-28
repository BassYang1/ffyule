USE Ticket;
GO

--1, 添加彩种大类
DECLARE @bigId INT, @sort INT, @lttype INT = 2;
SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;

INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @ltType, N'任选拖胆', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'任选拖胆');

--2, 初使彩种小类: 任选拖胆
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'任选拖胆';

--二拖胆
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'任选拖胆', N'二拖胆', 'P11_RXTD2', N'二拖胆', N'从01-11中，选取2个及以上的号码进行投注，每注需至少包括1个胆码及1个拖码。', N'如：选择胆码 08，选择拖码 06，开奖号码为 06 08 11 09 02，即为中奖。', N'分别从胆码和拖码的01-11中，至少选择1个胆码和1个拖码组成一注，只要当期顺序摇出的5个开奖号码中同时包含所选的1个胆码和1个拖码，所选胆码必须全中，即为中奖。', 10.758, 10.758, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P11_RXTD2' AND LotteryId=@lttype);

--三拖胆
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'任选拖胆', N'三拖胆', 'P11_RXTD3', N'三拖胆', N'从01-11中，选取3个及以上的号码进行投注，每注需至少包括1个胆码及2个拖码。', N'如：选择胆码 08，选择拖码 06 11，开奖号码为 06 08 11 09 02，即为中奖。', N'分别从胆码和拖码的01-11中，至少选择1个胆码和2个拖码组成一注，只要当期顺序摇出的5个开奖号码中同时包含所选的1个胆码和2个拖码，所选胆码必须全中，即为中奖。', 32.274, 32.274, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P11_RXTD3' AND LotteryId=@lttype);

--四拖胆
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'任选拖胆', N'四拖胆', 'P11_RXTD4', N'四拖胆', N'从01-11中，选取4个及以上的号码进行投注，每注需至少包括1个胆码及3个拖码。', N'如：选择胆码 08，选择拖码 06 09 11，开奖号码为 06 08 11 09 02，即为中奖。', N'分别从胆码和拖码的01-11中，至少选择1个胆码和3个拖码组成一注，只要当期顺序摇出的5个开奖号码中同时包含所选的1个胆码和3个拖码，所选胆码必须全中，即为中奖。', 129.096, 129.096, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P11_RXTD4' AND LotteryId=@lttype);

--五拖胆
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'任选拖胆', N'五拖胆', 'P11_RXTD5', N'五拖胆', N'从01-11中，选取5个及以上的号码进行投注，每注需至少包括1个胆码及4个拖码。', N'如：选择胆码 08，选择拖码 02 06 09 11，开奖号码为  06 08 11 09 02，即为中奖。', N'分别从胆码和拖码的01-11中，至少选择1个胆码和4个拖码组成一注，只要当期顺序摇出的5个开奖号码中同时包含所选的1个胆码和4个拖码，所选胆码必须全中，即为中奖。', 903.672, 903.672, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P11_RXTD5' AND LotteryId=@lttype);

--六拖胆
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'任选拖胆', N'六拖胆', 'P11_RXTD6', N'六拖胆', N'从01-11中，选取6个及以上的号码进行投注，每注需至少包括1个胆码及5个拖码。', N'如：选择胆码 08，选择拖码 01 02 05 06 09 11，开奖号码为 06 08 11 09 02，即为中奖。', N'分别从胆码和拖码的01-11中，至少选择1个胆码和5个拖码组成一注，只要当期顺序摇出的5个开奖号码同时存在于胆码和拖码的任意组合中，即为中奖。', 150.612, 150.612, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P11_RXTD6' AND LotteryId=@lttype);

--七拖胆
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'任选拖胆', N'七拖胆', 'P11_RXTD7', N'七拖胆', N'从01-11中，选取7个及以上的号码进行投注，每注需至少包括1个胆码及6个拖码。', N'如：选择胆码 08，选择拖码 01 02 05 06 07 09 11，开奖号码为 06 08 11 09 02，即为中奖。', N'分别从胆码和拖码的01-11中，至少选择1个胆码和6个拖码组成一注，只要当期顺序摇出的5个开奖号码同时存在于胆码和拖码的任意组合中，即为中奖。', 43.032, 43.032, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P11_RXTD7' AND LotteryId=@lttype);

--八拖胆
SELECT @sort = MAX(Sort) + 1 FROM Sys_PlaySmallType;
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'任选拖胆', N'八拖胆', 'P11_RXTD8', N'八拖胆', N'从01-11中，选取8个及以上的号码进行投注，每注需至少包括1个胆码及7个拖码。', N'如：选择胆码 08，选择拖码 01 02 03 05 06 07 09 11，开奖号码为 06 08 11 09 02，即为中奖。', N'分别从胆码和拖码的01-11中，至少选择1个胆码和7个拖码组成一注，只要当期顺序摇出的5个开奖号码同时存在于胆码和拖码的任意组合中，即为中奖。', 16.137, 16.137, 0.01, 0, 0, 0, 0, 0, 99999, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='P11_RXTD8' AND LotteryId=@lttype);

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

--更新赔率
UPDATE Sys_PlaySmallType SET MinBonus = '10.758', MaxBonus = '10.758' WHERE Title2 = 'P11_RXTD2';
UPDATE Sys_PlaySmallType SET MinBonus = '32.274', MaxBonus = '32.274' WHERE Title2 = 'P11_RXTD3';
UPDATE Sys_PlaySmallType SET MinBonus = '129.096', MaxBonus = '129.096' WHERE Title2 = 'P11_RXTD4';
UPDATE Sys_PlaySmallType SET MinBonus = '903.672', MaxBonus = '903.672' WHERE Title2 = 'P11_RXTD5';
UPDATE Sys_PlaySmallType SET MinBonus = '150.612', MaxBonus = '150.612' WHERE Title2 = 'P11_RXTD6';
UPDATE Sys_PlaySmallType SET MinBonus = '43.032', MaxBonus = '43.032' WHERE Title2 = 'P11_RXTD7';
UPDATE Sys_PlaySmallType SET MinBonus = '16.137', MaxBonus = '16.137' WHERE Title2 = 'P11_RXTD8';
