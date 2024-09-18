using System.Collections.Generic;
using UnityEngine;

namespace Omnis.Flowchart
{
    public partial class Node
    {
        #region Serialized Fields
        public NodeMode mode;
        [SerializeField] private GameObject addChildButton;
        [SerializeField] private List<Sprite> nodeSprites;
        [SerializeField] private bool canChangeMode;
        #endregion

        #region Fields
        private SpriteRenderer spriteRenderer;
        #endregion

        #region Interfaces
        public void InslandMode()
        {
            inSlot.BreakAll();
            outSlot.BreakAll();
            ChangeMode(NodeMode.Island);
        }
        public void RootMode()
        {
            inSlot.BreakAll();
            ChangeMode(NodeMode.Root);
        }
        public void BranchMode() => ChangeMode(NodeMode.Branch);
        public void LeafMode()
        {
            outSlot.BreakAll();
            ChangeMode(NodeMode.Leaf);
        }
        public void ChangeMode(NodeMode newMode)
        {
            if (!canChangeMode) return;
            mode = newMode;
            UpdateMode();
        }
        public void UpdateMode()
        {
            if (spriteRenderer)
                spriteRenderer.sprite = nodeSprites[(int)mode];
            switch (mode)
            {
                case NodeMode.Island:
                    inSlot.gameObject.SetActive(false);
                    outSlot.gameObject.SetActive(false);
                    addChildButton.SetActive(false);
                    break;
                case NodeMode.Root:
                    inSlot.gameObject.SetActive(false);
                    outSlot.gameObject.SetActive(true);
                    addChildButton.SetActive(true);
                    break;
                case NodeMode.Branch:
                    inSlot.gameObject.SetActive(true);
                    outSlot.gameObject.SetActive(true);
                    addChildButton.SetActive(true);
                    break;
                case NodeMode.Leaf:
                    inSlot.gameObject.SetActive(true);
                    outSlot.gameObject.SetActive(false);
                    addChildButton.SetActive(false);
                    break;
            }
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
