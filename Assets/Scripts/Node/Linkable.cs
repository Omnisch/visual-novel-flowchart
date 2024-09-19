using System.Collections.Generic;
using System.Linq;

namespace Omnis.Flowchart
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
            var link = Instantiate(GameManager.Instance.settings.linkPrefab).GetComponent<NodeLink>();
            link.Connect(fromPoint, this);
        }
        public void CreateLinkTo(Linkable toPoint)
        {
            var link = Instantiate(GameManager.Instance.settings.linkPrefab).GetComponent<NodeLink>();
            link.Connect(this, toPoint);
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
