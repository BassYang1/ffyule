--测试账号
SELECT * FROM N_USER WHERE UserName IN ('yzz1230','taiyi', 'lululu', 'zxr588', 'lin988', 'Zhang1212', 'hao1699', 'ceshi008');
SELECT * FROM N_USER WHERE UserGroup IN (0);
SELECT * FROM N_USER WHERE ParentId IN (0);
SELECT * FROM N_USER WHERE Id=1963;

/*

UPDATE N_USER SET Password='cdd7e692858094371afa6affc351d71030aa93a45a1ffe132b3b97282b6a49b8' 
WHERE UserName IN ('yzz1230','hao1699','azf223366', 'admin', 'taiyi', 'lululu', 'l11111111', 'qqq111', 'ceshi008');

*/



--SELECT * INTO N_User_20180305 FROM N_USER;
--SELECT * INTO N_UserGroup_20180305 FROM N_UserGroup;

--DELETE FROM N_UserGroup WHERE Id IN (2, 3, 4, 5);
--UPDATE N_User SET UserGroup = 1 WHERE UserGroup IN (2, 3, 4, 5);

SELECT * FROM N_UserGroup;
SELECT * FROM N_USER;

--会员注册人数限制
SELECT * FROM N_UserGroupQuota;
