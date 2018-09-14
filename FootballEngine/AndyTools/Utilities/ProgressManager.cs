// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProgressManager.cs" company="Andrew Scott">
//   Andrew Scott
// </copyright>
// <summary>
//   Progress manager class that may be used to callback to other objects while another operation progresses.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace AndyTools.Utilities
{
    using System;

    using AndyTools.Wpf;

    /// <summary>Progress manager class that may be used to callback to other objects while another operation progresses.</summary>
    public class ProgressManager
    {
        /// <summary>The logger.</summary>
        private readonly ILogger _logger;

        /// <summary>Action to be called when the progress is updated.</summary>
        private readonly Action<int> _externalSetProgressAction;

        /// <summary>Initializes a new instance of the <see cref="ProgressManager"/> class.</summary>
        /// <param name="logger">The logger to use for providing string updates.</param>
        /// <param name="setProgressValueAction">The action to perform when progress is updated.</param>
        public ProgressManager(ILogger logger, Action<int> setProgressValueAction)
        {
            this._logger = logger;
            this._externalSetProgressAction = setProgressValueAction;

            this.ProgressString = new Progress<string>(this.Log);
            this.ProgressPercentage = new Progress<int>(this.SetProgressValue);

            this.ProgressPercentage.Report(0);
        }

        /// <summary>Gets the progress string.</summary>
        public IProgress<string> ProgressString { get; }

        /// <summary>Gets the progress percentage.</summary>
        public IProgress<int> ProgressPercentage { get; }

        /// <summary>Log a message.</summary>
        /// <param name="message">The message.</param>
        private void Log(string message)
        {
            this._logger.Add(message);
        }

        /// <summary>Set the progress value, performing the callback.</summary>
        /// <param name="value">The value.</param>
        private void SetProgressValue(int value)
        {
            value = value.Clamp(0, 100);
            this._externalSetProgressAction(value);
        }
    }
}
