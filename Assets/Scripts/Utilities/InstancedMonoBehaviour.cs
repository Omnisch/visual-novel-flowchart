using System.Collections;
using UnityEngine;

namespace Omnis
{
    public abstract class InstancedMonoBehaviour : MonoBehaviour
    {
        #region Fields
        protected static InstancedMonoBehaviour instance;
        #endregion

        #region Functions
        private bool EnsureSingleton()
        {
            if (instance != null && instance != this)
            {
                Destroy(gameObject);
                return false;
            }
            else
            {
                instance = this;
                StartCoroutine(DontDestroySelfOnLoadCoroutine());
                return true;
            }
        }

        private IEnumerator DontDestroySelfOnLoadCoroutine()
        {
            yield return new WaitUntil(() => gameObject.scene.isLoaded);
            DontDestroyOnLoad(gameObject);
        }

        /// <summary>
        /// Do not use Unity method Awake(),
        /// because it's occupied.<br/>
        /// Use OnAwake() instead.
        /// </summary>
        protected abstract void OnAwake();
        #endregion

        #region Unity Methods
        protected void Awake()
        {
            if (!EnsureSingleton())
                return;
            OnAwake();
        }
        #endregion
    }
}
