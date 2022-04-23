namespace WorkBreakReminder.Core.Config
{
    /// <summary>
    /// Storage configuration.
    /// </summary>
    public interface IStorageConfig
    {
        /// <summary>
        /// File path for the user preferences.
        /// </summary>
        string PreferencesFileLocation
        {
            get;
        }
    }
}
