using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis
{
    public class PlayerInputHandler : MonoBehaviour
    {
        #region Serialized Fields
        [SerializeField] private PlayerInput playerInput;
        [SerializeField] private Logic debugLogic;
        [SerializeField] private bool stopAtFirstHit;
        #endregion

        #region Fields
        private List<Collider> PointerHits;
        #endregion

        #region Interfaces
        public void SetInputEnabled(bool value)
        {
            enabled = value;
            Cursor.visible = value;
        }
        #endregion

        #region Functions
        private void FlushInput() {}
        private void ForwardMessage(string methodName, object value = null)
        {
            foreach (var hit in PointerHits)
            {
                hit.SendMessage(methodName, value, SendMessageOptions.DontRequireReceiver);
                if (hit.GetComponent<InteractBase>() && hit.GetComponent<InteractBase>().opaque) break;
            }
        }
        #endregion

        #region Unity Methods
        private void Start()
        {
            foreach (var map in playerInput.actions.actionMaps)
                map.Enable();

            PointerHits = new();
        }

        private void OnEnable()
        {
            playerInput.enabled = true;
            FlushInput();
        }

        private void OnDisable()
        {
            playerInput.enabled = false;
        }
        #endregion

        #region Handlers
        protected void OnInteract() => ForwardMessage("OnInteract");
        protected void OnPress() => ForwardMessage("OnPress");
        protected void OnRelease() => ForwardMessage("OnRelease");
        protected void OnScroll(InputValue value) => ForwardMessage("OnScroll", value.Get<float>());
        protected void OnDebugTest() => debugLogic.Invoke();
        protected void OnPointer(InputValue value)
        {
            Ray r = Camera.main.ScreenPointToRay(value.Get<Vector2>());
            var rawHits = Physics.RaycastAll(r);
            System.Array.Sort(rawHits, (a, b) => a.distance.CompareTo(b.distance));
            List<Collider> newHits = rawHits.Select(hit => hit.collider).ToList();
            foreach (var hit in PointerHits.Except(newHits).ToList())
                if (hit)
                {
                    hit.SendMessage("OnPointerExit", options: SendMessageOptions.DontRequireReceiver);
                    if (hit.GetComponent<InteractBase>() && hit.GetComponent<InteractBase>().opaque) break;
                }
            foreach (var hit in newHits.Except(PointerHits).ToList())
                if (hit)
                {
                    hit.SendMessage("OnPointerEnter", options: SendMessageOptions.DontRequireReceiver);
                    if (hit.GetComponent<InteractBase>() && hit.GetComponent<InteractBase>().opaque) break;
                }
            PointerHits = newHits;
        }
        protected void OnEscape()
        {
#if UNITY_STANDALONE
            Application.Quit();
#endif
#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#endif
        }
        #endregion
    }
}
