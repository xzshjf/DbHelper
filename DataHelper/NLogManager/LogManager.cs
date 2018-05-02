using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NLog;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace NLogManager
{
    public static class Log
    {
        public static readonly string CONFIGPATH = AppDomain.CurrentDomain.BaseDirectory + "sysconfig.ini";
        public static Logger logger = LogManager.GetCurrentClassLogger();

        public static EventLog SysLog;
        public const int STRINGMAX = 255;
        //系统日志配置
        public static string DATALOGSOURCE = "Data Operations";
        public static string DATALOGNAME = "Data Log";
        public static string LogMode = "1";
        
        static Log()
        {
            try
            {
                if (File.Exists(CONFIGPATH))
                {
                    StringBuilder sb = new StringBuilder(STRINGMAX);
                    WinAPI.GetPrivateProfileString("LOGMANAGER", "DATALOGSOURCE", "", sb, STRINGMAX, CONFIGPATH);
                    DATALOGSOURCE = sb.ToString();
                    WinAPI.GetPrivateProfileString("LOGMANAGER", "DATALOGNAME", "", sb, STRINGMAX, CONFIGPATH);
                    DATALOGNAME = sb.ToString();
                    WinAPI.GetPrivateProfileString("LOGMANAGER", "MODE", "", sb, STRINGMAX, CONFIGPATH);
                    LogMode = sb.ToString();
                }
                switch (LogMode)
                {
                    case "1": //Txt文件记录日志
                        break;
                    case "2": //windows系统记录日志
                        if (!EventLog.SourceExists(DATALOGSOURCE))
                            EventLog.CreateEventSource(DATALOGSOURCE, DATALOGNAME);
                        SysLog = new EventLog(DATALOGNAME);
                        break;
                    case "3": //数据库记录日志[预留]
                        break;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// 增加错误信息到日志
        /// </summary>
        /// <param name="e"></param>
        public static void AddErrorLog(string msg)
        {
            switch (LogMode)
            {
                case "1": //Txt文件记录日志
                    logger.Error(msg);
                    break;
                case "2": //windows系统记录日志
                    SysLog.Source = DATALOGSOURCE;
                    SysLog.WriteEntry(msg, EventLogEntryType.Error);
                    break;
                case "3"://预留写数据库表
                    break;
            } 
        }

        public static void AddInfoLog(string msg)
        {
            switch (LogMode)
            {
                case "1": //Txt文件记录日志
                    logger.Info(msg);
                    break;
                case "2": //windows系统记录日志
                    SysLog.Source = DATALOGSOURCE;
                    SysLog.WriteEntry(msg, EventLogEntryType.Information);
                    break;
                case "3"://预留写数据库表
                    break;
            }
        }

    }
    public static class WinAPI
    {
        [DllImport("kernel32")]
        public static extern long WritePrivateProfileString(string section, string key, string val, string filepath);

        [DllImport("kernel32")]
        public static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retval, int size, string filePath);
    }
}
