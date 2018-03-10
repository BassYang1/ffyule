
USE Ticket
GO

IF (EXISTS (SELECT 1 FROM sys.objects WHERE name = N'fCompleteRecharge'))
    DROP PROC fCompleteRecharge
GO

/*
	用户完成充值
	@orderId: 订单Id
*/
CREATE PROCEDURE fCompleteRecharge    
	@orderId NVARCHAR(50), --订单Id
	@wfpNo NVARCHAR(50), --微付订单号
	@bankNo NVARCHAR(50), --银行流水号
	@result NVARCHAR(100) OUTPUT
AS
BEGIN
	SET NOCOUNT ON;
	
	BEGIN TRY
		BEGIN TRAN
		DECLARE @userId INT, 
				@state INT, 
				@money DECIMAL(18,4),
				@stime DATETIME --充值日期
        
		SET @result = N''

		SELECT 
			@userId = UserId, 
			@state = State, 
			@money = InMoney,
			@stime = STime 
		FROM N_UserCharge WHERE SsId=@orderId;

		IF @userId IS NULL 
		BEGIN
			SET @result = N'无效的充值订单'
			COMMIT TRAN
			RETURN 0
		END

		IF @state = 1
		BEGIN
			SET @result = N'支付已完成'
			COMMIT TRAN
			RETURN 1
		END

		--支付成功
		UPDATE N_UserCharge SET State=1, DzMoney=@money WHERE SsId=@orderId;

		--更新会员余额
		UPDATE N_USER SET Money = ISNULL(Money, 0) + @money WHERE Id = @userId;

		--更新账户汇总信息
		IF EXISTS (SELECT Id FROM N_UserMoneyStatAll WHERE UserId=@userId and DATEDIFF(d, STime, @stime)=0)
		BEGIN
			UPDATE N_UserMoneyStatAll SET Charge = ISNULL(Charge, 0) + @money WHERE  UserId=@userId and DATEDIFF(d, STime, @stime)=0
		END
		ELSE
		BEGIN
			INSERT INTO N_UserMoneyStatAll(UserId, Charge, STime) VALUES (@userId, @money, @stime)
		END

		--微付订单与平台订单映射
		INSERT INTO N_UserWfpOrder(UserId, SsId, WfpNo, BankNo) SELECT @userId, @orderId, @wfpNo, @bankNo WHERE NOT EXISTS (SELECT 1 FROM N_UserWfpOrder WHERE WfpNo=@wfpNo AND SsId=@orderId)
		COMMIT TRAN
		
		SET @result = N'支付成功'
		RETURN 1
    END TRY
    BEGIN CATCH
        IF @@TRANCOUNT > 0
        BEGIN
            ROLLBACK TRAN
        END 
		
		SET @result = error_message()
		RETURN 0
    END CATCH
END
