using UnityEngine;

namespace Omnis.BranchTracker
{
    public partial class GameManager : MonoBehaviour
    {
        #region Serialized Fields
        public GameSettings gameSettings;
        public NodePriority nodePriority;
        public Node root;
        #endregion

        #region Fields
        private Node activeNode;
        #endregion

        #region Interfaces
        public Node ActiveNode
        {
            get => activeNode;
            set
            {
                activeNode = value;
                nodePriority.Prioritize(activeNode);
                UpdateInputFieldText();
            }
        }
        public LinkSlot TargetSlot { get; set; }
        #endregion

        #region Unity Methods
        private void Awake()
        {
            if (!EnsureSingleton())
                return;
        }

        private void Update()
        {
            if (ActiveNode) ActiveNode.OnUpdate();
        }
        #endregion
    }
}
