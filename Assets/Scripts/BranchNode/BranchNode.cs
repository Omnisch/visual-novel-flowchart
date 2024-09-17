using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Omnis.BranchTracker
{
    public partial class BranchNode : InteractBase
    {
        #region Serialized Fields
        [Header("LinkSlots")]
        public Linkable inSlot;
        public Linkable outSlot;
        [Header("Offsets")]
        public Vector3 childOffset;
        #endregion

        #region Interfaces
        public List<BranchNode> Parents => inSlot.inLinks.Select(link => link.fromPoint.master).ToList();
        public List<BranchNode> Children => outSlot.outLinks.Select(link => link.toPoint.master).ToList();
        public void CreateChild()
        {
            Vector3 childPos = transform.position + childOffset;
            var child = Instantiate(GameManager.Instance.gameSettings.nodePrefab, childPos, Quaternion.identity).GetComponent<BranchNode>();
            outSlot.CreateLinkTo(child.inSlot);
            GameManager.Instance.nodePriority.Prioritize(child);
        }
        public void RemoveSelf()
        {
            GameManager.Instance.nodePriority.Remove(this);
            inSlot.BreakAll();
            outSlot.BreakAll();
            Destroy(gameObject);
        }
        #endregion

        #region Functions
        private void UpdateLinks()
        {
            inSlot.UpdateLinks();
            outSlot.UpdateLinks();
        }
        #endregion
    }
}
