using UnityEngine;

namespace Omnis
{
    public class Logic : MonoBehaviour
    {
        #region Serialized fields
        public UnityEngine.Events.UnityEvent callback;
        #endregion

        #region Interfaces
        [ContextMenu("Invoke")]
        public virtual void Invoke()
        {
            callback?.Invoke();
        }
        #endregion
    }
}
