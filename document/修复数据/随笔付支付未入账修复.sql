Use Ticket
GO

/*

--验证

SELECT * FROM N_UserCharge WHERE SSid='C_5014673988269306208';

--UPDATE N_UserMoneyStatAll SET Charge = 0 WHERE UserId=2322 and DATEDIFF(d, STime, '2018-02-20 09:43:22.000')=0;
--UPDATE N_UserMoneyStatAll SET Charge = 0 WHERE Id IN (626, 622);
SELECT * FROM N_UserMoneyStatAll WHERE UserId=2322 and DATEDIFF(d, STime, '2018-02-20 09:43:22.000')=0;
SELECT * FROM N_UserMoneyStatAll WHERE UserId=2322 and DATEDIFF(d, STime, '2018-02-19 20:47:35.000')=0;
SELECT * FROM N_UserMoneyStatAll WHERE UserId=1937 ORDER BY STime DESC;

*/

IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TOrder'))
		DROP TABLE #TOrder;
	
--id: 自增Id, ssid: 订单号
CREATE TABLE #TOrder(id INT IDENTITY, ssid NVARCHAR(50));

--SELECT * FROM N_UserCharge WHERE BankId=1074 AND State = 1 AND Id < 265 AND ISNULL(DzMoney, 0) > 0 AND UserId = 1937 ORDER BY STime dESC;
--SELECT DISTINCT UserId FROM N_UserCharge WHERE BankId=1074 AND State = 1 AND Id < 265 AND ISNULL(DzMoney, 0) > 0;
INSERT INTO #TOrder(ssid)
--SELECT SsId FROM N_UserCharge WHERE BankId=1074 AND State = 1 AND Id < 265 AND ISNULL(DzMoney, 0) > 0 AND UserId = 1937 ORDER BY STime dESC;
SELECT SsId FROM N_UserCharge WHERE BankId=1074 AND State = 1 AND Id < 265 AND ISNULL(DzMoney, 0) > 0 ORDER BY STime dESC;

DECLARE @count INT, 
		@idx INT = 1,
		@ssid NVARCHAR(50)

SELECT @count = count(1) FROM #TOrder;

--订单信息
DECLARE @userId INT, 
	@state INT, 
	@money DECIMAL(18,4),
	@stime DATETIME --充值日期
        
BEGIN TRY
	BEGIN TRAN

	WHILE @idx <= @count
	BEGIN
		SELECT @ssid = ssid FROM #TOrder WHERE id = @idx;

		SELECT 
			@userId = UserId, 
			@state = State, 
			@money = InMoney,
			@stime = STime 
		FROM N_UserCharge WHERE SsId=@ssid;

		IF @userId IS NULL 
		BEGIN
			PRINT N'无效的充值订单: ' + @ssid
			ROLLBACK TRAN
			RETURN
		END

		--支付成功
		UPDATE N_UserCharge SET State=1, DzMoney=@money WHERE SsId=@ssid;

		--更新会员余额
		--UPDATE N_USER SET Money = ISNULL(Money, 0) + @money WHERE Id = @userId;

		--更新账户汇总信息
		IF EXISTS (SELECT Id FROM N_UserMoneyStatAll WHERE UserId=@userId and DATEDIFF(d, STime, @stime)=0)
		BEGIN
			UPDATE N_UserMoneyStatAll SET Charge = ISNULL(Charge, 0) + @money WHERE  UserId=@userId and DATEDIFF(d, STime, @stime)=0
		END
		ELSE
		BEGIN
			INSERT INTO N_UserMoneyStatAll(UserId, Charge, STime) VALUES (@userId, @money, @stime)
		END

		SET @idx = @idx + 1
	END
		
	COMMIT TRAN		
END TRY
BEGIN CATCH
    IF @@TRANCOUNT > 0
    BEGIN
        ROLLBACK TRAN
    END 

	PRINT error_message()
END CATCH

IF EXISTS(SELECT 1 FROM tempdb..sysobjects WHERE id=OBJECT_ID('tempdb..#TOrder'))
		DROP TABLE #TOrder;