using System.Collections.Generic;
using UnityEngine;

namespace Omnis.Flowchart
{
    [CreateAssetMenu(menuName = "Omnis/Game Settings")]
    public class GameSettings : ScriptableObject
    {
        [Header("Node")]
        public GameObject nodePrefab;
        public GameObject linkPrefab;
        public GameObject floatSlotPrefab;
        [Header("Context Menu")]
        public List<ContextMenuEntry> contextMenuRegistry;
    }

    [System.Serializable]
    public struct ContextMenuEntry
    {
        public string typeName;
        public string label;
        public string message;
    }
}
