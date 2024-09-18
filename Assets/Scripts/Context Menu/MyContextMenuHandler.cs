using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Omnis.Flowchart
{
    // Add this script to a collider that receives the context menu call.
    public class MyContextMenuHandler : InteractBase
    {
        #region Serialized Fields
        [SerializeField] private MyContextMenu menu;
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
                if (value) CallContextMenu();
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

        public void CreateNode() => GameManager.Instance.nodeRegistry.NewNode(Camera.main.ScreenToWorldPoint(menu.transform.position));
        #endregion

        #region Functions
        protected override void OnInteracted(List<Collider> hits)
        {
            menu.targets = hits.Select(hit => hit.gameObject).ToList();
        }
        private IEnumerator IShowContextMenu()
        {
            menu.transform.position = UnityEngine.InputSystem.Mouse.current.position.ReadValue();
            menu.gameObject.SetActive(true);
            yield return new WaitForSecondsRealtime(0.1f);
            if (menu.GetComponent<UnityEngine.UI.Image>())
                menu.GetComponent<UnityEngine.UI.Image>().color = new(1f, 1f, 1f, 1f);
        }
        private IEnumerator IHideContextMenu()
        {
            if (menu.GetComponent<UnityEngine.UI.Image>())
                menu.GetComponent<UnityEngine.UI.Image>().color = new(1f, 1f, 1f, 0.6f);
            yield return new WaitForSecondsRealtime(0.1f);
            menu.gameObject.SetActive(false);
        }
        #endregion
    }
}
