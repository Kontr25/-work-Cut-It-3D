using _Scripts.UI;
using UnityEngine;

public class Level : MonoBehaviour
{
    [SerializeField] private int _attempsOnLevel;
    [SerializeField] private Vector3 _gravity = new Vector3(0, -9.81f, 0);

    private void Start()
    {
        AttempsCounter.Instance.LevelAttemps = _attempsOnLevel;
        Physics.gravity = _gravity;
    }
}
