USE [Ticket]
GO

/****** Object:  View [dbo].[V_User]    Script Date: 2018/2/4 12:08:48 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO



--/****** Script for SelectTopNRows command from SSMS  ******/
ALTER view [dbo].[V_User]
as 
SELECT [Id]
,[ParentId]
,case ParentId when 0 then '--' else (select top 1 userName from N_User where Id=a.ParentId) end as ParentName
,[UserCode]
,[UserGroup]
,(select name from N_UserGroup where Id=a.UserGroup) as UserGroupName
,(select Bonus from N_UserLevel where Point=a.Point) as UserLevel
,Point as UPoint
,Convert(varchar(10),cast(round([Point]/10.0,2) as numeric(5,2)))+'%' as [Point]
,[UserName]
,[Password]
,[PayPass]
,[Money]
,[Score]
,(select count(*) from N_User where parentid=a.id and iD<>a.Id and IsDel=0) as [ChildNum]
,case when datediff(minute,ontime ,getdate())<5 then '1' else '0' end [IsOnline]
,[IsGetCash]
,[IsBet]
,[IsTranAcc]
,[EnableSeason]
,[IsEnable]
,[IsDel]
,[IsJoin]
,[SessionId]
,RegTime
,[UpdateTime]
,[LastTime]
,Convert(varchar(16),OnTime,120) as OnTime
,[GetMoneyTime]
,[IsJoinTime]
,[STime8]
,[STime9]
,(select count(*) from N_UserBank where UserId=a.Id) as Bank
,AgentId
,TrueName
--,case when isnull((select isnull(sum(bet),0) from [N_UserMoneyStatAll] where userId=a.Id and Convert(varchar(10),STime,120)=Convert(varchar(10),getdate(),120)),0)>0 then Convert(int,a.Id)-1000
--else a.Id end as sort
,0 as sort
,[Parent]
,[Question]
,[Answer]
,[IP]
,[LoginNum]      
,[GetMoneyNum]
,Mobile
,Email
--是否签订工资契约
,(CASE WHEN ISNULL((SELECT DISTINCT IsUsed FROM N_UserContract WHERE UserId = a.Id AND Type = 2), 0) = 1 THEN 1 ELSE 0 END) ContractGZ
--是否签订分红契约
,(CASE WHEN ISNULL((SELECT DISTINCT IsUsed FROM N_UserContract WHERE UserId = a.Id AND Type = 1), 0) = 1 THEN 1 ELSE 0 END) ContractFH
FROM [N_User] a



GO


