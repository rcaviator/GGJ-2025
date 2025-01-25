using UnityEngine;

namespace GGJ2025.Utilities
{
  public class CameraFollow : MonoBehaviour
  {
    [SerializeField] private float minX;
    [SerializeField] private float maxX;

    private Transform player = null!;
    private Transform self = null!;

    private void Start()
    {
      player = GameObject.FindGameObjectWithTag("Player").transform;
      self = transform;
    }

    private void Update()
    {
      var position = self.position;
      position.x = Mathf.Clamp(player.position.x, minX, maxX);
      self.position = position;
    }
  }
}