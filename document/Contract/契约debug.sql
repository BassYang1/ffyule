--测试账号
SELECT * FROM N_USER WHERE UserName IN ('taiyi', 'lululu', 'admin', 'lin988', 'Zhang1212');
SELECT * FROM N_USER WHERE UserGroup IN (0);
SELECT * FROM N_USER WHERE ParentId IN (0);
--UPDATE N_USER SET Password='c1601dc1bf00f0a2c9d5f9a8c34eb27f97d0a8b7fcdba0f7a038ab51cf619bad' WHERE UserName IN ('azf223366', 'admin', 'taiyi', 'lululu', 'l11111111', 'qqq111');

--UPDATE N_USER SET UserGroup=1 WHERE Id = '1961';

SELECT Id, UserCode, * FROM N_User WHERE parentId = 1966;
SELECT UserCode, * FROM N_User WHERE ParentId in (
SELECT Id FROM N_User WHERE parentId = 1966);

--会员
SELECT A.Id, A.ParentId, A.UserName, C.Name,  B.Id, B.UserName, D.Name FROM N_USER A, N_USER B, N_UserGroup C, N_UserGroup D 
WHERE A.ParentId = B.Id AND A.UserGroup = C.Id AND B.UserGroup = D.Id;

SELECT * FROM N_UserGroup;
SELECT * FROM N_UserLevel;

select * from V_User where Id=1000;

--契约
--P:admin
--UPDATE N_User SET Password = 'c1601dc1bf00f0a2c9d5f9a8c34eb27f97d0a8b7fcdba0f7a038ab51cf619bad' WHERE Id =1771;
SELECT top 1 Id from N_UserContract where UserId=1000;
SELECT top 1 UserGroup from N_User where Id=1000;
SELECT UserCode, *  from N_User where Id IN (1000, 1771, 1770);
SELECT * from N_User where UserGroup IN (2, 3, 4);
SELECT * from N_User A where A.UserGroup IN (1) AND EXISTS(SELECT 1 FROM N_USER WHERE ParentId=A.ID); --代理
SELECT * FROM N_UserContract;
SELECT * FROM N_UserContractDetail;
SELECT * FROM Act_DayGzSet;  --系统级别的契约
SELECT MAX(money) FROM N_UserContract A 
                    INNER JOIN N_UserContractDetail B ON A.Id = B.UcId WHERE A.Type=2 AND UserId=1844;

SELECT ContractGZ, ContractFH, * FROM [V_User] WHERE Id = 1771;

SELECT * FROM V_User;
--获取会员上级
SELECT TOP 12 1771 as CurUserId,* From V_User with(nolock)  Where parentId=10 and UserGroup <3 ORDER BY Id ASC
SELECT UserCode, * FROM N_User WHERE ParentId IS NOT NULL;

--契约详细
SELECT * FROM Act_DayGzSet; --系统默认契约
SELECT top 1 UserGroup, * from N_User where Id=1883;
SELECT 0 as groupId,[Type],[ParentId],[UserId],[IsUsed],[STime],b.* FROM [N_UserContract] a left join [N_UserContractDetail] b on a.Id=b.UcId where Type=2 and UserId= 1883;
SELECT * FROM [N_UserContract] WHERE Id = '15';
UPDATE [N_UserContract] SET IsUsed = 0 WHERE Id = 15;

SELECT *  FROM [N_UserContract] order by stime desc;

--分配记录
SELECT * FROM Act_ActiveRecord;
SELECT * FROM V_AgentFHRecord ORDER BY STime DESC;

SELECT top 1 UserGroup from N_User where Id=1770

SELECT * FROM Act_AgentFHRecord;


---契约存储过程分析

/*

DELETE FROM Act_AgentFHRecord;
DELETE FROM Log_Sys WHERE Id > 254;
DELETE FROM Act_ActiveRecord WHERE ActiveType='ActGongziContract';

exec FH0115BatchByDate '2018-01-31';

*/

SELECT * FROM Act_ActiveRecord WHERE ActiveType='ActGongziContract';
SELECT * FROM Act_AgentFHRecord;


SELECT * FROM N_User WHERE Id IN (1961, 1966, 1988, 2317, 2318, 2322);
SELECT * FROM N_User WHERE Id IN (1963);
SELECT * FROM Log_Sys WHERE UserId != 0 ORDER BY STime DESC;
SELECT * FROM Log_Sys WHERE UserId != 0 AND UserID IN (1961, 1966, 1988, 2317, 2318, 2322) ORDER BY STime DESC;

