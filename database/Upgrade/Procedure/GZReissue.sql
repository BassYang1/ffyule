USE [Ticket]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

if (exists (select 1 from sys.objects where name = N'GZReissue'))
    drop proc GZReissue
go

--工资补发
CREATE PROCEDURE GZReissue
@logId int, --派发日志Id
@result varchar(200) output
as
BEGIN
	BEGIN TRY
		DECLARE @userId INT, @parentId INT, @gzdate DATE, @money DECIMAL(18,4), @bet DECIMAL(18,4)
		SELECT @userId=UserId, @parentId=ParentId, @gzdate=OperTime, @money=Money, @bet=Bet FROM Log_ContractOper WHERE id=@logId and allowed=1
                            
		IF @userId IS NULL OR @money IS NULL OR @money <= 0
		BEGIN
			SET @result = N'无效数据'
			RETURN
		END
                            
		DECLARE @isGet INT
		select @isGet=count(*) from Act_ActiveRecord where UserId=@userId and ActiveType = 'ActGongziContract' and DATEDIFF(day, STime, @gzdate) = 0
    
		if(@isGet>0)
		begin
			SET @result = N'今天已领取！'
			return;
		end
		          
		BEGIN TRAN
		exec GZTranByDate @parentId, @userId, 'ActGongziContract', N'契约工资', @bet, @money, @gzdate, N'工资补发', @result output

		UPDATE Log_ContractOper SET Allowed=0, Remark=N'工资补发成功', STime=GETDATE() WHERE id=@logId and allowed=1
	    COMMIT TRAN  

		SET @result = N'' 
	END TRY
	BEGIN CATCH
		ROLLBACK TRAN
		SET @result = error_message()
	END CATCH
END