
--会员账户余额
SELECT Money, * FROM N_User WHERE Money < 0;
SELECT Money, * FROM N_User WHERE UserName = 'zxr588';

SELECT * FROM N_UserMoneyLog WHERE Remark LIKE '工资' ORDER BY STime DESC;
SELECT * FROM N_UserMoneyLog WHERE SSID = 'A_F77209EA3D804E0C8D';
SELECT * FROM N_UserMoneyLog WHERE Code= 13 and Userid = 2318;

--账变记录
--gzj888
SELECT * FROM V_History WHERE SSID = 'A_F77209EA3D804E0C8D';
SELECT * FROM N_UserBet;

SELECT TOP 20 [sort],
isnull(sum(Charge),0) as Charge,
isnull(sum(getcash),0) as getcash, 
isnull(sum(bet),0) as bet ,
isnull(sum(win),0) as win,
isnull(sum(Point),0) as Point,
isnull(sum(Give),0) as Give,
isnull(sum(other),0) as other,  
isnull(sum(-total),0) as total,
isnull(sum(moneytotal),0) as moneytotal 
From V_UserMoneyStatAllUserTotal with(nolock)  
Where dbo.f_GetUserCode(userId) like '%,2322,%' 
and sort=6 
group by sort  ORDER BY sort ASC