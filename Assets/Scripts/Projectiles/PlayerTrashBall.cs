using UnityEngine;
using GGJ2025.Utilities;

namespace GGJ2025
{
  public class PlayerTrashBall : Projectile
  {
    protected override void Awake()
    {
      base.Awake();
      SetInitialData(Constants.Projectiles.PlayerTrashBall);
    }

    public void SetTargetLocation(Vector2 location)
    {
      var diff = location - (Vector2)transform.position;
      Direction = diff.normalized;
      LifeTime = diff.magnitude / Speed;
    }

    protected override (bool shouldDamage, bool shouldDestroy) GetHitHandling(Collider2D other)
    {
      var shouldDestroy = !other.CompareTag("Player") && !other.isTrigger;
      return (false, shouldDestroy);
    }
  }
}