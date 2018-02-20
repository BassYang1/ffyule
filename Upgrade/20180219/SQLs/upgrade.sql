USE Ticket
GO

--增加支付方式唯一码 UCode
IF EXISTS(SElECT 1 FROM dbo.SYSOBJECTS WHERE Id = OBJECT_ID(N'Sys_Lottery') AND XType = N'U')
	AND NOT EXISTS (SELECT 1 FROM sys.columns WHERE object_id = OBJECT_ID(N'Sys_Lottery') AND name = N'ApiUrl')
	ALTER TABLE Sys_Lottery ADD ApiUrl NVARCHAR(100);
GO

--腾迅分分彩 qqffc
UPDATE Sys_Lottery SET ApiUrl=N'http://www.b1cp.com/api?p=json&t=txffc&limit=1&token=00fb782bad8e5241' WHERE Code = 'qqffc';

--重庆时时彩 cqssc
UPDATE Sys_Lottery SET ApiUrl=N'http://api.b1cp.com/api?p=json&t=cqssc&limit=1&token=00fb782bad8e5241' WHERE Code = 'cqssc';

--新疆时时彩 xjssc
UPDATE Sys_Lottery SET ApiUrl=N'http://api.b1cp.com/api?p=json&t=xjssc&limit=1&token=00fb782bad8e5241' WHERE Code = 'xjssc';

--广东11选5 gd11x5
UPDATE Sys_Lottery SET ApiUrl=N'http://api.b1cp.com/t?p=json&t=gd115&token=CECD3BBE17680C67' WHERE Code = 'gd11x5';

--上海11选5 sh11x5
UPDATE Sys_Lottery SET ApiUrl=N'http://api.b1cp.com/t?p=json&t=sh115&token=38A390F17B004619' WHERE Code = 'sh11x5';

--江西11选5 jx11x5
UPDATE Sys_Lottery SET ApiUrl=N'http://api.b1cp.com/t?p=json&t=jx115&token=28A92EAACA64DD08' WHERE Code = 'jx11x5';

--福彩3D 3d
UPDATE Sys_Lottery SET ApiUrl=N'http://api.b1cp.com/t?p=json&t=fc3d&token=D91E74A076A94253' WHERE Code = '3d';

--体彩 排列3
UPDATE Sys_Lottery SET ApiUrl=N'http://api.b1cp.com/t?p=json&t=pl3&token=3880B1212E4A22EA' WHERE Code = 'p3';

--上海时时乐 ssl
UPDATE Sys_Lottery SET ApiUrl=N'http://api.b1cp.com/t?p=json&t=shssl&token=89598FB138DC2AFD' WHERE Code = 'ssl';
