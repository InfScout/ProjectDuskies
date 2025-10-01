using UnityEngine;

namespace Player
{
    public class WallCheck : MonoBehaviour
    {
        [SerializeField] private LayerMask _wallLayer;
        [SerializeField] private Transform _wallCheckPos;
        [SerializeField] private Vector2 _wallCheckSize =  new Vector2(1f, 1f);

        public bool IsWalled()
        {
            if (Physics2D.OverlapBox(_wallCheckPos.position, _wallCheckSize, 0, _wallLayer))
            {
                return true;
            }

            return false;
        }

        public void OnDrawGizmos()
        {
            Gizmos.color = Color.greenYellow;
            Gizmos.DrawCube(_wallCheckPos.position, _wallCheckSize);
        } 
    }
}