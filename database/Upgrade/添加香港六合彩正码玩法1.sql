
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

--4, 初使彩种小类: 正码, 平特一肖
--新增小类: Type 1-直选,2-复选,3-特殊, null-不显示在页面上, flag 是否显示在页面上
SELECT @bigid = Id, @sort = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'正码';

--平特一肖
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'正码', N'平特一肖', 'H_ZMPTX1', N'平特一肖', N'只要当期号码(所有正码与最后开出的特码)，落在下注生肖范围内，则视为中奖。', N'只要当期号码(所有正码与最后开出的特码)，落在下注生肖范围内，则视为中奖。', N'', 2.1, 2.1, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMPTX1' AND LotteryId=@lttype);

--平特二肖
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'正码', N'平特二肖', 'H_ZMPTX2', N'平特二肖', N'挑选两个生肖为一个组合，当期号码(所有正码与最后开出的特码)坐落于投注时所勾选之生肖组合所属号码内，所勾选之生肖皆至少有中一个号码，则视为中奖，其余情视为不中奖。', N'挑选两个生肖为一个组合，当期号码(所有正码与最后开出的特码)坐落于投注时所勾选之生肖组合所属号码内，所勾选之生肖皆至少有中一个号码，则视为中奖，其余情视为不中奖。', N'例如：如果当期号码为19、24、12、34、40、39.特码：49，所勾选两个生肖，若所有生肖的所属号码内至少一个出现于当期号码，则视为中奖。', 4.12, 4.12, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMPTX2' AND LotteryId=@lttype);

--平特三肖
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'正码', N'平特三肖', 'H_ZMPTX3', N'平特三肖', N'挑选三个生肖为一个组合，当期号码(所有正码与最后开出的特码)坐落于投注时所勾选之生肖组合所属号码内，所勾选之生肖皆至少有中一个号码，则视为中奖，其余情视为不中奖。', N'挑选三个生肖为一个组合，当期号码(所有正码与最后开出的特码)坐落于投注时所勾选之生肖组合所属号码内，所勾选之生肖皆至少有中一个号码，则视为中奖，其余情视为不中奖。', N'例如：如果当期号码为19、24、12、34、40、39.特码：49，所勾选三个生肖，若所有生肖的所属号码内至少一个出现于当期号码，则视为中奖。', 11.12, 11.12, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMPTX3' AND LotteryId=@lttype);

--平特四肖
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'正码', N'平特四肖', 'H_ZMPTX4', N'平特四肖', N'挑选四个生肖为一个组合，当期号码(所有正码与最后开出的特码)坐落于投注时所勾选之生肖组合所属号码内，所勾选之生肖皆至少有中一个号码，则视为中奖，其余情视为不中奖。', N'挑选四个生肖为一个组合，当期号码(所有正码与最后开出的特码)坐落于投注时所勾选之生肖组合所属号码内，所勾选之生肖皆至少有中一个号码，则视为中奖，其余情视为不中奖。', N'例如：如果当期号码为19、24、12、34、40、39.特码：49，所勾选四个生肖，若所有生肖的所属号码内至少一个出现于当期号码，则视为中奖。', 32, 32, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMPTX4' AND LotteryId=@lttype);

--平特五肖
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 3, N'正码', N'平特五肖', 'H_ZMPTX5', N'平特五肖', N'挑选五个生肖为一个组合，当期号码(所有正码与最后开出的特码)坐落于投注时所勾选之生肖组合所属号码内，所勾选之生肖皆至少有中一个号码，则视为中奖，其余情视为不中奖。', N'挑选五个生肖为一个组合，当期号码(所有正码与最后开出的特码)坐落于投注时所勾选之生肖组合所属号码内，所勾选之生肖皆至少有中一个号码，则视为中奖，其余情视为不中奖。', N'例如：如果当期号码为19、24、12、34、40、39.特码：49，所勾选五个生肖，若所有生肖的所属号码内至少一个出现于当期号码，则视为中奖。', 98, 98, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMPTX5' AND LotteryId=@lttype);

