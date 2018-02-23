USE [Ticket]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if (exists (select 1 from sys.objects where name = N'FH1631OperByDate'))
    drop proc FH1631OperByDate
go

/*
	结算指定月份16号到31号的工资
	@fhdate: 结算日期
*/

CREATE PROCEDURE FH1631OperByDate
@contractId varchar(200), --契约Id
@fhdate DateTime, --工资结算日期
@result varchar(200) output
as
BEGIN
	declare @parentId varchar(20),
			@userId varchar(20) 
	select @parentId=[ParentId], @userId=[UserId] from [N_UserContract] where Id = @contractId
	
	declare @IsGet int

	--判断父级是否发放分红，并且父级账号不是平台管理账户，如果未发放，则不允许发放分红
	select @isGet=count(*) from Act_AgentFHRecord where UserId=@parentId and DATEDIFF(day, STime, @fhdate)=0
	if(@isGet<=0 AND EXISTS(SELECT 1 FROM N_User WHERE Id=@parentId AND ISNULL(parentId, 0) > 0))
	begin
		set @result='父级会员未领取分红'
		return;
	end

	--判断活动是否已领取
	select @IsGet=count(*) from Act_AgentFHRecord where UserId=@UserId and DATEDIFF(day,STime,@fhdate)=0
	
	if(@IsGet>0)
	begin
		set @result='今天已领取！'
		return;
	end

	declare @money decimal(18,4),
			@bet decimal(18,4),
			@loss decimal(18,4),
			@Per decimal(18,4),
			@GroupName varchar(200)

	--查询25-09消费量
	SELECT @bet=(isnull(sum(Bet),0)-isnull(sum(Cancellation),0)) FROM [N_UserMoneyStatAll] with(nolock)
	where (STime>=Convert(varchar(7),@fhdate,120)+'-16 00:00:00' and STime<Convert(varchar(7),DateAdd(mm,1,@fhdate),120)+'-01 00:00:00')
	and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code(@UserId)+'%'

	--查询10-24亏损量
	SELECT @loss=isnull(sum(Bet),0)-(isnull(sum(Win),0)+isnull(sum(Give),0)+isnull(sum(Change),0)+isnull(sum(Cancellation),0)+isnull(sum(Point),0))
	FROM [N_UserMoneyStatAll] with(nolock)
	where (STime>=Convert(varchar(7),@fhdate,120)+'-16 00:00:00' and STime<Convert(varchar(7),DateAdd(mm,1,@fhdate),120)+'-01 00:00:00')
	and dbo.f_GetUserCode(UserId) like '%'+dbo.f_User8Code(@UserId)+'%'

	if(@loss<0)
	begin
		set @result='您未亏损！'
		return;
	end
	--判断消费是否具备条件
	declare @IsTrue int
	select @IsTrue=count(*) from N_UserContractDetail with(nolock) where UcId=@contractId and MinMoney > 0 and [Money] > 0 and @bet>=MinMoney*150000 
	if(@IsTrue<1)
	begin
		set @Per=0
	end
	else
	begin
		--取出对应的工资百分比
		select top 1 @Per=[Money] from N_UserContractDetail with(nolock) where UcId=@contractId and MinMoney > 0 and [Money] > 0 and @bet>=MinMoney*150000 order by MinMoney desc
	end
	
	--计算得到的金额
	set @money=convert(decimal(18,4),@Per)*convert(decimal(18,4),@loss)/100
	set @result=@money
	
	
	--派发工资
	if(@money>0)
	begin
		DECLARE @startTime DATETIME, @endTime DATETIME
		SET @startTime = CAST((Convert(varchar(7),@fhdate,120)+'-16 00:00:00') AS DATETIME)
		SET @endTime = DATEADD(DAY, -1, CAST((Convert(varchar(7),DateAdd(mm,1,@fhdate),120)+'-01 00:00:00') AS DATETIME))

		INSERT INTO [Act_AgentFHRecord]([UserId],[AgentId],[StartTime],[EndTime],[Bet],[Total],[Per],[InMoney],[STime],[Remark])
			VALUES(@userId,99,@startTime,@endTime,@bet,@loss,@per,@money,GETDATE(),'契约分红')
		
		exec FHTranByDate @parentId, @userId,'ActFenHong','契约分红',@money,@fhdate,@result output

		set @result = N'领取成功' + CONVERT(varchar(10),@money)
	end
	else
		set @result = N'未满足分红契约亏损额度'
			

	return 1;
END