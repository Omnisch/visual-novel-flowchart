using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Omnis.BranchTracker
{
    public partial class GameManager
    {
        #region Serialized Fields
        public TMP_InputField inputField;
        #endregion

        #region Fields
        #endregion

        #region Interfaces
        public void UpdateActiveNodeText() => ActiveNode.Description = inputField.text;
        #endregion

        #region Functions
        private void UpdateInputFieldText() => inputField.text = ActiveNode.Description;
        #endregion
    }
}
