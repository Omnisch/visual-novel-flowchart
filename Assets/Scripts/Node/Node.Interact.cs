using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.BranchTracker
{
    public partial class Node
    {
        #region Fields
        private Vector3 cursorOffset;
        #endregion

        #region Interfaces
        public override bool IsLeftPressed
        {
            get => base.IsLeftPressed;
            set
            {
                base.IsLeftPressed = value;
                if (value)
                {
                    GameManager.Instance.ActiveNode = this;
                    cursorOffset = transform.position - Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                }
            }
        }

        public void OnUpdate()
        {
            if (IsLeftPressed)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + cursorOffset;
                UpdateLinks();
            }
        }
        #endregion
    }
}
