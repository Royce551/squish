using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

#pragma warning disable
// ReSharper disable All

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace PowerProfiles.DBus
{
    [DBusInterface("net.hadess.PowerProfiles")]
    interface IPowerProfiles : IDBusObject
    {
        Task<uint> HoldProfileAsync(string Profile, string Reason, string ApplicationId);
        Task ReleaseProfileAsync(uint Cookie);
        Task<IDisposable> WatchProfileReleasedAsync(Action<uint> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<PowerProfilesProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class PowerProfilesProperties
    {
        private string _ActiveProfile = default(string);
        public string ActiveProfile
        {
            get
            {
                return _ActiveProfile;
            }

            set
            {
                _ActiveProfile = (value);
            }
        }

        private string _PerformanceInhibited = default(string);
        public string PerformanceInhibited
        {
            get
            {
                return _PerformanceInhibited;
            }

            set
            {
                _PerformanceInhibited = (value);
            }
        }

        private string _PerformanceDegraded = default(string);
        public string PerformanceDegraded
        {
            get
            {
                return _PerformanceDegraded;
            }

            set
            {
                _PerformanceDegraded = (value);
            }
        }

        private IDictionary<string, object>[] _Profiles = default(IDictionary<string, object>[]);
        public IDictionary<string, object>[] Profiles
        {
            get
            {
                return _Profiles;
            }

            set
            {
                _Profiles = (value);
            }
        }

        private string[] _Actions = default(string[]);
        public string[] Actions
        {
            get
            {
                return _Actions;
            }

            set
            {
                _Actions = (value);
            }
        }

        private IDictionary<string, object>[] _ActiveProfileHolds = default(IDictionary<string, object>[]);
        public IDictionary<string, object>[] ActiveProfileHolds
        {
            get
            {
                return _ActiveProfileHolds;
            }

            set
            {
                _ActiveProfileHolds = (value);
            }
        }
    }

    static class PowerProfilesExtensions
    {
        public static Task<string> GetActiveProfileAsync(this IPowerProfiles o) => o.GetAsync<string>("ActiveProfile");
        public static Task<string> GetPerformanceInhibitedAsync(this IPowerProfiles o) => o.GetAsync<string>("PerformanceInhibited");
        public static Task<string> GetPerformanceDegradedAsync(this IPowerProfiles o) => o.GetAsync<string>("PerformanceDegraded");
        public static Task<IDictionary<string, object>[]> GetProfilesAsync(this IPowerProfiles o) => o.GetAsync<IDictionary<string, object>[]>("Profiles");
        public static Task<string[]> GetActionsAsync(this IPowerProfiles o) => o.GetAsync<string[]>("Actions");
        public static Task<IDictionary<string, object>[]> GetActiveProfileHoldsAsync(this IPowerProfiles o) => o.GetAsync<IDictionary<string, object>[]>("ActiveProfileHolds");
        public static Task SetActiveProfileAsync(this IPowerProfiles o, string val) => o.SetAsync("ActiveProfile", val);
    }
}