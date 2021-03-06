﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace NetStash.Log
{
    public class NetStashLog
    {
        private string logstashIp = string.Empty;
        private int logstashPort = -1;
        private string currentAppVersion = string.Empty;
        private string user = string.Empty;


        public NetStashLog(string logstashIp, int logstashPort, string currentAppVersion, string User )
        {
            if (string.IsNullOrWhiteSpace(logstashIp))
                throw new ArgumentNullException("logstashIp");

            if (string.IsNullOrWhiteSpace(currentAppVersion))
                throw new ArgumentNullException("system");

            Worker.TcpWorker.Initialize(logstashIp, logstashPort, currentAppVersion, User);

            this.logstashIp = logstashIp;
            this.logstashPort = logstashPort;
            this.currentAppVersion = currentAppVersion;
            this.user = User;
        }

        public void Stop()
        {
            Worker.TcpWorker.Stop();
        }

        public void Restart()
        {
            Worker.TcpWorker.Restart();
        }

        public void Verbose(string message, System.Reflection.MethodBase currentMethod, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Verbose.ToString();
            netStashEvent.Message = message;
            netStashEvent.Method = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            if (OldValue.Length > 0) Fields.Add("OldValue", OldValue);
            if (NewValue.Length > 0) Fields.Add("NewValue", NewValue);
            netStashEvent.Fields = Fields;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Debug(string message, System.Reflection.MethodBase currentMethod, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Debug.ToString();
            netStashEvent.Message = message;
            netStashEvent.Method = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            if (OldValue.Length > 0) Fields.Add("OldValue", OldValue);
            if (NewValue.Length > 0) Fields.Add("NewValue", NewValue);
            netStashEvent.Fields = Fields;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Information(string message, System.Reflection.MethodBase currentMethod, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Information.ToString();
            netStashEvent.Message = message;
            netStashEvent.Method = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            if (OldValue.Length > 0) Fields.Add("OldValue", OldValue);
            if (NewValue.Length > 0) Fields.Add("NewValue", NewValue);
            netStashEvent.Fields = Fields;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Warning(string message, System.Reflection.MethodBase currentMethod, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Warning.ToString();
            netStashEvent.Message = message;
            netStashEvent.Method = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            if (OldValue.Length > 0) Fields.Add("OldValue", OldValue);
            if (NewValue.Length > 0) Fields.Add("NewValue", NewValue);
            netStashEvent.Fields = Fields;

            this.AddSendToLogstash(netStashEvent);
        }


        internal void InternalError(string message, System.Reflection.MethodBase currentMethod, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Error.ToString();
            netStashEvent.Message = message;
            netStashEvent.Method = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            if (OldValue.Length > 0) Fields.Add("OldValue", OldValue);
            if (NewValue.Length > 0) Fields.Add("NewValue", NewValue);
            netStashEvent.Fields = Fields;

            this.AddSendToLogstash(netStashEvent, false);
        }

        public void Error(Exception exception, System.Reflection.MethodBase currentMethod, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Error.ToString();
            netStashEvent.Message = exception.Message;
            netStashEvent.ExceptionDetails = exception.StackTrace;
            netStashEvent.Method = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            if (OldValue.Length > 0) Fields.Add("OldValue", OldValue);
            if (NewValue.Length > 0) Fields.Add("NewValue", NewValue);
            netStashEvent.Fields = Fields;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Error(string message, System.Reflection.MethodBase currentMethod, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Error.ToString();
            netStashEvent.Message = message;
            netStashEvent.Method = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            if (OldValue.Length > 0) Fields.Add("OldValue", OldValue);
            if (NewValue.Length > 0) Fields.Add("NewValue", NewValue);
            netStashEvent.Fields = Fields;

            this.AddSendToLogstash(netStashEvent);
        }

        public void Fatal(string message, System.Reflection.MethodBase currentMethod, string OldValue = "", string NewValue = "")
        {
            NetStashEvent netStashEvent = new NetStashEvent();
            netStashEvent.Level = NetStashLogLevel.Fatal.ToString();
            netStashEvent.Message = message;
            netStashEvent.Method = currentMethod.DeclaringType.FullName + "." + currentMethod.Name;
            Dictionary<string, string> Fields = new Dictionary<string, string>();
            if (OldValue.Length > 0) Fields.Add("OldValue", OldValue);
            if (NewValue.Length > 0) Fields.Add("NewValue", NewValue);
            netStashEvent.Fields = Fields;

            this.AddSendToLogstash(netStashEvent);
        }

        private void AddSendToLogstash(NetStashEvent e, bool run = true)
        {
            e.Machine = Environment.MachineName;
            e.MacAddress = (from nic in System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces() where nic.OperationalStatus == System.Net.NetworkInformation.OperationalStatus.Up select nic.GetPhysicalAddress().ToString()).FirstOrDefault();
            e.AppVersion = currentAppVersion;
            e.Username = user;

            Storage.Proxy.LogProxy proxy = new Storage.Proxy.LogProxy();
            proxy.Add(e);

            if (run)
                Worker.TcpWorker.Run();
        }
    }
}
