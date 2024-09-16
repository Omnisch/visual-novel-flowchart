using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.BranchTracker
{
    public partial class BranchNode
    {
        #region Fields
        private Vector3 pointerOffset;
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
                    GameManager.Instance.nodePriority.Prioritize(this);
                    pointerOffset = transform.position - Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                }
            }
        }
        #endregion
    }
}
