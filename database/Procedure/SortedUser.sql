

SELECT * FROM N_User WHERE ISNULL(IsDel, 0) = 0;



SELECT Id, ParentId FROM N_User WHERE ISNULL(IsDel, 0) = 0;




IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TUser'))
	DROP TABLE #TUser;

--id: 用户Id, parentId: 父Id, orderId: 排序号
CREATE TABLE #TUser(id INT, parentId INT, ordered INT IDENTITY, level INT);

DECLARE @last INT = 0, --上一次临时表记录数
		@current INT = 0, --当前临时表记录数
		@level INT =0	--执行次数

--插入用户根节点记录
SET @level = 0
INSERT INTO #TUser(id, parentId, level)
	SELECT Id, 0, @level FROM N_User WHERE ISNULL(IsDel, 0) = 0 AND ParentId = 0;
SELECT @current = count(1) FROM #TUser;

WHILE(@last != @current)
BEGIN
	SET @last = @current;
	
	--插入下一级用户
	INSERT INTO #TUser(id, parentId, level)
		SELECT U.Id, T.Id, @level + 1 FROM N_User U, #TUser T WHERE U.ParentId = T.Id AND T.level = @level;
	
	SELECT @current = count(1) FROM #TUser;
	SET @level = @level + 1
END

SELECT * FROM #TUser;

IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TUser'))
	DROP TABLE #TUser;