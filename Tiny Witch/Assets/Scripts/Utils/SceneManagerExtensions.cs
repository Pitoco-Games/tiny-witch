using System.Collections.Generic;
using System.Linq;
using UnityEngine.SceneManagement;

public static class SceneManagerExtensions
{
    public static List<Scene> GetAllLoadedScenes()
    {
        int countLoaded = SceneManager.sceneCount;
        List<Scene> loadedScenes = new ();

        for (int i = 0; i < countLoaded; i++)
        {
            loadedScenes.Add(SceneManager.GetSceneAt(i));
        }

        return loadedScenes;
    }

    public static bool IsSceneLoaded(string sceneName)
    {
        List<Scene> loadedScenes = GetAllLoadedScenes();
        return loadedScenes.Any(loadedScene => loadedScene.name.Equals(sceneName.ToString()));
    }
}
