USE Ticket;
GO

/*
	2018-01-31
		1, 增加随笔付支付(Sys_ChargeSet)
		2, 增加随笔付支方式映射表(Sys_SbfChannelMap)

	2018-02-02
		1, 增加提现记录表(N_UserGetCashHistory)
		2, 增加支付方式唯一编号(Sys_ChargeSet.UCode)
		
	2018-02-09
		1, 增加契约是否生效(N_UserContractDetail.Effect)
*/

--//////2018-01-31 增加随笔付支付 Start

DECLARE @maxCsId INT, @maxMerCode INT;
SELECT @maxCsId = MAX(id), @maxMerCode = MAX(MerCode) FROM Sys_ChargeSet;

INSERT INTO Sys_ChargeSet(Id, [Type], Name, MerName, MerCode, MerKey, MerCard, MinCharge, MaxCharge, StartTime, EndTime, Total, IsUsed, Sort, STime)
SELECT @maxCsId + 1, 8, N'随笔付', N'随笔付', @maxMerCode + 1, N'SuiBiPay','http://zf.suibipay.com', 50, 3000, '00:00:00', '23:59:59', 1000000, 0, 3, GETDATE()
WHERE NOT EXISTS(SELECT 1 FROM Sys_ChargeSet WHERE MerKey = N'SuiBiPay');

--SELECT * from Sys_ChargeSet where IsUsed=0 and Id<>1020  ORDER BY Sort asc;
IF NOT EXISTS(SELECT 1 FROM sysObjects WHERE xtype = N'U' AND Id = OBJECT_ID('Sys_SbfChannelMap'))
BEGIN
CREATE TABLE Sys_SbfChannelMap
(
	Id INT IDENTITY Primary Key,
	SysCode NVARCHAR(50) NOT NULL,
	SbfChannel NVARCHAR(10) NOT NULL,
	SbfCode NVARCHAR(20),
	[DESC] NVARCHAR(100),
	IsUsed BIT DEFAULT(1) NOT NULL,
	STime DATETIME DEFAULT(GETDATE())
);
END;

INSERT INTO Sys_SbfChannelMap(SysCode, SbfChannel, SbfCode, [DESC]) SELECT N'ZFB', N'2', N'alipay', N'支付宝即时倒账' WHERE NOT EXISTS(SELECT 1 FROM Sys_SbfChannelMap WHERE SysCode = N'ZFB');
INSERT INTO Sys_SbfChannelMap(SysCode, SbfChannel, SbfCode, [DESC]) SELECT N'WX', N'22', N'weixin', N'微信扫码支付' WHERE NOT EXISTS(SELECT 1 FROM Sys_SbfChannelMap WHERE SysCode = N'WX');
--SELECT * FROM Sys_SbfChannel;

--//////2018-01-31 增加随笔付支付 End

--//////2018-02-02 Start

--提现历史记录
--IF NOT EXISTS(SELECT 1 FROM sysObjects WHERE xtype = N'U' AND Id = OBJECT_ID('N_UserGetCashHistory'))
--BEGIN
--CREATE TABLE N_UserGetCashHistory
--(
--	Id INT IDENTITY Primary Key,
--	SsId NVARCHAR(50) NOT NULL,
--	UserId INT NOT NULL,
--	UserName NVARCHAR(50),
--	STime DATETIME DEFAULT(GETDATE())
--);
--END;

--//////2018-02-02 End

--//////2018-02-09 Start
--增加契约是否有效
--IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'N_UserContractDetail') AND XType = N'U')
--	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'N_UserContractDetail') AND name = N'Effect')
--	ALTER TABLE N_UserContractDetail ADD Effect BIT DEFAULT(0);
--GO

--更新已生效的契约Id
--UPDATE B SET B.Effect=1 FROM N_UserContract A, N_UserContractDetail B WHERE A.Id = B.UcId AND ISNULL(A.IsUsed, 0)=1;
--GO
--//////2018-02-09 End

--//////2018-02-12 Start

--增加支付方式唯一码 UCode
IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'Sys_ChargeSet') AND XType = N'U')
	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'Sys_ChargeSet') AND name = N'UCode')
	ALTER TABLE Sys_ChargeSet ADD UCode NVARCHAR(20);
GO

