using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

#pragma warning disable
// ReSharper disable All

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace login1.DBus
{
    [DBusInterface("org.freedesktop.login1.Manager")]
    interface IManager : IDBusObject
    {
        Task<ObjectPath> GetSessionAsync(string SessionId);
        Task<ObjectPath> GetSessionByPIDAsync(uint Pid);
        Task<ObjectPath> GetUserAsync(uint Uid);
        Task<ObjectPath> GetUserByPIDAsync(uint Pid);
        Task<ObjectPath> GetSeatAsync(string SeatId);
        Task<(string, uint, string, string, ObjectPath)[]> ListSessionsAsync();
        Task<(uint, string, ObjectPath)[]> ListUsersAsync();
        Task<(string, ObjectPath)[]> ListSeatsAsync();
        Task<(string, string, string, string, uint, uint)[]> ListInhibitorsAsync();
        Task<(string sessionId, ObjectPath objectPath, string runtimePath, CloseSafeHandle fifoFd, uint uid, string seatId, uint vtnr, bool existing)> CreateSessionAsync(uint Uid, uint Pid, string Service, string Type, string Class, string Desktop, string SeatId, uint Vtnr, string Tty, string Display, bool Remote, string RemoteUser, string RemoteHost, (string, object)[] Properties);
        Task ReleaseSessionAsync(string SessionId);
        Task ActivateSessionAsync(string SessionId);
        Task ActivateSessionOnSeatAsync(string SessionId, string SeatId);
        Task LockSessionAsync(string SessionId);
        Task UnlockSessionAsync(string SessionId);
        Task LockSessionsAsync();
        Task UnlockSessionsAsync();
        Task KillSessionAsync(string SessionId, string Who, int SignalNumber);
        Task KillUserAsync(uint Uid, int SignalNumber);
        Task TerminateSessionAsync(string SessionId);
        Task TerminateUserAsync(uint Uid);
        Task TerminateSeatAsync(string SeatId);
        Task SetUserLingerAsync(uint Uid, bool Enable, bool Interactive);
        Task AttachDeviceAsync(string SeatId, string SysfsPath, bool Interactive);
        Task FlushDevicesAsync(bool Interactive);
        Task PowerOffAsync(bool Interactive);
        Task PowerOffWithFlagsAsync(ulong Flags);
        Task RebootAsync(bool Interactive);
        Task RebootWithFlagsAsync(ulong Flags);
        Task HaltAsync(bool Interactive);
        Task HaltWithFlagsAsync(ulong Flags);
        Task SuspendAsync(bool Interactive);
        Task SuspendWithFlagsAsync(ulong Flags);
        Task HibernateAsync(bool Interactive);
        Task HibernateWithFlagsAsync(ulong Flags);
        Task HybridSleepAsync(bool Interactive);
        Task HybridSleepWithFlagsAsync(ulong Flags);
        Task SuspendThenHibernateAsync(bool Interactive);
        Task SuspendThenHibernateWithFlagsAsync(ulong Flags);
        Task<string> CanPowerOffAsync();
        Task<string> CanRebootAsync();
        Task<string> CanHaltAsync();
        Task<string> CanSuspendAsync();
        Task<string> CanHibernateAsync();
        Task<string> CanHybridSleepAsync();
        Task<string> CanSuspendThenHibernateAsync();
        Task ScheduleShutdownAsync(string Type, ulong Usec);
        Task<bool> CancelScheduledShutdownAsync();
        Task<CloseSafeHandle> InhibitAsync(string What, string Who, string Why, string Mode);
        Task<string> CanRebootParameterAsync();
        Task SetRebootParameterAsync(string Parameter);
        Task<string> CanRebootToFirmwareSetupAsync();
        Task SetRebootToFirmwareSetupAsync(bool Enable);
        Task<string> CanRebootToBootLoaderMenuAsync();
        Task SetRebootToBootLoaderMenuAsync(ulong Timeout);
        Task<string> CanRebootToBootLoaderEntryAsync();
        Task SetRebootToBootLoaderEntryAsync(string BootLoaderEntry);
        Task SetWallMessageAsync(string WallMessage, bool Enable);
        Task<IDisposable> WatchSessionNewAsync(Action<(string sessionId, ObjectPath objectPath)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchSessionRemovedAsync(Action<(string sessionId, ObjectPath objectPath)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchUserNewAsync(Action<(uint uid, ObjectPath objectPath)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchUserRemovedAsync(Action<(uint uid, ObjectPath objectPath)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchSeatNewAsync(Action<(string seatId, ObjectPath objectPath)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchSeatRemovedAsync(Action<(string seatId, ObjectPath objectPath)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchPrepareForShutdownAsync(Action<bool> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchPrepareForSleepAsync(Action<bool> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<ManagerProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class ManagerProperties
    {
        private bool _EnableWallMessages = default(bool);
        public bool EnableWallMessages
        {
            get
            {
                return _EnableWallMessages;
            }

            set
            {
                _EnableWallMessages = (value);
            }
        }

        private string _WallMessage = default(string);
        public string WallMessage
        {
            get
            {
                return _WallMessage;
            }

            set
            {
                _WallMessage = (value);
            }
        }

        private uint _NAutoVTs = default(uint);
        public uint NAutoVTs
        {
            get
            {
                return _NAutoVTs;
            }

            set
            {
                _NAutoVTs = (value);
            }
        }

        private string[] _KillOnlyUsers = default(string[]);
        public string[] KillOnlyUsers
        {
            get
            {
                return _KillOnlyUsers;
            }

            set
            {
                _KillOnlyUsers = (value);
            }
        }

        private string[] _KillExcludeUsers = default(string[]);
        public string[] KillExcludeUsers
        {
            get
            {
                return _KillExcludeUsers;
            }

            set
            {
                _KillExcludeUsers = (value);
            }
        }

        private bool _KillUserProcesses = default(bool);
        public bool KillUserProcesses
        {
            get
            {
                return _KillUserProcesses;
            }

            set
            {
                _KillUserProcesses = (value);
            }
        }

        private string _RebootParameter = default(string);
        public string RebootParameter
        {
            get
            {
                return _RebootParameter;
            }

            set
            {
                _RebootParameter = (value);
            }
        }

        private bool _RebootToFirmwareSetup = default(bool);
        public bool RebootToFirmwareSetup
        {
            get
            {
                return _RebootToFirmwareSetup;
            }

            set
            {
                _RebootToFirmwareSetup = (value);
            }
        }

        private ulong _RebootToBootLoaderMenu = default(ulong);
        public ulong RebootToBootLoaderMenu
        {
            get
            {
                return _RebootToBootLoaderMenu;
            }

            set
            {
                _RebootToBootLoaderMenu = (value);
            }
        }

        private string _RebootToBootLoaderEntry = default(string);
        public string RebootToBootLoaderEntry
        {
            get
            {
                return _RebootToBootLoaderEntry;
            }

            set
            {
                _RebootToBootLoaderEntry = (value);
            }
        }

        private string[] _BootLoaderEntries = default(string[]);
        public string[] BootLoaderEntries
        {
            get
            {
                return _BootLoaderEntries;
            }

            set
            {
                _BootLoaderEntries = (value);
            }
        }

        private bool _IdleHint = default(bool);
        public bool IdleHint
        {
            get
            {
                return _IdleHint;
            }

            set
            {
                _IdleHint = (value);
            }
        }

        private ulong _IdleSinceHint = default(ulong);
        public ulong IdleSinceHint
        {
            get
            {
                return _IdleSinceHint;
            }

            set
            {
                _IdleSinceHint = (value);
            }
        }

        private ulong _IdleSinceHintMonotonic = default(ulong);
        public ulong IdleSinceHintMonotonic
        {
            get
            {
                return _IdleSinceHintMonotonic;
            }

            set
            {
                _IdleSinceHintMonotonic = (value);
            }
        }

        private string _BlockInhibited = default(string);
        public string BlockInhibited
        {
            get
            {
                return _BlockInhibited;
            }

            set
            {
                _BlockInhibited = (value);
            }
        }

        private string _DelayInhibited = default(string);
        public string DelayInhibited
        {
            get
            {
                return _DelayInhibited;
            }

            set
            {
                _DelayInhibited = (value);
            }
        }

        private ulong _InhibitDelayMaxUSec = default(ulong);
        public ulong InhibitDelayMaxUSec
        {
            get
            {
                return _InhibitDelayMaxUSec;
            }

            set
            {
                _InhibitDelayMaxUSec = (value);
            }
        }

        private ulong _UserStopDelayUSec = default(ulong);
        public ulong UserStopDelayUSec
        {
            get
            {
                return _UserStopDelayUSec;
            }

            set
            {
                _UserStopDelayUSec = (value);
            }
        }

        private string _HandlePowerKey = default(string);
        public string HandlePowerKey
        {
            get
            {
                return _HandlePowerKey;
            }

            set
            {
                _HandlePowerKey = (value);
            }
        }

        private string _HandleSuspendKey = default(string);
        public string HandleSuspendKey
        {
            get
            {
                return _HandleSuspendKey;
            }

            set
            {
                _HandleSuspendKey = (value);
            }
        }

        private string _HandleHibernateKey = default(string);
        public string HandleHibernateKey
        {
            get
            {
                return _HandleHibernateKey;
            }

            set
            {
                _HandleHibernateKey = (value);
            }
        }

        private string _HandleLidSwitch = default(string);
        public string HandleLidSwitch
        {
            get
            {
                return _HandleLidSwitch;
            }

            set
            {
                _HandleLidSwitch = (value);
            }
        }

        private string _HandleLidSwitchExternalPower = default(string);
        public string HandleLidSwitchExternalPower
        {
            get
            {
                return _HandleLidSwitchExternalPower;
            }

            set
            {
                _HandleLidSwitchExternalPower = (value);
            }
        }

        private string _HandleLidSwitchDocked = default(string);
        public string HandleLidSwitchDocked
        {
            get
            {
                return _HandleLidSwitchDocked;
            }

            set
            {
                _HandleLidSwitchDocked = (value);
            }
        }

        private ulong _HoldoffTimeoutUSec = default(ulong);
        public ulong HoldoffTimeoutUSec
        {
            get
            {
                return _HoldoffTimeoutUSec;
            }

            set
            {
                _HoldoffTimeoutUSec = (value);
            }
        }

        private string _IdleAction = default(string);
        public string IdleAction
        {
            get
            {
                return _IdleAction;
            }

            set
            {
                _IdleAction = (value);
            }
        }

        private ulong _IdleActionUSec = default(ulong);
        public ulong IdleActionUSec
        {
            get
            {
                return _IdleActionUSec;
            }

            set
            {
                _IdleActionUSec = (value);
            }
        }

        private bool _PreparingForShutdown = default(bool);
        public bool PreparingForShutdown
        {
            get
            {
                return _PreparingForShutdown;
            }

            set
            {
                _PreparingForShutdown = (value);
            }
        }

        private bool _PreparingForSleep = default(bool);
        public bool PreparingForSleep
        {
            get
            {
                return _PreparingForSleep;
            }

            set
            {
                _PreparingForSleep = (value);
            }
        }

        private (string, ulong) _ScheduledShutdown = default((string, ulong));
        public (string, ulong) ScheduledShutdown
        {
            get
            {
                return _ScheduledShutdown;
            }

            set
            {
                _ScheduledShutdown = (value);
            }
        }

        private bool _Docked = default(bool);
        public bool Docked
        {
            get
            {
                return _Docked;
            }

            set
            {
                _Docked = (value);
            }
        }

        private bool _LidClosed = default(bool);
        public bool LidClosed
        {
            get
            {
                return _LidClosed;
            }

            set
            {
                _LidClosed = (value);
            }
        }

        private bool _OnExternalPower = default(bool);
        public bool OnExternalPower
        {
            get
            {
                return _OnExternalPower;
            }

            set
            {
                _OnExternalPower = (value);
            }
        }

        private bool _RemoveIPC = default(bool);
        public bool RemoveIPC
        {
            get
            {
                return _RemoveIPC;
            }

            set
            {
                _RemoveIPC = (value);
            }
        }

        private ulong _RuntimeDirectorySize = default(ulong);
        public ulong RuntimeDirectorySize
        {
            get
            {
                return _RuntimeDirectorySize;
            }

            set
            {
                _RuntimeDirectorySize = (value);
            }
        }

        private ulong _RuntimeDirectoryInodesMax = default(ulong);
        public ulong RuntimeDirectoryInodesMax
        {
            get
            {
                return _RuntimeDirectoryInodesMax;
            }

            set
            {
                _RuntimeDirectoryInodesMax = (value);
            }
        }

        private ulong _InhibitorsMax = default(ulong);
        public ulong InhibitorsMax
        {
            get
            {
                return _InhibitorsMax;
            }

            set
            {
                _InhibitorsMax = (value);
            }
        }

        private ulong _NCurrentInhibitors = default(ulong);
        public ulong NCurrentInhibitors
        {
            get
            {
                return _NCurrentInhibitors;
            }

            set
            {
                _NCurrentInhibitors = (value);
            }
        }

        private ulong _SessionsMax = default(ulong);
        public ulong SessionsMax
        {
            get
            {
                return _SessionsMax;
            }

            set
            {
                _SessionsMax = (value);
            }
        }

        private ulong _NCurrentSessions = default(ulong);
        public ulong NCurrentSessions
        {
            get
            {
                return _NCurrentSessions;
            }

            set
            {
                _NCurrentSessions = (value);
            }
        }
    }

    static class ManagerExtensions
    {
        public static Task<bool> GetEnableWallMessagesAsync(this IManager o) => o.GetAsync<bool>("EnableWallMessages");
        public static Task<string> GetWallMessageAsync(this IManager o) => o.GetAsync<string>("WallMessage");
        public static Task<uint> GetNAutoVTsAsync(this IManager o) => o.GetAsync<uint>("NAutoVTs");
        public static Task<string[]> GetKillOnlyUsersAsync(this IManager o) => o.GetAsync<string[]>("KillOnlyUsers");
        public static Task<string[]> GetKillExcludeUsersAsync(this IManager o) => o.GetAsync<string[]>("KillExcludeUsers");
        public static Task<bool> GetKillUserProcessesAsync(this IManager o) => o.GetAsync<bool>("KillUserProcesses");
        public static Task<string> GetRebootParameterAsync(this IManager o) => o.GetAsync<string>("RebootParameter");
        public static Task<bool> GetRebootToFirmwareSetupAsync(this IManager o) => o.GetAsync<bool>("RebootToFirmwareSetup");
        public static Task<ulong> GetRebootToBootLoaderMenuAsync(this IManager o) => o.GetAsync<ulong>("RebootToBootLoaderMenu");
        public static Task<string> GetRebootToBootLoaderEntryAsync(this IManager o) => o.GetAsync<string>("RebootToBootLoaderEntry");
        public static Task<string[]> GetBootLoaderEntriesAsync(this IManager o) => o.GetAsync<string[]>("BootLoaderEntries");
        public static Task<bool> GetIdleHintAsync(this IManager o) => o.GetAsync<bool>("IdleHint");
        public static Task<ulong> GetIdleSinceHintAsync(this IManager o) => o.GetAsync<ulong>("IdleSinceHint");
        public static Task<ulong> GetIdleSinceHintMonotonicAsync(this IManager o) => o.GetAsync<ulong>("IdleSinceHintMonotonic");
        public static Task<string> GetBlockInhibitedAsync(this IManager o) => o.GetAsync<string>("BlockInhibited");
        public static Task<string> GetDelayInhibitedAsync(this IManager o) => o.GetAsync<string>("DelayInhibited");
        public static Task<ulong> GetInhibitDelayMaxUSecAsync(this IManager o) => o.GetAsync<ulong>("InhibitDelayMaxUSec");
        public static Task<ulong> GetUserStopDelayUSecAsync(this IManager o) => o.GetAsync<ulong>("UserStopDelayUSec");
        public static Task<string> GetHandlePowerKeyAsync(this IManager o) => o.GetAsync<string>("HandlePowerKey");
        public static Task<string> GetHandleSuspendKeyAsync(this IManager o) => o.GetAsync<string>("HandleSuspendKey");
        public static Task<string> GetHandleHibernateKeyAsync(this IManager o) => o.GetAsync<string>("HandleHibernateKey");
        public static Task<string> GetHandleLidSwitchAsync(this IManager o) => o.GetAsync<string>("HandleLidSwitch");
        public static Task<string> GetHandleLidSwitchExternalPowerAsync(this IManager o) => o.GetAsync<string>("HandleLidSwitchExternalPower");
        public static Task<string> GetHandleLidSwitchDockedAsync(this IManager o) => o.GetAsync<string>("HandleLidSwitchDocked");
        public static Task<ulong> GetHoldoffTimeoutUSecAsync(this IManager o) => o.GetAsync<ulong>("HoldoffTimeoutUSec");
        public static Task<string> GetIdleActionAsync(this IManager o) => o.GetAsync<string>("IdleAction");
        public static Task<ulong> GetIdleActionUSecAsync(this IManager o) => o.GetAsync<ulong>("IdleActionUSec");
        public static Task<bool> GetPreparingForShutdownAsync(this IManager o) => o.GetAsync<bool>("PreparingForShutdown");
        public static Task<bool> GetPreparingForSleepAsync(this IManager o) => o.GetAsync<bool>("PreparingForSleep");
        public static Task<(string, ulong)> GetScheduledShutdownAsync(this IManager o) => o.GetAsync<(string, ulong)>("ScheduledShutdown");
        public static Task<bool> GetDockedAsync(this IManager o) => o.GetAsync<bool>("Docked");
        public static Task<bool> GetLidClosedAsync(this IManager o) => o.GetAsync<bool>("LidClosed");
        public static Task<bool> GetOnExternalPowerAsync(this IManager o) => o.GetAsync<bool>("OnExternalPower");
        public static Task<bool> GetRemoveIPCAsync(this IManager o) => o.GetAsync<bool>("RemoveIPC");
        public static Task<ulong> GetRuntimeDirectorySizeAsync(this IManager o) => o.GetAsync<ulong>("RuntimeDirectorySize");
        public static Task<ulong> GetRuntimeDirectoryInodesMaxAsync(this IManager o) => o.GetAsync<ulong>("RuntimeDirectoryInodesMax");
        public static Task<ulong> GetInhibitorsMaxAsync(this IManager o) => o.GetAsync<ulong>("InhibitorsMax");
        public static Task<ulong> GetNCurrentInhibitorsAsync(this IManager o) => o.GetAsync<ulong>("NCurrentInhibitors");
        public static Task<ulong> GetSessionsMaxAsync(this IManager o) => o.GetAsync<ulong>("SessionsMax");
        public static Task<ulong> GetNCurrentSessionsAsync(this IManager o) => o.GetAsync<ulong>("NCurrentSessions");
        public static Task SetEnableWallMessagesAsync(this IManager o, bool val) => o.SetAsync("EnableWallMessages", val);
        public static Task SetWallMessageAsync(this IManager o, string val) => o.SetAsync("WallMessage", val);
    }

    [DBusInterface("org.freedesktop.login1.Seat")]
    interface ISeat : IDBusObject
    {
        Task TerminateAsync();
        Task ActivateSessionAsync(string SessionId);
        Task SwitchToAsync(uint Vtnr);
        Task SwitchToNextAsync();
        Task SwitchToPreviousAsync();
        Task<T> GetAsync<T>(string prop);
        Task<SeatProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SeatProperties
    {
        private string _Id = default(string);
        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = (value);
            }
        }

        private (string, ObjectPath) _ActiveSession = default((string, ObjectPath));
        public (string, ObjectPath) ActiveSession
        {
            get
            {
                return _ActiveSession;
            }

            set
            {
                _ActiveSession = (value);
            }
        }

        private bool _CanTTY = default(bool);
        public bool CanTTY
        {
            get
            {
                return _CanTTY;
            }

            set
            {
                _CanTTY = (value);
            }
        }

        private bool _CanGraphical = default(bool);
        public bool CanGraphical
        {
            get
            {
                return _CanGraphical;
            }

            set
            {
                _CanGraphical = (value);
            }
        }

        private (string, ObjectPath)[] _Sessions = default((string, ObjectPath)[]);
        public (string, ObjectPath)[] Sessions
        {
            get
            {
                return _Sessions;
            }

            set
            {
                _Sessions = (value);
            }
        }

        private bool _IdleHint = default(bool);
        public bool IdleHint
        {
            get
            {
                return _IdleHint;
            }

            set
            {
                _IdleHint = (value);
            }
        }

        private ulong _IdleSinceHint = default(ulong);
        public ulong IdleSinceHint
        {
            get
            {
                return _IdleSinceHint;
            }

            set
            {
                _IdleSinceHint = (value);
            }
        }

        private ulong _IdleSinceHintMonotonic = default(ulong);
        public ulong IdleSinceHintMonotonic
        {
            get
            {
                return _IdleSinceHintMonotonic;
            }

            set
            {
                _IdleSinceHintMonotonic = (value);
            }
        }
    }

    static class SeatExtensions
    {
        public static Task<string> GetIdAsync(this ISeat o) => o.GetAsync<string>("Id");
        public static Task<(string, ObjectPath)> GetActiveSessionAsync(this ISeat o) => o.GetAsync<(string, ObjectPath)>("ActiveSession");
        public static Task<bool> GetCanTTYAsync(this ISeat o) => o.GetAsync<bool>("CanTTY");
        public static Task<bool> GetCanGraphicalAsync(this ISeat o) => o.GetAsync<bool>("CanGraphical");
        public static Task<(string, ObjectPath)[]> GetSessionsAsync(this ISeat o) => o.GetAsync<(string, ObjectPath)[]>("Sessions");
        public static Task<bool> GetIdleHintAsync(this ISeat o) => o.GetAsync<bool>("IdleHint");
        public static Task<ulong> GetIdleSinceHintAsync(this ISeat o) => o.GetAsync<ulong>("IdleSinceHint");
        public static Task<ulong> GetIdleSinceHintMonotonicAsync(this ISeat o) => o.GetAsync<ulong>("IdleSinceHintMonotonic");
    }

    [DBusInterface("org.freedesktop.login1.Session")]
    interface ISession : IDBusObject
    {
        Task TerminateAsync();
        Task ActivateAsync();
        Task LockAsync();
        Task UnlockAsync();
        Task SetIdleHintAsync(bool Idle);
        Task SetLockedHintAsync(bool Locked);
        Task KillAsync(string Who, int SignalNumber);
        Task TakeControlAsync(bool Force);
        Task ReleaseControlAsync();
        Task SetTypeAsync(string Type);
        Task<(CloseSafeHandle fd, bool inactive)> TakeDeviceAsync(uint Major, uint Minor);
        Task ReleaseDeviceAsync(uint Major, uint Minor);
        Task PauseDeviceCompleteAsync(uint Major, uint Minor);
        Task SetBrightnessAsync(string Subsystem, string Name, uint Brightness);
        Task<IDisposable> WatchPauseDeviceAsync(Action<(uint major, uint minor, string type)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchResumeDeviceAsync(Action<(uint major, uint minor, CloseSafeHandle fd)> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchLockAsync(Action handler, Action<Exception> onError = null);
        Task<IDisposable> WatchUnlockAsync(Action handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<SessionProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class SessionProperties
    {
        private string _Id = default(string);
        public string Id
        {
            get
            {
                return _Id;
            }

            set
            {
                _Id = (value);
            }
        }

        private (uint, ObjectPath) _User = default((uint, ObjectPath));
        public (uint, ObjectPath) User
        {
            get
            {
                return _User;
            }

            set
            {
                _User = (value);
            }
        }

        private string _Name = default(string);
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = (value);
            }
        }

        private ulong _Timestamp = default(ulong);
        public ulong Timestamp
        {
            get
            {
                return _Timestamp;
            }

            set
            {
                _Timestamp = (value);
            }
        }

        private ulong _TimestampMonotonic = default(ulong);
        public ulong TimestampMonotonic
        {
            get
            {
                return _TimestampMonotonic;
            }

            set
            {
                _TimestampMonotonic = (value);
            }
        }

        private uint _VTNr = default(uint);
        public uint VTNr
        {
            get
            {
                return _VTNr;
            }

            set
            {
                _VTNr = (value);
            }
        }

        private (string, ObjectPath) _Seat = default((string, ObjectPath));
        public (string, ObjectPath) Seat
        {
            get
            {
                return _Seat;
            }

            set
            {
                _Seat = (value);
            }
        }

        private string _TTY = default(string);
        public string TTY
        {
            get
            {
                return _TTY;
            }

            set
            {
                _TTY = (value);
            }
        }

        private string _Display = default(string);
        public string Display
        {
            get
            {
                return _Display;
            }

            set
            {
                _Display = (value);
            }
        }

        private bool _Remote = default(bool);
        public bool Remote
        {
            get
            {
                return _Remote;
            }

            set
            {
                _Remote = (value);
            }
        }

        private string _RemoteHost = default(string);
        public string RemoteHost
        {
            get
            {
                return _RemoteHost;
            }

            set
            {
                _RemoteHost = (value);
            }
        }

        private string _RemoteUser = default(string);
        public string RemoteUser
        {
            get
            {
                return _RemoteUser;
            }

            set
            {
                _RemoteUser = (value);
            }
        }

        private string _Service = default(string);
        public string Service
        {
            get
            {
                return _Service;
            }

            set
            {
                _Service = (value);
            }
        }

        private string _Desktop = default(string);
        public string Desktop
        {
            get
            {
                return _Desktop;
            }

            set
            {
                _Desktop = (value);
            }
        }

        private string _Scope = default(string);
        public string Scope
        {
            get
            {
                return _Scope;
            }

            set
            {
                _Scope = (value);
            }
        }

        private uint _Leader = default(uint);
        public uint Leader
        {
            get
            {
                return _Leader;
            }

            set
            {
                _Leader = (value);
            }
        }

        private uint _Audit = default(uint);
        public uint Audit
        {
            get
            {
                return _Audit;
            }

            set
            {
                _Audit = (value);
            }
        }

        private string _Type = default(string);
        public string Type
        {
            get
            {
                return _Type;
            }

            set
            {
                _Type = (value);
            }
        }

        private string _Class = default(string);
        public string Class
        {
            get
            {
                return _Class;
            }

            set
            {
                _Class = (value);
            }
        }

        private bool _Active = default(bool);
        public bool Active
        {
            get
            {
                return _Active;
            }

            set
            {
                _Active = (value);
            }
        }

        private string _State = default(string);
        public string State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = (value);
            }
        }

        private bool _IdleHint = default(bool);
        public bool IdleHint
        {
            get
            {
                return _IdleHint;
            }

            set
            {
                _IdleHint = (value);
            }
        }

        private ulong _IdleSinceHint = default(ulong);
        public ulong IdleSinceHint
        {
            get
            {
                return _IdleSinceHint;
            }

            set
            {
                _IdleSinceHint = (value);
            }
        }

        private ulong _IdleSinceHintMonotonic = default(ulong);
        public ulong IdleSinceHintMonotonic
        {
            get
            {
                return _IdleSinceHintMonotonic;
            }

            set
            {
                _IdleSinceHintMonotonic = (value);
            }
        }

        private bool _LockedHint = default(bool);
        public bool LockedHint
        {
            get
            {
                return _LockedHint;
            }

            set
            {
                _LockedHint = (value);
            }
        }
    }

    static class SessionExtensions
    {
        public static Task<string> GetIdAsync(this ISession o) => o.GetAsync<string>("Id");
        public static Task<(uint, ObjectPath)> GetUserAsync(this ISession o) => o.GetAsync<(uint, ObjectPath)>("User");
        public static Task<string> GetNameAsync(this ISession o) => o.GetAsync<string>("Name");
        public static Task<ulong> GetTimestampAsync(this ISession o) => o.GetAsync<ulong>("Timestamp");
        public static Task<ulong> GetTimestampMonotonicAsync(this ISession o) => o.GetAsync<ulong>("TimestampMonotonic");
        public static Task<uint> GetVTNrAsync(this ISession o) => o.GetAsync<uint>("VTNr");
        public static Task<(string, ObjectPath)> GetSeatAsync(this ISession o) => o.GetAsync<(string, ObjectPath)>("Seat");
        public static Task<string> GetTTYAsync(this ISession o) => o.GetAsync<string>("TTY");
        public static Task<string> GetDisplayAsync(this ISession o) => o.GetAsync<string>("Display");
        public static Task<bool> GetRemoteAsync(this ISession o) => o.GetAsync<bool>("Remote");
        public static Task<string> GetRemoteHostAsync(this ISession o) => o.GetAsync<string>("RemoteHost");
        public static Task<string> GetRemoteUserAsync(this ISession o) => o.GetAsync<string>("RemoteUser");
        public static Task<string> GetServiceAsync(this ISession o) => o.GetAsync<string>("Service");
        public static Task<string> GetDesktopAsync(this ISession o) => o.GetAsync<string>("Desktop");
        public static Task<string> GetScopeAsync(this ISession o) => o.GetAsync<string>("Scope");
        public static Task<uint> GetLeaderAsync(this ISession o) => o.GetAsync<uint>("Leader");
        public static Task<uint> GetAuditAsync(this ISession o) => o.GetAsync<uint>("Audit");
        public static Task<string> GetTypeAsync(this ISession o) => o.GetAsync<string>("Type");
        public static Task<string> GetClassAsync(this ISession o) => o.GetAsync<string>("Class");
        public static Task<bool> GetActiveAsync(this ISession o) => o.GetAsync<bool>("Active");
        public static Task<string> GetStateAsync(this ISession o) => o.GetAsync<string>("State");
        public static Task<bool> GetIdleHintAsync(this ISession o) => o.GetAsync<bool>("IdleHint");
        public static Task<ulong> GetIdleSinceHintAsync(this ISession o) => o.GetAsync<ulong>("IdleSinceHint");
        public static Task<ulong> GetIdleSinceHintMonotonicAsync(this ISession o) => o.GetAsync<ulong>("IdleSinceHintMonotonic");
        public static Task<bool> GetLockedHintAsync(this ISession o) => o.GetAsync<bool>("LockedHint");
    }

    [DBusInterface("org.freedesktop.login1.User")]
    interface IUser : IDBusObject
    {
        Task TerminateAsync();
        Task KillAsync(int SignalNumber);
        Task<T> GetAsync<T>(string prop);
        Task<UserProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class UserProperties
    {
        private uint _UID = default(uint);
        public uint UID
        {
            get
            {
                return _UID;
            }

            set
            {
                _UID = (value);
            }
        }

        private uint _GID = default(uint);
        public uint GID
        {
            get
            {
                return _GID;
            }

            set
            {
                _GID = (value);
            }
        }

        private string _Name = default(string);
        public string Name
        {
            get
            {
                return _Name;
            }

            set
            {
                _Name = (value);
            }
        }

        private ulong _Timestamp = default(ulong);
        public ulong Timestamp
        {
            get
            {
                return _Timestamp;
            }

            set
            {
                _Timestamp = (value);
            }
        }

        private ulong _TimestampMonotonic = default(ulong);
        public ulong TimestampMonotonic
        {
            get
            {
                return _TimestampMonotonic;
            }

            set
            {
                _TimestampMonotonic = (value);
            }
        }

        private string _RuntimePath = default(string);
        public string RuntimePath
        {
            get
            {
                return _RuntimePath;
            }

            set
            {
                _RuntimePath = (value);
            }
        }

        private string _Service = default(string);
        public string Service
        {
            get
            {
                return _Service;
            }

            set
            {
                _Service = (value);
            }
        }

        private string _Slice = default(string);
        public string Slice
        {
            get
            {
                return _Slice;
            }

            set
            {
                _Slice = (value);
            }
        }

        private (string, ObjectPath) _Display = default((string, ObjectPath));
        public (string, ObjectPath) Display
        {
            get
            {
                return _Display;
            }

            set
            {
                _Display = (value);
            }
        }

        private string _State = default(string);
        public string State
        {
            get
            {
                return _State;
            }

            set
            {
                _State = (value);
            }
        }

        private (string, ObjectPath)[] _Sessions = default((string, ObjectPath)[]);
        public (string, ObjectPath)[] Sessions
        {
            get
            {
                return _Sessions;
            }

            set
            {
                _Sessions = (value);
            }
        }

        private bool _IdleHint = default(bool);
        public bool IdleHint
        {
            get
            {
                return _IdleHint;
            }

            set
            {
                _IdleHint = (value);
            }
        }

        private ulong _IdleSinceHint = default(ulong);
        public ulong IdleSinceHint
        {
            get
            {
                return _IdleSinceHint;
            }

            set
            {
                _IdleSinceHint = (value);
            }
        }

        private ulong _IdleSinceHintMonotonic = default(ulong);
        public ulong IdleSinceHintMonotonic
        {
            get
            {
                return _IdleSinceHintMonotonic;
            }

            set
            {
                _IdleSinceHintMonotonic = (value);
            }
        }

        private bool _Linger = default(bool);
        public bool Linger
        {
            get
            {
                return _Linger;
            }

            set
            {
                _Linger = (value);
            }
        }
    }

    static class UserExtensions
    {
        public static Task<uint> GetUIDAsync(this IUser o) => o.GetAsync<uint>("UID");
        public static Task<uint> GetGIDAsync(this IUser o) => o.GetAsync<uint>("GID");
        public static Task<string> GetNameAsync(this IUser o) => o.GetAsync<string>("Name");
        public static Task<ulong> GetTimestampAsync(this IUser o) => o.GetAsync<ulong>("Timestamp");
        public static Task<ulong> GetTimestampMonotonicAsync(this IUser o) => o.GetAsync<ulong>("TimestampMonotonic");
        public static Task<string> GetRuntimePathAsync(this IUser o) => o.GetAsync<string>("RuntimePath");
        public static Task<string> GetServiceAsync(this IUser o) => o.GetAsync<string>("Service");
        public static Task<string> GetSliceAsync(this IUser o) => o.GetAsync<string>("Slice");
        public static Task<(string, ObjectPath)> GetDisplayAsync(this IUser o) => o.GetAsync<(string, ObjectPath)>("Display");
        public static Task<string> GetStateAsync(this IUser o) => o.GetAsync<string>("State");
        public static Task<(string, ObjectPath)[]> GetSessionsAsync(this IUser o) => o.GetAsync<(string, ObjectPath)[]>("Sessions");
        public static Task<bool> GetIdleHintAsync(this IUser o) => o.GetAsync<bool>("IdleHint");
        public static Task<ulong> GetIdleSinceHintAsync(this IUser o) => o.GetAsync<ulong>("IdleSinceHint");
        public static Task<ulong> GetIdleSinceHintMonotonicAsync(this IUser o) => o.GetAsync<ulong>("IdleSinceHintMonotonic");
        public static Task<bool> GetLingerAsync(this IUser o) => o.GetAsync<bool>("Linger");
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