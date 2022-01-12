using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

#nullable disable

// TODO: check which types here should be nullable and annotate them accordingly
// error, however it is possible other things here can be null (such as return values)

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace UPower.DBus
{
    [DBusInterface("org.freedesktop.UPower")]
    interface IUPower : IDBusObject
    {
        Task<ObjectPath[]> EnumerateDevicesAsync();
        Task<ObjectPath> GetDisplayDeviceAsync();
        Task<string> GetCriticalActionAsync();
        Task<IDisposable> WatchDeviceAddedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchDeviceRemovedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<UPowerProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class UPowerProperties
    {
        private string _DaemonVersion = default(string);
        public string DaemonVersion
        {
            get
            {
                return _DaemonVersion;
            }

            set
            {
                _DaemonVersion = (value);
            }
        }

        private bool _OnBattery = default(bool);
        public bool OnBattery
        {
            get
            {
                return _OnBattery;
            }

            set
            {
                _OnBattery = (value);
            }
        }

        private bool _LidIsClosed = default(bool);
        public bool LidIsClosed
        {
            get
            {
                return _LidIsClosed;
            }

            set
            {
                _LidIsClosed = (value);
            }
        }

        private bool _LidIsPresent = default(bool);
        public bool LidIsPresent
        {
            get
            {
                return _LidIsPresent;
            }

            set
            {
                _LidIsPresent = (value);
            }
        }
    }

    static class UPowerExtensions
    {
        public static Task<string> GetDaemonVersionAsync(this IUPower o) => o.GetAsync<string>("DaemonVersion");
        public static Task<bool> GetOnBatteryAsync(this IUPower o) => o.GetAsync<bool>("OnBattery");
        public static Task<bool> GetLidIsClosedAsync(this IUPower o) => o.GetAsync<bool>("LidIsClosed");
        public static Task<bool> GetLidIsPresentAsync(this IUPower o) => o.GetAsync<bool>("LidIsPresent");
    }

    [DBusInterface("org.freedesktop.UPower.Wakeups")]
    interface IWakeups : IDBusObject
    {
        Task<uint> GetTotalAsync();
        Task<(bool, uint, double, string, string)[]> GetDataAsync();
        Task<IDisposable> WatchTotalChangedAsync(Action<uint> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchDataChangedAsync(Action handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<WakeupsProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class WakeupsProperties
    {
        private bool _HasCapability = default(bool);
        public bool HasCapability
        {
            get
            {
                return _HasCapability;
            }

            set
            {
                _HasCapability = (value);
            }
        }
    }

    static class WakeupsExtensions
    {
        public static Task<bool> GetHasCapabilityAsync(this IWakeups o) => o.GetAsync<bool>("HasCapability");
    }

    [DBusInterface("org.freedesktop.UPower.Device")]
    interface IDevice : IDBusObject
    {
        Task RefreshAsync();
        Task<(uint, double, uint)[]> GetHistoryAsync(string Type, uint Timespan, uint Resolution);
        Task<(double, double)[]> GetStatisticsAsync(string Type);
        Task<T> GetAsync<T>(string prop);
        Task<DeviceProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class DeviceProperties
    {
        private string _NativePath = default(string);
        public string NativePath
        {
            get
            {
                return _NativePath;
            }

            set
            {
                _NativePath = (value);
            }
        }

        private string _Vendor = default(string);
        public string Vendor
        {
            get
            {
                return _Vendor;
            }

            set
            {
                _Vendor = (value);
            }
        }

        private string _Model = default(string);
        public string Model
        {
            get
            {
                return _Model;
            }

            set
            {
                _Model = (value);
            }
        }

        private string _Serial = default(string);
        public string Serial
        {
            get
            {
                return _Serial;
            }

            set
            {
                _Serial = (value);
            }
        }

        private ulong _UpdateTime = default(ulong);
        public ulong UpdateTime
        {
            get
            {
                return _UpdateTime;
            }

            set
            {
                _UpdateTime = (value);
            }
        }

        private uint _Type = default(uint);
        public uint Type
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

        private bool _PowerSupply = default(bool);
        public bool PowerSupply
        {
            get
            {
                return _PowerSupply;
            }

            set
            {
                _PowerSupply = (value);
            }
        }

        private bool _HasHistory = default(bool);
        public bool HasHistory
        {
            get
            {
                return _HasHistory;
            }

            set
            {
                _HasHistory = (value);
            }
        }

        private bool _HasStatistics = default(bool);
        public bool HasStatistics
        {
            get
            {
                return _HasStatistics;
            }

            set
            {
                _HasStatistics = (value);
            }
        }

        private bool _Online = default(bool);
        public bool Online
        {
            get
            {
                return _Online;
            }

            set
            {
                _Online = (value);
            }
        }

        private double _Energy = default(double);
        public double Energy
        {
            get
            {
                return _Energy;
            }

            set
            {
                _Energy = (value);
            }
        }

        private double _EnergyEmpty = default(double);
        public double EnergyEmpty
        {
            get
            {
                return _EnergyEmpty;
            }

            set
            {
                _EnergyEmpty = (value);
            }
        }

        private double _EnergyFull = default(double);
        public double EnergyFull
        {
            get
            {
                return _EnergyFull;
            }

            set
            {
                _EnergyFull = (value);
            }
        }

        private double _EnergyFullDesign = default(double);
        public double EnergyFullDesign
        {
            get
            {
                return _EnergyFullDesign;
            }

            set
            {
                _EnergyFullDesign = (value);
            }
        }

        private double _EnergyRate = default(double);
        public double EnergyRate
        {
            get
            {
                return _EnergyRate;
            }

            set
            {
                _EnergyRate = (value);
            }
        }

        private double _Voltage = default(double);
        public double Voltage
        {
            get
            {
                return _Voltage;
            }

            set
            {
                _Voltage = (value);
            }
        }

        private double _Luminosity = default(double);
        public double Luminosity
        {
            get
            {
                return _Luminosity;
            }

            set
            {
                _Luminosity = (value);
            }
        }

        private long _TimeToEmpty = default(long);
        public long TimeToEmpty
        {
            get
            {
                return _TimeToEmpty;
            }

            set
            {
                _TimeToEmpty = (value);
            }
        }

        private long _TimeToFull = default(long);
        public long TimeToFull
        {
            get
            {
                return _TimeToFull;
            }

            set
            {
                _TimeToFull = (value);
            }
        }

        private double _Percentage = default(double);
        public double Percentage
        {
            get
            {
                return _Percentage;
            }

            set
            {
                _Percentage = (value);
            }
        }

        private double _Temperature = default(double);
        public double Temperature
        {
            get
            {
                return _Temperature;
            }

            set
            {
                _Temperature = (value);
            }
        }

        private bool _IsPresent = default(bool);
        public bool IsPresent
        {
            get
            {
                return _IsPresent;
            }

            set
            {
                _IsPresent = (value);
            }
        }

        private uint _State = default(uint);
        public uint State
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

        private bool _IsRechargeable = default(bool);
        public bool IsRechargeable
        {
            get
            {
                return _IsRechargeable;
            }

            set
            {
                _IsRechargeable = (value);
            }
        }

        private double _Capacity = default(double);
        public double Capacity
        {
            get
            {
                return _Capacity;
            }

            set
            {
                _Capacity = (value);
            }
        }

        private uint _Technology = default(uint);
        public uint Technology
        {
            get
            {
                return _Technology;
            }

            set
            {
                _Technology = (value);
            }
        }

        private uint _WarningLevel = default(uint);
        public uint WarningLevel
        {
            get
            {
                return _WarningLevel;
            }

            set
            {
                _WarningLevel = (value);
            }
        }

        private uint _BatteryLevel = default(uint);
        public uint BatteryLevel
        {
            get
            {
                return _BatteryLevel;
            }

            set
            {
                _BatteryLevel = (value);
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
    }

    static class DeviceExtensions
    {
        public static Task<string> GetNativePathAsync(this IDevice o) => o.GetAsync<string>("NativePath");
        public static Task<string> GetVendorAsync(this IDevice o) => o.GetAsync<string>("Vendor");
        public static Task<string> GetModelAsync(this IDevice o) => o.GetAsync<string>("Model");
        public static Task<string> GetSerialAsync(this IDevice o) => o.GetAsync<string>("Serial");
        public static Task<ulong> GetUpdateTimeAsync(this IDevice o) => o.GetAsync<ulong>("UpdateTime");
        public static Task<uint> GetTypeAsync(this IDevice o) => o.GetAsync<uint>("Type");
        public static Task<bool> GetPowerSupplyAsync(this IDevice o) => o.GetAsync<bool>("PowerSupply");
        public static Task<bool> GetHasHistoryAsync(this IDevice o) => o.GetAsync<bool>("HasHistory");
        public static Task<bool> GetHasStatisticsAsync(this IDevice o) => o.GetAsync<bool>("HasStatistics");
        public static Task<bool> GetOnlineAsync(this IDevice o) => o.GetAsync<bool>("Online");
        public static Task<double> GetEnergyAsync(this IDevice o) => o.GetAsync<double>("Energy");
        public static Task<double> GetEnergyEmptyAsync(this IDevice o) => o.GetAsync<double>("EnergyEmpty");
        public static Task<double> GetEnergyFullAsync(this IDevice o) => o.GetAsync<double>("EnergyFull");
        public static Task<double> GetEnergyFullDesignAsync(this IDevice o) => o.GetAsync<double>("EnergyFullDesign");
        public static Task<double> GetEnergyRateAsync(this IDevice o) => o.GetAsync<double>("EnergyRate");
        public static Task<double> GetVoltageAsync(this IDevice o) => o.GetAsync<double>("Voltage");
        public static Task<double> GetLuminosityAsync(this IDevice o) => o.GetAsync<double>("Luminosity");
        public static Task<long> GetTimeToEmptyAsync(this IDevice o) => o.GetAsync<long>("TimeToEmpty");
        public static Task<long> GetTimeToFullAsync(this IDevice o) => o.GetAsync<long>("TimeToFull");
        public static Task<double> GetPercentageAsync(this IDevice o) => o.GetAsync<double>("Percentage");
        public static Task<double> GetTemperatureAsync(this IDevice o) => o.GetAsync<double>("Temperature");
        public static Task<bool> GetIsPresentAsync(this IDevice o) => o.GetAsync<bool>("IsPresent");
        public static Task<uint> GetStateAsync(this IDevice o) => o.GetAsync<uint>("State");
        public static Task<bool> GetIsRechargeableAsync(this IDevice o) => o.GetAsync<bool>("IsRechargeable");
        public static Task<double> GetCapacityAsync(this IDevice o) => o.GetAsync<double>("Capacity");
        public static Task<uint> GetTechnologyAsync(this IDevice o) => o.GetAsync<uint>("Technology");
        public static Task<uint> GetWarningLevelAsync(this IDevice o) => o.GetAsync<uint>("WarningLevel");
        public static Task<uint> GetBatteryLevelAsync(this IDevice o) => o.GetAsync<uint>("BatteryLevel");
        public static Task<string> GetIconNameAsync(this IDevice o) => o.GetAsync<string>("IconName");
    }
}