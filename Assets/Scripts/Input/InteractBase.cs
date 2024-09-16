using UnityEngine;

namespace Omnis
{
    [RequireComponent(typeof(Collider))]
    public abstract class InteractBase : MonoBehaviour
    {
        #region Serialized Fields
        public bool opaque;
        #endregion

        #region Fields
        protected bool interactable;
        protected bool isPointed;
        protected bool isPressed;
        #endregion

        #region Interfaces
        public virtual bool Interactable
        {
            get => interactable;
            set => interactable = value;
        }
        public virtual bool IsPointed
        {
            get => isPointed;
            set => isPointed = value;
        }
        public virtual bool IsPressed
        {
            get => isPressed;
            set => isPressed = value;
        }
        #endregion

        #region Functions
        protected virtual void OnInteracted() {}
        #endregion

        #region Unity Methods
        protected virtual void Start()
        {
            interactable = true;
            isPointed = false;
        }
        #endregion

        #region Handlers
        private void OnPointerEnter()
        {
            if (!Interactable) return;
            
            IsPointed = true;
        }

        private void OnPointerExit()
        {
            if (!Interactable) return;

            IsPointed = false;
        }

        private void OnPress()
        {
            if (!Interactable) return;

            IsPressed = true;
        }

        private void OnRelease()
        {
            if (!Interactable) return;

            IsPressed = false;
        }

        private void OnInteract()
        {
            if (!Interactable) return;

            OnInteracted();
        }
        #endregion
    }
}
