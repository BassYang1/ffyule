
USE Ticket;
GO

--3, 添加彩种大类, 正码
DECLARE @bigId INT, @lttype INT = 6, @sort INT = 0

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;

INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @ltType, N'正码', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'正码');

--4, 初使彩种小类: 正码
--新增小类: Type 1-直选,2-复选,3-特殊, null-不显示在页面上, flag 是否显示在页面上
SELECT @bigid = Id, @sort = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'正码';

--正码，正码大小
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'正码大小', 'H_ZMDX', N'正码大小', N'以指定出现正码的位置与号码大于或等于25为大，小于或等于24为小，开出49为和。', N'以指定出现正码的位置与号码大于或等于25为大，小于或等于24为小，开出49为和。', N'', 1.985, 1.985, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMDX' AND LotteryId=@lttype);

--正码单双
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'正码单双', 'H_ZMDS', N'正码单双', N'以指定出现正码的位置与号码为单数或双数下注，开出49为和。', N'以指定出现正码的位置与号码为单数或双数下注，开出49为和。', N'', 1.985, 1.985, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMDS' AND LotteryId=@lttype);

--合数大小
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'合数大小', 'H_ZMHSDX', N'合数大小', N'以指定出现正码的位置与号码个位和十位数字总和来判断胜负，和数大于或等于7为合大，小于或等于6为合小，开出49号为和。', N'以指定出现正码的位置与号码个位和十位数字总和来判断胜负，和数大于或等于7为合大，小于或等于6为合小，开出49号为和。', N'', 1.985, 1.985, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMHSDX' AND LotteryId=@lttype);

--合数单双
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'合数单双', 'H_ZMHSDS', N'合数单双', N'以指定出现正码的位置与号码个位和十位数字总和来判断单双，开出49号为和。', N'以指定出现正码的位置与号码个位和十位数字总和来判断单双，开出49号为和。', N'', 1.985, 1.985, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMHSDS' AND LotteryId=@lttype);

--红波
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'红波', 'H_ZMSBH', N'红波', N'以指定出现正码的位置的球色下注，开奖之球色与下注之颜色相同时，视为中奖，其余情形视为不中奖。', N'以指定出现正码的位置的球色下注，开奖之球色与下注之颜色相同时，视为中奖，其余情形视为不中奖。', N'', 2.7, 2.7, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMSBH' AND LotteryId=@lttype);

--蓝波
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'蓝波', 'H_ZMSBLAN', N'蓝波', N'以指定出现正码的位置的球色下注，开奖之球色与下注之颜色相同时，视为中奖，其余情形视为不中奖。', N'以指定出现正码的位置的球色下注，开奖之球色与下注之颜色相同时，视为中奖，其余情形视为不中奖。', N'', 2.85, 2.85, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMSBLAN' AND LotteryId=@lttype);

--绿波
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'绿波', 'H_ZMSBLV', N'绿波', N'以指定出现正码的位置的球色下注，开奖之球色与下注之颜色相同时，视为中奖，其余情形视为不中奖。', N'以指定出现正码的位置的球色下注，开奖之球色与下注之颜色相同时，视为中奖，其余情形视为不中奖。', N'', 2.85, 2.85, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMSBLV' AND LotteryId=@lttype);

--属数大小
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'正码', N'尾数大小', 'H_ZMWSDX', N'尾数大小', N'以指定出现正码的位置与号码尾数大小下注，若0尾~4尾为小、5尾~9尾为大。如01、32、44为正尾小；如05、18、19为正尾大，开出49号为和。', N'以指定出现正码的位置与号码尾数大小下注，若0尾~4尾为小、5尾~9尾为大。如01、32、44为正尾小；如05、18、19为正尾大，开出49号为和。', N'', 1.985, 1.985, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMWSDX' AND LotteryId=@lttype);
