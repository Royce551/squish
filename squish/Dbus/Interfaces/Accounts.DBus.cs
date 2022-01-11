using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Tmds.DBus;

#pragma warning disable
// ReSharper disable All

[assembly: InternalsVisibleTo(Tmds.DBus.Connection.DynamicAssemblyName)]
namespace Accounts.DBus
{
    [DBusInterface("org.freedesktop.Accounts")]
    interface IAccounts : IDBusObject
    {
        Task<ObjectPath[]> ListCachedUsersAsync();
        Task<ObjectPath> FindUserByIdAsync(long Id);
        Task<ObjectPath> FindUserByNameAsync(string Name);
        Task<ObjectPath> CreateUserAsync(string Name, string Fullname, int AccountType);
        Task<ObjectPath> CacheUserAsync(string Name);
        Task UncacheUserAsync(string Name);
        Task DeleteUserAsync(long Id, bool RemoveFiles);
        Task<IDisposable> WatchUserAddedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<IDisposable> WatchUserDeletedAsync(Action<ObjectPath> handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<AccountsProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class AccountsProperties
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

        private bool _HasNoUsers = default(bool);
        public bool HasNoUsers
        {
            get
            {
                return _HasNoUsers;
            }

            set
            {
                _HasNoUsers = (value);
            }
        }

        private bool _HasMultipleUsers = default(bool);
        public bool HasMultipleUsers
        {
            get
            {
                return _HasMultipleUsers;
            }

            set
            {
                _HasMultipleUsers = (value);
            }
        }

        private ObjectPath[] _AutomaticLoginUsers = default(ObjectPath[]);
        public ObjectPath[] AutomaticLoginUsers
        {
            get
            {
                return _AutomaticLoginUsers;
            }

            set
            {
                _AutomaticLoginUsers = (value);
            }
        }
    }

    static class AccountsExtensions
    {
        public static Task<string> GetDaemonVersionAsync(this IAccounts o) => o.GetAsync<string>("DaemonVersion");
        public static Task<bool> GetHasNoUsersAsync(this IAccounts o) => o.GetAsync<bool>("HasNoUsers");
        public static Task<bool> GetHasMultipleUsersAsync(this IAccounts o) => o.GetAsync<bool>("HasMultipleUsers");
        public static Task<ObjectPath[]> GetAutomaticLoginUsersAsync(this IAccounts o) => o.GetAsync<ObjectPath[]>("AutomaticLoginUsers");
    }

