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
                if (inSlot)
                    inSlot.gameObject.SetActive(value switch
                    {
                        NodeMode.Island => false,
                        NodeMode.Root => false,
                        _ => true
                    });
                if (outSlot)
                    outSlot.gameObject.SetActive(value switch
                    {
                        NodeMode.Island => false,
                        NodeMode.Leaf => false,
                        _ => true
                    });
            }
        }
        public void IslandMode()
        {
            inSlot.BreakAll();
            outSlot.BreakAll();
            Mode = NodeMode.Island;
        }
        public void RootMode()
        {
            inSlot.BreakAll();
            Mode = NodeMode.Root;
        }
        public void BranchMode()
        {
            Mode = NodeMode.Branch;
        }
        public void LeafMode()
        {
            outSlot.BreakAll();
            Mode = NodeMode.Leaf;
        }
        #endregion
    }
}
