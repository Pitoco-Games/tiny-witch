using UnityEngine;
using Utils;
using Utils.Save;

namespace CoreGameplay.Controls
{
    public class PlayerMovement : Movement, IUpdatesSave
    {
        private PlayerLocationSaveData locationSaveData;

        private void Awake()
        {
            locationSaveData = (PlayerLocationSaveData)ServicesLocator.Get<SaveService>().TryGetSavedData<PlayerLocationSaveData>();
            locationSaveData.AddListenerToSaveUpdateRequest(UpdateSaveData);
        }

        private void OnDestroy()
        {
            locationSaveData.RemoveListenerToSaveUpdateRequest(UpdateSaveData);
        }

        public void UpdateSaveData()
        {
            Vector2 pos = transform.position;

            locationSaveData.locationX = pos.x;
            locationSaveData.locationY = pos.y;
        }
    }
}