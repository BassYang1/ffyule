using log4net;
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
        private static string connStr = string.Empty;
        private static string hourStr = "3"; //执行时间, 默认凌晨3点
        private static readonly ILog log = log4net.LogManager.GetLogger(typeof(LotterySrv));
        private DateTime? lastGzDate; //上一次发放工资的日期
        private DateTime? lastFH0115Date; //上一次发放1到15分红的日期
        private DateTime? lastFH1631Date; //上一次发放16到31分红的日期
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
                hour = 3;
            }

            //定时发放工资(每天凌晨3点派发前一天契约工资)
            System.Timers.Timer gzTimer = new System.Timers.Timer(30 * 60 * 1000);
            gzTimer.Elapsed += new ElapsedEventHandler(GZ_Elapsed);
            gzTimer.Enabled = true;
            gzTimer.AutoReset = true;
            log.Info("开始定时发放工资(每天凌晨3点派发前一天契约工资)...");
            
            //定时发放分红(每个月16号凌晨3点派发1到15号的分红)
            System.Timers.Timer fh0115Timer = new System.Timers.Timer(40 * 60 * 1000);
            gzTimer.Elapsed += new ElapsedEventHandler(FH0115_Elapsed);
            gzTimer.Enabled = true;
            gzTimer.AutoReset = true;
            log.Info("开始定时发放工资(每个月16号凌晨3点派发1到15号的分红)...");

            //定时发放分红(每个月1号凌晨3点派发上一月16到31号的分红)
            System.Timers.Timer fh1631Timer = new System.Timers.Timer(40 * 60 * 1000);
            gzTimer.Elapsed += new ElapsedEventHandler(FH1631_Elapsed);
            gzTimer.Enabled = true;
            gzTimer.AutoReset = true;
            log.Info("开始定时发放工资(每个月1号凌晨3点派发上一月16到31号的分红)...");
        }

        protected override void OnStart(string[] args)
        {
            log.Info("启动Lottery平台工资和分红派发平台...");
        }

        protected override void OnStop()
        {
            log.Info("停止Lottery平台工资和分红派发平台...");
        }

        protected override void OnShutdown()
        {
            log.Info("服务器计算机关闭");
        }
        
        /// <summary>
        /// 每天凌晨3点派发前一天契约工资
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void GZ_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                //每天
                //凌晨3 && 当天没有发放过工资
                if (DateTime.Now.Hour == hour && (!lastGzDate.HasValue || lastGzDate.Value.Date != DateTime.Now.Date))
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
        /// 每个月16号凌晨3点派发1到15号的分红
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void FH0115_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                //每天
                //当月16号 && 凌晨3 && 当天没有发放过分红
                if (DateTime.Now.Day == 16 && DateTime.Now.Hour == hour && (!lastFH0115Date.HasValue || lastFH0115Date.Value.Date != DateTime.Now.Date))
                {
                    lastFH0115Date = DateTime.Now;
                    log.Info("开始发放1到15号的分红...");

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "FH0115BatchByDate";
                            SqlParameter parm = new SqlParameter("@fhdate", SqlDbType.DateTime);
                            parm.Value = DateTime.Now.AddDays(-1); //派发1到15号的分红
                            cmd.Parameters.Add(parm);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }

                    log.Info("结束发放1到15号的分红...");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }

        /// <summary>
        /// 每个月1号凌晨3点派发上一月16到31号的分红
        /// </summary>
        /// <param name="source"></param>
        /// <param name="e"></param>
        private void FH1631_Elapsed(object source, ElapsedEventArgs e)
        {
            try
            {
                //每天
                //当月1号 && 凌晨3 && 当天没有发放过分红
                if (DateTime.Now.Day == 1 && DateTime.Now.Hour == hour && (!lastFH1631Date.HasValue || lastFH1631Date.Value.Date != DateTime.Now.Date))
                {
                    lastFH1631Date = DateTime.Now;
                    log.Info("开始发放16到31号的分红...");

                    using (SqlConnection conn = new SqlConnection(connStr))
                    {
                        using (SqlCommand cmd = conn.CreateCommand())
                        {
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.CommandText = "FH0115BatchByDate";
                            SqlParameter parm = new SqlParameter("@fhdate", SqlDbType.DateTime);
                            parm.Value = DateTime.Now.AddDays(-1); //派发上一月16到31号的分红
                            cmd.Parameters.Add(parm);
                            conn.Open();
                            cmd.ExecuteNonQuery();
                            cmd.Dispose();
                            conn.Close();
                        }
                    }

                    log.Info("结束发放16到31号的分红...");
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
            }
        }
    }
}
