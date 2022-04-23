using System.Threading.Tasks;
using WorkBreakReminder.Core.Model;

namespace WorkBreakReminder.Core
{
    /// <summary>
    /// Storage manager represents the reminder storage.
    /// </summary>
    /// <typeparam name="TStorageData">Data to be saved into the current storage.</typeparam>
    /// <typeparam name="TDataKeyId">Unique key representing the target path for the given data.</typeparam>
    public interface IReminderStorage<TStorageData, TDataKeyId>
    {
        /// <summary>
        /// Saves the data into the storage that this class deals with.
        /// </summary>
        /// <param name="data">The data to be saved into storage.</param>
        /// <param name="dataId">Unique value that provides the unique storage location for the data.</param>
        Task<bool> SaveAsync(TStorageData data, TDataKeyId dataId);

        /// <summary>
        /// Gets the data stored into the storage using the given unique key.
        /// </summary>
        /// <param name="dataId">Unique key representing the storage location.</param>
        /// <returns>Data that was saved into the storage against the given data Id.</returns>
        Task<TStorageData> GetAsync(TDataKeyId dataId);

        /// <summary>
        /// Validates whether the given unique data id presents in the storage.
        /// </summary>
        /// <param name="dataId">Unique id that represents the unique storage path for a data.</param>
        /// <returns>Storage validation result.</returns>
        /// <seealso cref="IStorageValidationResult"/>
        IStorageValidationResult Validate(TDataKeyId dataId);

    }
}
