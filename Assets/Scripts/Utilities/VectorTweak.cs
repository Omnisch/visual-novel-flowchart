using UnityEngine;

namespace Omnis
{
    public class VectorTweak : MonoBehaviour
    {
        /// <returns>(x, y, 0)</returns>
        public static Vector3 V2ToV3xy(Vector2 v2) => new(v2.x, v2.y, 0f);
        /// <returns>(x, 0, y)</returns>
        public static Vector3 V2ToV3xz(Vector2 v2) => new(v2.x, 0f, v2.y);



        /// <returns>(x, y, n)</returns>
        public static Vector3 xyn(Vector3 v, float n) => new(v.x, v.y, n);
        /// <returns>(x, n, y)</returns>
        public static Vector3 xnz(Vector3 v, float n) => new(v.x, n, v.z);
        /// <returns>(n, y, z)</returns>
        public static Vector3 nyz(Vector3 v, float n) => new(n, v.y, v.z);
        /// <returns>(x, y, 0)</returns>
        public static Vector3 xyo(Vector3 v) => xyn(v, 0);
        /// <returns>(x, 0, y)</returns>
        public static Vector3 xoz(Vector3 v) => xnz(v, 0);
        /// <returns>(0, y, z)</returns>
        public static Vector3 oyz(Vector3 v) => nyz(v, 0);
    }
}
