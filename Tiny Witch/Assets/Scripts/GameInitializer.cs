using CoreGameplay.Inventory;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.Save;

public class GameInitializer : MonoBehaviour
{
    private async void Awake()
    {
        var saveService = new SaveService();
        ServicesLocator.Register<SaveService>(saveService);

        var playerInventoryService = await PlayerInventoryService.Create(saveService);
        ServicesLocator.Register<PlayerInventoryService>(playerInventoryService);

        LoadPlayerSavedLocation(saveService);
    }

    private void LoadPlayerSavedLocation(SaveService saveService)
    {
        const string NoSaveStartingSceneName = "Region1";
        PlayerLocationSaveData locationSavedData;

        locationSavedData = (PlayerLocationSaveData) saveService.GetSavedData<PlayerLocationSaveData>();
        string sceneToLoad = locationSavedData.HasSavedData ? locationSavedData.lastVisitedLocationSceneName : NoSaveStartingSceneName;

        SceneManager.LoadScene(sceneToLoad);
    }
}
