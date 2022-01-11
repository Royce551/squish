using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace timedate1.DBus
{
    [DBusInterface("org.freedesktop.timedate1")]
    interface ITimedate1 : IDBusObject
    {
        Task SetTimeAsync(long UsecUtc, bool Relative, bool Interactive);
        Task SetTimezoneAsync(string Timezone, bool Interactive);
        Task SetLocalRTCAsync(bool LocalRtc, bool FixSystem, bool Interactive);
        Task SetNTPAsync(bool UseNtp, bool Interactive);
        Task<string[]> ListTimezonesAsync();
        Task<T> GetAsync<T>(string prop);
        Task<Timedate1Properties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class Timedate1Properties
    {
        private string _Timezone = default(string);
        public string Timezone
        {
            get
            {
                return _Timezone;
            }

            set
            {
                _Timezone = (value);
            }
        }

        private bool _LocalRTC = default(bool);
        public bool LocalRTC
        {
            get
            {
                return _LocalRTC;
            }

            set
            {
                _LocalRTC = (value);
            }
        }

        private bool _CanNTP = default(bool);
        public bool CanNTP
        {
            get
            {
                return _CanNTP;
            }

            set
            {
                _CanNTP = (value);
            }
        }

        private bool _NTP = default(bool);
        public bool NTP
        {
            get
            {
                return _NTP;
            }

            set
            {
                _NTP = (value);
            }
        }

        private bool _NTPSynchronized = default(bool);
        public bool NTPSynchronized
        {
            get
            {
                return _NTPSynchronized;
            }

            set
            {
                _NTPSynchronized = (value);
            }
        }

        private ulong _TimeUSec = default(ulong);
        public ulong TimeUSec
        {
            get
            {
                return _TimeUSec;
            }

            set
            {
                _TimeUSec = (value);
            }
        }

        private ulong _RTCTimeUSec = default(ulong);
        public ulong RTCTimeUSec
        {
            get
            {
                return _RTCTimeUSec;
            }

            set
            {
                _RTCTimeUSec = (value);
            }
        }
    }

    static class Timedate1Extensions
    {
        public static Task<string> GetTimezoneAsync(this ITimedate1 o) => o.GetAsync<string>("Timezone");
        public static Task<bool> GetLocalRTCAsync(this ITimedate1 o) => o.GetAsync<bool>("LocalRTC");
        public static Task<bool> GetCanNTPAsync(this ITimedate1 o) => o.GetAsync<bool>("CanNTP");
        public static Task<bool> GetNTPAsync(this ITimedate1 o) => o.GetAsync<bool>("NTP");
        public static Task<bool> GetNTPSynchronizedAsync(this ITimedate1 o) => o.GetAsync<bool>("NTPSynchronized");
        public static Task<ulong> GetTimeUSecAsync(this ITimedate1 o) => o.GetAsync<ulong>("TimeUSec");
        public static Task<ulong> GetRTCTimeUSecAsync(this ITimedate1 o) => o.GetAsync<ulong>("RTCTimeUSec");
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