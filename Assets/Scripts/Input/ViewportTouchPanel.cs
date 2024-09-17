using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis
{
    // Add this script to a collider binding to the cam that you want to control.
    public class ViewportTouchPanel : InteractBase
    {
        #region Serialized Fields
        [SerializeField] private Camera cam;
        [SerializeField] private Camera anchorCam;
        [SerializeField] private float dragScale = 1f;
        public Vector2 scrollLimit;
        public bool scrollControl;
        public bool dragControl;
        #endregion

        #region Fields
        private float scrollScale;
        private Vector3 cursorOffset;
        #endregion

        #region Interfaces
        public override bool IsMiddlePressed
        {
            get => base.IsMiddlePressed;
            set
            {
                base.IsMiddlePressed = value;
                if (value)
                    cursorOffset = cam.transform.position + anchorCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                else
                    anchorCam.transform.position = cam.transform.position;
            }
        }
        public float ScrollScale
        {
            get => scrollScale;
            set
            {
                scrollScale = Mathf.Clamp(value, scrollLimit.x, scrollLimit.y);
                cam.orthographicSize = anchorCam.orthographicSize = scrollScale;
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
                cam.transform.position = - anchorCam.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + cursorOffset;
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
