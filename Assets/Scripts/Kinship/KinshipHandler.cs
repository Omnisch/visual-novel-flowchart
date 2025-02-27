using System.Linq;
using System.Runtime.InteropServices;
using UnityEngine;

namespace Omnis.Flowchart.Kinship
{
    public partial class KinshipHandler : MonoBehaviour
    {
        #region Public functions
        public string TranslateKinship(string query)
        {
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                string rawAnswer = Marshal.PtrToStringAuto(Relationship(query));
                string[] answers = rawAnswer.Trim('[', ']').Split(',').Select((e) => e.Trim(' ', '\'', '\"')).ToArray();
                return string.Join('/', answers);
            }
            else
                return "WebGL Only";
        }
        #endregion

        #region Functions
        [DllImport("__Internal")]
        private static extern System.IntPtr Relationship(string query);
        #endregion
    }
}
