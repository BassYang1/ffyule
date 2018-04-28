USE Ticket;
GO

--1, 添加彩种
DECLARE @sort INT, 
		@msort INT, 
		@ltid INT = 3010,
		@lttype INT = 3,
		@iss INT = 1

SELECT @sort = MAX(Sort), @msort = MAX(IphoneSort) FROM Sys_Lottery;

--新增彩种: Ltype 彩种类别, IndexType 显示顺序
INSERT INTO Sys_Lottery(Id, Title, Code, MinTimes, MaxTimes, IsOpen, CloseTime, second, Sort, Ltype, IsAuto, IndexType, Url, AutoUrl, 
	IphoneIsOpen, IphoneSort, IphoneRemark, IphoneImg, IssNum)
SELECT @ltid, N'六合彩', 'hk6', 1, 50000, 0, 0, 0, @sort, @lttype, 0, 7, N'', 0, 
	0, @msort, N'香港⑥合彩每星期搅珠三次，通常于星期二、星期四及非赛马日之星期六或日晚上举行', 84, @iss
WHERE NOT EXISTS(SELECT 1 FROM Sys_Lottery WHERE Code='hk6');

DECLARE @num INT = 1

--2, 添加彩种时间
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '001', '2018-1-2 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='001' AND Time='2018-1-2 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '002', '2018-1-6 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='002' AND Time='2018-1-6 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '003', '2018-1-11 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='003' AND Time='2018-1-11 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '004', '2018-1-16 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='004' AND Time='2018-1-16 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '005', '2018-1-20 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='005' AND Time='2018-1-20 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '006', '2018-1-23 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='006' AND Time='2018-1-23 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '007', '2018-1-25 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='007' AND Time='2018-1-25 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '008', '2018-1-27 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='008' AND Time='2018-1-27 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '009', '2018-1-30 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='009' AND Time='2018-1-30 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '010', '2018-2-1 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='010' AND Time='2018-2-1 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '011', '2018-2-3 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='011' AND Time='2018-2-3 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '012', '2018-2-6 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='012' AND Time='2018-2-6 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '013', '2018-2-8 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='013' AND Time='2018-2-8 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '014', '2018-2-11 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='014' AND Time='2018-2-11 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '015', '2018-2-13 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='015' AND Time='2018-2-13 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '016', '2018-2-15 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='016' AND Time='2018-2-15 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '017', '2018-2-20 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='017' AND Time='2018-2-20 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '018', '2018-2-22 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='018' AND Time='2018-2-22 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '019', '2018-2-24 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='019' AND Time='2018-2-24 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '020', '2018-2-27 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='020' AND Time='2018-2-27 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '021', '2018-3-1 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='021' AND Time='2018-3-1 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '022', '2018-3-4 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='022' AND Time='2018-3-4 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '023', '2018-3-6 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='023' AND Time='2018-3-6 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '024', '2018-3-8 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='024' AND Time='2018-3-8 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '025', '2018-3-10 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='025' AND Time='2018-3-10 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '026', '2018-3-13 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='026' AND Time='2018-3-13 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '027', '2018-3-15 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='027' AND Time='2018-3-15 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '028', '2018-3-17 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='028' AND Time='2018-3-17 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '029', '2018-3-20 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='029' AND Time='2018-3-20 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '030', '2018-3-22 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='030' AND Time='2018-3-22 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '031', '2018-3-24 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='031' AND Time='2018-3-24 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '032', '2018-3-29 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='032' AND Time='2018-3-29 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '033', '2018-3-31 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='033' AND Time='2018-3-31 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '034', '2018-4-3 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='034' AND Time='2018-4-3 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '035', '2018-4-5 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='035' AND Time='2018-4-5 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '036', '2018-4-7 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='036' AND Time='2018-4-7 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '037', '2018-4-10 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='037' AND Time='2018-4-10 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '038', '2018-4-12 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='038' AND Time='2018-4-12 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '039', '2018-4-14 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='039' AND Time='2018-4-14 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '040', '2018-4-17 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='040' AND Time='2018-4-17 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '041', '2018-4-19 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='041' AND Time='2018-4-19 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '042', '2018-4-22 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='042' AND Time='2018-4-22 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '043', '2018-4-24 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='043' AND Time='2018-4-24 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '044', '2018-4-26 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='044' AND Time='2018-4-26 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '045', '2018-4-28 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='045' AND Time='2018-4-28 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '046', '2018-5-1 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='046' AND Time='2018-5-1 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '047', '2018-5-3 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='047' AND Time='2018-5-3 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '048', '2018-5-5 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='048' AND Time='2018-5-5 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '049', '2018-5-8 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='049' AND Time='2018-5-8 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '050', '2018-5-10 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='050' AND Time='2018-5-10 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '051', '2018-5-13 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='051' AND Time='2018-5-13 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '052', '2018-5-15 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='052' AND Time='2018-5-15 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '053', '2018-5-17 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='053' AND Time='2018-5-17 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '054', '2018-5-19 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='054' AND Time='2018-5-19 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '055', '2018-5-22 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='055' AND Time='2018-5-22 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '056', '2018-5-24 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='056' AND Time='2018-5-24 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '057', '2018-5-26 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='057' AND Time='2018-5-26 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '058', '2018-5-29 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='058' AND Time='2018-5-29 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '059', '2018-5-31 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='059' AND Time='2018-5-31 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '060', '2018-6-2 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='060' AND Time='2018-6-2 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '061', '2018-6-5 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='061' AND Time='2018-6-5 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '062', '2018-6-7 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='062' AND Time='2018-6-7 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '063', '2018-6-9 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='063' AND Time='2018-6-9 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '064', '2018-6-12 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='064' AND Time='2018-6-12 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '065', '2018-6-14 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='065' AND Time='2018-6-14 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '066', '2018-6-17 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='066' AND Time='2018-6-17 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '067', '2018-6-19 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='067' AND Time='2018-6-19 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '068', '2018-6-21 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='068' AND Time='2018-6-21 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '069', '2018-6-23 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='069' AND Time='2018-6-23 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '070', '2018-6-26 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='070' AND Time='2018-6-26 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '071', '2018-6-28 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='071' AND Time='2018-6-28 21:30:00'); 
INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime) 
SELECT 3010, '072', '2018-6-30 21:30:00', 0, GETDATE() 
WHERE NOT EXISTS(SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=3010 AND Sn='072' AND Time='2018-6-30 21:30:00'); 
