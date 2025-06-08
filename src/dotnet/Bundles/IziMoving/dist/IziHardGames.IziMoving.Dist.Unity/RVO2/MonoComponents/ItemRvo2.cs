using UnityEngine;

namespace IziHardGames.IziMoving.RVO2.MonoComponents
{
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
