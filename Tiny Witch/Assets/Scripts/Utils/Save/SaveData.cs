using System;

namespace Utils.Save
{
    public abstract class SaveData
    {
        private Action OnSaveDataUpdateRequested;
        public bool HasSavedData;

        public void UpdateSaveData()
        {
            OnSaveDataUpdateRequested?.Invoke();
        }

        public void AddListenerToSaveUpdateRequest(Action callback)
        {
            OnSaveDataUpdateRequested += callback;
        }
        
        public void RemoveListenerToSaveUpdateRequest(Action callback)
        {
            OnSaveDataUpdateRequested -= callback;
        }
    }
}