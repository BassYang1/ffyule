USE Ticket
GO

--Radio=1001, LotteryId=1, 五星直选复式
UPDATE Sys_PlaySmallType SET MaxBonus = 2 * 97800, MinBonus = 2 * 97800
WHERE Title2 = 'P_5FS' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星直选单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97800, MinBonus = 2 * 97800
WHERE Title2 = 'P_5DS' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星直选组合 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97800, MinBonus = 2 * 97800
WHERE Title2 = 'P_5ZH' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组合五星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97800, MinBonus = 2 * 97800
WHERE Title2 = 'P_5ZH_WQBSG' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组合四星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   8780, MinBonus = 2 * 8780
WHERE Title2 = 'P_5ZH_QBSG' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组合三星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_5ZH_BSG' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组合二星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97.8, MinBonus = 2 * 97.8
WHERE Title2 = 'P_5ZH_SG' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组合一星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9.78, MinBonus = 2 * 9.78
WHERE Title2 = 'P_5ZH_G' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组选120 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   815.013, MinBonus = 2 * 815.013
WHERE Title2 = 'P_5ZX120' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组选60 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   1629.999, MinBonus = 2 * 1629.999
WHERE Title2 = 'P_5ZX60' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组选30 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   3260.000, MinBonus = 2 * 3260.000
WHERE Title2 = 'P_5ZX30' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组选20 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   4890, MinBonus = 2 * 4890
WHERE Title2 = 'P_5ZX20' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组选10 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'P_5ZX10' AND LotteryId = 1 AND Radio=1001;

--Radio=1001, LotteryId=1, 五星组选5 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   19560, MinBonus = 2 * 19560
WHERE Title2 = 'P_5ZX5' AND LotteryId = 1 AND Radio=1001;

--Radio=1002, LotteryId=1, 前四直选复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'P_4FS_L' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 后四直选复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'P_4FS_R' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星复式万千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4FS_WQBS' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星复式万千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4FS_WQBG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星复式万千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4FS_WQSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星复式万百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4FS_WBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星复式千百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4FS_QBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 前四直选单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'P_4DS_L' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 后四直选单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'P_4DS_R' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星单式万千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4DS_WQBS' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星单式万千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4DS_WQBG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星单式万千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4DS_WQSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星单式万百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4DS_WBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星单式千百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'R_4DS_QBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 前四直选组合 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'P_4ZH_L' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 前四组合四星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'P_4ZH_L_WQBS' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 前四组合三星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_4ZH_L_QBS' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 前四组合二星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97.8, MinBonus = 2 * 97.8
WHERE Title2 = 'P_4ZH_L_BS' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 前四组合一星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9.78, MinBonus = 2 * 9.78
WHERE Title2 = 'P_4ZH_L_S' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 后四直选组合 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'P_4ZH_R' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 后四组合四星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9780, MinBonus = 2 * 9780
WHERE Title2 = 'P_4ZH_R_QBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 后四组合三星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_4ZH_R_BSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 后四组合二星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97.8, MinBonus = 2 * 97.8
WHERE Title2 = 'P_4ZH_R_SG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 后四组合一星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9.78, MinBonus = 2 * 9.78
WHERE Title2 = 'P_4ZH_R_G' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星组选24 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   407.499, MinBonus = 2 * 407.499
WHERE Title2 = 'P_4ZX24' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选24万千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   407.499, MinBonus = 2 * 407.499
WHERE Title2 = 'R_4ZX24_WQBS' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选24万千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   407.499, MinBonus = 2 * 407.499
WHERE Title2 = 'R_4ZX24_WQBG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选24万千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   407.499, MinBonus = 2 * 407.499
WHERE Title2 = 'R_4ZX24_WQSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选24万百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   407.499, MinBonus = 2 * 407.499
WHERE Title2 = 'R_4ZX24_WBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选24千百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   407.499, MinBonus = 2 * 407.499
WHERE Title2 = 'R_4ZX24_QBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星组选12 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   815.000, MinBonus = 2 * 815.000
WHERE Title2 = 'P_4ZX12' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选12千百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   815.000, MinBonus = 2 * 815.000
WHERE Title2 = 'R_4ZX12_QBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选12万百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   815.000, MinBonus = 2 * 815.000
WHERE Title2 = 'R_4ZX12_WBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选12万千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   815.000, MinBonus = 2 * 815.000
WHERE Title2 = 'R_4ZX12_WQSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选12万千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   815.000, MinBonus = 2 * 815.000
WHERE Title2 = 'R_4ZX12_WQBG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选12万千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   815.000, MinBonus = 2 * 815.000
WHERE Title2 = 'R_4ZX12_WQBS' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星组选6 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   1629.999, MinBonus = 2 * 1629.999
WHERE Title2 = 'P_4ZX6' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选6万千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   1629.999, MinBonus = 2 * 1629.999
WHERE Title2 = 'R_4ZX6_WQBS' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选6万千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   1629.999, MinBonus = 2 * 1629.999
WHERE Title2 = 'R_4ZX6_WQBG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选6万千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   1629.999, MinBonus = 2 * 1629.999
WHERE Title2 = 'R_4ZX6_WQSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选6万百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   1629.999, MinBonus = 2 * 1629.999
WHERE Title2 = 'R_4ZX6_WBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选6千百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   1629.999, MinBonus = 2 * 1629.999
WHERE Title2 = 'R_4ZX6_QBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 四星组选4 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   2445, MinBonus = 2 * 2445
WHERE Title2 = 'P_4ZX4' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选4万千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   2445, MinBonus = 2 * 2445
WHERE Title2 = 'R_4ZX4_WQBS' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选4万千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   2445, MinBonus = 2 * 2445
WHERE Title2 = 'R_4ZX4_WQBG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选4万千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   2445, MinBonus = 2 * 2445
WHERE Title2 = 'R_4ZX4_WQSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选4万百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   2445, MinBonus = 2 * 2445
WHERE Title2 = 'R_4ZX4_WBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1002, LotteryId=1, 组选4千百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   2445, MinBonus = 2 * 2445
WHERE Title2 = 'R_4ZX4_QBSG' AND LotteryId = 1 AND Radio=1002;

