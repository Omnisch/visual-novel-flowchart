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
            GameManager.Instance.nodeRegistry.Remove(this);
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

        #region Unity Methods
        protected override void Start()
        {
            base.Start();

            spriteRenderer = GetComponent<SpriteRenderer>();
            Mode = mode;
        }
        #endregion
    }
}
