using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// La classe rattachée au gameObject.
/// </summary>
public class ProjectileScript : MonoBehaviour
{
    /// <summary>
    /// Vitesse de la balle.
    /// </summary>
    public float moveSpeed = 250.0f;

    /// <summary>
    /// Dommages causés par la balle.
    /// </summary>
    private int damage = 30;
    public int Damage
    {
        get { return damage; }
        set { damage = value;}
    }

    /// <summary>
    /// C'est là que la balle fera son mouvement.
    /// </summary>
    void Update()
    {
        this.transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Si l'objet est en dehors de la caméra, il sera supprimé instantanément.
    /// </summary>
    void OnBecameInvisible()
    {
        Destroy(this.gameObject);
    }

    /// <summary>
    /// Si deux balles entre en collision.
    /// </summary>
    /// <param name="col">Ce avec quoi la balle entre en collision</param>
    void OnTriggerEnter2D(Collider2D col)
    {
        if (gameObject.layer == LayerMask.NameToLayer("blueBallHitBox") && col.gameObject.layer == LayerMask.NameToLayer("redBallHitBox") ||
        gameObject.layer == LayerMask.NameToLayer("redBallHitBox") && col.gameObject.layer == LayerMask.NameToLayer("blueBallHitBox"))
        {
            Destroy(col.gameObject);
            Destroy(this.gameObject);
        }
    }
}