--Radio=1003, LotteryId=1, 前三直选复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3FS_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星复式万千百 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_WQB' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星复式万千十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_WQS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星复式万千个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_WQG' AND LotteryId = 1 AND Radio=1003;
	
--Radio=1003, LotteryId=1, 三星复式万百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_WBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星复式万百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_WBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星复式万十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_WSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星复式千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_QBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星复式千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_QBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星复式千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_QSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星复式百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3FS_BSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三直选单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3DS_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式万千百 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_WQB' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式万千十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_WQS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式万千个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_WQG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式万百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_WBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式万百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_WBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式万十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_WSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_QBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_QBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_QSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星单式百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3DS_BSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三直选组合 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3ZH_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三组合三星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3ZH_L_WQB' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三组合二星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97.8, MinBonus = 2 * 97.8
WHERE Title2 = 'P_3ZH_L_QB' AND LotteryId = 1 AND Radio=1003;
	
--Radio=1003, LotteryId=1, 前三组合一星 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9.78, MinBonus = 2 * 9.78
WHERE Title2 = 'P_3ZH_L_B' AND LotteryId = 1 AND Radio=1003;
	
--Radio=1003, LotteryId=1, 直选和值万千百 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_WQB' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万千十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_WQS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万千个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_WQG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_WBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_WBG' AND LotteryId = 1 AND Radio=1003;
	
--Radio=1003, LotteryId=1, 直选和值万十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_WSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_QBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_QBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_BSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3HE_WSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三直选跨度 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3KD_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选跨度万千百 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3KD_WQB' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选跨度万千十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3KD_WQS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选跨度万千个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9.78, MinBonus = 2 * 9.78
WHERE Title2 = 'R_3KD_WQG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选跨度万百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3KD_WBS' AND LotteryId = 1 AND Radio=1003;
	
--Radio=1003, LotteryId=1, 直选跨度万百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3KD_WBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选跨度万十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3KD_WSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选跨度千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3KD_QBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选跨度千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3KD_QBG' AND LotteryId = 1 AND Radio=1003;
	
