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
        public void CreateLinkFromFloat()
        {
            Vector3 fromPointPos = VectorTweak.xyn(transform.position, -100f);
            var fromPoint = Instantiate(GameManager.Instance.gameSettings.floatSlotPrefab, fromPointPos, Quaternion.identity).GetComponent<Linkable>();
            CreateLinkFrom(fromPoint);
        }
        public void CreateLinkToFloat()
        {
            Vector3 toPointPos = VectorTweak.xyn(transform.position, -100f);
            var toPoint = Instantiate(GameManager.Instance.gameSettings.floatSlotPrefab, toPointPos, Quaternion.identity).GetComponent<Linkable>();
            CreateLinkTo(toPoint);
        }
        public bool TryAcceptInLink(NodeLink link)
        {
            if (!allowIn) return false;
            if (link.fromPoint.master == master) return false;
            if (inLinks.Select(link => link.fromPoint.master).Contains(link.fromPoint.master)) return false;
            AddInLink(link);
            return true;
        }
        public bool TryAcceptOutLink(NodeLink link)
        {
            if (!allowOut) return false;
            if (link.toPoint.master == master) return false;
            if (outLinks.Select(link => link.toPoint.master).Contains(link.toPoint.master)) return false;
            AddOutLink(link);
            return true;
        }
        #endregion
    }
}
