using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Omnis.BranchTracker
{
    public class Linkable : InteractBase
    {
        #region Serialized Fields
        public BranchNode master;
        public List<NodeLink> inLinks;
        public List<NodeLink> outLinks;
        #endregion

        #region Fields
        #endregion

        #region Interfaces
        public override bool IsPointed
        {
            get => base.IsPointed;
            set
            {
                base.IsPointed = value;
            }
        }
        public override bool IsLeftPressed
        {
            get => base.IsLeftPressed;
            set
            {
                base.IsLeftPressed = value;
            }
        }
        public void CreateFloatLink()
        {
            var toPoint = Instantiate(GameManager.Instance.gameSettings.slotPrefab).GetComponent<Linkable>();
            CreateLinkTo(toPoint);
        }
        public void CreateLinkTo(Linkable toPoint)
        {
            var link = Instantiate(GameManager.Instance.gameSettings.linkPrefab).GetComponent<NodeLink>();
            link.fromPoint = this;
            link.toPoint = toPoint;
            outLinks.Add(link);
            toPoint.inLinks.Add(link);
            UpdateLinks();
        }
        public void UpdateLinks()
        {
            inLinks.ForEach(link => link.UpdatePositions());
            outLinks.ForEach(link => link.UpdatePositions());
        }
        public void BreakAll()
        {
            while (inLinks.Count > 0) inLinks.First().Break();
            while (outLinks.Count > 0) outLinks.First().Break();
        }
        #endregion
    }
}