--Radio=1003, LotteryId=1, 直选跨度千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3KD_QSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选跨度百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'R_3KD_BSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三组三复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3Z3_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三组三单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3Z3DS_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三组六复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   162.999, MinBonus = 2 * 162.999
WHERE Title2 = 'P_3Z6_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三组六单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   162.999, MinBonus = 2 * 162.999
WHERE Title2 = 'P_3Z6DS_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三混合组选 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3HX_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星混选万千百 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_WQB' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星混选万千十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_WQS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星混选万千个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_WQG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星混选万百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_WBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星混选万百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_WBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星混选万十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_WSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星混选千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_QBS' AND LotteryId = 1 AND Radio=1003;
	
--Radio=1003, LotteryId=1, 三星混选千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_QBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星混选千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_QSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 三星混选百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HX_BSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 前三组选和值 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3ZHE_L' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万千百 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_WQB' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万千十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_WQS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万千个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_WQG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_WBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_WBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值万十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_WSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_QBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_QBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_QSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 直选和值百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3HE_BSG' AND LotteryId = 1 AND Radio=1003;
	
	
	
	
--Radio=1003, LotteryId=1, 组选包胆万千百 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_WQB' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 组选包胆万千十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_WQS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 组选包胆万千个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_WQG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 组选包胆万百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_WBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 组选包胆万百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_WBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 组选包胆万十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_WSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 组选包胆千百十 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_QBS' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 组选包胆千百个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_QBG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 组选包胆千十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_QSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1003, LotteryId=1, 组选包胆百十个 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'R_3ZBD_BSG' AND LotteryId = 1 AND Radio=1003;

--Radio=1004, LotteryId=1, 中三直选复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3FS_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三直选单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3DS_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三直选和值 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3HE_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三直选跨度 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3KD_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三直选组合 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3ZH_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三组三复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3Z3_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三组六复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3Z6_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三组三单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   162.999, MinBonus = 2 * 162.999
WHERE Title2 = 'P_3Z3DS_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三组六单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   162.999, MinBonus = 2 * 162.999
WHERE Title2 = 'P_3Z6DS_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三混合组选 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3HX_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三组选和值 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3ZHE_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三组选包胆 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3ZBD_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1004, LotteryId=1, 中三和值尾数 
--UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 162.999
--WHERE Title2 = 'P_3QTWS_C' AND LotteryId = 1 AND Radio=1004;

--Radio=1007, LotteryId=1, 直选定位胆 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9.78, MinBonus = 2 * 9.78
WHERE Title2 = 'P_DD' AND LotteryId = 1 AND Radio=1007;

--Radio=2003, LotteryId=2, 定位胆 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   10.758, MinBonus = 2 * 10.758
WHERE Title2 = 'P11_DD' AND LotteryId = 2 AND Radio=2003;

--Radio=3004, LotteryId=3, 定位胆 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   10.758, MinBonus = 2 * 10.758
WHERE Title2 = 'P_DD_3' AND LotteryId = 3 AND Radio=3004;

--Radio=4004, LotteryId=3, 定位胆, 一到五名复式
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9.6, MinBonus = 2 * 9.6
WHERE Title2 = 'PK10_DD1_5' AND LotteryId = 4 AND Radio=4004;

--Radio=4004, LotteryId=4, 定位胆, 六到十名复式
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   9.6, MinBonus = 2 * 9.6
WHERE Title2 = 'PK10_DD6_10' AND LotteryId = 4 AND Radio=4004;

--Radio=1005, LotteryId=1, 后三直选复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3FS_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三直选单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3DS_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三直选组合 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3ZH_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三直选和值 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3HE_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三直选跨度 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   978, MinBonus = 2 * 978
WHERE Title2 = 'P_3KD_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三组三复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3Z3_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三组三单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3Z3DS_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三组六复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   162.999, MinBonus = 2 * 162.999
WHERE Title2 = 'P_3Z3_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三组六单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   162.999, MinBonus = 2 * 162.999
WHERE Title2 = 'P_3Z6DS_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三混合组选 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   326, MinBonus = 2 * 326
WHERE Title2 = 'P_3HX_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三组选和值 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   162.999, MinBonus = 2 * 162.999
WHERE Title2 = 'P_3ZHE_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1005, LotteryId=1, 后三组选包胆 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   162.999, MinBonus = 2 * 162.999
WHERE Title2 = 'P_3ZBD_R' AND LotteryId = 1 AND Radio=1005;

