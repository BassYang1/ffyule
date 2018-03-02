
--开奖数据
SELECT * FROM Sys_LotteryData WHERE Id > 465150 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0
AND Type = 1011 
--AND Id > 443890 
ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData ORDER BY STime DESC;
--delete from Sys_LotteryData where id = 454848;

--奖种配置
SELECT * FROM Sys_Lottery;
SELECT * FROM Sys_Lottery WHERE 1 > 0 AND Id = 1018;
SELECT * FROM Sys_LotteryTime WHERE 1 > 0 AND LotteryId = 1018;

--系统开奖时间
SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 1018 ORDER BY STime DESC;



SELECT * FROM Sys_Lottery;
SELECT * FROM Sys_Lottery WHERE Id = '1015';
SELECT * FROM Sys_LotteryTime WHERE LotteryId = '1015' AND Sn IN ('629', '628', '627', '626') ORDER BY Id DESC;
SELECT * FROM Sys_LotteryData WHERE Type = '1015' AND Title IN ('20180222629', '20180222627', '20180222626') ORdER BY OpenTime DESC;
SELECT * FROM Sys_LotteryData WHERE Type = '1015' ORdER BY OpenTime DESC;
SELECT * FROM Sys_LotteryTime WHERE LotteryId = '1015' ORDER BY Id DESC;

SELECT * FROM Sys_LotteryTime WHERE LotteryId = '1015' AND Time > '16:58:34' ORDER BY Time ASC;
SELECT * FROM Sys_LotteryTime WHERE LotteryId = '1015' AND Time > '17:51:51' ORDER BY Time ASC;

--OpenTime: 16:42:01
--CurrentTime: 16:42:00


--UPDATE Sys_Lottery SET ApiUrl = 'http://baidu.com?url' + ApiUrl WHERE ApiUrl IS NOT NULL;