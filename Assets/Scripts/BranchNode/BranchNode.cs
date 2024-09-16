using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.BranchTracker
{
    public partial class BranchNode : InteractBase
    {
        #region Serialized Fields
        [Header("Links")]
        public List<NodeLink> parentLinks;
        public List<NodeLink> childLinks;
        [Header("Offsets")]
        public Vector3 childOffset;
        public Transform inOffset;
        public Transform outOffset;
        #endregion

        #region Interfaces
        public void CreateChild()
        {
            Vector3 childPos = transform.position + childOffset;
            var child = Instantiate(GameManager.Instance.gameSettings.nodePrefab, childPos, Quaternion.identity).GetComponent<BranchNode>();
            CreateChildLink(child);
            GameManager.Instance.nodePriority.Prioritize(child);
        }
        public void CreateChildLink(BranchNode child)
        {
            var link = Instantiate(GameManager.Instance.gameSettings.linkPrefab).GetComponent<NodeLink>();
            link.fromNode = this;
            link.toNode = child;
            child.parentLinks.Add(link);
            childLinks.Add(link);
            UpdateLinks();
        }
        public void RemoveSelf()
        {
            GameManager.Instance.nodePriority.Remove(this);
            while (childLinks.Count > 0) childLinks.First().Break();
            while (parentLinks.Count > 0) parentLinks.First().Break();
            Destroy(gameObject);
        }
        #endregion

        #region Functions
        private void UpdateLinks()
        {
            parentLinks.ForEach(link => link.UpdatePositions());
            childLinks.ForEach(link => link.UpdatePositions());
        }
        #endregion
    }
}
