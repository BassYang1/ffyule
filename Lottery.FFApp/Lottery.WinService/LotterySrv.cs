using log4net;
using Lottery.Collect;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Timers;

namespace Lottery.WinService
{
    public partial class LotterySrv : ServiceBase
    {
        private System.Timers.Timer gzTimer;
        private System.Timers.Timer fh0115Timer;
        private System.Timers.Timer fh1631Timer;
        private static string connStr = string.Empty;
        private static string hourStr = "3"; //执行时间, 默认凌晨3点
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(LotterySrv));
        private DateTime? lastGzDate; //上一次发放工资的日期
        private DateTime? lastFH0115Date; //上一次发放1到15分红的日期
        private DateTime? lastFH1631Date; //结算当月11号到25号的分红
        private int hour = 3;

        static LotterySrv()
        {
            if (ConfigurationManager.ConnectionStrings["ConnStr"] != null)
            {
                connStr = ConfigurationManager.ConnectionStrings["ConnStr"].ToString();
            }

            if (ConfigurationManager.AppSettings["DoHour"] != null)
            {
                hourStr = ConfigurationManager.AppSettings["DoHour"].ToString();
            }
        }

        public LotterySrv()
        {
            InitializeComponent();

            if (Int32.TryParse(hourStr, out hour) == false)
            {
                hour = 1;
            }

            //定时发放工资(每天凌晨1点派发前一天契约工资)
            gzTimer = new System.Timers.Timer(30 * 60 * 1000);
            gzTimer.Elapsed += new ElapsedEventHandler(GZ_Elapsed);
            gzTimer.AutoReset = true;
            log.Info("开始定时发放工资(每天凌晨1点派发前一天契约工资)...");

            //定时发放分红(结算当月1号到当月15号的分红)
            fh0115Timer = new System.Timers.Timer(40 * 60 * 1000);
            fh0115Timer.Elapsed += new ElapsedEventHandler(FH0115_Elapsed);
            fh0115Timer.AutoReset = true;
            log.Info("开始定时发放工资(结算当月1号到当月15号的分红)...");

            //定时发放分红(结算当月16号到当月最后一天的分红)
            fh1631Timer = new System.Timers.Timer(40 * 60 * 1000);
            fh1631Timer.Elapsed += new ElapsedEventHandler(FH1631_Elapsed);
            fh1631Timer.AutoReset = true;
            log.Info("开始定时发放工资(结算当月16号到当月最后一天的分红)...");
        }

        protected override void OnStart(string[] args)
        {
            log.Info("启动Lottery平台工资和分红派发平台...");
            gzTimer.Enabled = true;
            fh0115Timer.Enabled = true;
            fh1631Timer.Enabled = true;

            //log.Info("开始采集程序...");
            TimeData.Run();
        }

        protected override void OnStop()
        {
            log.Info("停止Lottery平台工资和分红派发平台...");
            gzTimer.Enabled = false;
            fh0115Timer.Enabled = false;
            fh1631Timer.Enabled = false;

            //log.Info("停止采集程序...");
            TimeData.Stop();
        }

        protected override void OnShutdown()
        {
            log.Info("服务器计算机关闭");
            gzTimer.Enabled = false;
            fh0115Timer.Enabled = false;
            fh1631Timer.Enabled = false;

            //log.Info("停止采集程序...");
            TimeData.Stop();
        }
        
        /// <summary>
        /// 每天凌晨1点派发前一天契约工资
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void GZ_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                log.Debug("发放工资...");

                //每天
                //凌晨1 && 当天没有发放过工资
                if (DateTime.Now.Hour == 1 && (!lastGzDate.HasValue || lastGzDate.Value.Date != DateTime.Now.Date))
                {
                    lastGzDate = DateTime.Now;
                    log.Info("开始发放工资...");

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "GZBatchByDate";
                            SqlParameter parm = new SqlParameter("@gzdate", SqlDbType.DateTime);
                            parm.Value = DateTime.Now.AddDays(-1); //发放该日期的工资
                            cmd.Parameters.Add(parm);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }

                    log.Info("结束发放工资...");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
                
        /// <summary>
        /// 结算当月1号到当月15号的分红
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void FH0115_Elapsed(object source, ElapsedEventArgs e)
        {
            log.Debug("发放当月1号到当月15号的分红...");

            try
            {
                //每天
                //当月16号 && 凌晨1 && 当天没有发放过分红
                if (DateTime.Now.Day == 16 && DateTime.Now.Hour == 10 && (!lastFH0115Date.HasValue || lastFH0115Date.Value.Date != DateTime.Now.Date))
                {
                    lastFH0115Date = DateTime.Now;
                    log.Info("结算上月26号到当月10号的分红...");

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "FH0115BatchByDate";
                            SqlParameter parm = new SqlParameter("@fhdate", SqlDbType.DateTime);
                            parm.Value = DateTime.Now.AddDays(-1); //结算当月1号到当月15号的分红
                            cmd.Parameters.Add(parm);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }

                    log.Info("结束结算当月1号到当月15号的分红...");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 结算当月16号到当月最后一天的分红
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void FH1631_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                log.Debug("发放当月16号到当月最后一天的分红...");
                
                //每天
                //下月1号 && 上午10点 && 当天没有发放过分红
                if (DateTime.Now.Day == 1 && DateTime.Now.Hour == 10 && (!lastFH1631Date.HasValue || lastFH1631Date.Value.Date != DateTime.Now.Date))
                {
                    lastFH1631Date = DateTime.Now;
                    log.Info("开始结算当月16号到当月最后一天的分红...");

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "FH1631BatchByDate";
                            SqlParameter parm = new SqlParameter("@fhdate", SqlDbType.DateTime);
                            parm.Value = DateTime.Now.AddDays(-1); //结算当月16号到当月最后一天的分红
                            cmd.Parameters.Add(parm);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }

                    log.Info("结束结算当月16号到当月最后一天的分红...");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