UPDATE Sys_ChargeSet SET UCode = ID WHERE UCode IS NULL;
UPDATE Sys_ChargeSet SET UCode = N'suibipay' WHERE UCode = N'1074';
GO

--增加随笔付支付方式
INSERT INTO Sys_SbfChannelMap(SysCode, SbfChannel, SbfCode, [DESC]) SELECT N'ZFBWAP', N'2', N'alipaywap', N'支付宝移动支付' WHERE NOT EXISTS(SELECT 1 FROM Sys_SbfChannelMap WHERE SysCode = N'ZFBWAP');
INSERT INTO Sys_SbfChannelMap(SysCode, SbfChannel, SbfCode, [DESC]) SELECT N'WXWAP', N'23', N'wxwap', N'微信移动支付' WHERE NOT EXISTS(SELECT 1 FROM Sys_SbfChannelMap WHERE SysCode = N'WXWAP');

--//////2018-02-12 End


--//////2018-02-28 Start

--增加工资发放日志表
IF NOT EXISTS(SELECT 1 FROM sysObjects WHERE xtype = N'U' AND Id = OBJECT_ID('Log_ContractOper'))
BEGIN

CREATE TABLE Log_ContractOper(
	Id int IDENTITY(1,1) PRIMARY KEY,
	UserId int NULL,
	ParentId INT NULL,
	ContractId int NULL,
	Type nvarchar(255) NULL,
	Bet DECIMAL(18,4),
	Loss  DECIMAL(18,4),
	Per decimal(18, 4) NULL,
	Money DECIMAL(18,4),
	Remark NVARCHAR(100) NULL,
	Allowed BIT DEFAULT(0), --是否可派发
	OperTime datetime NULL,
	STime datetime NULL
);
END;

--修改Act_ActiveRecord.InMoney字段类型
--IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'Act_ActiveRecord') AND XType = N'U')
--	AND EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'Act_ActiveRecord') AND name = N'InMoney')
--	ALTER TABLE Act_ActiveRecord ALTER COLUMN InMoney DECIMAL(18, 4);
--GO
--/////2018-02-28 End

--关才后台活动配置页面
UPDATE Sys_Menu SET IsUsed=1 WHERE Name='活动配置'; 


--增加支付方式唯一码 UCode
IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'Sys_ChargeSet') AND XType = N'U')
	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'Sys_ChargeSet') AND name = N'UCode')
	ALTER TABLE Sys_ChargeSet ADD UCode NVARCHAR(20);
GO

--增加网银微付支付方式
DECLARE @maxCsId INT, @maxType INT, @maxMerCode INT;
SELECT @maxCsId = MAX(id), @maxType = MAX(Type), @maxMerCode = MAX(MerCode) FROM Sys_ChargeSet;

INSERT INTO Sys_ChargeSet(Id, [Type], Name, MerName, MerCode, MerKey, MerCard, MinCharge, MaxCharge, StartTime, EndTime, Total, IsUsed, Sort, STime, UCode)
SELECT @maxCsId + 1, @maxType + 1, N'自助支付', N'自助支付', @maxMerCode + 1, N'commonpay','', 50, 
3000, '00:00:00', '23:59:59', 1000000, 0, 3, GETDATE(), N'commonpay'
WHERE NOT EXISTS(SELECT 1 FROM Sys_ChargeSet WHERE MerKey = N'commonpay');


--返点
DECLARE @maxPoint INT, @maxBonus INT, @curId INT;
SELECT @maxPoint = MAX(Point), @maxBonus = MAX(Bonus) FROM N_UserLevel;
WHILE @maxPoint < 145 
BEGIN
	SET @maxPoint = @maxPoint + 1;
	SET @maxBonus = @maxBonus + 2;
	
	INSERT INTO N_UserLevel([Point],[Bonus],[Score],[Times])
	SELECT @maxPoint, @maxBonus, 0, 10
	WHERE NOT EXISTS (SELECT 1 FROM N_UserLevel WHERE Point = @maxPoint);
	
	SELECT @curId = Id FROM N_UserLevel WHERE Point = @maxPoint;
	
	UPDATE N_UserLevel SET Title = 'VIP' + CAST(@curId AS NVARCHAR(10)), Sort = @curId WHERE Point = @maxPoint;
