using System.Collections.Generic;
using UnityEngine;

namespace Omnis.Flowchart
{
    public partial class Node
    {
        #region Serialized Fields
        [SerializeField] private NodeMode mode;
        [SerializeField] private List<Sprite> nodeSprites;
        #endregion

        #region Fields
        private SpriteRenderer spriteRenderer;
        private bool syncked;
        #endregion

        #region Properties
        public NodeMode Mode
        {
            get => mode;
            set
            {
                if (syncked) return;
                syncked = true;

                mode = value;
                if (spriteRenderer)
                    spriteRenderer.sprite = nodeSprites[(int)mode];
                OnModeChanged(value);

                if (copy) copy.Mode = value;
                syncked = false;
            }
        }
        #endregion

        #region Public Functions
        public void IslandMode() => Mode = NodeMode.Island;
        public void RootMode() => Mode = NodeMode.Root;
        public void BranchMode() => Mode = NodeMode.Branch;
        public void LeafMode() => Mode = NodeMode.Leaf;
        #endregion

        #region Functions
        protected virtual void OnModeChanged(NodeMode value)
        {
            if (inSlots.Count > 0 && inSlots[0])
            {
                if (value == NodeMode.Island || value == NodeMode.Root)
                    inSlots[0].BreakAll();
                inSlots[0].gameObject.SetActive(value switch
                {
                    NodeMode.Island => false,
                    NodeMode.Root => false,
                    _ => true
                });
            }
            if (outSlots.Count > 0 && outSlots[0])
            {
                if (value == NodeMode.Island || value == NodeMode.Leaf)
                    outSlots[0].BreakAll();
                outSlots[0].gameObject.SetActive(value switch
                {
                    NodeMode.Island => false,
                    NodeMode.Leaf => false,
                    _ => true
                });
            }
        }
        #endregion
    }
}
