using System;

namespace WorkBreakReminder.Core
{
    public interface IReminderStorage<TStorageData, TDataKeyId>
    {
        void Save(TStorageData data, TDataKeyId dataId);

        TStorageData Get(TDataKeyId dataId);
    }
}
