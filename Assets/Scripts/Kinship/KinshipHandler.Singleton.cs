using System.Collections;
using UnityEngine;

namespace Omnis.Flowchart.Kinship
{
    public partial class KinshipHandler
    {
        #region Fields
        private static KinshipHandler instance;
        #endregion

        #region Interfaces
        public static KinshipHandler Instance => instance;
        #endregion

        #region Functions
        private bool EnsureSingleton()
        {
            if (Instance != null && Instance != this)
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
        #endregion

        #region Unity Methods
        private void Awake()
        {
            if (!EnsureSingleton())
                return;
        }
        #endregion
    }
}