--平一中一
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 2, N'正码', N'平一中一', 'H_ZMP1Z1', N'平一中一', N'香港彩当期开出之前6个号码叫平码。每一个号码平一中一，假如投注号码为开奖号码之平码，视为中奖，其馀情形视为不中奖。', N'香港彩当期开出之前6个号码叫平码。每一个号码平一中一，假如投注号码为开奖号码之平码，视为中奖，其馀情形视为不中奖。', N'', 7.16, 7.16, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMP1Z1' AND LotteryId=@lttype);

--平二中二
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 2, N'正码', N'平二中二', 'H_ZMP2Z2', N'平二中二', N'所投注的每二个号码为一组合，二个号码都是开奖号码之平码，视为中奖，其馀情形视为不 中奖（含一个平码加一个特别号码之情形）。', N'所投注的每二个号码为一组合，二个号码都是开奖号码之平码，视为中奖，其馀情形视为不 中奖（含一个平码加一个特别号码之情形）。', N'', 70, 70, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMP2Z2' AND LotteryId=@lttype);

--平三中二，选三
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 2, N'正码', N'平三中二', 'H_ZMP3Z2X3', N'平三中二', N'所投注的每三个号码为一组合，若其中2个是开奖号码中的平码，即为三中二，视为中奖； 若3个都是开奖号码中的平码，即为三中二之中三，其馀情形视为不中奖。', N'所投注的每三个号码为一组合，若其中2个是开奖号码中的平码，即为三中二，视为中奖； 若3个都是开奖号码中的平码，即为三中二之中三，其馀情形视为不中奖。', N'如06、07、08 为一组合，开奖号码中有06、07两个平码，没有08，即为三中二，按三中二赔付；如开奖 号码中有06、07、08三个平码，即为三中二之中三，按中三赔付；如出现1个或没有，视为不中奖。', 23, 23, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMP3Z2X3' AND LotteryId=@lttype);

--平三中二全中
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'正码', N'平三中二全中', 'H_ZMP3Z2QZ', N'平三中二全中', N'所投注的每三个号码为一组合，若其中2个是开奖号码中的平码，即为三中二，视为中奖； 若3个都是开奖号码中的平码，即为三中二之中三，其馀情形视为不中奖。', N'所投注的每三个号码为一组合，若其中2个是开奖号码中的平码，即为三中二，视为中奖； 若3个都是开奖号码中的平码，即为三中二之中三，其馀情形视为不中奖。', N'如06、07、08 为一组合，开奖号码中有06、07两个平码，没有08，即为三中二，按三中二赔付；如开奖 号码中有06、07、08三个平码，即为三中二之中三，按中三赔付；如出现1个或没有，视为不中奖。', 100, 100, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 0, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMP3Z2QZ' AND LotteryId=@lttype);

--平三中三
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 2, N'正码', N'平三中三', 'H_ZMP3Z3', N'平三中三', N'所投注的每三个号码为一组合，若三个号码都是开奖号码之平码，视为中奖，其馀情形视为 不中奖。如06、07、08三个都是开奖号码之平码，视为中奖，如两个平码加上一个特别号码视为不中奖 。', N'所投注的每三个号码为一组合，若三个号码都是开奖号码之平码，视为中奖，其馀情形视为 不中奖。如06、07、08三个都是开奖号码之平码，视为中奖，如两个平码加上一个特别号码视为不中奖 。', N'', 650, 650, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMP3Z3' AND LotteryId=@lttype);

--平四中四
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 2, N'正码', N'平四中四', 'H_ZMP4Z4', N'平四中四', N'所投注的每四个号码为一组合，若四个号码都是开奖号码之平码，视为中奖，其馀情形视为 不中奖。如06、07、08、21四个都是开奖号码之平码，视为中奖，如两个平码加上一个特别号码视为不中奖 。', N'所投注的每四个号码为一组合，若四个号码都是开奖号码之平码，视为中奖，其馀情形视为 不中奖。如06、07、08、21四个都是开奖号码之平码，视为中奖，如两个平码加上一个特别号码视为不中奖 。', N'', 8000, 8000, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_ZMP4Z4' AND LotteryId=@lttype);
