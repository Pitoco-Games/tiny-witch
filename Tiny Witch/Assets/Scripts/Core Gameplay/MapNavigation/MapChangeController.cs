using CoreGameplay.MapNavigation;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MapChangeController : MonoBehaviour
{
    [SerializeField] private List<MapChangeDetector> mapChangeDetectors;
    private static MapChangeData lastMapChangeTriggeredData;

    private void Awake()
    {
        foreach (MapChangeDetector mapChangeDetector in mapChangeDetectors)
        {
            mapChangeDetector.Setup(HandlePlayerDetected);
        }
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
