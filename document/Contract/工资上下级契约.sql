USE [Ticket]
GO

/*
--会员信息
SELECT * FROM N_User WHERE Id IN (1961, 1966, 1988, 2317, 2318, 2322);
SELECT * FROM N_User WHERE ParentID IN (2318);

--工资账变记录
--Zhang1212 > lin988 >  hao1688 > gzj888 > zxr588
SELECT B.UserName, A.* FROM N_UserMoneyLog A, N_User B WHERE Code= 13 
and UserId IN (1961, 1966, 2317, 2318, 2322) 
and CONVERT(NVARCHAR(10), STime, 120) = '2018-02-20'
and A.UserId=B.Id;

SELECT * FROM N_UserMoneyLog WHERE Code= 13 
and UserId IN (1961) 
and CONVERT(NVARCHAR(10), STime, 120) = '2018-02-20';

SELECT * FROM N_UserMoneyLog WHERE Code= 13 
and UserId IN (1966) 
and CONVERT(NVARCHAR(10), STime, 120) = '2018-02-20';

SELECT A.Id, A.ParentId, A.UserName, B.Id, C.MinMoney, C.Money FROM N_User A, N_UserContract B, N_UserContractDetail C
WHERE A.Id=B.UserId AND B.Id=C.UcId
AND A.Id IN (1961, 1966, 2317, 2318, 2322)
ORDER BY A.Id ASC;


*/

DECLARE @gzdate DateTime
DECLARE @contractId varchar(50),--临时变量，用来保存游标值
		@userId varchar(50),		--用户Id
		@parentId varchar(20)   --父用户ID
	
SET @gzdate = CAST('2018-02-19' AS DATE)

--parent id 2318, child id 2322
--parent id 2317, child id 2318

--删除临时表
IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TUser'))
	DROP TABLE #TUser;
	
--id: 用户Id, parentId: 父Id, orderId: 排序号
CREATE TABLE #TUser(tmpId INT IDENTITY, id INT);
INSERT INTO #TUser(Id) VALUES(2322);

--SELECT N'临时会员信息', * FROM #TUser;

DECLARE @id INT, @num INT, @idx INT = 1
SELECT @num = COUNT(1) FROM #TUser

