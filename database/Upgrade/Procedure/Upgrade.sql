USE Ticket
Go

--增加工资发放日志表
IF NOT EXISTS(SELECT 1 FROM sysObjects WHERE xtype = N'U' AND Id = OBJECT_ID('Log_ContractOper'))
BEGIN

CREATE TABLE Log_ContractOper(
	Id int IDENTITY(1,1) PRIMARY KEY,
	UserId int NULL,
	ParentId INT NULL,
	ContractId int NULL,
	Per decimal(18, 4) NULL,
	Type nvarchar(255) NULL,
	Bet DECIMAL(18,4),
	Loss  DECIMAL(18,4),
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
