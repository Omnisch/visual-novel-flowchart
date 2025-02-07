using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Omnis.Flowchart
{
    public partial class Node : InteractBase
    {
        #region Serialized Fields
        public List<Linkable> inSlots;
        public List<Linkable> outSlots;
        #endregion

        #region Interfaces
        public List<Node> Parents => inSlots[0].inLinks.Select(link => link.fromPoint.master).ToList();
        public List<Node> Children => outSlots[0].outLinks.Select(link => link.toPoint.master).ToList();
        public void RemoveSelf()
        {
            inSlots.ForEach((slot) => slot.BreakAll());
            outSlots.ForEach((slot) => slot.BreakAll());
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
