using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

#pragma warning disable
// ReSharper disable All

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace hostname1.DBus
{
    [DBusInterface("org.freedesktop.hostname1")]
    interface IHostname1 : IDBusObject
    {
        Task SetHostnameAsync(string Hostname, bool Interactive);
        Task SetStaticHostnameAsync(string Hostname, bool Interactive);
        Task SetPrettyHostnameAsync(string Hostname, bool Interactive);
        Task SetIconNameAsync(string Icon, bool Interactive);
        Task SetChassisAsync(string Chassis, bool Interactive);
        Task SetDeploymentAsync(string Deployment, bool Interactive);
        Task SetLocationAsync(string Location, bool Interactive);
        Task<byte[]> GetProductUUIDAsync(bool Interactive);
        Task<string> DescribeAsync();
        Task<T> GetAsync<T>(string prop);
        Task<Hostname1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class Hostname1Properties
    {
        private string _Hostname = default(string);
        public string Hostname
        {
            get
            {
                return _Hostname;
            }

            set
            {
                _Hostname = (value);
            }
        }

        private string _StaticHostname = default(string);
        public string StaticHostname
        {
            get
            {
                return _StaticHostname;
            }

            set
            {
                _StaticHostname = (value);
            }
        }

        private string _PrettyHostname = default(string);
        public string PrettyHostname
        {
            get
            {
                return _PrettyHostname;
            }

            set
            {
                _PrettyHostname = (value);
            }
        }

        private string _DefaultHostname = default(string);
        public string DefaultHostname
        {
            get
            {
                return _DefaultHostname;
            }

            set
            {
                _DefaultHostname = (value);
            }
        }

        private string _HostnameSource = default(string);
        public string HostnameSource
        {
            get
            {
                return _HostnameSource;
            }

            set
            {
                _HostnameSource = (value);
            }
        }

        private string _IconName = default(string);
        public string IconName
        {
            get
            {
                return _IconName;
            }

            set
            {
                _IconName = (value);
            }
        }

        private string _Chassis = default(string);
        public string Chassis
        {
            get
            {
                return _Chassis;
            }

            set
            {
                _Chassis = (value);
            }
        }

        private string _Deployment = default(string);
        public string Deployment
        {
            get
            {
                return _Deployment;
            }

            set
            {
                _Deployment = (value);
            }
        }

        private string _Location = default(string);
        public string Location
        {
            get
            {
                return _Location;
            }

            set
            {
                _Location = (value);
            }
        }

        private string _KernelName = default(string);
        public string KernelName
        {
            get
            {
                return _KernelName;
            }

            set
            {
                _KernelName = (value);
            }
        }

        private string _KernelRelease = default(string);
        public string KernelRelease
        {
            get
            {
                return _KernelRelease;
            }

            set
            {
                _KernelRelease = (value);
            }
        }

        private string _KernelVersion = default(string);
        public string KernelVersion
        {
            get
            {
                return _KernelVersion;
            }

            set
            {
                _KernelVersion = (value);
            }
        }

        private string _OperatingSystemPrettyName = default(string);
        public string OperatingSystemPrettyName
        {
            get
            {
                return _OperatingSystemPrettyName;
            }

            set
            {
                _OperatingSystemPrettyName = (value);
            }
        }

        private string _OperatingSystemCPEName = default(string);
        public string OperatingSystemCPEName
        {
            get
            {
                return _OperatingSystemCPEName;
            }

            set
            {
                _OperatingSystemCPEName = (value);
            }
        }

        private string _HomeURL = default(string);
        public string HomeURL
        {
            get
            {
                return _HomeURL;
            }

            set
            {
                _HomeURL = (value);
            }
        }

        private string _HardwareVendor = default(string);
        public string HardwareVendor
        {
            get
            {
                return _HardwareVendor;
            }

            set
            {
                _HardwareVendor = (value);
            }
        }

        private string _HardwareModel = default(string);
        public string HardwareModel
        {
            get
            {
                return _HardwareModel;
            }

            set
            {
                _HardwareModel = (value);
            }
        }
    }

    static class Hostname1Extensions
    {
        public static Task<string> GetHostnameAsync(this IHostname1 o) => o.GetAsync<string>("Hostname");
        public static Task<string> GetStaticHostnameAsync(this IHostname1 o) => o.GetAsync<string>("StaticHostname");
        public static Task<string> GetPrettyHostnameAsync(this IHostname1 o) => o.GetAsync<string>("PrettyHostname");
        public static Task<string> GetDefaultHostnameAsync(this IHostname1 o) => o.GetAsync<string>("DefaultHostname");
        public static Task<string> GetHostnameSourceAsync(this IHostname1 o) => o.GetAsync<string>("HostnameSource");
        public static Task<string> GetIconNameAsync(this IHostname1 o) => o.GetAsync<string>("IconName");
        public static Task<string> GetChassisAsync(this IHostname1 o) => o.GetAsync<string>("Chassis");
        public static Task<string> GetDeploymentAsync(this IHostname1 o) => o.GetAsync<string>("Deployment");
        public static Task<string> GetLocationAsync(this IHostname1 o) => o.GetAsync<string>("Location");
        public static Task<string> GetKernelNameAsync(this IHostname1 o) => o.GetAsync<string>("KernelName");
        public static Task<string> GetKernelReleaseAsync(this IHostname1 o) => o.GetAsync<string>("KernelRelease");
        public static Task<string> GetKernelVersionAsync(this IHostname1 o) => o.GetAsync<string>("KernelVersion");
        public static Task<string> GetOperatingSystemPrettyNameAsync(this IHostname1 o) => o.GetAsync<string>("OperatingSystemPrettyName");
        public static Task<string> GetOperatingSystemCPENameAsync(this IHostname1 o) => o.GetAsync<string>("OperatingSystemCPEName");
        public static Task<string> GetHomeURLAsync(this IHostname1 o) => o.GetAsync<string>("HomeURL");
        public static Task<string> GetHardwareVendorAsync(this IHostname1 o) => o.GetAsync<string>("HardwareVendor");
        public static Task<string> GetHardwareModelAsync(this IHostname1 o) => o.GetAsync<string>("HardwareModel");
    }

    [DBusInterface("org.freedesktop.LogControl1")]
    interface ILogControl1 : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<LogControl1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class LogControl1Properties
    {
        private string _LogLevel = default(string);
        public string LogLevel
        {
            get
            {
                return _LogLevel;
            }

            set
            {
                _LogLevel = (value);
            }
        }

        private string _LogTarget = default(string);
        public string LogTarget
        {
            get
            {
                return _LogTarget;
            }

            set
            {
                _LogTarget = (value);
            }
        }

        private string _SyslogIdentifier = default(string);
        public string SyslogIdentifier
        {
            get
            {
                return _SyslogIdentifier;
            }

            set
            {
                _SyslogIdentifier = (value);
            }
        }
    }

    static class LogControl1Extensions
    {
        public static Task<string> GetLogLevelAsync(this ILogControl1 o) => o.GetAsync<string>("LogLevel");
        public static Task<string> GetLogTargetAsync(this ILogControl1 o) => o.GetAsync<string>("LogTarget");
        public static Task<string> GetSyslogIdentifierAsync(this ILogControl1 o) => o.GetAsync<string>("SyslogIdentifier");
        public static Task SetLogLevelAsync(this ILogControl1 o, string val) => o.SetAsync("LogLevel", val);
        public static Task SetLogTargetAsync(this ILogControl1 o, string val) => o.SetAsync("LogTarget", val);
    }
}