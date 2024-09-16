using UnityEngine;
using UnityEngine.Events;

namespace Omnis
{
    [RequireComponent(typeof(Collider))]
    public class ClickLogic : PointerBase
    {
        #region Serialized Fields
        public UnityEvent enterCallback;
        public UnityEvent pressCallback;
        public UnityEvent releaseCallback;
        public UnityEvent exitCallback;
        #endregion

        #region Interfaces
        public override bool IsPointed
        {
            get => isPointed;
            set
            {
                isPointed = value;
                if (isPointed) enterCallback?.Invoke();
                else exitCallback?.Invoke();
            }
        }
        public override bool IsPressed
        {
            get => isPressed;
            set
            {
                isPressed = value;
                if (isPressed) pressCallback?.Invoke();
                else releaseCallback?.Invoke();
            }
        }
        #endregion
    }
}
