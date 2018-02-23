USE [Ticket]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

if (exists (select 1 from sys.objects where name = N'FH0115BatchByDate'))
    drop proc FH0115BatchByDate
go

/*
	结算指定月份1号到15号的工资
	@fhdate: 结算日期
*/
CREATE PROCEDURE FH0115BatchByDate
	@fhdate DateTime --工资结算日期
AS
BEGIN
	/** 1, 按用户层级由高到低生成用户临时表 Start**/
	--删除临时表
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

	WHILE @last != @current
	BEGIN
		SET @last = @current;
		
		--插入下一级用户
		INSERT INTO #TUser(id, parentId, level)
			SELECT U.Id, T.Id, @level + 1 FROM N_User U, #TUser T WHERE ISNULL(U.IsDel, 0) = 0 AND U.ParentId = T.Id AND T.level = @level;
			
		SELECT @current = count(1) FROM #TUser;
		SET @level = @level + 1
	END	
	/** 按用户层级由高到低生成用户临时表 End**/
	
	/** 2, 层级由高到底为用户派发1到15分红 Start**/
	DECLARE @error int = 0 --错误记数器
	DECLARE @contractId varchar(50),--临时变量，用来保存游标值
			@userId varchar(50)		--用户Id
	DECLARE @result varchar(200) --执行结果
	
	DECLARE @logTitle NVARCHAR(100) --日志
	SET @logTitle = N'发放' + CONVERT(NVARCHAR(8), @fhdate, 112) + N'分红'

	--申明事务
	BEGIN TRAN
	
	--申明游标 为userid
	DECLARE order_cursor CURSOR FOR SELECT Id FROM #TUser ORDER BY ordered ASC

	--打开游标
	open order_cursor
	WHILE @@FETCH_STATUS = 0 --返回被 FETCH  语句执行的最后游标的状态，而不是任何当前被连接打开的游标的状态。
	begin
		--开始循环游标变量
		FETCH NEXT FROM order_cursor INTO @userId
		
		--获取契约
		SELECT @contractId = Id FROM [N_UserContract] WHERE Type=1 AND isUsed=1 AND UserId = @userId
		
		--执行sql操作
		IF @contractId IS NOT NULL
		BEGIN
			exec FH0115OperByDate @contractId, @fhdate, @result output

			--添加日志
			INSERT INTO Log_Sys(UserId, Title, Content, STime) VALUES(@userId, @logTitle, @result, GETDATE());
		END
		
		--记录每次运行sql后 是否正确  0正确
		SET @error=@error + @@error 
		SET @contractId = NULL
	end
	
	--没有错误 统一提交事务
	if @error=0
	begin
		commit tran--提交
	end
	else
	begin
		rollback tran--回滚
	end
		
	CLOSE order_cursor--关闭游标
	DEALLOCATE order_cursor--释放游标
	/** 层级由高到底为用户派发1到15分红 End**/
	
	--3, 删除临时表
	IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TUser'))
		DROP TABLE #TUser;
END