--Radio=1006, LotteryId=1, 前二直选复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97.8, MinBonus = 2 * 97.8
WHERE Title2 = 'P_2FS_L' AND LotteryId = 1 AND Radio=1006;

--Radio=1006, LotteryId=1, 前二直选单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97.8, MinBonus = 2 * 97.8
WHERE Title2 = 'P_2DS_L' AND LotteryId = 1 AND Radio=1006;

--Radio=1006, LotteryId=1, 前二直选和值 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 *   97.8, MinBonus = 2 * 97.8
WHERE Title2 = 'P_2HE_L' AND LotteryId = 1 AND Radio=1006;

--Radio=1006, LotteryId=1, 前二直选跨度 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 * 97.8, MinBonus = 2 * 97.8
WHERE Title2 = 'P_2KD_L' AND LotteryId = 1 AND Radio=1006;

--Radio=1006, LotteryId=1, 前二组选复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 * 48.9, MinBonus = 2 * 48.9
WHERE Title2 = 'P_2Z2_L' AND LotteryId = 1 AND Radio=1006;

--Radio=1006, LotteryId=1, 前二组选单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 * 48.9, MinBonus = 2 * 48.9
WHERE Title2 = 'P_2ZDS_L' AND LotteryId = 1 AND Radio=1006;

--Radio=1006, LotteryId=1, 前二组选和值 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 * 48.9, MinBonus = 2 * 48.9
WHERE Title2 = 'P_2ZHE_L' AND LotteryId = 1 AND Radio=1006;

--Radio=1006, LotteryId=1, 前二组选包胆 
UPDATE Sys_PlaySmallType SET MaxBonus = 2 * 48.9, MinBonus = 2 * 48.9
WHERE Title2 = 'P_2ZBD_L' AND LotteryId = 1 AND Radio=1006;



/******-------------------------------------------*******************/
--Radio=4001, LotteryId=4, 猜冠军 
UPDATE Sys_PlaySmallType SET MaxBonus = 19.56, MinBonus = 19.56
WHERE Title2 = 'PK10_One' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 冠亚军复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 176, MinBonus = 176
WHERE Title2 = 'PK10_TwoFS' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 冠亚军单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 176, MinBonus = 176
WHERE Title2 = 'PK10_TwoDS' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 前三名复式 
UPDATE Sys_PlaySmallType SET MaxBonus = 1415, MinBonus = 1415
WHERE Title2 = 'PK10_ThreeFS' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 前三名单式 
UPDATE Sys_PlaySmallType SET MaxBonus = 1415, MinBonus = 1415
WHERE Title2 = 'PK10_ThreeDS' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 定位胆, 一到五名复式,  
UPDATE Sys_PlaySmallType SET MaxBonus = 19.56, MinBonus = 19.56
WHERE Title2 = 'PK10_DD1_5' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 定位胆, 六到十名复式,  
UPDATE Sys_PlaySmallType SET MaxBonus = 19.56, MinBonus = 19.56
WHERE Title2 = 'PK10_DD6_10' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXOne' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXTwo' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXThree' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXFour' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXFive' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXSix' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXSeven' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXEight' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXNine' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜大小
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DXTen' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSOne' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSTwo' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSThree' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSFour' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSFive' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSSix' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSSeven' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSEight' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSNine' AND LotteryId = 4;

--Radio=4001, LotteryId=4, 猜单双
UPDATE Sys_PlaySmallType SET MaxBonus = 3.96, MinBonus = 3.96
WHERE Title2 = 'PK10_DSTen' AND LotteryId = 4;

--LotteryId=2, 前三直选复式
UPDATE Sys_PlaySmallType SET MaxBonus = 1946, MinBonus = 1946
WHERE Title2 = 'P11_3FS_L' AND LotteryId = 2;

--LotteryId=2, 前三直选单式
UPDATE Sys_PlaySmallType SET MaxBonus = 1946, MinBonus = 1946
WHERE Title2 = 'P11_3DS_L' AND LotteryId = 2;