    [DBusInterface("org.freedesktop.Accounts.User")]
    interface IUser : IDBusObject
    {
        Task SetUserNameAsync(string Name);
        Task SetRealNameAsync(string Name);
        Task SetEmailAsync(string Email);
        Task SetLanguageAsync(string Language);
        Task SetXSessionAsync(string XSession);
        Task SetSessionAsync(string Session);
        Task SetSessionTypeAsync(string SessionType);
        Task SetLocationAsync(string Location);
        Task SetHomeDirectoryAsync(string Homedir);
        Task SetShellAsync(string Shell);
        Task SetIconFileAsync(string Filename);
        Task SetLockedAsync(bool Locked);
        Task SetAccountTypeAsync(int AccountType);
        Task SetPasswordModeAsync(int Mode);
        Task SetPasswordAsync(string Password, string Hint);
        Task SetPasswordHintAsync(string Hint);
        Task SetAutomaticLoginAsync(bool Enabled);
        Task<(long expirationTime, long lastChangeTime, long minDaysBetweenChanges, long maxDaysBetweenChanges, long daysToWarn, long daysAfterExpirationUntilLock)> GetPasswordExpirationPolicyAsync();
        Task<IDisposable> WatchChangedAsync(Action handler, Action<Exception> onError = null);
        Task<T> GetAsync<T>(string prop);
        Task<UserProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class UserProperties
    {
        private ulong _Uid = default(ulong);
        public ulong Uid
        {
            get
            {
                return _Uid;
            }

            set
            {
                _Uid = (value);
            }
        }

        private string _UserName = default(string);
        public string UserName
        {
            get
            {
                return _UserName;
            }

            set
            {
                _UserName = (value);
            }
        }

        private string _RealName = default(string);
        public string RealName
        {
            get
            {
                return _RealName;
            }

            set
            {
                _RealName = (value);
            }
        }

        private int _AccountType = default(int);
        public int AccountType
        {
            get
            {
                return _AccountType;
            }

            set
            {
                _AccountType = (value);
            }
        }

        private string _HomeDirectory = default(string);
        public string HomeDirectory
        {
            get
            {
                return _HomeDirectory;
            }

            set
            {
                _HomeDirectory = (value);
            }
        }

        private string _Shell = default(string);
        public string Shell
        {
            get
            {
                return _Shell;
            }

            set
            {
                _Shell = (value);
            }
        }

        private string _Email = default(string);
        public string Email
        {
            get
            {
                return _Email;
            }

            set
            {
                _Email = (value);
            }
        }

        private string _Language = default(string);
        public string Language
        {
            get
            {
                return _Language;
            }

            set
            {
                _Language = (value);
            }
        }

        private string _Session = default(string);
        public string Session
        {
            get
            {
                return _Session;
            }

            set
            {
                _Session = (value);
            }
        }

        private string _SessionType = default(string);
        public string SessionType
        {
            get
            {
                return _SessionType;
            }

            set
            {
                _SessionType = (value);
            }
        }

        private string _XSession = default(string);
        public string XSession
        {
            get
            {
                return _XSession;
            }

            set
            {
                _XSession = (value);
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

        private ulong _LoginFrequency = default(ulong);
        public ulong LoginFrequency
        {
            get
            {
                return _LoginFrequency;
            }

            set
            {
                _LoginFrequency = (value);
            }
        }

        private long _LoginTime = default(long);
        public long LoginTime
        {
            get
            {
                return _LoginTime;
            }

            set
            {
                _LoginTime = (value);
            }
        }

        private (long, long, IDictionary<string, object>)[] _LoginHistory = default((long, long, IDictionary<string, object>)[]);
        public (long, long, IDictionary<string, object>)[] LoginHistory
        {
            get
            {
                return _LoginHistory;
            }

            set
            {
                _LoginHistory = (value);
            }
        }

        private string _IconFile = default(string);
        public string IconFile
        {
            get
            {
                return _IconFile;
            }

            set
            {
                _IconFile = (value);
            }
        }

        private bool _Saved = default(bool);
        public bool Saved
        {
            get
            {
                return _Saved;
            }

            set
            {
                _Saved = (value);
            }
        }

        private bool _Locked = default(bool);
        public bool Locked
        {
            get
            {
                return _Locked;
            }

            set
            {
                _Locked = (value);
            }
        }

        private int _PasswordMode = default(int);
        public int PasswordMode
        {
            get
            {
                return _PasswordMode;
            }

            set
            {
                _PasswordMode = (value);
            }
        }

        private string _PasswordHint = default(string);
        public string PasswordHint
        {
            get
            {
                return _PasswordHint;
            }

            set
            {
                _PasswordHint = (value);
            }
        }

        private bool _AutomaticLogin = default(bool);
        public bool AutomaticLogin
        {
            get
            {
                return _AutomaticLogin;
            }

            set
            {
                _AutomaticLogin = (value);
            }
        }

        private bool _SystemAccount = default(bool);
        public bool SystemAccount
        {
            get
            {
                return _SystemAccount;
            }

            set
            {
                _SystemAccount = (value);
            }
        }

        private bool _LocalAccount = default(bool);
        public bool LocalAccount
        {
            get
            {
                return _LocalAccount;
            }

            set
            {
                _LocalAccount = (value);
            }
        }
    }

    static class UserExtensions
    {
        public static Task<ulong> GetUidAsync(this IUser o) => o.GetAsync<ulong>("Uid");
        public static Task<string> GetUserNameAsync(this IUser o) => o.GetAsync<string>("UserName");
        public static Task<string> GetRealNameAsync(this IUser o) => o.GetAsync<string>("RealName");
        public static Task<int> GetAccountTypeAsync(this IUser o) => o.GetAsync<int>("AccountType");
        public static Task<string> GetHomeDirectoryAsync(this IUser o) => o.GetAsync<string>("HomeDirectory");
        public static Task<string> GetShellAsync(this IUser o) => o.GetAsync<string>("Shell");
        public static Task<string> GetEmailAsync(this IUser o) => o.GetAsync<string>("Email");
        public static Task<string> GetLanguageAsync(this IUser o) => o.GetAsync<string>("Language");
        public static Task<string> GetSessionAsync(this IUser o) => o.GetAsync<string>("Session");
        public static Task<string> GetSessionTypeAsync(this IUser o) => o.GetAsync<string>("SessionType");
        public static Task<string> GetXSessionAsync(this IUser o) => o.GetAsync<string>("XSession");
        public static Task<string> GetLocationAsync(this IUser o) => o.GetAsync<string>("Location");
        public static Task<ulong> GetLoginFrequencyAsync(this IUser o) => o.GetAsync<ulong>("LoginFrequency");
        public static Task<long> GetLoginTimeAsync(this IUser o) => o.GetAsync<long>("LoginTime");
        public static Task<(long, long, IDictionary<string, object>)[]> GetLoginHistoryAsync(this IUser o) => o.GetAsync<(long, long, IDictionary<string, object>)[]>("LoginHistory");
        public static Task<string> GetIconFileAsync(this IUser o) => o.GetAsync<string>("IconFile");
        public static Task<bool> GetSavedAsync(this IUser o) => o.GetAsync<bool>("Saved");
        public static Task<bool> GetLockedAsync(this IUser o) => o.GetAsync<bool>("Locked");
        public static Task<int> GetPasswordModeAsync(this IUser o) => o.GetAsync<int>("PasswordMode");
        public static Task<string> GetPasswordHintAsync(this IUser o) => o.GetAsync<string>("PasswordHint");
        public static Task<bool> GetAutomaticLoginAsync(this IUser o) => o.GetAsync<bool>("AutomaticLogin");
        public static Task<bool> GetSystemAccountAsync(this IUser o) => o.GetAsync<bool>("SystemAccount");
        public static Task<bool> GetLocalAccountAsync(this IUser o) => o.GetAsync<bool>("LocalAccount");
    }

    [DBusInterface("org.freedesktop.DisplayManager.AccountsService")]
    interface IAccountsService : IDBusObject
    {
        Task<T> GetAsync<T>(string prop);
        Task<AccountsServiceProperties> GetAllAsync();
        Task SetAsync(string prop, object val);
        Task<IDisposable> WatchPropertiesAsync(Action<PropertyChanges> handler);
    }

    [Dictionary]
    class AccountsServiceProperties
    {
        private string _BackgroundFile = default(string);
        public string BackgroundFile
        {
            get
            {
                return _BackgroundFile;
            }

            set
            {
                _BackgroundFile = (value);
            }
        }

        private bool _HasMessages = default(bool);
        public bool HasMessages
        {
            get
            {
                return _HasMessages;
            }

            set
            {
                _HasMessages = (value);
            }
        }

        private string[] _KeyboardLayouts = default(string[]);
        public string[] KeyboardLayouts
        {
            get
            {
                return _KeyboardLayouts;
            }

            set
            {
                _KeyboardLayouts = (value);
            }
        }
    }

    static class AccountsServiceExtensions
    {
        public static Task<string> GetBackgroundFileAsync(this IAccountsService o) => o.GetAsync<string>("BackgroundFile");
        public static Task<bool> GetHasMessagesAsync(this IAccountsService o) => o.GetAsync<bool>("HasMessages");
        public static Task<string[]> GetKeyboardLayoutsAsync(this IAccountsService o) => o.GetAsync<string[]>("KeyboardLayouts");
        public static Task SetBackgroundFileAsync(this IAccountsService o, string val) => o.SetAsync("BackgroundFile", val);
        public static Task SetHasMessagesAsync(this IAccountsService o, bool val) => o.SetAsync("HasMessages", val);
        public static Task SetKeyboardLayoutsAsync(this IAccountsService o, string[] val) => o.SetAsync("KeyboardLayouts", val);
    }
}