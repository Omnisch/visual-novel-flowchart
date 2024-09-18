using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.Flowchart
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
        public override bool IsRightPressed
        {
            get => base.IsRightPressed;
            set
            {
                base.IsRightPressed = value;
                if (value) GameManager.Instance.ActiveNode = this;
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
