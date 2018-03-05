
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
--UPDATE Sys_Lottery SET ApiUrl = 'http://baidu.com?url=' + ApiUrl WHERE ApiUrl IS NOT NULL;

--用户买同一期，ykl588开奖, zxr588不开奖, 父级会员gzj888
--0-未开奖
--1-已撤单
--2-未中奖
--3-已中奖
--123456
--UPDATE N_USER SET Password='cdd7e692858094371afa6affc351d71030aa93a45a1ffe132b3b97282b6a49b8' 
SELECT * FROM N_User WHERE Id IN (2318, 2337, 2322);
SELECT * FROM Flex_UserBet WHERE UserName IN ('ykl588', 'zxr588') ORDER BY STime DESC;
SELECT TOP 12 1961as isme,row_number() over (order by Id desc) as rowid,Id,SsId,UserId,UserName,UserMoney,PlayId,PlayName,PlayCode,LotteryId,LotteryName,IssueNum,SingleMoney,moshi,Times,
Num,DX,DS,cast(Times*Total as decimal(15,4)) as Total,Point,PointMoney,Bonus,Bonus2,WinNum,WinBonus,RealGet,Pos,STime,STime2,substring(Convert(varchar(20),STime2,120),6,11) as ShortTime,
IsOpen,State,IsWin,number,poslen From Flex_UserBet with(nolock)  
Where 1 > 0
--and UserId IN (2318, 2337, 2322)
and UserName IN ('ykl588', 'zxr588')
and UserCode like '%,1961,%' 
and STime2 >='2018-03-04 10:20:09' and STime2 <='2018-03-04 10:23:09'
and IssueNum IN ('20180304-0622', '20180304-0623')
ORDER BY Id DESC

SELECT U.UserName, B.UserId, B.State, B.* FROM N_UserBet B, N_User U 
Where 1 > 0
and B.UserId = U.Id
and STime >='2018-03-04 10:20:09' and STime <='2018-03-04 10:25:09'
and IssueNum IN ('20180304-0622', '20180304-0623')



