using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Cette classe sert à gérer la collision entre le champ de vision
/// d'un soldat et 
/// Note : Ce script a été réalisé avec l'aide d'Alex Bouchard
/// </summary>
public class EnemyDetect : MonoBehaviour
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="col"></param>
    void OnTriggerEnter2D(Collider2D col)
    {
        //Détection d'une forêt.
        if (gameObject.GetComponentInParent<SoldierScript>().gameObject.layer == LayerMask.NameToLayer("blueHitBox") && col.gameObject.layer == LayerMask.NameToLayer("Terrain") ||
        gameObject.GetComponentInParent<SoldierScript>().gameObject.layer == LayerMask.NameToLayer("redHitBox") && col.gameObject.layer == LayerMask.NameToLayer("Terrain"))
        {
            SoldierState soldierState = gameObject.GetComponentInParent<SoldierScript>().SoldierState;
            soldierState.DetectForest(gameObject.GetComponentInParent<SoldierScript>().gameObject, col.gameObject);
        }
        
        ////Détecter une tour ennemie.
        if (gameObject.GetComponentInParent<SoldierScript>().gameObject.layer == LayerMask.NameToLayer("blueHitBox") && col.gameObject.layer == LayerMask.NameToLayer("redTower") ||
                gameObject.GetComponentInParent<SoldierScript>().gameObject.layer == LayerMask.NameToLayer("redHitBox") && col.gameObject.layer == LayerMask.NameToLayer("blueTower"))
        {
            SoldierState soldierState = gameObject.GetComponentInParent<SoldierScript>().SoldierState;
            soldierState.DetectTower(gameObject.GetComponentInParent<SoldierScript>().gameObject, col.gameObject);
        }

        ////Détecter d'un autre soldat.
        if (gameObject.GetComponentInParent<SoldierScript>().gameObject.layer == LayerMask.NameToLayer("blueHitBox") && col.gameObject.layer == LayerMask.NameToLayer("redHitBox") ||
        gameObject.GetComponentInParent<SoldierScript>().gameObject.layer == LayerMask.NameToLayer("redHitBox") && col.gameObject.layer == LayerMask.NameToLayer("blueHitBox"))
        {
            SoldierState soldierState = gameObject.GetComponentInParent<SoldierScript>().SoldierState;
            soldierState.DetectEnemy(gameObject.GetComponentInParent<SoldierScript>().gameObject, col.gameObject);
        }
    }
}
