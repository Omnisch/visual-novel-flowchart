using UnityEngine;

namespace Omnis.BranchTracker
{
    public partial class GameManager : MonoBehaviour
    {
        #region Serialized Fields
        public GameSettings gameSettings;
        public BranchNode root;
        public NodePriority nodePriority;
        #endregion

        #region Fields
        #endregion

        #region Interfaces
        #endregion

        #region Functions
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
