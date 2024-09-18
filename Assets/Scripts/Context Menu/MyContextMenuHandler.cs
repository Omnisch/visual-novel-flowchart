using System.Collections;
using UnityEngine;

namespace Omnis
{
    // Add this script to a collider that receives the context menu call.
    public class MyContextMenuHandler : InteractBase
    {
        #region Serialized Fields
        [SerializeField] private GameObject menu;
        #endregion

        #region Interfaces
        public override bool IsLeftPressed
        {
            get => base.IsLeftPressed;
            set
            {
                base.IsLeftPressed = value;
                if (!value) HideContextMenu();
            }
        }
        public override bool IsRightPressed
        {
            get => base.IsRightPressed;
            set
            {
                base.IsRightPressed = value;
                if (!value) CallContextMenu();
            }
        }
        public override bool IsMiddlePressed
        {
            get => base.IsMiddlePressed;
            set
            {
                base.IsMiddlePressed = value;
                if (!value) HideContextMenu();
            }
        }
        public void CallContextMenu()
        {
            StopAllCoroutines();
            StartCoroutine(IShowContextMenu());
        }

        public void HideContextMenu()
        {
            StopAllCoroutines();
            StartCoroutine(IHideContextMenu());
        }
        #endregion

        #region Functions
        private IEnumerator IShowContextMenu()
        {
            menu.transform.position = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
            menu.SetActive(true);
            yield return new WaitForSecondsRealtime(0.1f);
            if (menu.GetComponent<UnityEngine.UI.Image>())
                menu.GetComponent<UnityEngine.UI.Image>().color = new(1f, 1f, 1f, 1f);
        }
        private IEnumerator IHideContextMenu()
        {
            if (menu.GetComponent<UnityEngine.UI.Image>())
                menu.GetComponent<UnityEngine.UI.Image>().color = new(1f, 1f, 1f, 0.6f);
            yield return new WaitForSecondsRealtime(0.1f);
            menu.SetActive(false);
        }
        #endregion
    }
}
