using System.Collections.Generic;
using System.Linq;

namespace Omnis.BranchTracker
{
    public abstract class Linkable : InteractBase
    {
        #region Serialized Fields
        public Node master;
        public bool allowIn;
        public List<NodeLink> inLinks;
        public bool allowOut;
        public List<NodeLink> outLinks;
        #endregion

        #region Interfaces
        public void CreateLinkFrom(Linkable fromPoint)
        {
            var link = Instantiate(GameManager.Instance.gameSettings.linkPrefab).GetComponent<NodeLink>();
            AddInLink(link);
            fromPoint.AddOutLink(link);
        }
        public void CreateLinkTo(Linkable toPoint)
        {
            var link = Instantiate(GameManager.Instance.gameSettings.linkPrefab).GetComponent<NodeLink>();
            AddOutLink(link);
            toPoint.AddInLink(link);
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

        #region Functions
        protected void AddOutLink(NodeLink link)
        {
            link.fromPoint = this;
            outLinks.Add(link);
            UpdateLinks();
        }
        protected void AddInLink(NodeLink link)
        {
            link.toPoint = this;
            inLinks.Add(link);
            UpdateLinks();
        }
        #endregion
    }
}
