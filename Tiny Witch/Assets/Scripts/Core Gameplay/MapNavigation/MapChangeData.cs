using System.Collections.Generic;
using UnityEngine;

namespace CoreGameplay.MapNavigation
{
    [CreateAssetMenu(fileName = "MapChangeData_Scene1-Scene2", menuName = "ScriptableObjects/MapChangeData")]
    public class MapChangeData : ScriptableObject
    {
        public List<MapSceneName> ScenesToMoveBetween;
    }
}
