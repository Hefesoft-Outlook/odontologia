using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using System;
using System.Globalization;
using System.Management;
using System.Runtime.CompilerServices;

namespace MSBuild.ExtensionPack
{
    public abstract class BaseTask : Task
    {
        private string machineName;

        private AuthenticationLevel authenticationLevel;

        public string AuthenticationLevel
        {
            get
            {
                return this.authenticationLevel.ToString();
            }
            set
            {
                this.authenticationLevel = (AuthenticationLevel)Enum.Parse(typeof(AuthenticationLevel), value);
            }
        }

        public string Authority
        {
            get;
            set;
        }

        public bool ErrorOnDeprecated
        {
            get;
            set;
        }

        public bool LogExceptionStack
        {
            get;
            set;
        }

        public virtual string MachineName
        {
            get
            {
                return this.machineName ?? Environment.MachineName;
            }
            set
            {
                this.machineName = value;
            }
        }

        internal ManagementScope Scope
        {
            get;
            set;
        }

        public bool SuppressTaskMessages
        {
            get;
            set;
        }

        public virtual string TaskAction
        {
            get;
            set;
        }

        public virtual string UserName
        {
            get;
            set;
        }

        public virtual string UserPassword
        {
            get;
            set;
        }

        protected BaseTask()
        {
        }

        private void DetermineLogging()
        {
            string s = Environment.GetEnvironmentVariable("SuppressTaskMessages", EnvironmentVariableTarget.Machine);
            if (!string.IsNullOrEmpty(s))
            {
                this.SuppressTaskMessages = Convert.ToBoolean(s, CultureInfo.CurrentCulture);
            }
        }

        public sealed override bool Execute()
        {
            bool hasLoggedErrors;
            this.DetermineLogging();
            try
            {
                this.InternalExecute();
                hasLoggedErrors = !base.Log.HasLoggedErrors;
            }
            catch (Exception exception)
            {
                Exception ex = exception;
                this.GetExceptionLevel();
                base.Log.LogErrorFromException(ex, this.LogExceptionStack, true, null);
                hasLoggedErrors = !base.Log.HasLoggedErrors;
            }
            return hasLoggedErrors;
        }

        private void GetExceptionLevel()
        {
            string s = Environment.GetEnvironmentVariable("LogExceptionStack", EnvironmentVariableTarget.Machine);
            if (!string.IsNullOrEmpty(s))
            {
                this.LogExceptionStack = Convert.ToBoolean(s, CultureInfo.CurrentCulture);
            }
        }

        internal void GetManagementScope(string wmiNamespace)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            object[] objArray = new object[] { string.Concat("\\\\", this.MachineName, wmiNamespace) };
            this.LogTaskMessage(MessageImportance.Low, string.Format(currentCulture, "ManagementScope Set: {0}", objArray));
            if (string.Compare(this.MachineName, Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                this.Scope = new ManagementScope(string.Concat("\\\\", this.MachineName, wmiNamespace));
                return;
            }
            ConnectionOptions connectionOption = new ConnectionOptions()
            {
                Authentication = this.authenticationLevel,
                Username = this.UserName,
                Password = this.UserPassword,
                Authority = this.Authority
            };
            ConnectionOptions options = connectionOption;
            this.Scope = new ManagementScope(string.Concat("\\\\", this.MachineName, wmiNamespace), options);
        }

        internal void GetManagementScope(string wmiNamespace, ConnectionOptions options)
        {
            CultureInfo currentCulture = CultureInfo.CurrentCulture;
            object[] objArray = new object[] { string.Concat("\\\\", this.MachineName, wmiNamespace) };
            this.LogTaskMessage(MessageImportance.Low, string.Format(currentCulture, "ManagementScope Set: {0}", objArray));
            this.Scope = new ManagementScope(string.Concat("\\\\", this.MachineName, wmiNamespace), options);
        }

        protected abstract void InternalExecute();

        internal void LogTaskMessage(MessageImportance messageImportance, string message)
        {
            this.LogTaskMessage(messageImportance, message, null);
        }

        internal void LogTaskMessage(string message, object[] arguments)
        {
            this.LogTaskMessage(MessageImportance.Normal, message, arguments);
        }

        internal void LogTaskMessage(string message)
        {
            this.LogTaskMessage(MessageImportance.Normal, message, null);
        }

        internal void LogTaskMessage(MessageImportance messageImportance, string message, object[] arguments)
        {
            if (!this.SuppressTaskMessages)
            {
                if (arguments == null)
                {
                    base.Log.LogMessage(messageImportance, message, new object[0]);
                    return;
                }
                base.Log.LogMessage(messageImportance, message, arguments);
            }
        }

        internal void LogTaskWarning(string message)
        {
            base.Log.LogWarning(message, new object[0]);
        }

        internal bool TargetingLocalMachine()
        {
            return this.TargetingLocalMachine(false);
        }

        internal bool TargetingLocalMachine(bool canExecuteRemotely)
        {
            if (string.Compare(this.MachineName, Environment.MachineName, StringComparison.OrdinalIgnoreCase) == 0)
            {
                return true;
            }
            if (!canExecuteRemotely)
            {
                TaskLoggingHelper log = base.Log;
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                object[] machineName = new object[] { this.MachineName };
                log.LogError(string.Format(currentCulture, "This task does not support remote execution. Please remove the MachineName: {0}", machineName), new object[0]);
            }
            return false;
        }
    }
}
