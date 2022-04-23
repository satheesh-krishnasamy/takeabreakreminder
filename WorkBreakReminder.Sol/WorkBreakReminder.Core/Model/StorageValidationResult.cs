using System.Collections.Generic;

namespace WorkBreakReminder.Core.Model
{
    /// <summary>
    /// Storage validation result.
    /// </summary>
    public interface IStorageValidationResult
    {
        /// <summary>
        /// Error in the storage path given.
        /// </summary>
        IEnumerable<string> Errors { get; }

        /// <summary>
        /// Validation status.
        /// true - if the storage path exists; false - otherwise.
        /// </summary>
        bool IsValid { get; }
    }

    public class StorageValidationResult : IStorageValidationResult
    {
        public static readonly StorageValidationResult Success = new StorageValidationResult(true);

        public StorageValidationResult(bool isValidStoragePath)
        {
            this.IsValid = isValidStoragePath;
            Errors = new List<string>();
        }

        public StorageValidationResult(bool isValidStoragePath, IEnumerable<string> errors)
        {
            this.IsValid = isValidStoragePath;
            if (errors != null)
                Errors = errors;
            else
                Errors = new List<string>();
        }

        public bool IsValid { get; set; }

        public IEnumerable<string> Errors { get; }
    }
}