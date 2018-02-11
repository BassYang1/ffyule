
SELECT * FROM N_UserContract UC with(nolock) INNER JOIN N_UserContractDetail CD with(nolock) ON UC.Id = CD.UcId 
WHERE UC.UserId=1966 AND ISNULL(UC.IsUsed, 0) =1;

--定时任务(活动)
SELECT * FROM Act_ActiveSet;

--系统契约
SELECT * FROM Act_Day15FHSet;
SELECT * FROM Act_Day15FHSet WHERE GroupId=3;

--消费记录表
SELECT * FROM N_UserMoneyStatAll;

--统计用户Id
SELECT UserCode, * FROM N_User;