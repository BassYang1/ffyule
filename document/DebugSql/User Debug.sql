--≤‚ ‘’À∫≈
SELECT * FROM N_USER WHERE UserName IN ('yzz1230','taiyi', 'lululu', 'zxr588', 'lin988', 'Zhang1212', 'hao1699', 'ceshi008');
SELECT * FROM N_USER WHERE UserGroup IN (0);
SELECT * FROM N_USER WHERE ParentId IN (0);
SELECT * FROM N_USER WHERE Id=1963;

SELECT * FROM N_UserGroup;
SELECT * FROM N_User WHERE UserName IN ('test001', 'Zhang1212');

/*
--123456
UPDATE N_USER SET Password='cdd7e692858094371afa6affc351d71030aa93a45a1ffe132b3b97282b6a49b8' 
WHERE UserName IN ('Zhang1212','hao1699','azf223366', 'admin', 'taiyi', 'lululu', 'l11111111', 'qqq111', 'ceshi008');

UPDATE N_User SET UserGroup=0 WHERE UserName IN ('test001');

*/
