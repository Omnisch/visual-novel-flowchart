using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.BranchTracker
{
    public partial class BranchNode : PointerBase
    {
        #region Serialized Fields
        public List<NodeLink> parentLinks;
        public List<NodeLink> childLinks;
        [SerializeField] private string savePath;
        [SerializeField] private string description;
        [Space]
        public Vector3 inOffset;
        public Vector3 outOffset;
        #endregion

        #region Interfaces
        public void CreateChild()
        {
            Vector3 childPos = transform.position + new Vector3(0.5f, -1f, 0f);
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

        #region Unity Methods
        private void Update()
        {
            if (isPressed)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + pointerOffset;
                UpdateLinks();
            }
        }
        #endregion
    }
}