WHILE @idx <= @num
BEGIN
	SELECT @userId = id FROM #TUser WHERE tmpId = @idx;
	SET @idx = @idx + 1

	--SELECT N'会员信息', * FROM N_User WHERE Id = @userId;

	--获取契约
	SELECT @contractId = Id FROM [N_UserContract] WHERE Type=2 AND isUsed=1 AND UserId = @userId
	
	
	SELECT N'工资契约', * FROM [N_UserContract] WHERE Type=2 AND isUsed=1 AND UserId = @userId


	--执行sql操作
	IF @contractId IS NOT NULL
	BEGIN
		--契约信息
		select @parentId=[ParentId], @userId=[UserId] from [N_UserContract] where Id = @contractId

		--判断活动是否已领取
		--declare @isGet int
		--select @isGet=count(*) from Act_ActiveRecord where UserId=@userId and ActiveType = 'ActGongziContract' and DATEDIFF(day, STime, @gzdate) = 0
	
		--if(@isGet>0)
		--begin
		--	return;
		--end

		declare @bet decimal(18,4),
				@money decimal(18,4)

		SELECT @bet=isnull(cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)),0) FROM [N_UserMoneyStatAll] a 
		where dbo.f_GetUserCode(UserId) like '%,'+@userId+',%'  and DateDiff(dd, STime, @gzdate) = 0 

		SELECT @bet

		declare @count int
		select @count = count(*) from N_UserContractDetail with(nolock) where UcId=@contractId and MinMoney > 0 
		and [Money] > 0 and @bet >= MinMoney * 10000

		SELECT N'全部工资契约详细', @bet AS Bet, MinMoney * 10000 AS MinMoney1W, * FROM N_UserContractDetail 
			where UcId=@contractId and MinMoney > 0 order by Id desc;

		if(@count>0)
		begin
		
			SELECT top 1 @money = cast(round([Money] * @bet * 0.01,4) as numeric(10,4)) FROM N_UserContractDetail 
				where UcId=@contractId and MinMoney > 0 and Money > 0 and @bet >= MinMoney*10000 order by Id desc;

				
			SELECT top 1 N'工资契约详细', cast(round([Money] * @bet * 0.01,4) as numeric(10,4)) FROM N_UserContractDetail 
				where UcId=@contractId and MinMoney > 0 and Money > 0 and @bet >= MinMoney*10000 order by Id desc;
		end
		else
		begin
			set @money=0
		end

		SELECT @userId, @bet, @money

		--派发工资
		if(@money>0)
		begin
			declare @ActiveType NVARCHAR(20) = N'ActGongziContract', 
					@ActiveName NVARCHAR(20)= N'契约工资'

			declare @ssId varchar(50),
					@moneyAfter decimal(18,4),
					@userMoney decimal(18,4)
			
			select @userMoney=Money from N_User where Id=@parentId;
			set @moneyAfter=convert(decimal(18,4),@userMoney) - convert(decimal(18,4),@money)

			--if(@moneyAfter>=0)
			--begin
				--update [N_User] set Money=convert(decimal(18,4),Money) - convert(decimal(18,4),@money) where Id=@parentId
				--select @SsId='A_'+SUBSTRING(replace(newid(), '-', ''),0,19)
				----插入账变记录
				--insert into N_UserMoneyLog (SsId,UserId,LotteryId,PlayId,SysId,MoneyChange,MoneyAgo,moneyAfter,STime,IsOk,Code,IsSoft,Remark,STime2,Md5Code) 
				--values(@ssId, @parentId, 0, 0, 0, -@money, @UserMoney, @moneyAfter, @gzdate, 1, 100, 2, @ActiveName, GETDATE(), substring(sys.fn_sqlvarbasetostr(HashBytes('MD5',@ssId+''+Convert(varchar(10),'9')+''+Convert(varchar(10),@parentId))),3,32))
		
				--if exists (select Id from N_UserMoneyStatAll where UserId=@parentId and datediff(d, STime, @gzdate)=0)
				--begin
				--	Update N_UserMoneyStatAll set Give=Give-@money where  UserId=@parentId and datediff(d, STime, @gzdate)=0
				--end
				--else
				--begin
				--	Insert into N_UserMoneyStatAll(UserId,Give,STime) values (@parentId, -@money, @gzdate)
				--end
			--end

			declare @SsId2 varchar(50),@MoneyAfter2 decimal(18,4),@UserMoney2 decimal(18,4)
			select @UserMoney2=Money from N_User where Id=@UserId;
			set @MoneyAfter2=convert(decimal(18,4),@UserMoney2)+convert(decimal(18,4),@money)

			--if(@MoneyAfter2>=0)
			--begin
				--update [N_User] set Money=convert(decimal(18,4),Money)+convert(decimal(18,4),@money) where Id=@UserId
				--select @SsId2='A_'+SUBSTRING(replace(newid(), '-', ''),0,19)
				--插入账变记录
				--insert into N_UserMoneyLog (SsId,UserId,LotteryId,PlayId,SysId,MoneyChange,MoneyAgo,MoneyAfter,STime,IsOk,Code,IsSoft,Remark,STime2,Md5Code) 
				--values(@SsId2,@UserId,0,0,0,@money,@UserMoney2,@MoneyAfter2,@gzdate,1,100,2,@ActiveName,GETDATE(),substring(sys.fn_sqlvarbasetostr(HashBytes('MD5',@SsId2+''+Convert(varchar(10),'9')+''+Convert(varchar(10),@UserId))),3,32))

				--if exists (select Id from N_UserMoneyStatAll where UserId=@UserId and datediff(d,STime,@gzdate)=0)
				--begin
				--	Update N_UserMoneyStatAll set Give=Give+@money where  UserId=@UserId and datediff(d,STime,@gzdate)=0
				--end
				--else
				--begin
				--	Insert into N_UserMoneyStatAll(UserId,Give,STime) values (@UserId,@money,@gzdate)
				--end

				--插入活动记录
				--INSERT INTO [Act_ActiveRecord](SsId,[UserId],[ActiveType],[ActiveName],[Bet],[InMoney],[STime],[CheckIp],[CheckMachine],[FromUserId],[Remark])
				--VALUES(@SsId2, @userId, @ActiveType, @ActiveName, @bet, @money, @gzdate, N'系统自动派发', N'系统自动派发', @parentId, @ActiveName)
			--end
		end
	END
END
	
IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TUser'))
	DROP TABLE #TUser;