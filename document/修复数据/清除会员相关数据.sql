
SELECT * FROM N_User WHERE UserName='xxp12345';


--会员银行卡
SELECT * FROM N_UserBank WHERE UserId='2370';
--添加限行卡日志
SELECT * FROM N_UserBankLog WHERE UserId='2370';
--会员投注记录
SELECT * FROM N_UserBet WHERE UserId='2370';
--会员充值记录
SELECT * FROM N_UserCharge WHERE UserId='2370';
--会员充值日志
SELECT * FROM N_UserChargeLog WHERE UserId='2370';
--会员契约
SELECT * FROM N_UserContract WHERE UserId='2370';
SELECT * FROM N_UserContractDetail WHERE UcId IN (SELECT Id FROM N_UserContract WHERE UserId='2370');
--会员邮件记录
SELECT * FROM N_UserEmail WHERE SendId='2370' OR ReceiveId='2370';
--会员提现记录
SELECT * FROM N_UserGetCash WHERE UserId='2370';
--会员提现日志
SELECT * FROM N_UserGetCashHistory WHERE UserId='2370';
--会员通知消息
SELECT * FROM N_UserMessage WHERE UserId='2370';
--会员账变记录
SELECT * FROM N_UserMoneyLog WHERE UserId='2370';
SELECT * FROM N_UserMoneyLog WHERE UserId=1966 AND Code=4 AND Remark = 'xxp12345 游戏返点';

--会员每日账户汇总
SELECT * FROM N_UserMoneyStatAll WHERE UserId='2370';
--会员玩儿法配置
SELECT * FROM N_UserPlaySetting WHERE UserId='2370';
SELECT * FROM N_UserQuota WHERE UserId='2370';
SELECT * FROM N_UserQuotas WHERE UserId='2370';
SELECT * FROM N_UserRegLink WHERE UserId='2370'
SELECT * FROM N_UserZhBet WHERE UserId='2370';
SELECT * FROM Log_AdminOper WHERE UserId='2370';
SELECT * FROM Log_ContractOper WHERE UserId='2370';
SELECT * FROM Log_Exception WHERE UserId='2370';
SELECT * FROM Log_Point WHERE UserId='2370';
SELECT * FROM Log_Sys WHERE UserId='2370';
SELECT * FROM Log_UserLogin WHERE UserId='2370';
SELECT * FROM Act_UserFHDetail WHERE UserId='2370';
SELECT * FROM Act_ActiveRecord WHERE UserId='2370';
SELECT * FROM Act_AgentFHRecord WHERE UserId='2370';
SELECT * FROM Act_BetRecond WHERE UserId='2370';
SELECT * FROM Pay_temp WHERE UserId='2370';
SELECT * FROM Pay_temp WHERE UserId='2370';
