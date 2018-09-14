﻿// <auto-generated />
namespace AndyTools.Wpf
{
    using System.Collections.ObjectModel;

    /// <summary>
    /// The Logger interface.
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        /// Gets the log as an observable collection.
        /// </summary>
        ObservableCollection<string> Log { get; }

        /// <summary>
        /// Add entries to the log
        /// </summary>
        /// <param name="logEntry">
        /// The log entry.
        /// </param>
        void Add(string logEntry);

        /// <summary>
        /// Append an item to the latest log entry
        /// </summary>
        /// <param name="logAppend">
        /// The string to be appended
        /// </param>
        void Append(string logAppend);

    }
}