--LotteryId=2, 前三组选复式
UPDATE Sys_PlaySmallType SET MaxBonus = 324, MinBonus = 324
WHERE Title2 = 'P11_3ZFS_L' AND LotteryId = 2;

--LotteryId=2, 前三组选单式
UPDATE Sys_PlaySmallType SET MaxBonus = 324, MinBonus = 324
WHERE Title2 = 'P11_3ZDS_L' AND LotteryId = 2;

--LotteryId=2, 前二直选复式
UPDATE Sys_PlaySmallType SET MaxBonus = 215, MinBonus = 215
WHERE Title2 = 'P11_2FS_L' AND LotteryId = 2;

--LotteryId=2, 前二直选单式
UPDATE Sys_PlaySmallType SET MaxBonus = 215, MinBonus = 215
WHERE Title2 = 'P11_2DS_L' AND LotteryId = 2;

--LotteryId=2, 前二组选复式
UPDATE Sys_PlaySmallType SET MaxBonus = 107, MinBonus = 107
WHERE Title2 = 'P11_2ZFS_L' AND LotteryId = 2;

--LotteryId=2, 前二组选单式
UPDATE Sys_PlaySmallType SET MaxBonus = 107, MinBonus = 107
WHERE Title2 = 'P11_2ZDS_L' AND LotteryId = 2;

--LotteryId=2, 任选单式一中一
UPDATE Sys_PlaySmallType SET MaxBonus = 4.3, MinBonus = 4.3
WHERE Title2 = 'P11_RXDS_1' AND LotteryId = 2;

--LotteryId=2, 任选复式一中一
UPDATE Sys_PlaySmallType SET MaxBonus = 4.3, MinBonus = 4.3
WHERE Title2 = 'P11_RXFS_1' AND LotteryId = 2;

--LotteryId=2, 任选单式二中二
UPDATE Sys_PlaySmallType SET MaxBonus = 10.7, MinBonus = 10.7
WHERE Title2 = 'P11_RXDS_2' AND LotteryId = 2;

--LotteryId=2, 任选复式二中二
UPDATE Sys_PlaySmallType SET MaxBonus = 10.7, MinBonus = 10.7
WHERE Title2 = 'P11_RXFS_2' AND LotteryId = 2;

--LotteryId=2, 任选单式三中三
UPDATE Sys_PlaySmallType SET MaxBonus = 32.2, MinBonus = 32.2
WHERE Title2 = 'P11_RXDS_3' AND LotteryId = 2;

--LotteryId=2, 任选复式三中三
UPDATE Sys_PlaySmallType SET MaxBonus = 32.2, MinBonus = 32.2
WHERE Title2 = 'P11_RXFS_3' AND LotteryId = 2;

--LotteryId=2, 任选单式四中四
UPDATE Sys_PlaySmallType SET MaxBonus = 129, MinBonus = 129
WHERE Title2 = 'P11_RXDS_4' AND LotteryId = 2;

--LotteryId=2, 任选复式四中四
UPDATE Sys_PlaySmallType SET MaxBonus = 129, MinBonus = 129
WHERE Title2 = 'P11_RXFS_4' AND LotteryId = 2;

--LotteryId=2, 任选单式五中五
UPDATE Sys_PlaySmallType SET MaxBonus = 903, MinBonus = 903
WHERE Title2 = 'P11_RXDS_5' AND LotteryId = 2;

--LotteryId=2, 任选复式五中五
UPDATE Sys_PlaySmallType SET MaxBonus = 903, MinBonus = 903
WHERE Title2 = 'P11_RXFS_5' AND LotteryId = 2;

--LotteryId=2, 任选单式六中五
UPDATE Sys_PlaySmallType SET MaxBonus = 150.6, MinBonus = 150.6
WHERE Title2 = 'P11_RXDS_6' AND LotteryId = 2;

--LotteryId=2, 任选复式六中五
UPDATE Sys_PlaySmallType SET MaxBonus = 150.6, MinBonus = 150.6
WHERE Title2 = 'P11_RXFS_5' AND LotteryId = 2;

--LotteryId=2, 任选单式七中五
UPDATE Sys_PlaySmallType SET MaxBonus = 42.3, MinBonus = 42.3
WHERE Title2 = 'P11_RXDS_7' AND LotteryId = 2;

