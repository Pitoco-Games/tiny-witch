using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.Save;

public class GameInitializer : MonoBehaviour
{
    private void Awake()
    {
        SaveService saveService = new SaveService();
        ServicesLocator.Register<SaveService>(saveService);

        LoadPlayerSavedLocation(saveService);
    }

    private void LoadPlayerSavedLocation(SaveService saveService)
    {
        const string NoSaveStartingSceneName = "Region1";
        PlayerLocationSaveData locationSavedData;

        locationSavedData = (PlayerLocationSaveData) saveService.TryGetSavedData<PlayerLocationSaveData>();
        string sceneToLoad = locationSavedData.HasSavedData ? locationSavedData.lastVisitedLocationSceneName : NoSaveStartingSceneName;

        SceneManager.LoadScene(sceneToLoad);
    }
}
