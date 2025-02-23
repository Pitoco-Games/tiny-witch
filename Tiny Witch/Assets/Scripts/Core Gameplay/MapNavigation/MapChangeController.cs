using CoreGameplay.MapNavigation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChangeController : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    [SerializeField] private List<MapChangeDetector> mapChangeDetectors;
    [SerializeField] private Transform initialSpawnPoint;
    private static MapChangeData lastMapChangeTriggeredData;

    private void Awake()
    {
        foreach (MapChangeDetector mapChangeDetector in mapChangeDetectors)
        {
            mapChangeDetector.Setup(HandlePlayerDetected);
        }

        GameObject player = Instantiate(playerPrefab, GetPositionToSpawnIn(), Quaternion.identity);
    }

    private Vector2 GetPositionToSpawnIn()
    {
        if(lastMapChangeTriggeredData == null)
        {
            return initialSpawnPoint != null ? initialSpawnPoint.position : transform.position;
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
