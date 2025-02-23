using System;
using UnityEngine;

namespace CoreGameplay.MapNavigation
{
    public class MapChangeDetector : MonoBehaviour
    {
        [SerializeField] private MapChangeData mapChangeData; 
        [SerializeField] private Transform positionToSpawn;

        public string Id => mapChangeData.name;
        public Vector2 PositionToSpawn => positionToSpawn.position;

        private Action<MapChangeData> OnPlayerDetected;

        public void Setup(Action<MapChangeData> onPlayerDetectedCallback)
        {
            OnPlayerDetected = onPlayerDetectedCallback;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.tag.Equals("Player"))
            {
                OnPlayerDetected?.Invoke(mapChangeData);
            }
        }
    }
}

