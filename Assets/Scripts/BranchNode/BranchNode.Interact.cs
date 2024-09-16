using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.BranchTracker
{
    public partial class BranchNode
    {
        #region Fields
        private Vector3 cursorOffset;
        #endregion

        #region Interfaces
        public override bool IsPressed
        {
            get => isPressed;
            set
            {
                isPressed = value;
                if (isPressed)
                {
                    GameManager.Instance.ActiveNode = this;
                    cursorOffset = transform.position - Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                }
            }
        }

        public void OnUpdate()
        {
            if (isPressed)
            {
                transform.position = Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + cursorOffset;
                UpdateLinks();
            }
        }
        #endregion
    }
}
