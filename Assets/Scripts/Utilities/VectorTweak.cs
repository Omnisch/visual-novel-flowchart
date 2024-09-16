using UnityEngine;

namespace Omnis
{
    public class VectorTweak : MonoBehaviour
    {
        public static Vector3 V2ToV3xy(Vector2 v2) => new(v2.x, v2.y, 0f);
        public static Vector3 V2ToV3xz(Vector2 v2) => new(v2.x, 0f, v2.y);
    }
}
