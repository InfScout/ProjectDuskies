using UnityEngine;

public class PlayerChecker : MonoBehaviour
{
    [SerializeField] private float _checkRadius;
    [SerializeField] private Transform _playerTransform;

    public bool IsPlayerInRange()
    {
        float distance = Vector3.Distance(transform.position, _playerTransform.position);
        return distance <= _checkRadius;
    }

    public Vector3 GetplayerPosition()
    {
        return _playerTransform.position;
    }
}
