using System.Collections.Generic;
using UnityEngine;

namespace Omnis.BranchTracker
{
    public partial class Node
    {
        #region Serialized Fields
        public NodeMode mode;
        [SerializeField] private GameObject removeButton;
        [SerializeField] private GameObject addChildButton;
        [SerializeField] private List<Sprite> nodeSprites;
        #endregion

        #region Fields
        private SpriteRenderer spriteRenderer;
        #endregion

        #region Interfaces
        public void ChangeMode(NodeMode newMode)
        {
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
                    removeButton.SetActive(false);
                    outSlot.gameObject.SetActive(false);
                    addChildButton.SetActive(false);
                    break;
                case NodeMode.Root:
                    inSlot.gameObject.SetActive(false);
                    removeButton.SetActive(false);
                    outSlot.gameObject.SetActive(true);
                    addChildButton.SetActive(true);
                    break;
                case NodeMode.Branch:
                    inSlot.gameObject.SetActive(true);
                    removeButton.SetActive(true);
                    outSlot.gameObject.SetActive(true);
                    addChildButton.SetActive(true);
                    break;
                case NodeMode.Leaf:
                    inSlot.gameObject.SetActive(true);
                    removeButton.SetActive(true);
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
