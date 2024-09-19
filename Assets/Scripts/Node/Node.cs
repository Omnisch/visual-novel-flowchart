using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Omnis.Flowchart
{
    public partial class Node : InteractBase
    {
        #region Serialized Fields
        public Linkable inSlot;
        public Linkable outSlot;
        #endregion

        #region Interfaces
        public List<Node> Parents => inSlot.inLinks.Select(link => link.fromPoint.master).ToList();
        public List<Node> Children => outSlot.outLinks.Select(link => link.toPoint.master).ToList();
        public void RemoveSelf()
        {
            inSlot.BreakAll();
            outSlot.BreakAll();
            GameManager.Instance.registry.Remove(this);
            Destroy(gameObject);
        }
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
