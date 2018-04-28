USE Ticket;
GO

--1, 添加彩种
DECLARE @sort INT, 
		@msort INT, 
		@ltid INT = 5005,
		@lttype INT = 5,
		@iss INT = 78

SELECT @sort = MAX(Sort), @msort = MAX(IphoneSort) FROM Sys_Lottery;

--新增彩种: Ltype 彩种类别, IndexType 显示顺序
INSERT INTO Sys_Lottery(Id, Title, Code, MinTimes, MaxTimes, IsOpen, CloseTime, second, Sort, Ltype, IsAuto, IndexType, Url, AutoUrl, 
	IphoneIsOpen, IphoneSort, IphoneRemark, IphoneImg, IssNum)
SELECT @ltid, N'广西快三', 'gxk3', 1, 99, 0, 0, 0, @sort, @lttype, 0, 7, N'', 0, 
	0, @msort, N'当日9点30分至当日22点40分', 84, @iss
WHERE NOT EXISTS(SELECT 1 FROM Sys_Lottery WHERE Code='gxk3');

DECLARE @num INT = 1, 
		@sn NVARCHAR(100) = '001', 
		@time NVARCHAR(10) = '09:38:00'

--2, 添加彩种时间
WHILE @num <= @iss
BEGIN	
	INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
	SELECT @ltid, @sn, @time, 0, GETDATE()
	WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=@ltid AND Sn=@sn)

	SET @num = @num + 1;
	SET @sn = '00000' + CAST(@num AS NVARCHAR(10))
	SET @sn = SUBSTRING(@sn, len(@sn) - 2, 3)
	SET @time = CONVERT(NVARCHAR(8), DATEADD(MI, 10, CONVERT(DATETIME,@time,103)), 108)
END

--3, 添加彩种大类
DECLARE @bigId INT

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;

INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @ltType, N'和值', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'和值');

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;
INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @lttype, N'三同号', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'三同号');

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;
INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @lttype, N'二同号', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'二同号');

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;
INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @lttype, N'三不同', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'三不同');

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;
INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @lttype, N'二不同', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'二不同');

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;
INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @lttype, N'三连号', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'三连号');

--4, 初使彩种小类: 和值
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'和值';

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'和值', N'和值', 'K_3HZ', N'和值', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', N'投注号码与当期开奖号码的三个号码的和值相符，即中奖', N'和值：投注号码与当期开奖号码的三个号码的和值相符，即中奖。', 207.36, 207.36, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值3', 'K_3HZ3', N'和值3', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 207.36, 207.36, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ3' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值4', 'K_3HZ4', N'和值', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 69.119, 69.119, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ4' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值5', 'K_3HZ5', N'和值5', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 34.559, 34.559, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ5' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值6', 'K_3HZ6', N'和值', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 20.736, 20.736, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ6' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值7', 'K_3HZ7', N'和值', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 13.824, 13.824, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ7' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值8', 'K_3HZ8', N'和值8', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 9.878, 9.878, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ8' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值9', 'K_3HZ9', N'和值9', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 8.294, 8.294, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ9' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值10', 'K_3HZ10', N'和值10', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 7.68, 7.68, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ10' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值11', 'K_3HZ11', N'和值11', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 7.68, 7.68, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ11' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值12', 'K_3HZ12', N'和值12', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 8.294, 8.294, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ12' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值13', 'K_3HZ13', N'和值13', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 9.878, 9.878, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ13' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值14', 'K_3HZ14', N'和值14', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 13.824, 13.824, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ14' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值15', 'K_3HZ15', N'和值15', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 20.736, 20.736, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ15' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值16', 'K_3HZ16', N'和值16', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 34.559, 34.559, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ16' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值17', 'K_3HZ17', N'和值17', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 69.119, 69.119, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ17' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'和值', N'和值18', 'K_3HZ18', N'和值18', N'对三个号码的和值进行投注，包括“和值3”至“和值18”投注', NULL, NULL, 207.36, 207.36, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3HZ18' AND LotteryId=@lttype);

--5, 初使彩种小类: 三同号
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'三同号';

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'三同号', N'单选', 'K_3STDX', N'三同号单选', N'从所有相同的三个号码（111、222、…、666）中任意选择一组号码进行投注。', N'当期开奖号码的三个号码相同，且投注号码与当期开奖号码相符，即中奖。', N'三同号单选：当期开奖号码的三个号码相同，且投注号码与当期开奖号码相符，即中奖。', 422.496, 422.496, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3STDX' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'三同号', N'通选', 'K_3STTX', N'三同号通选', N'对所有相同的三个号码（111、222、…、666）进行投注。', N'当期开奖号码的三个号码相同，且投注号码与当期开奖号码相符，即中奖。', N'三同号通选：当期开奖号码的三个号码相同，即中奖。', 70.416, 70.416, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_3STTX' AND LotteryId=@lttype);


--6, 初使彩种小类: 二同号
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'二同号';

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'二同号', N'单选', 'K_32TDX', N'二同号单选', N'对三个号码中两个指定的相同号码和一个指定的不同号码进行投注。', N'当期开奖号码中有两个号码相同，且投注号码与当期开奖号码中两个相同号码和一个不同号码分别相符，即中奖。', N'二同号单选：当期开奖号码中有两个号码相同，且投注号码与当期开奖号码中两个相同号码和一个不同号码分别相符，即中奖。', 140.832, 140.832, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_32TDX' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'二同号', N'通选', 'K_32TTX', N'二同号通选', N'对三个号码中两个指定的相同号码和一个任意号码进行投注。', N'当期开奖号码中有两个号码相同，且投注号码中的两个相同号码与当期开奖号码中两个相同号码相符，即中奖。', N'二同号复选：当期开奖号码中有两个号码相同，且投注号码中的两个相同号码与当期开奖号码中两个相同号码相符，即中奖。', 26.406, 26.406, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_32TTX' AND LotteryId=@lttype);

--7, 初使彩种小类: 二不同
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'二不同';

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'二不同', N'直选', 'K_32BT', N'二不同直选', N'对三个号码中两个指定的不同号码和一个任意号码进行投注。', N'当期开奖号码中有两个号码不相同，且投注号码中的两个不同号码与当期开奖号码中的两个不同号码相符，即中奖。', N'二不同号投注：当期开奖号码中有两个号码不相同，且投注号码中的两个不同号码与当期开奖号码中的两个不同号码相符，即中奖。', 14.083, 14.083, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_32BT' AND LotteryId=@lttype);

--8, 初使彩种小类: 三不同
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'三不同';

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'三不同', N'直选', 'K_33BT', N'三不同直选', N'对三个各不相同的号码进行投注。', N'当期开奖号码的三个号码各不相同，且投注号码与当期开奖号码全部相符，即中奖。', N'三不同号投注：当期开奖号码的三个号码各不相同，且投注号码与当期开奖号码全部相符，即中奖。', 74.106, 74.106, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_33BT' AND LotteryId=@lttype);

--8, 初使彩种小类: 三连号
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'三连号';

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, 
MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, 
IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'三连号', N'通选', 'K_33LTX', N'三连号通选', N'对所有三个相连的号码（仅限：123、234、345、456）进行投注。', N'当期开奖号码为三个相连的号码（仅限：123、234、345、456），即中奖。', N'三连号通选：当期开奖号码为三个相连的号码（仅限：123、234、345、456），即中奖。', 15.648, 15.648, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='K_33LTX' AND LotteryId=@lttype);
