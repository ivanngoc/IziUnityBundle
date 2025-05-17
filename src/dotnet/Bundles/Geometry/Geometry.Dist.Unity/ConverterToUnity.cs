using IziHardGames.Geometry.Domain.Vectors;
using UnityEngine;

namespace IziHardGames.Geometry.ForUnity
{
    public static class ConverterToUnity
    {
        public static Vector3 AsVector3(in this Vector3RO vector) => new Vector3(vector.x, vector.y, vector.z);
        public static Vector3Int AsVector3Int(in this Vector3IntRO vector) => new Vector3Int(vector.x, vector.y, vector.z);
    }
}
