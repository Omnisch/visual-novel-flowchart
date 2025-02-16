using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Omnis.Flowchart
{
    public partial class Node : InteractBase
    {
        #region Serialized Fields
        public Node copy;
        public string pairHash;
        public List<Linkable> inSlots;
        public List<Linkable> outSlots;
        #endregion

        #region Public Functions
        public List<Node> Parents => inSlots[0].inLinks.Select(link => link.fromPoint.master).ToList();
        public List<Node> Children => outSlots[0].outLinks.Select(link => link.toPoint.master).ToList();
        public void BreakAllLinks()
        {
            inSlots.ForEach((slot) => slot.BreakAll());
            outSlots.ForEach((slot) => slot.BreakAll());
        }
        public void RemoveSelf()
        {
            BreakAllLinks();
            if (copy) copy.copy = null;
            GameManager.Instance.registry.Remove(this);
            Destroy(gameObject);
        }
        public void ToCopy()
        {
            if (!AddCopy()) JumpToCopy();
        }
        public bool AddCopy()
        {
            if (copy) return false;

            copy = GameManager.Instance.CreateNode(transform.position + VectorTweaker.xyo(Vector3.one));
            copy.copy = this;
            copy.Mode = this.Mode;
            copy.Description = this.Description;

            copy.pairHash = pairHash = System.Guid.NewGuid().ToString("N");
            return true;
        }
        public void JumpToCopy() => GameManager.Instance.FollowNode(copy);
        #endregion

        #region Unity Methods
        protected override void Start()
        {
            base.Start();

            spriteRenderer = GetComponent<SpriteRenderer>();
            Mode = mode;
            GameManager.Instance.registry.Prioritize(this);
        }
        #endregion
    }
}
