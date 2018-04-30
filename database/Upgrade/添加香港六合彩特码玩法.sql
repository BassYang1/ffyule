
USE Ticket;
GO

--3, 添加彩种大类, 特码
DECLARE @bigId INT, @lttype INT = 6, @sort INT = 0

SELECT @bigId = MAX(Id) + 1 FROM Sys_PlayBigType;

INSERT INTO Sys_PlayBigType(Id, TypeId, Title, Title2, Sort, IsOpen, IsOpenIphone, STime)
SELECT @bigId, @ltType, N'特码', NULL, @bigId, 0, 1, GETDATE()
WHERE NOT EXISTS (SELECT 1 FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'特码');

--4, 初使彩种小类: 特码
--新增小类: Type 1-直选,2-复选,3-特殊, flag 是否显示在页面上
SELECT @bigid = Id, @sort = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'特码';

--特码
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'特码', 'H_TM', N'特码', N'香港⑥合彩公司当期开出的最后一码为特码', N'开奖号码：30 40 34 08 25 15 + 42, 投注特码：42', N'香港⑥合彩公司当期开出的最后一码为特码。', 42.5, 42.5, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TM' AND LotteryId=@lttype);
SELECT @bigid = Id FROM Sys_PlayBigType WHERE TypeId=@lttype AND Title=N'特码';

--特码大小
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'特码大小', 'H_TMDX', N'特码大小', N'特小：开出的特码，(01~24)小于或等于24。特大：开出的特码，(25~48)大于或等于25。和局：特码为49，即退回本金。', N'', N'', 1.96, 1.96, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMDX' AND LotteryId=@lttype);

--特码单双
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'特码单双', 'H_TMDS', N'特码单双', N'特双：特码为双数。特单：特码为单数。和局：特码为49，即退回本金。', N'', N'', 1.96, 1.96, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMDS' AND LotteryId=@lttype);

--特码合数大小
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'合数大小', 'H_TMHDX', N'合数大小', N'以特码个位和十位数字之和值来判断胜负，和值大于等于7为合数大，如07、17、26、36、44；和值小于等于6为合数小，如01、12、23、32、42；开出49则视为和局。', N'', N'', 1.96, 1.96, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMHDX' AND LotteryId=@lttype);

--特码合数单双
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'合数单双', 'H_TMHDS', N'合数单双', N'特双：指开出的特码，个位加上十位之和为双数。特单：指开出的特码，个位加上十位之和为单数。和局：特码为49，即退回本金。', N'', N'', 1.96, 1.96, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMHDS' AND LotteryId=@lttype);

--特码头数
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'特码头数', 'H_TMTS', N'特码头数', N'特码头数：是指特码属头数的号码。包括：0头、1头、2头、3头和4头。', N'如：开奖结果特别号码为21则2头为中奖，其他头数都不中奖。', N'0头：01、02、03、04、05、06、07、08、09。1头：10、11、12、13、14、15、16、17、18、19。2头：20、21、22、23、24、25、26、27、28、29。3头：30、31、32、33、34、35、36、37、38、39 
4头：40、41、42、43、44、45、46、47、48、49。', 4.5, 4.5, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMTS' AND LotteryId=@lttype);

--特码尾数
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'特码尾数', 'H_TMWS', N'特码尾数', N'特码尾数：是指特码属尾数的号码。包括：0尾、1尾、2尾、3尾、4尾、5尾、6尾、7尾、8尾和9尾。', N'例如：开奖结果特别号码为21则1尾数为中奖，其他尾数都不中奖。', N'0尾：10、20、30、40。1尾：01、11、21、31、41。2尾：02、12、22、32、42。3尾：03、13、23、33、43。4尾：04、14、24、34、44。5尾：05、15、25、35、45。6尾：06、16、26、36、46。7尾：07、17、27、37、47。8尾：08、18、28、38、48。9尾：09、19、29、39、49。', 9.2, 9.2, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMWS' AND LotteryId=@lttype);

--特码尾数大小
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'尾数大小', 'H_TMWDX', N'尾数大小', N'特尾大：5尾~9尾为大，如：05、18、19。特尾小：0尾~4尾为小，如：01、32、44。和局：特码为49。', N'', N'', 1.96, 1.96, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMWDX' AND LotteryId=@lttype);

--特码尾数单双
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'尾数单双', 'H_TMWDS', N'尾数单双', N'特码尾数为单数，如：05、13、29。特码尾数为双数，如：02、32、44。和局：特码为49。', N'', N'', 1.96, 1.96, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMWDS' AND LotteryId=@lttype);

--特码半特
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 2, N'特码', N'特码半特', 'H_TMBT', N'特码半特', N'以特码大小与特码单双游戏为一个投注组合；当期特码开出符合投注组合，即视为中奖；若当期特码开出49号，其余情形视为不中奖。', N'如，大单：25、27、29、31、33、35、37、39，41、43、45、47。小双：02、04、06、08、10、12、14、16，18、20、22、24。', N'', 1.96, 1.96, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBT' AND LotteryId=@lttype);

