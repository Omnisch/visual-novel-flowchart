using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.Flowchart
{
    public class FloatLinkSlot : Linkable
    {
        #region Interfaces
        public void TryPassLink()
        {
            bool passed = false;
            if (GameManager.Instance.TargetSlot)
            {
                if (inLinks.Count > 0)
                    passed = GameManager.Instance.TargetSlot.TryAcceptInLink(inLinks[0]);
                else if (outLinks.Count > 0)
                    passed = GameManager.Instance.TargetSlot.TryAcceptOutLink(outLinks[0]);
            }

            if (!passed) BreakAll();
            Destroy(gameObject);
        }
        #endregion

        #region Unity Methods
        private void Update()
        {
            transform.position = VectorTweak.xyn(Camera.main.ScreenToWorldPoint(Mouse.current.position.ReadValue()), transform.position.z);
            UpdateLinks();
        }
        #endregion
    }
}
