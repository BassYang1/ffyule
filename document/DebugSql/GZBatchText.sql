USE [Ticket]
GO

DECLARE @sdate DATETIME = '2018-02-23' --开始日期
DECLARE @edate DATETIME = '2018-02-24' --结束日期
DECLARE @parentId INT = 1961 --父级会员Id
DECLARE @gzdate DATE  --工资发放日期

IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TRecord'))
	DROP TABLE #TRecord;

IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TGzDate'))
	DROP TABLE #TGzDate;

IF @sdate > @edate
	SET @edate = @sdate

CREATE TABLE #TGzDate(Id INT IDENTITY, GzDate DATE);

SET @gzdate = @sdate
WHILE @gzdate <= @edate 
BEGIN
	INSERT INTO #TGzDate(GzDate) VALUES(@gzdate)

	SET @gzdate = DATEADD(d, 1, @gzdate)
END

--SELECT * FROM #TGzDate
	
--会员Id, 会员名称, 销量, 工资, 余额, 备注, 发放情况, 契约Id
CREATE TABLE #TRecord(Id INT IDENTITY, UserId INT, UserName NVARCHAR(100), Bet DECIMAL(18,4), InMoney DECIMAL(18,4), Money DECIMAL(18,4), Remark NVARCHAR(100), State INT, UcId INT, STime DATE);

INSERT INTO #TRecord(UserId, UserName, Money, UcId, Bet, InMoney, State, Remark, STime)
	SELECT U.Id, U.UserName, U.Money, C.Id, R.Bet, R.InMoney, CASE WHEN R.SsId IS NULL THEN 0 ELSE 1 END, R.Remark, D.GzDate
	FROM N_User U
		INNER JOIN #TGzDate D ON 1 > 0
		INNER JOIN N_UserContract C ON U.Id=C.UserId AND C.Type=2 AND C.IsUsed=1
		LEFT JOIN Act_ActiveRecord R ON R.UserId=U.Id and R.ActiveType = 'ActGongziContract' and DATEDIFF(day, R.STime, D.GzDate) = 0
	WHERE ISNULL(U.IsDel, 0) = 0 AND U.ParentId = @parentId
	ORDER BY U.Id ASC;
		

SELECT * FROM #TRecord;

DECLARE @userId INT, @ucId INT, @state INT
DECLARE @count INT, @idx INT = 1
SELECT @count = count(1) FROM #TRecord;


DECLARE @bet decimal(18,4),
		@money decimal(18,4),
		@remark NVARCHAR(100)

WHILE @idx <= @count
BEGIN
	SET @bet = 0
	SET @money = 0
	SET @remark = N''

	SELECT @userId = UserId, @ucId = UcId, @state = State FROM #TRecord WHERE Id=@idx;

	IF @state != 0
	BEGIN
		SELECT @bet=isnull(cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)),0) FROM [N_UserMoneyStatAll] a 
		WHERE dbo.f_GetUserCode(UserId) like '%,'+@userId+',%'  and DateDiff(dd, STime, @gzdate) = 0 
				
		SELECT TOP 1 @money = cast(round([Money] * @bet * 0.01,4) as numeric(10,4)) FROM N_UserContractDetail 
			WHERE UcId=@ucId and Money > 0 and @bet >= MinMoney*10000 order by Id desc
			--派发工资

		IF @money <= 0
			SET @remark = N'未满足工资契约销量额度'
		ELSE
			应领取工资' + CONVERT(Nvarchar(100),@money)

		UPDATE 
	END

	SET @idx = @idx + 1
END	
/** 按用户层级由高到低生成用户临时表 End**/

/** 2, 层级由高到底为用户派发工资 Start**/
DECLARE @error int = 0 --错误记数器
DECLARE @contractId varchar(50),--临时变量，用来保存游标值
		@userId varchar(50)		--用户Id
DECLARE @result varchar(200) --执行结果

DECLARE @logTitle NVARCHAR(100) --日志
SET @logTitle = N'发放' + CONVERT(NVARCHAR(8), @gzdate, 112) + N'工资'
	
BEGIN TRY		
	BEGIN TRAN --申明事务
		
	DECLARE @ucount INT, @idx INT
	SELECT @idx = 1
	SELECT @ucount = COUNT(1) FROM #TUser
	
	WHILE @idx <= @ucount
	BEGIN
		SELECT @userId = id FROM #TUser WHERE ordered = @idx;
		
		--获取契约
		SELECT @contractId = Id FROM [N_UserContract] WHERE Type=2 AND isUsed=1 AND UserId = @userId
			
		--执行sql操作
		IF @contractId IS NOT NULL
		BEGIN
			exec GZOperByDate @contractId, @gzdate, @result output

			--添加日志
			INSERT INTO Log_Sys(UserId, Title, Content, STime) VALUES(@userId, @logTitle, @result, GETDATE());
		END
			
		SET @contractId = NULL
		SET @idx = @idx + 1
	END
		
	COMMIT TRAN--提交事务
END TRY
BEGIN CATCH
	rollback tran--回滚事务
		
	--添加错误日志
	INSERT INTO Log_Sys(UserId, Title, Content, STime) VALUES(@userId, @logTitle, Error_Message(), GETDATE());
END CATCH
/** 层级由高到底为用户派发工资 End**/
	
--4, 删除临时表
IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TUser'))
	DROP TABLE #TUser;

