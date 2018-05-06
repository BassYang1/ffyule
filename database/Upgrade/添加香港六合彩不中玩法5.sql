
USE Ticket;
GO

--3, 添加彩种大类, 不中
DECLARE @bigId INT, @lttype INT = 6, @sort INT = 0

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;

INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @ltType, N'不中', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'不中');

--4, 初使彩种小类: 不中
--新增小类: Type 1-直选,2-复选,3-特殊, null-不显示在页面上, flag 是否显示在页面上
SELECT @bigid = Id, @sort = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'不中';

--五不中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'不中', N'五不中', 'H_BZ5', N'五不中', N'挑选5个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'挑选5个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'例如当期号码为19,24,17,34,40,39,特别号49，所挑选5个号码(称为五不中)，若所挑选的号码内皆沒有坐落于当期号码，则为中奖。', 2.1, 2.1, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_BZ5' AND LotteryId=@lttype);

--六不中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'不中', N'六不中', 'H_BZ6', N'六不中', N'挑选6个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'挑选6个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'例如当期号码为19,24,17,34,40,39,特别号49，所挑选5个号码(称为五不中)，若所挑选的号码内皆沒有坐落于当期号码，则为中奖。', 2.5, 2.5, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_BZ6' AND LotteryId=@lttype);

--七不中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'不中', N'七不中', 'H_BZ7', N'七不中', N'挑选7个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'挑选7个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'例如当期号码为19,24,17,34,40,39,特别号49，所挑选5个号码(称为五不中)，若所挑选的号码内皆沒有坐落于当期号码，则为中奖。', 3, 3, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_BZ7' AND LotteryId=@lttype);

--八不中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'不中', N'八不中', 'H_BZ8', N'八不中', N'挑选8个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'挑选8个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'例如当期号码为19,24,17,34,40,39,特别号49，所挑选5个号码(称为五不中)，若所挑选的号码内皆沒有坐落于当期号码，则为中奖。', 3.6, 3.6, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_BZ8' AND LotteryId=@lttype);

--九不中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'不中', N'九不中', 'H_BZ9', N'九不中', N'挑选9个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'挑选9个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'例如当期号码为19,24,17,34,40,39,特别号49，所挑选5个号码(称为五不中)，若所挑选的号码内皆沒有坐落于当期号码，则为中奖。', 4.3, 4.3, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_BZ9' AND LotteryId=@lttype);

--十不中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'不中', N'十不中', 'H_BZ10', N'十不中', N'挑选10个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'挑选10个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'例如当期号码为19,24,17,34,40,39,特别号49，所挑选5个号码(称为五不中)，若所挑选的号码内皆沒有坐落于当期号码，则为中奖。', 5.2, 5.2, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_BZ10' AND LotteryId=@lttype);

--十一不中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'不中', N'十一不中', 'H_BZ11', N'十一不中', N'挑选11个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'挑选11个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'例如当期号码为19,24,17,34,40,39,特别号49，所挑选5个号码(称为五不中)，若所挑选的号码内皆沒有坐落于当期号码，则为中奖。', 6.35, 6.35, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_BZ11' AND LotteryId=@lttype);

--十二不中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'不中', N'十二不中', 'H_BZ12', N'十二不中', N'挑选12个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'挑选12个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'例如当期号码为19,24,17,34,40,39,特别号49，所挑选5个号码(称为五不中)，若所挑选的号码内皆沒有坐落于当期号码，则为中奖。', 7.8, 7.8, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_BZ12' AND LotteryId=@lttype);

--十五不中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'不中', N'十五不中', 'H_BZ15', N'十五不中', N'挑选15个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'挑选15个号码为一个组合，当期号码(所有正码与最后开出的特码)皆没有坐落于投注时所挑选之号码组合内，则视为中奖，若是有任何一个当期号码开在所挑选的号码组合情形视为不中奖。', N'例如当期号码为19,24,17,34,40,39,特别号49，所挑选5个号码(称为五不中)，若所挑选的号码内皆沒有坐落于当期号码，则为中奖。', 12, 12, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_BZ15' AND LotteryId=@lttype);
