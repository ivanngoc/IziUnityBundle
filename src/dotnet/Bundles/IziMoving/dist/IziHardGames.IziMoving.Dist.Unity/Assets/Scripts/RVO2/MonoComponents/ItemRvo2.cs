using System.Runtime.InteropServices;
using UnityEngine;

namespace IziHardGames.IziMoving.RVO2.MonoComponents
{
    [Guid("873619e3-969f-6554-a9f3-659ad03a3840")]
    public abstract class ItemRvo2 : MonoBehaviour
    {
        public int Id => id;

        [SerializeField] protected RefRvo2System? system;
        [SerializeField] private int id;

        public void Bind(int id, RefRvo2System system)
        {
            this.id = id;
            this.system = system;
        }
    }
}
