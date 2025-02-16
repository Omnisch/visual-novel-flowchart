using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Omnis.Flowchart
{
    // Add this script to a collider that receives the context menu call.
    public class ContextMenuHandler : InteractBase
    {
        #region Serialized Fields
        [SerializeField] private ContextMenu menu;
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
            menu.gameObject.SetActive(true);
            StartCoroutine(YieldTweaker.Lerp((value) => {
                menu.GetComponent<CanvasGroup>().alpha = value;
            }, 15f));
        }
        public void HideContextMenu()
        {
            StopAllCoroutines();
            StartCoroutine(YieldTweaker.Lerp((value) => {
                menu.GetComponent<CanvasGroup>().alpha = 1f - value;
                if (value == 1f) menu.gameObject.SetActive(false);
            }, 30f));
        }

        public void CreateNode()
            => GameManager.Instance.CreateNode(Camera.main.ScreenToWorldPoint(menu.transform.position));
        public void SaveFile()
            => IO.OpenBrowserAndSaveFile(GameManager.Instance.registry.Data);
        public void LoadFile()
            => IO.OpenBrowserAndLoadFile<FlowchartData>((data) => GameManager.Instance.registry.LoadData(data));
        #endregion

        #region Functions
        protected override void OnInteracted(List<Collider> hits)
        {
            menu.targets = hits.Select(hit => hit.gameObject).ToList();
        }
        #endregion

        #region Handlers
        protected void OnSave() => SaveFile();
        protected void OnLoad() => LoadFile();
        #endregion
    }
}
