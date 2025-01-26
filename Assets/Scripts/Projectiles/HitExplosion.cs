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
}
