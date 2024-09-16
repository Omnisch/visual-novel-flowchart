using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Omnis.BranchTracker
{
    public partial class BranchNode : PointerBase
    {
        #region Serialized Fields
        [SerializeField] private List<BranchNode> parents;
        [SerializeField] private List<BranchNode> children;
        [SerializeField] private string savePath;
        [SerializeField] private string description;
        #endregion

        #region Fields
        #endregion

        #region Interfaces
        public void CreateChild()
        {
            var childGo = Instantiate(GameManager.Instance.gameSettings.prefab, new Vector3(transform.position.x, transform.position.y - 1f, transform.position.z), Quaternion.identity);
            var child = childGo.GetComponent<BranchNode>();
            AddChild(child);
            GameManager.Instance.nodePriority.Prioritize(child);
        }
        public void AddChild(BranchNode child)
        {
            child.AddParent(this);
            children.Add(child);
        }
        public void RemoveSelf()
        {
            GameManager.Instance.nodePriority.Remove(this);
            foreach (var child in children)
            {
                child.parents.Remove(this);
                child.parents.AddRange(parents);
            }
            foreach (var parent in parents)
            {
                parent.children.Remove(this);
                parent.children.AddRange(children);
            }
            Destroy(gameObject);
        }
        #endregion

        #region Functions
        protected void AddParent(BranchNode parent) => parents.Add(parent);
        #endregion

        #region Unity Methods
        protected override void Start()
        {
            base.Start();
        }
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            foreach (var child in children)
                Gizmos.DrawLine(transform.position, child.transform.position);
        }
        #endregion
    }
}
