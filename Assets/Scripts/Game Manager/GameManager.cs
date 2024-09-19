using UnityEngine;

namespace Omnis.Flowchart
{
    public partial class GameManager : MonoBehaviour
    {
        #region Serialized Fields
        public GameSettings settings;
        public NodeRegistry registry;
        [SerializeField] private Node activeNode;
        #endregion

        #region Interfaces
        public Node ActiveNode
        {
            get => activeNode;
            set
            {
                activeNode = value;
                UpdateInputFieldText();
            }
        }
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
