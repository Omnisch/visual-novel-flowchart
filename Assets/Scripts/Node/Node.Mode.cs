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
        public NodeMode Mode
        {
            get => mode;
            set
            {
                mode = value;
                if (spriteRenderer)
                    spriteRenderer.sprite = nodeSprites[(int)mode];
                switch (value)
                {
                    case NodeMode.Island:
                        inSlot.gameObject.SetActive(false);
                        outSlot.gameObject.SetActive(false);
                        break;
                    case NodeMode.Root:
                        inSlot.gameObject.SetActive(false);
                        outSlot.gameObject.SetActive(true);
                        break;
                    case NodeMode.Branch:
                        inSlot.gameObject.SetActive(true);
                        outSlot.gameObject.SetActive(true);
                        break;
                    case NodeMode.Leaf:
                        inSlot.gameObject.SetActive(true);
                        outSlot.gameObject.SetActive(false);
                        break;
                }
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

    public enum NodeMode
    {
        Island,
        Root,
        Branch,
        Leaf
    }
}