--LotteryId=2, 任选复式七中五
UPDATE Sys_PlaySmallType SET MaxBonus = 42.3, MinBonus = 42.3
WHERE Title2 = 'P11_RXFS_7' AND LotteryId = 2;

--LotteryId=2, 任选单式八中五
UPDATE Sys_PlaySmallType SET MaxBonus = 16.1, MinBonus = 16.1
WHERE Title2 = 'P11_RXDS_8' AND LotteryId = 2;

--LotteryId=2, 任选复式八中五
UPDATE Sys_PlaySmallType SET MaxBonus = 16.1, MinBonus = 16.1
WHERE Title2 = 'P11_RXFS_8' AND LotteryId = 2;

--LotteryId=2, 定位胆
UPDATE Sys_PlaySmallType SET MaxBonus = 21.52, MinBonus = 21.52
WHERE Title2 = 'P11_DD' AND LotteryId = 2;

--LotteryId=2, 不定位
UPDATE Sys_PlaySmallType SET MaxBonus = 7.1, MinBonus = 7.1
WHERE Title2 = 'P11_BDD_L' AND LotteryId = 2;

--关闭, 任四, 任三, 任二
UPDATE Sys_PlayBigType SET IsOpen = 1, IsOpenIphone=1 WHERE Title IN ('任四', '任三' ,'任二');
UPDATE Sys_PlaySmallType SET IsOpen = 1, IsOpenIphone=1 WHERE LotteryId = 1 AND Radio IN (1009, 1010, 1011);

--关闭龙虎合
UPDATE Sys_PlayBigType SET IsOpen = 1, IsOpenIphone=1 WHERE Title IN ('龙虎');
UPDATE Sys_PlaySmallType SET IsOpen = 1, IsOpenIphone=1 WHERE LotteryId = 1 AND Radio IN (1015);

--LotteryId=1, 直选定位胆
UPDATE Sys_PlaySmallType SET MaxBonus = 19.56, MinBonus = 19.56
WHERE Title2 = 'P_DD' AND LotteryId = 1;

--LotteryId=1, 不定位
UPDATE Sys_PlaySmallType SET MaxBonus = 7.2, MinBonus = 7.2
WHERE Title2 IN ('P_3BDD1_R', 'P_3BDD2_R', 'P_3BDD1_L', 'P_3BDD2_L', 'P_4BDD1', 'P_4BDD2', 'P_5BDD2', 'P_5BDD3') AND LotteryId = 1;

--前二, 后二, 二星直选, 单式、复试、和值、跨度
UPDATE Sys_PlaySmallType SET MaxBonus = 195.6, MinBonus = 195.6
WHERE (Title2 LIKE 'P_2FS%' OR Title2 LIKE 'P_2DS%' OR Title2 LIKE 'P_2HE%' OR Title2 LIKE 'P_2KD%') AND LotteryId = 1;

--前二, 后二, 二星组选, 复试、单式、和值、包胆
UPDATE Sys_PlaySmallType SET MaxBonus = 97.8, MinBonus = 97.8
WHERE (Title2 LIKE 'P_2Z2%' OR Title2 LIKE 'P_2ZDS%' OR Title2 LIKE 'P_2ZHE%' OR Title2 LIKE 'P_2ZBD%') AND LotteryId = 1;

--前三 中三 后三, 三星直选 单式复试、单式、和值、跨度
UPDATE Sys_PlaySmallType SET MaxBonus = 1956, MinBonus = 1956
WHERE (Title2 LIKE 'P_3FS%' OR Title2 LIKE 'P_3DS%' OR Title2 LIKE 'P_2ZHE%' OR Title2 LIKE 'P_3KD%') AND LotteryId = 1;

--前三 中三 后三, 组选, 组3单式、复试
UPDATE Sys_PlaySmallType SET MaxBonus = 652, MinBonus = 652
WHERE (Title2 LIKE 'P_3Z3%' OR Title2 LIKE 'P_3Z3DS%') AND LotteryId = 1;

