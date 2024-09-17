using System.Linq;
using UnityEngine;

namespace Omnis.BranchTracker
{
    public class LinkSlot : Linkable
    {
        #region Interfaces
        public override bool IsPointed
        {
            get => base.IsPointed;
            set
            {
                base.IsPointed = value;
                GameManager.Instance.TargetSlot = value ? this : null;
            }
        }
        public void CreateFloatLink()
        {
            Vector3 toPointPos = VectorTweak.xyn(transform.position, -100f);
            var toPoint = Instantiate(GameManager.Instance.gameSettings.slotPrefab, toPointPos, Quaternion.identity).GetComponent<Linkable>();
            CreateLinkTo(toPoint);
        }
        public bool TryAcceptLink(NodeLink link)
        {
            if (!allowIn) return false;
            if (link.fromPoint.master == master) return false;
            if (inLinks.Select(link => link.fromPoint.master).Contains(link.fromPoint.master)) return false;
            AddInLink(link);
            return true;
        }
        #endregion
    }
}
