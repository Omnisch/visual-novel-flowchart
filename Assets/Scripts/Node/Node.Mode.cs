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
        #endregion

        #region Interfaces
        public virtual NodeMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                if (spriteRenderer)
                    spriteRenderer.sprite = nodeSprites[(int)mode];
                if (inSlots.Count > 0 && inSlots[0])
                    inSlots[0].gameObject.SetActive(value switch
                    {
                        NodeMode.Island => false,
                        NodeMode.Root => false,
                        _ => true
                    });
                if (outSlots.Count > 0 && outSlots[0])
                    outSlots[0].gameObject.SetActive(value switch
                    {
                        NodeMode.Island => false,
                        NodeMode.Leaf => false,
                        _ => true
                    });
            }
        }
        public void IslandMode()
        {
            if (inSlots.Count > 0 && inSlots[0]) inSlots[0].BreakAll();
            if (outSlots.Count > 0 && outSlots[0]) outSlots[0].BreakAll();
            Mode = NodeMode.Island;
        }
        public void RootMode()
        {
            if (inSlots.Count > 0 && inSlots[0]) inSlots[0].BreakAll();
            Mode = NodeMode.Root;
        }
        public void BranchMode()
        {
            Mode = NodeMode.Branch;
        }
        public void LeafMode()
        {
            if (outSlots.Count > 0 && outSlots[0]) outSlots[0].BreakAll();
            Mode = NodeMode.Leaf;
        }
        #endregion
    }
}
