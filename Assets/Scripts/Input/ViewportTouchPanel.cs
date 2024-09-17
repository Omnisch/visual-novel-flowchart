using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis
{
    // Add this script to a collider binding to the cam that you want to control.
    public class ViewportTouchPanel : InteractBase
    {
        #region Serialized Fields
        [SerializeField] private Camera cam;
        [SerializeField] private float dragScale = 1f;
        public Vector2 scrollLimit;
        public bool scrollControl;
        public bool dragControl;
        #endregion

        #region Fields
        private float scrollScale;
        #endregion

        #region Interfaces
        public float ScrollScale
        {
            get => scrollScale;
            set
            {
                scrollScale = Mathf.Clamp(value, scrollLimit.x, scrollLimit.y);
                cam.orthographicSize = scrollScale;
            }
        }
        #endregion

        #region Unity Methods
        protected override void Start()
        {
            base.Start();
            scrollScale = 5f;
        }

        private void Update()
        {
            if (dragControl && IsMiddlePressed)
                cam.transform.position -= (2f * dragScale * ScrollScale / Screen.height) * VectorTweak.V2ToV3xy(Mouse.current.delta.ReadValue());
        }
        #endregion

        #region Handlers
        protected void OnScroll(float value)
        {
            if (!scrollControl) return;
            if (value == 0f) return;
            ScrollScale -= Mathf.Sign(value);
        }
        #endregion
    }
}
