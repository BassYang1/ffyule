--1, 添加彩种
DECLARE @sort INT, 
		@msort INT, 
		@ltid INT = 6001,
		@lttype INT = 6,
		@iss INT = 400


DECLARE @num INT = 1, 
		@sn NVARCHAR(100) = '001', 
		@time NVARCHAR(20) = '2018-05-01 00:01:00'

--2, 添加彩种时间
WHILE @num <= @iss
BEGIN	
	INSERT INTO Sys_LotteryDateTime(LotteryId, Sn, Time, Sort, STime)
	SELECT @ltid, @sn, @time, 0, GETDATE()
	WHERE NOT EXISTS (SELECT 1 FROM Sys_LotteryDateTime WHERE LotteryId=@ltid AND Sn=@sn)

	SET @num = @num + 1;
	SET @sn = '00000' + CAST(@num AS NVARCHAR(10))
	SET @sn = SUBSTRING(@sn, len(@sn) - 2, 3)
	SET @time = CONVERT(NVARCHAR(20), DATEADD(MI, 5, CONVERT(DATETIME,@time,120)), 120)
END
