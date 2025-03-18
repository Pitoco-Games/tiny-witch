using CoreGameplay.MapNavigation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utils;
using Utils.Save;

public class MapChangeController : MonoBehaviour, IUpdatesSave
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<MapChangeDetector> mapChangeDetectors;
    [SerializeField] private Transform initialSpawnPoint;
    private static MapChangeData lastMapChangeTriggeredData;

    private PlayerLocationSaveData locationSaveData;

    private void Awake()
    {
        SaveService saveService = ServicesLocator.Get<SaveService>();
        locationSaveData = (PlayerLocationSaveData) saveService.TryGetSavedData<PlayerLocationSaveData>();

        locationSaveData.AddListenerToSaveUpdateRequest(UpdateSaveData);

        foreach (MapChangeDetector mapChangeDetector in mapChangeDetectors)
        {
            mapChangeDetector.Setup(HandlePlayerDetected);
        }

        GameObject player = Instantiate(playerPrefab, GetPositionToSpawnIn(), Quaternion.identity);

        saveService.SaveAll();
    }

    private void OnDestroy()
    {
        locationSaveData.RemoveListenerToSaveUpdateRequest(UpdateSaveData);
    }

    public void UpdateSaveData()
    {
        locationSaveData.lastVisitedLocationSceneName = SceneManager.GetActiveScene().name;
    }

    private Vector2 GetPositionToSpawnIn()
    {
        if(lastMapChangeTriggeredData == null)
        {
            return locationSaveData.HasSavedData ? new Vector2(locationSaveData.locationX, locationSaveData.locationY) : initialSpawnPoint.position;
        }

        foreach(MapChangeDetector mapChangeDetector in mapChangeDetectors)
        {
            if(mapChangeDetector.Id.Equals(lastMapChangeTriggeredData.name))
            {
                return mapChangeDetector.PositionToSpawn;
            }
        }

        return Vector2.zero;
    }

    private void HandlePlayerDetected(MapChangeData data)
    {
        lastMapChangeTriggeredData = data;
        string firstMapName = data.ScenesToMoveBetween.First().ToString();

        string sceneToLoad;

        if (!SceneManagerExtensions.IsSceneLoaded(firstMapName))
        {
            sceneToLoad = firstMapName;
        }
        else
        {
            sceneToLoad = data.ScenesToMoveBetween.Last().ToString();
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}
