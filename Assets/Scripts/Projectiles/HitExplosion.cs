using System;
using GGJ2025;
using GGJ2025.Soap;
using UnityEngine;
/// <summary>
/// Hit explosion animation 
/// </summary>
public class HitExplosion : MonoBehaviour
{
 
    /// <summary>
    /// Destroy explosion after being played
    /// </summary>
    public void DestroyExplosion()
    {
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<Trash>(out _) || other.TryGetComponent<EnemyNavigation>(out _))
        {
            Destroy(other.gameObject);
        }
    }
}
