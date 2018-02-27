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

ALTER TABLE Log_ContractOper ADD
--修改Act_ActiveRecord.InMoney字段类型
--IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'Act_ActiveRecord') AND XType = N'U')
--	AND EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'Act_ActiveRecord') AND name = N'InMoney')
--	ALTER TABLE Act_ActiveRecord ALTER COLUMN InMoney DECIMAL(18, 4);
--GO
--/////2018-02-28 End
