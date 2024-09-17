using UnityEngine;

namespace Omnis.BranchTracker
{
    [CreateAssetMenu(menuName = "Omnis/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Node")]
        public GameObject nodePrefab;
        public GameObject linkPrefab;
        public GameObject floatSlotPrefab;
    }
}
