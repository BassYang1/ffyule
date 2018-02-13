升级流程
1, 执行SQLs/upgrade.sql
2, 更新PC端和移动端web.config, 随笔付API为http://api.suibipay.com
3, 更新移动端web.config，引用Log4NET
4, 更新PC端文件
	bin/Lottery.WebAPP.dll
	statics/base/lotMenu.js, lotData.js
	contract/ContractfhPop.aspx, ContractgzPop.aspx
5, 更新Mobile随笔付文件
	bin/Lottery.IPhone.dll, Lottery.DAL.dll, Lottery.Utils.dll, Lottery.DBUtility.dll, Lottery.Entity.dll
	pay/*
	main.html
	center.html
	money/chargeSbf.aspx
	Global.apax
6, 更新Admin端
	bin/Lottery.Admin.dll, Lottery.DAL.dll, Lottery.Utils.dll, Lottery.DBUtility.dll, Lottery.Entity.dll
	statics/admin/js/adm.notify.js
	statics/music/pay.mp3
	admin/default.aspx
	
	
完成任务
1, 接入随笔付移动端支付
2, 修改会员报表BUG(未排除已删除的会员)


问题备注
1, 支付宝支付未授权
2, 随笔付微信支付完成，页面无跳转，一直停在随笔付页面
3, channelID, P_ChannelId需要怎么传递，PC端的demo和移动端的Demo不太一样
	PC端: channelID = weixin, P_ChannelId=22
	移动端: channelID = wxwap, P_ChannelId=wxwap
4, 随笔付返回的参数没有P_ChannelId，无法完成PostKey的较验，现在没有做PostKey的校验， 以下是请求参数和返回参数
	2018-02-12 08:47:48,468 [9] INFO  Lottery.IPhone.SBF.pay - 开始请求第三方支付: http://zf.suibipay.com/Payapi_Index_Pay.html?P_UserId=10441&P_OrderId=C_5478962121626673679&P_CardId=&P_CardPass=&P_FaceValue=0.01&P_ChannelId=2&P_Subject=随笔付支付&P_Price=0.01&P_Quantity=1&P_Description=随笔付支付ZFBWAP&P_Notic=随笔付支付&P_Result_url=http://m.lbgj888.com:80/pay/suibipay/result.aspx&P_Notify_url=http://m.lbgj888.com:80/pay/suibipay/notify.aspx&channelID=alipaywap&P_PostKey=99168eb50e8ab939d31b4c9117865cd2
	2018-02-12 08:52:50,750 [6] DEBUG Lottery.IPhone.SBF.result - P_UserId=10441&P_OrderId=C_5751816010742905246&P_FaceValue=0.010&P_ErrCode=0&P_PostKey=b1d0f3f9ad99e1785829bbc39b448ee1
