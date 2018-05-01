
USE Ticket;
GO

--3, 添加彩种大类, 特码生肖
DECLARE @bigId INT, @lttype INT = 6, @sort INT = 0

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;

INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @ltType, N'生肖', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'生肖');

--4, 初使彩种小类: 特码
--新增小类: Type 1-直选,2-复选,3-特殊, null-不显示在页面上, flag 是否显示在页面上
SELECT @bigid = Id, @sort = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'生肖';

--特码
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'生肖', N'特码生肖', 'H_TMSX', N'特码生肖', N'若当期特别号，落在下注生肖范围内，视为中奖。', N'生肖顺序为鼠、牛、虎、兔、龙、蛇、马、羊、猴、鸡、狗和猪。若当期特码落在下注生肖范围内，视为中奖。', N'如今年是鸡年，就以鸡为开始，如今年是鸡年，生肖鸡：01、13、25、37、49 ，狗：12、24、36、48 ，猪：11、23、35、47 ，鼠：10、22、34、46 ，牛：09、21、33、45 ，虎：08、20、32、44 ，兔：07、19、31、43 ，龙：06、18、30、42 ，蛇：05、17、29、41 ，马：04、16、28、40 ，羊：03、15、27、39 ，猴：02、14、26、38。', 11.6, 11.6, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMSX' AND LotteryId=@lttype);

--总肖
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'生肖', N'总肖', 'H_SXZX', N'总肖', N'当期号码(所有正码与最后开出的特码)开出的不同生肖总数，分为2肖、3肖、4肖、5肖、6肖和7肖。', N'当期号码(所有正码与最后开出的特码)开出的不同生肖总数，与所投注之预计开出之生肖总和数(不用指定特定生肖)，则视为中奖，其余情形视为不中奖。', N'如果当期号码为19、24、12、34、40、39 特别号：49，总计六个生肖，若选总肖【6】则为中奖(请注意：49号亦算输赢，不为和）。', 11.6, 11.6, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZX' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'生肖', N'总肖', 'H_SXZX2', N'2肖', N'', N'', N'', 16.813, 16.813, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZX2' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'生肖', N'总肖', 'H_SXZX3', N'3肖', N'', N'', N'', 16.813, 16.813, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZX3' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'生肖', N'总肖', 'H_SXZX4', N'4肖', N'', N'', N'', 16.813, 16.813, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZX4' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'生肖', N'总肖', 'H_SXZX5', N'5肖', N'', N'', N'', 3.192, 3.192, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZX5' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'生肖', N'总肖', 'H_SXZX6', N'6肖', N'', N'', N'', 2.126, 2.126, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZX6' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'生肖', N'总肖', 'H_SXZX7', N'7肖', N'', N'', N'', 5.650, 5.650, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZX7' AND LotteryId=@lttype);

--总肖单双
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'生肖', N'总肖单双', 'H_SXZXDS', N'总肖单双', N'当期号码（正码和特码）开出的不同生肖总数若为单数则为单，若为双数则为双。', N'当期号码（正码和特码）开出的不同生肖总数若为单数则为单，若为双数则为双。', N'如，总肖为2、4和6，则双中奖；总肖数为3、5和7，则单中奖。', 2.028, 2.028, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZXDS' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'生肖', N'总肖', 'H_SXZXDSD', N'总肖单', N'', N'', N'', 2.028, 2.028, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZXDSD' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'生肖', N'总肖', 'H_SXZXDSS', N'总肖双', N'', N'', N'', 1.887, 1.887, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_SXZXDSS' AND LotteryId=@lttype);
