using UnityEngine;
using UnityEngine.InputSystem;

namespace Omnis.BranchTracker
{
    public class FloatLinkSlot : Linkable
    {
        #region Interfaces
        public void TryPassLink()
        {
            if (inLinks.Count > 0 && GameManager.Instance.TargetSlot)
                if (GameManager.Instance.TargetSlot.TryAcceptLink(inLinks[0]))
                {
                    Destroy(gameObject);
                    return;
                }

            BreakAll();
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
