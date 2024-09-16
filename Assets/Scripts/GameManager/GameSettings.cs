using System.Collections.Generic;
using UnityEngine;

namespace Omnis.BranchTracker
{
    [CreateAssetMenu(menuName = "Omnis/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Node")]
        public GameObject prefab;
    }
}
