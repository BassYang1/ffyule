SQLs
	所有更新脚本汇重

	
20180212
1, 接入随笔付移动端支付


20180211
1, 修复报表[代理首页]时间设置的BUG
2, 契约统计处理
	2.1, 编写存付过程GZBatchByDate, GZOperByDate, GZTranByDate, FHTranByDate, FH0115BatchByDate, FH0115OperByDate, FH1631BatchByDate, FH1631OperByDate
	2.2, 触发器TR_UserMoneyLog_Update，更新契约工资Code
	2.3, 函数f_GetHistoryCode, 资金明细log，增加工资Code
	
	FHTranByDate, FH0115BatchByDate, FH0115OperByDate, FH1631BatchByDate, FH1631OperByDate
	处理分红

	GZBatchByDate, GZOperByDate, GZTranByDate
	处理工资

	TR_UserMoneyLog_Update
	修正分红和工资的Code字段

	f_GetHistoryCode
	获取Code字段对应的描述

20180210
1, 更新分红和工资存储过程
	Task_AutoActFenHongContract01_15: 处理1号-15号契约分红
	Act_FenHongContract01_15: 处理1号-15号契约分红
	Act_UserAgentFHOperTran: 添加账户分户记录
	
	Task_AutoActFenHongContract16_31: 处理16号-31号契约分红
	Act_FenHongContract16_31: 处理16号-31号契约分红
	Act_UserAgentFHOperTran: 添加账户分红记录
	
	Task_AutoActGongzi_Contract: 处理日结工资
	Act_Gongzi_Contract: 处理日结工资
	Act_ContractGZOperTran: 添加账户工资记录
	
	Task_AutoActFenHong01_15_Group3
	Task_AutoActFenHong01_15_Group4
	Task_AutoActFenHong16_31_Group3
	Task_AutoActFenHong16_31_Group4
		处理用户组默认系统分红契约
		
	Task_AutoActGongZi_Group2, Task_AutoActGongZi_Group3, Task_AutoActGongZi_Group4
		处理用户组默认系统公资契约
	
	
##5, 已签定生效的契约修改，已生效的契约项不允许修改

20180209
1, 所有会员都允许签约下级会员
2, 返点数判断改为大于0，小于自身的返点数
3, 普通会员不允许注册平级普通会员
4, 代理会员允许开平级代理会员

20180208
1, 添加"等待确认"状态
2, 已签约的用户可继续增加契约
3, 更新我的分红契约，显示"半月周期销量"

20180207
1, 更新契约页面

20180131
1, PC端App接入随笔付



