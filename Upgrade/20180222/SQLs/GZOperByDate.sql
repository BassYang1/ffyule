USE [Ticket]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

if (exists (select 1 from sys.objects where name = N'GZOperByDate'))
    drop proc GZOperByDate
go

/*
	结算指定日期的工资
	@gzdate: 结算日期
*/
CREATE PROCEDURE GZOperByDate
	@contractId varchar(200), --契约Id
	@gzdate DateTime, --工资结算日期
	@result varchar(200) output
as
BEGIN
	declare @parentId varchar(20), --父用户ID
			@userId varchar(20)		--用户Id
	select @parentId=[ParentId], @userId=[UserId] from [N_UserContract] where Id = @contractId
	
	declare @isGet int

	--判断父级是否发放工资, 并且父级账号不是平台管理账户，如果未发放，则不允许发放工资
	select @isGet=count(*) from Act_ActiveRecord where UserId=@parentId and ActiveType = 'ActGongziContract' and DATEDIFF(day, STime, @gzdate) = 0
	if(@isGet<=0 AND EXISTS(SELECT 1 FROM N_User WHERE Id=@parentId AND ISNULL(parentId, 0) > 0))
	begin
		set @result='父级会员未领取工资'
		return;
	end

	--判断活动是否已领取
	select @isGet=count(*) from Act_ActiveRecord where UserId=@userId and ActiveType = 'ActGongziContract' and DATEDIFF(day, STime, @gzdate) = 0
	
	if(@isGet>0)
	begin
		set @result='今天已领取！'
		return;
	end

	declare @bet decimal(18,4),
			@money decimal(18,4)

	SELECT @bet=isnull(cast(round((isnull(Sum(bet),0)-isnull(Sum(Cancellation),0)),4) as numeric(20,4)),0) FROM [N_UserMoneyStatAll] a 
	where dbo.f_GetUserCode(UserId) like '%,'+@userId+',%'  and DateDiff(dd, STime, @gzdate) = 0 


	declare @count int
	select @count = count(*) from N_UserContractDetail with(nolock) where UcId=@contractId and MinMoney > 0 and [Money] > 0 and @bet >= MinMoney * 10000
	if(@count>0)
	begin
		SELECT top 1 @money = cast(round([Money] * @bet * 0.01,4) as numeric(10,4)) FROM N_UserContractDetail 
			where UcId=@contractId and MinMoney > 0 and Money > 0 and @bet >= MinMoney*10000 order by Id desc
	end
	else
	begin
		set @money=0
	end

	--派发工资
	if(@money>0)
	begin
		exec GZTranByDate @parentId, @userId, 'ActGongziContract', N'契约工资', @bet, @money, @gzdate, @result output

		set @result = N'领取成功' + CONVERT(varchar(10),@money)
	end
	else
		set @result = N'未满足工资契约销量额度'
	
	return 1;
END