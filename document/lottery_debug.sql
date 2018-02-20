
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
SELECT * FROM Sys_Lottery WHERE 1 > 0 AND Id = 4004;

--系统开奖时间
SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 4004 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 4004 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 4003 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 4002 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 4001 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 3004 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 3005 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 2006 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 2005 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 1020 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 1011 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 1013 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 1018 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 1004 ORDER BY STime DESC;

SELECT * FROM Sys_LotteryData WHERE 1 > 0 AND Type = 1019 ORDER BY STime DESC;
