--测试账号
SELECT * FROM N_USER WHERE UserName IN ('taiyi', 'lululu');
UPDATE N_USER SET Password='c1601dc1bf00f0a2c9d5f9a8c34eb27f97d0a8b7fcdba0f7a038ab51cf619bad' WHERE UserName IN ('admin', 'taiyi', 'lululu');

--支付方式
SELECT * from Sys_ChargeSet where IsUsed=0 and Id<>1020  ORDER BY Sort asc;
SELECT * FROM Sys_SbfChannelMap;

--支付
select top 1 MinCharge, * from Sys_Info;
select * from Sys_Info;
--支付记录
SELECT * FROM N_UserCharge WHERE UserId = 1771 ORDER BY STime DESC;
--UPDATE N_UserCharge SET State=0 WHERE Ssid='C_4818321444600212141';
SELECT * FROM Pay_temp;

--提现
SELECT * FROM N_UserGetCash;
SELECT * FROM N_UserGetCashHistory;