﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetStash.Log
{
    public class NetStashLog
    {
        private string logstashIp = string.Empty;
        private int logstashPort = -1;
        private string system = string.Empty;
        private string currentApp = string.Empty;
        private string currentAppVersion = string.Empty;


        public NetStashLog(string logstashIp, int logstashPort, string currentApp, string currentAppVersion )
        {
            if (string.IsNullOrWhiteSpace(logstashIp))
                throw new ArgumentNullException("logstashIp");

            if (string.IsNullOrWhiteSpace(currentApp))
                throw new ArgumentNullException("system");

            Worker.TcpWorker.Initialize(logstashIp, logstashPort);

            this.logstashIp = logstashIp;
            this.logstashPort = logstashPort;
            this.currentApp = currentApp;
            this.currentAppVersion = currentAppVersion;
        }

        public void Stop()
        {
            Worker.TcpWorker.Stop();
        }

        public void Restart()
        {
            Worker.TcpWorker.Restart();
        }

        public void Verbose(string message, string currentmodule, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Verbose.ToString();
            netStashEvent.Message = message;
            netStashEvent.Module = currentmodule;
            netStashEvent.OldValue = OldValue;
            netStashEvent.NewValue = NewValue;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Debug(string message, string currentmodule, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Debug.ToString();
            netStashEvent.Message = message;
            netStashEvent.Module = currentmodule;
            netStashEvent.OldValue = OldValue;
            netStashEvent.NewValue = NewValue;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Information(string message, string currentmodule, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Information.ToString();
            netStashEvent.Message = message;
            netStashEvent.Module = currentmodule;
            netStashEvent.OldValue = OldValue;
            netStashEvent.NewValue = NewValue;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Warning(string message, string currentmodule, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Warning.ToString();
            netStashEvent.Message = message;
            netStashEvent.Module = currentmodule;
            netStashEvent.OldValue = OldValue;
            netStashEvent.NewValue = NewValue;

            this.AddSendToLogstash(netStashEvent);
        }


        internal void InternalError(string message, string currentmodule, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Error.ToString();
            netStashEvent.Message = message;
            netStashEvent.Module = currentmodule;
            netStashEvent.OldValue = OldValue;
            netStashEvent.NewValue = NewValue;

            this.AddSendToLogstash(netStashEvent, false);
        }

        public void Error(Exception exception, string currentmodule, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Error.ToString();
            netStashEvent.Message = exception.Message;
            netStashEvent.ExceptionDetails = exception.StackTrace;
            netStashEvent.Module = currentmodule;
            netStashEvent.OldValue = OldValue;
            netStashEvent.NewValue = NewValue;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Error(string message, string currentmodule, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Error.ToString();
            netStashEvent.Message = message;
            netStashEvent.Module = currentmodule;
            netStashEvent.OldValue = OldValue;
            netStashEvent.NewValue = NewValue;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Fatal(string message, string currentmodule, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Fatal.ToString();
            netStashEvent.Message = message;
            netStashEvent.Module = currentmodule;
            netStashEvent.OldValue = OldValue;
            netStashEvent.NewValue = NewValue;

            this.AddSendToLogstash(netStashEvent);
        }

        private void AddSendToLogstash(NetStashEvent e, bool run = true)
        {
            e.Machine = Environment.MachineName;
            e.Source = system;
            e.App = currentApp;
            e.AppVersion = currentAppVersion;

            Storage.Proxy.LogProxy proxy = new Storage.Proxy.LogProxy();
            proxy.Add(e);

            if (run)
                Worker.TcpWorker.Run();
        }
    }
}
