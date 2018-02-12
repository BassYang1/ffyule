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
	declare @parentId varchar(20),
			@userId varchar(20) 
	select @parentId=[ParentId], @userId=[UserId] from [N_UserContract] where Id = @contractId


	--判断活动是否已领取
	declare @isGet int
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
	select @count = count(*) from N_UserContractDetail with(nolock) where UcId=@contractId and @bet >= MinMoney * 10000 
	if(@count>0)
	begin
		SELECT top 1 @money = cast(round([Money] * @bet * 0.01,4) as numeric(10,4)) FROM N_UserContractDetail where UcId=@contractId and @bet >= MinMoney*10000 order by Id desc
	end
	else
	begin
		set @money=0
	end

	--派发工资
	if(@money>0)
	begin
		exec GZTranByDate @parentId, @userId, 'ActGongziContract', N'契约工资', @bet, @money, @gzdate, @result output
	end
	
	set @result = N'领取成功' + CONVERT(varchar(10),@money)
	return 1;
END