--特码色波
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 1, N'特码', N'色波', 'H_TMSB', N'色波', N'香港⑥合彩49个号码球分别有红、蓝、绿三种颜色，以特码开出的颜色和投注的颜色相同视为中奖。', N'如,01，红波中奖；25，蓝波中奖。', N'红波：01 ,02 ,07 ,08 ,12 ,13 ,18 ,19 ,23 ,24 ,29 ,30 ,34 ,35 ,40 ,45 ,46。蓝波：03 ,04 ,09 ,10 ,14 ,15 ,20 ,25 ,26 ,31 ,36 ,37 ,41 ,42 ,47 ,48 。绿波：05 ,06 ,11 ,16 ,17 ,21 ,22 ,27 ,28 ,32 ,33 ,38 ,39 ,43 ,44 ,49。', 2.78, 2.78, 0.01, 2.86, 2.86, 0.01, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMSB' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'色波红', 'H_TMSB_H', N'色波红', N'', N'', N'', 2.78, 2.78, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMSB_H' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'色波蓝', 'H_TMSB_LAN', N'色波蓝', N'', N'', N'', 2.86, 2.86, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMSB_LAN' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'色波绿', 'H_TMSB_LV', N'色波绿', N'', N'', N'', 2.86, 2.86, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMSB_LV' AND LotteryId=@lttype);

--半波
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 2, N'特码', N'特码半波', 'H_TMBB', N'特码半波', N'以特码色波和特单，特双，特大，特小为一个投注组合，当期特码开出符合投注组合，即视为中奖； 若当期特码开出49号，则视为和局；其余情形视为不中奖。', N'如，红单：01，红小：01，绿单：05，绿小：05，蓝双：26，蓝大：26。', N'', 5.06, 5.06, 0.01, 6.51, 6.51, 0.01, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_HDAN', N'红单', N'', N'', N'', 5.61, 5.61, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_HDAN' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_HS', N'红双', N'', N'', N'', 5.06, 5.06, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_HS' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_HDA', N'红大', N'', N'', N'', 6.51, 6.51, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_HDA' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_HX', N'红小', N'', N'', N'', 4.51, 4.51, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_HX' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_LANDAN', N'蓝单', N'', N'', N'', 5.61, 5.61, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LANDAN' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_LANS', N'蓝双', N'', N'', N'', 5.61, 5.61, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LANS' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_LANDA', N'蓝大', N'', N'', N'', 5.06, 5.06, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LANDA' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_LANX', N'蓝小', N'', N'', N'', 6.51, 6.51, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LANX' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_LVDAN', N'绿单', N'', N'', N'', 5.61, 5.61, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LVDAN' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_LVS', N'绿双', N'', N'', N'', 6.51, 6.51, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LVS' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_LVDA', N'绿大', N'', N'', N'', 5.61, 5.61, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LVDA' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'半波', 'H_TMBB_LVX', N'绿小', N'', N'', N'', 6.51, 6.51, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LVX' AND LotteryId=@lttype);

--半半波
INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, 2, N'特码', N'特码半半波', 'H_TMBBB', N'特码半半波', N'以特码色波和特单双及特大小等游戏为一个投注组合，当期特码开出符合投注组合，即视为中奖； 若当期特码开出49号，则视为和局；其余情形视为不中奖。', N'如，红单大，红双小，绿单大，绿双小，蓝双大，蓝单大。', N'', 8.9, 8.9, 0.01, 14.8, 14.8, 0.01, 0, 0, 80, 0, 0, 0, 2, 0, 1, 0, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBBB' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'红大单', 'H_TMBB_HDD', N'红大单', N'', N'', N'', 14.8, 14.8, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_HDD' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'红大双', 'H_TMBB_HDS', N'红大双', N'', N'', N'', 11.1, 11.1, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_HDS' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'红小单', 'H_TMBB_HXD', N'红小单', N'', N'', N'', 8.9, 8.9, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_HXD' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'红小双', 'H_TMBB_HXS', N'红小双', N'', N'', N'', 8.9, 8.9, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_HXS' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'蓝大单', 'H_TMBB_LANDD', N'蓝大单', N'', N'', N'', 8.9, 8.9, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LANDD' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'蓝大双', 'H_TMBB_LANDS', N'蓝大双', N'', N'', N'', 11.1, 11.1, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LANDS' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'蓝小单', 'H_TMBB_LANXD', N'蓝小单', N'', N'', N'', 14.8, 14.8, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LANXD' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'蓝小双', 'H_TMBB_LANXS', N'蓝小双', N'', N'', N'', 11.1, 11.1, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LANXS' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'绿大单', 'H_TMBB_LVDD', N'绿大单', N'', N'', N'', 11.1, 11.1, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LVDD' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'绿大双', 'H_TMBB_LVDS', N'绿大双', N'', N'', N'', 11.1, 11.1, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LVDS' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'绿小单', 'H_TMBB_LVXD', N'绿小单', N'', N'', N'', 11.1, 11.1, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LVXD' AND LotteryId=@lttype);

INSERT INTO Sys_PlaySmallType(LotteryId, Radio, Sort, Type, Title0, Title, Title2, TitleName, remark, example, help, MaxBonus, MinBonus, PosBonus, MaxBonus2, MinBonus2, PosBonus2, WzNum, MinNum, MaxNum, Pos, PosNum, Bonus, randoms, IsOpen, IsOpenIphone, flag, STime)
SELECT @lttype, @bigId, @sort, NULL, N'特码', N'绿小双', 'H_TMBB_LVXS', N'绿小双', N'', N'', N'', 14.8, 14.8, 0.01, 0, 0, 0, 0, 0, 80, 0, 0, 0, 2, 0, 1, 1, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_PlaySmallType WHERE Title2='H_TMBB_LVXS' AND LotteryId=@lttype);


