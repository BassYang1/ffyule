
USE Ticket;
GO

--3, 添加彩种大类, 总和
DECLARE @bigId INT, @lttype INT = 6, @sort INT = 0

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;

INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @ltType, N'总和', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'总和');

--4, 初使彩种小类: 总和
--新增小类: Type 1-直选,2-复选,3-特殊, null-不显示在页面上, flag 是否显示在页面上
SELECT @bigid = Id, @sort = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'总和';

--总和，总和大小
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'总和大小', 'H_ZHDX', N'总和大小', N'⑥合彩公司每期开出的前面六个号码为正码，下注号码如在六个正码号码里中奖，最后一码为特码。', N'⑥合彩公司每期开出的前面六个号码为正码，下注号码如在六个正码号码里中奖，最后一码为特码。如开奖号码：30 40 34 08 25 15 + 48，投注正码：30 40 34 08 25 15，特码48。', N'总和大：以七个开奖号码的分数总和大于或等于175。总和小：以有七个开奖号码的分数总和小于或等于174。', 1.985, 1.985, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZHDX' AND LotteryId=@lttype);

--总和，总和单双
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'总和单双', 'H_ZHDS', N'总和单双', N'⑥合彩公司每期开出的前面六个号码为正码，下注号码如在六个正码号码里中奖，最后一码为特码。', N'⑥合彩公司每期开出的前面六个号码为正码，下注号码如在六个正码号码里中奖，最后一码为特码。如开奖号码：30 40 34 08 25 15 + 48，投注正码：30 40 34 08 25 15，特码48。', N'总和单：以七个开奖号码的分数总和是单数，如分数总和是133、197。总和双：以七个开奖号码的分数总和是双数，如分数总和是120、188。', 1.985, 1.985, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZHDS' AND LotteryId=@lttype);
