using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe sert à gérer les collisions entre le joueur et la forêt.
/// </summary>
public class ForetScript : MonoBehaviour
{
    /// <summary>
    /// Ceci sera appelé lorsque l'objet courant ne sera plus en contact avec un autre objet.
    /// Pour résumer, si un soldat entre dans la forêt, sa vitesse est diminuée.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        //Juste pour tester que les collisions avec les forêts fonctionnent.
        Debug.Log("Trigger de" + gameObject.name);

        if (col.gameObject.layer == LayerMask.NameToLayer("redHitBox") || col.gameObject.layer == LayerMask.NameToLayer("blueHitBox"))
        {
            col.gameObject.GetComponent<SoldierScript>().Speed = col.gameObject.GetComponent<SoldierScript>().Speed / 2;
        }
    }

    /// <summary>
    /// Ceci sera appelé lorsque l'objet courant ne sera plus en contact avec un autre objet.
    /// Pour résumer, si un soldat sort d'une forêt, sa vitesse est augmentée.
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerExit2D(Collider2D col)
    {
        //Juste pour tester que les collisions avec les forêts fonctionnent.
        Debug.Log("Trigger de" + gameObject.name);

        if (col.gameObject.layer == LayerMask.NameToLayer("redHitBox") || col.gameObject.layer == LayerMask.NameToLayer("blueHitBox"))
        {
            col.gameObject.GetComponent<SoldierScript>().Speed = col.gameObject.GetComponent<SoldierScript>().Speed * 2;
        }

    }
}
