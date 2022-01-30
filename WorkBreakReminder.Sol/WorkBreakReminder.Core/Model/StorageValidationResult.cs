using System.Collections.Generic;

namespace WorkBreakReminder.Core.Model
{
    public interface IStorageValidationResult
    {
        IEnumerable<string> Errors { get; }
        bool IsValid { get; }
    }

    public class StorageValidationResult : IStorageValidationResult
    {
        public static readonly StorageValidationResult Success = new StorageValidationResult(true);

        public StorageValidationResult(bool validFile)
        {
            this.IsValid = validFile;
            Errors = new List<string>();
        }

        public StorageValidationResult(bool validFile, IEnumerable<string> errors)
        {
            this.IsValid = validFile;
            if (errors != null)
                Errors = errors;
            else
                Errors = new List<string>();
        }

        public bool IsValid { get; set; }

        public IEnumerable<string> Errors { get; }
    }
}