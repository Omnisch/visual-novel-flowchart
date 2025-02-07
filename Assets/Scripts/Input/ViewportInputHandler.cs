using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.Flowchart
{
    // Add this script to a collider binding to the cam that you want to control.
    public class ViewportInputHandler : InteractBase
    {
        #region Serialized Fields
        [SerializeField] private Camera cam;
        [SerializeField] private Camera anchorCam;
        public Vector2 scrollLimit;
        #endregion

        #region Fields
        private float scrollScale;
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
                    cursorOffset = cam.transform.position + anchorCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                else
                    anchorCam.transform.position = cam.transform.position;
            }
        }
        public override bool IsMiddlePressed
        {
            get => base.IsMiddlePressed;
            set
            {
                base.IsMiddlePressed = value;
                if (!IsLeftPressed)
                {
                    if (value)
                        cursorOffset = cam.transform.position + anchorCam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
                    else
                        anchorCam.transform.position = cam.transform.position;
                }
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
            scrollScale = cam.orthographicSize;
        }

        private void Update()
        {
            if (IsLeftPressed || IsMiddlePressed)
                cam.transform.position = - anchorCam.ScreenToWorldPoint(Mouse.current.position.ReadValue()) + cursorOffset;
        }
        #endregion

        #region Handlers
        protected void OnScroll(float value)
        {
            if (value == 0f) return;
            ScrollScale -= Mathf.Sign(value);
        }
        #endregion
    }
}