--前三 中三 后三, 组选, 组6单式、复试
UPDATE Sys_PlaySmallType SET MaxBonus = 326, MinBonus = 326
WHERE (Title2 LIKE 'P_3Z6%' OR Title2 LIKE 'P_3Z6DS%') AND LotteryId = 1;

--前三 中三 后三, 组选, 混合组选
UPDATE Sys_PlaySmallType SET MaxBonus = 652, MinBonus = 652
WHERE (Title2 LIKE 'P_3HX%') AND LotteryId = 1;

--前三 中三 后三, 组选, 组选和值
UPDATE Sys_PlaySmallType SET MaxBonus = 326, MinBonus = 326
WHERE (Title2 LIKE 'P_3ZHE%') AND LotteryId = 1;

--前三 中三 后三, 组选, 组选包胆
UPDATE Sys_PlaySmallType SET MaxBonus = 326, MinBonus = 326
WHERE (Title2 LIKE 'P_3ZBD%') AND LotteryId = 1;

--关闭和值尾数、特殊号码玩法
UPDATE Sys_PlaySmallType SET IsOpen = 1, IsOpenIphone=1 WHERE Title2 LIKE 'P_3QTWS%';
UPDATE Sys_PlaySmallType SET IsOpen = 1, IsOpenIphone=1 WHERE Title2 LIKE 'P_3QTTS%';

--四星直选, 单式、复试
UPDATE Sys_PlaySmallType SET MaxBonus = 19560, MinBonus = 19560
WHERE (Title2 LIKE 'P_4FS%' OR Title2 LIKE 'P_4DS%') AND LotteryId = 1;

--四星直选, 组选24
UPDATE Sys_PlaySmallType SET MaxBonus = 815, MinBonus = 815
WHERE (Title2 LIKE 'P_4ZX24%') AND LotteryId = 1;

--四星直选, 组选12
UPDATE Sys_PlaySmallType SET MaxBonus = 1630, MinBonus = 1630
WHERE (Title2 LIKE 'P_4ZX12%') AND LotteryId = 1;

--四星直选, 组选6
UPDATE Sys_PlaySmallType SET MaxBonus = 3260, MinBonus = 3260
WHERE (Title2 LIKE 'P_4ZX6%') AND LotteryId = 1;

--四星直选, 组选4
UPDATE Sys_PlaySmallType SET MaxBonus = 4890, MinBonus = 4890
WHERE (Title2 LIKE 'P_4ZX4%') AND LotteryId = 1;

--五星直选, 单式、复试
UPDATE Sys_PlaySmallType SET MaxBonus = 195600, MinBonus = 195600
WHERE (Title2 LIKE 'P_5FS%' OR Title2 LIKE 'P_5DS%') AND LotteryId = 1;

--五星直选, 直选组合
UPDATE Sys_PlaySmallType SET MaxBonus = 195600, MinBonus = 195600
WHERE (Title2 LIKE 'P_5ZH%') AND LotteryId = 1;

--五星直选, 组选120
UPDATE Sys_PlaySmallType SET MaxBonus = 1630, MinBonus = 1630
WHERE (Title2 LIKE 'P_5ZX120%') AND LotteryId = 1;

--五星直选, 五星组选60
UPDATE Sys_PlaySmallType SET MaxBonus = 3260, MinBonus = 3260
WHERE (Title2 LIKE 'P_5ZX60%') AND LotteryId = 1;

--五星直选, 五星组选30
UPDATE Sys_PlaySmallType SET MaxBonus = 6520, MinBonus = 6520
WHERE (Title2 LIKE 'P_5ZX30%') AND LotteryId = 1;

--五星直选, 五星组选20
UPDATE Sys_PlaySmallType SET MaxBonus = 9780, MinBonus = 9780
WHERE (Title2 LIKE 'P_5ZX20%') AND LotteryId = 1;

--五星直选, 五星组选10
UPDATE Sys_PlaySmallType SET MaxBonus = 19560, MinBonus = 19560
WHERE (Title2 LIKE 'P_5ZX10%') AND LotteryId = 1;

--五星直选, 五星组选5
UPDATE Sys_PlaySmallType SET MaxBonus = 39120, MinBonus = 39120
WHERE (Title2 LIKE 'P_5ZX5%') AND LotteryId = 1;