END

--启用移动开启猜大小
UPDATE Sys_PlayBigType SET IsOpen=0 WHERE Id=4005;

--启用台湾5分彩杀奖率
INSERT INTO Sys_LotteryCheck(Id, CheckNum, CheckPer)
SELECT A.Id, 20, 100 FROM Sys_Lottery A 
	WHERE Code = 'tw5fc' AND NOT EXISTS(SELECT 1 FROM Sys_LotteryCheck WHERE Id = A.Id);

--手动开奖补单
IF NOT EXISTS(SELECT 1 FROM sysObjects WHERE xtype = N'U' AND Id = OBJECT_ID('Sys_LotteryManualData'))
BEGIN
CREATE TABLE Sys_LotteryManualData
(
	Id INT IDENTITY Primary Key,
	[Type] [int] NULL,
	[Title] [nvarchar](50) NULL,
	[Number] [nvarchar](200) NULL,
	[NumberAll] [nvarchar](200) NULL,
	[Total] [int] NULL,
	[OpenTime] [datetime] NULL,
	State INT NOT NULL DEFAULT(0),
	Target INT NOT NULL, --1-补单, 2-预先开奖
	[STime] [datetime] NULL,
);
END;

--预设开奖菜单
DECLARE @menuId INT;
SELECT @menuId = MAX(Id) FROM Sys_Menu;

INSERT INTO Sys_Menu(Id, Pid, Name, Url, IsUsed, Sort)
SELECT @menuId + 1, 8, '预设开奖', '/admin/conList.aspx?page=LotteryManualDataList', 0, 10
WHERE NOT EXISTS (SELECT 1 FROM Sys_Menu WHERE Pid=8 AND Name = '预设开奖');

--设置权限
SELECT @menuId = Id FROM Sys_Menu WHERE Pid=8 AND Name = '预设开奖';
UPDATE Sys_Role SET Setting=ISNULL(Setting, ',') + CAST(@menuId AS NVARCHAR(100)) + ',' 
WHERE CHARINDEX(N'系统管理员', Name) > 0 AND CHARINDEX(',' + CAST(@menuId AS NVARCHAR(100)) + ',', Setting) <= 0;

--增加奖种类型
IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'Sys_Lottery') AND XType = N'U')
	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'Sys_Lottery') AND name = N'IsSys')
	ALTER TABLE Sys_Lottery ADD IsSys INT DEFAULT(0);
GO

UPDATE Sys_Lottery SET IsSys=1 
WHERE Code IN ('ny30m','hg90m','xdl90m','xjp2fc','tw5fc','flb90m','dj15',
'xjp30m','tw60m','se60m','yfpk10','yg60sc','hg90sd','se60sd','hg11x5','yg120sc','yf11x5');

--山东11选5 官方开奖
UPDATE Sys_Lottery SET IsSys = 0 WHERE Id = 2001;

--补齐山东11选5开奖时间
INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
SELECT 2001, 79, '22:05:00', 9, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=2001 AND Sn = 79);

INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
SELECT 2001, 80, '22:15:00', 9, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=2001 AND Sn = 80);

INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
SELECT 2001, 81, '22:25:00', 9, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=2001 AND Sn = 81);

INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
SELECT 2001, 82, '22:35:00', 9, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=2001 AND Sn = 82);

INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
SELECT 2001, 83, '22:45:00', 9, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=2001 AND Sn = 83);

INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
SELECT 2001, 84, '22:55:00', 9, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=2001 AND Sn = 84);

INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
SELECT 2001, 85, '23:05:00', 9, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=2001 AND Sn = 85);

INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
SELECT 2001, 86, '23:15:00', 9, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=2001 AND Sn = 86);

INSERT INTO Sys_LotteryTime(LotteryId, Sn, Time, Sort, STime)
SELECT 2001, 87, '23:25:00', 9, GETDATE() WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryTime WHERE LotteryId=2001 AND Sn = 87);

--山东11选5，开奖时间调慢30分钟
UPDATE Sys_LotteryTime SET Time = CONVERT(NVARCHAR(8), DATEADD(MI, -30, CONVERT(datetime, Time, 101)), 108) WHERE LotteryId = 2001;
