using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Voici l'interface qui comprend l'ensemble des méthodes dont
/// les classes qui implémentent cette interface doit avoir.
/// </summary>
public interface SoldierState
{
    //Pour déplacer un soldat
    void MoveSoldier(GameObject soldier);
    
    //Vérification des attributs pour effectuer les actions
    void Update(GameObject soldier);

    //Est en train de tirer
    void IsFiring(GameObject soldier);

    //S'il apercoit un soldat ennemi!
    void DetectEnemy(GameObject soldier, GameObject enemy);

    //S'il apercoit une tour ennemie!
    void DetectTower(GameObject soldier, GameObject tower);

    //S'il apercoit une forêt!
    void DetectForest(GameObject soldier, GameObject forest);
}

/// <summary>
/// L'état normal est l'état de départ. C'est-à-dire celui ou le soldat
/// se déplace normalement et tire s'il croise un ennemi.
/// </summary>
public class NormalState : SoldierState
{
    //Pour déplacer un soldat
    public void MoveSoldier(GameObject soldier)
    {
        if (soldier.gameObject.GetComponent<SoldierScript>().IsShooting == false)
        {
            float speed = soldier.GetComponent<SoldierScript>().Speed;
        
            //Un pas est multiplié parce qui est habituellement 1/60 de seconde.
            float pas = speed * Time.deltaTime;

            //La méthode MoveTowards de vector2 fait pas mal la job de déplacement pour nous.  Pour le moment elle va chercher la nouvelle position, mais le déplacement n'est pas encore fait.
            Vector2 nouvellePosition = Vector2.MoveTowards(soldier.transform.position, soldier.GetComponent<SoldierScript>().Destination, pas);
        
            //Différence entre la nouvelle et l'ancienne position, pour calculer l'angle de rotation
            Vector2 mouvement = new Vector2(nouvellePosition.x - soldier.transform.position.x, nouvellePosition.y - soldier.transform.position.y);

            //Angle de rotation en degrée (car l'affichage de Unity veut les angles en degrée); Mathf.Rad2Deg nécessaire car Mathf.Atan2 un résultat en radiants
            float angle = Mathf.Atan2(mouvement.y, mouvement.x) * Mathf.Rad2Deg;

            //On applique la nouvelle position
            soldier.transform.position = nouvellePosition;

            //On applique la nouvelle rotation
            soldier.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    //Vérification des stats pour effectuer les actions
    public void Update(GameObject soldier)
    {
        soldier.GetComponent<SoldierScript>().AffichageDeTexte("Normal");

        GameObject cible = soldier.GetComponent<SoldierScript>().Target;

        if (soldier.GetComponent<SoldierScript>().IsShooting == true && cible != null)
        {
            //Différence entre la nouvelle et l'ancienne position, pour calculer l'angle de rotation
            Vector2 vecteurDeDirection = new Vector2(cible.transform.position.x - soldier.transform.position.x, cible.transform.position.y - soldier.transform.position.y);

            //Angle de rotation en degrée (car l'affichage de Unity veut les angles en degrée); Mathf.Rad2Deg nécessaire car Mathf.Atan2 un résultat en radiants
            float angle = Mathf.Atan2(vecteurDeDirection.y, vecteurDeDirection.x) * Mathf.Rad2Deg;

            //On applique la nouvelle rotation
            soldier.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            soldier.GetComponent<SoldierScript>().Fire();
        }

        //Si le joueur n'a pas de cible.
        if (cible == null)
        {
            soldier.GetComponent<SoldierScript>().Target = soldier.GetComponent<SoldierScript>().InitialTarget;
            soldier.GetComponent<SoldierScript>().IsShooting = false;
            soldier.GetComponent<SoldierScript>().Kills += 1;
            Debug.Log(soldier.name + "a fait un kill!");
        }

        if (soldier.GetComponent<SoldierScript>().Kills >= 3)
        {
            soldier.GetComponent<SoldierScript>().SoldierState = new ConfidentState();
            Debug.Log(soldier.name + "EST MAINTENANT CONFIANT!");
            soldier.GetComponent<SoldierScript>().AffichageDeTexte("Confiant");
        }

        if (soldier.GetComponent<SoldierScript>().HealthPoints < soldier.GetComponent<SoldierScript>().QuartDeVie)
        {
            soldier.GetComponent<SoldierScript>().Destination = soldier.GetComponent<SoldierScript>().DeparturePoint;
            soldier.GetComponent<SoldierScript>().AffichageDeTexte("Fuite");
            soldier.GetComponent<SoldierScript>().SoldierState = new RetreatState();
        }
    }

    //Si le joueur est en train de tirer.
    public void IsFiring(GameObject soldier)
    {
        //N'est pas utilisé dans cet état.
    }
    
    //S'il apercoit un soldat ennemi!
    public void DetectEnemy(GameObject soldier, GameObject enemy)
    {
        soldier.GetComponent<SoldierScript>().Target = enemy.gameObject;
        soldier.GetComponent<SoldierScript>().IsShooting = true;
    }

    //S'il apercoit une tour ennemie!
    public void DetectTower(GameObject soldier, GameObject tower)
    {
        soldier.GetComponent<SoldierScript>().Target = tower.gameObject;
        soldier.GetComponent<SoldierScript>().IsShooting = true;
    }

    //S'il apercoit une foret!
    public void DetectForest(GameObject soldier, GameObject forest)
    {
        Debug.Log("Foret DÉTECTÉE!!");
    }

}

/// <summary>
/// L'état confiant est déclenché lorsque le soldat effectue 3 "kills".
/// Ceci fera en sorte qu'il va se déplacer plus vite, en plus de se
/// régénerer lorsqu'il tire.
/// </summary>
public class ConfidentState : SoldierState
{
    //Pour déplacer un soldat
    public void MoveSoldier(GameObject soldier)
    {
        if (soldier.gameObject.GetComponent<SoldierScript>().IsShooting == false)
        {
            float speed = soldier.GetComponent<SoldierScript>().Speed;

            //Un pas est multiplié parce qui est habituellement 1/60 de seconde.
            float step = speed * Time.deltaTime * 2;

            //La méthode MoveTowards de vector2 fait pas mal la job de déplacement pour nous.  Pour le moment elle va chercher la nouvelle position, mais le déplacement n'est pas encore fait.
            Vector2 nouvellePosition = Vector2.MoveTowards(soldier.transform.position, soldier.GetComponent<SoldierScript>().Destination, step);

            //Différence entre la nouvelle et l'ancienne position, pour calculer l'angle de rotation
            Vector2 mouvement = new Vector2(nouvellePosition.x - soldier.transform.position.x, nouvellePosition.y - soldier.transform.position.y);

            //Angle de rotation en degrée (car l'affichage de Unity veut les angles en degrée); Mathf.Rad2Deg nécessaire car Mathf.Atan2 un résultat en radiants
            float angle = Mathf.Atan2(mouvement.y, mouvement.x) * Mathf.Rad2Deg;

            //On applique la nouvelle position
            soldier.transform.position = nouvellePosition;

            //On applique la nouvelle rotation
            soldier.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    //Vérification des stats pour effectuer les actions
    public void Update(GameObject soldier)
    {
        soldier.GetComponent<SoldierScript>().AffichageDeTexte("Confiant");

        GameObject target = soldier.GetComponent<SoldierScript>().Target;

        if (soldier.GetComponent<SoldierScript>().IsShooting == true && target != null)
        {
            ////Différence entre la nouvelle et l'ancienne position, pour calculer l'angle de rotation
            Vector2 vecteurDeDirection = new Vector2(target.transform.position.x - soldier.transform.position.x, target.transform.position.y - soldier.transform.position.y);

            ////Angle de rotation en degrée (car l'affichage de Unity veut les angles en degrée); Mathf.Rad2Deg nécessaire car Mathf.Atan2 un résultat en radiants
            float angle = Mathf.Atan2(vecteurDeDirection.y, vecteurDeDirection.x) * Mathf.Rad2Deg;

            //On applique la nouvelle rotation
            soldier.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            soldier.GetComponent<SoldierScript>().Fire();
        }

        if (target == null)
        {
            soldier.GetComponent<SoldierScript>().Target = soldier.GetComponent<SoldierScript>().InitialTarget;
            soldier.GetComponent<SoldierScript>().IsShooting = false;
            soldier.GetComponent<SoldierScript>().Kills += 1;
            Debug.Log(soldier.name + "a fait un kill!");
        }

        if (soldier.GetComponent<SoldierScript>().HealthPoints < soldier.GetComponent<SoldierScript>().QuartDeVie)
        {
            soldier.GetComponent<SoldierScript>().Destination = soldier.GetComponent<SoldierScript>().DeparturePoint;
            soldier.GetComponent<SoldierScript>().AffichageDeTexte("Fuite");
            soldier.GetComponent<SoldierScript>().SoldierState = new RetreatState();

        }
    }

    //Lorsque qu'un soldat en mode confiant tire, il va regagner de la vie.
    public void IsFiring(GameObject soldier)
    {
        if(soldier.GetComponent<SoldierScript>().HealthPoints < soldier.GetComponent<SoldierScript>().maxHealthPoints)
        {
            soldier.GetComponent<SoldierScript>().HealthPoints += 2;
        }
    }

    //S'il apercoit un soldat ennemi!
    public void DetectEnemy(GameObject soldier, GameObject enemy)
    {
        soldier.GetComponent<SoldierScript>().Target = enemy.gameObject;
        soldier.GetComponent<SoldierScript>().IsShooting = true;
    }

    //S'il apercoit une tour ennemie!
    public void DetectTower(GameObject soldier, GameObject tower)
    {
        soldier.GetComponent<SoldierScript>().Target = tower.gameObject;
        soldier.GetComponent<SoldierScript>().IsShooting = true;
    }

    //S'il apercoit une foret!
    public void DetectForest(GameObject soldier, GameObject forest)
    {
        //Ne sert pas ici.
        Debug.Log("Foret DÉTECTÉE!!");
    }
}

/// <summary>
/// L'état fuite est déclenché lorsque le soldat a moins du quart de sa
/// vie restante. Il va donc se mettre à fuir vers une forêt ou une tour
/// alliée afin de se régénerer.
/// </summary
public class RetreatState : SoldierState
{
    //Pour déplacer un soldat
    public void MoveSoldier(GameObject soldier)
    {
        //Variable de vitesse
        float speed = soldier.GetComponent<SoldierScript>().Speed;

        //Un pas est multiplié parce qui est habituellement 1/60 de seconde.
        float step = speed * Time.deltaTime * 1.5f;

        //La méthode MoveTowards de vector2 fait pas mal la job de déplacement pour nous.  Pour le moment elle va chercher la nouvelle position, mais le déplacement n'est pas encore fait.
        Vector2 newPosition = Vector2.MoveTowards(soldier.transform.position, soldier.GetComponent<SoldierScript>().Destination, step);

        //Différence entre la nouvelle et l'ancienne position, pour calculer l'angle de rotation
        Vector2 mouvement = new Vector2(newPosition.x - soldier.transform.position.x, newPosition.y - soldier.transform.position.y);

        //Angle de rotation en degrée (car l'affichage de Unity veut les angles en degrée); Mathf.Rad2Deg nécessaire car Mathf.Atan2 un résultat en radiants
        float angle = Mathf.Atan2(mouvement.y, mouvement.x) * Mathf.Rad2Deg;

        //On applique la nouvelle position
        soldier.transform.position = newPosition;

        //On applique la nouvelle rotation
        soldier.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    //S'il apercoit un soldat ennemi!
    public void DetectEnemy(GameObject soldier, GameObject enemy)
    {
        //Ne sert à rien ici, étant donné que le soldat en fuite ne peut pas attaquer.
    }

    //S'il apercoit une tour ennemie!
    public void DetectTower(GameObject soldier, GameObject tower)
    {
        //Ne sert à rien ici, étant donné que le soldat en fuite ne peut pas attaquer.
    }

    //S'il apercoit une foret!
    public void DetectForest(GameObject soldier, GameObject forest)
    {
        Debug.Log("Foret DÉTECTÉE! Soldat en fuite!!!");
        soldier.GetComponent<SoldierScript>().Destination = forest.transform.position;
    }

    //Vérification des stats pour effectuer les actions
    public void Update(GameObject soldier)
    {
        soldier.GetComponent<SoldierScript>().AffichageDeTexte("Fuite");

        if (soldier.transform.position == soldier.GetComponent<SoldierScript>().Destination)
        {
            soldier.GetComponent<SoldierScript>().AffichageDeTexte("Planqué");
            soldier.GetComponent<SoldierScript>().SoldierState = new CoverState();
            
        }
    }

    public void IsFiring(GameObject soldat)
    {
        //N'est pas utilisé dans cet état.
    }
}


/// <summary>
/// L'état planqué est déclenché lorsque le soldat a moins du quart de sa
/// vie restante. Il va donc se mettre à fuir vers une forêt ou une tour
/// alliée afin de se régénerer.
/// </summary
public class CoverState : SoldierState
{
    //Pour déplacer un soldat
    public void MoveSoldier(GameObject soldier)
    {
        //N'est pas utilisé dans cet état.
    }

    //S'il apercoit un soldat ennemi!
    public void DetectEnemy(GameObject soldier, GameObject enemy)
    {
        soldier.GetComponent<SoldierScript>().Target = enemy.gameObject;
        soldier.GetComponent<SoldierScript>().IsShooting = true;
    }

    //S'il apercoit une tour ennemie!
    public void DetectTower(GameObject soldier, GameObject tower)
    {
        soldier.GetComponent<SoldierScript>().Target = tower.gameObject;
        soldier.GetComponent<SoldierScript>().IsShooting = true;
    }

    //S'il apercoit une foret!
    public void DetectForest(GameObject soldier, GameObject forest)
    {
        //N'est pas utilisé dans cet état.
    }

    /// <summary>
    /// Vérification des attributs pour effectuer les actions
    /// </summary>
    /// <param name="soldier">Le soldat qui sera mis à jour.</param>
    public void Update(GameObject soldier)
    {
        soldier.GetComponent<SoldierScript>().AffichageDeTexte("Planqué");

        GameObject target = soldier.GetComponent<SoldierScript>().Target;

        if (soldier.GetComponent<SoldierScript>().IsShooting == true && target != null)
        {
            ////Différence entre la nouvelle et l'ancienne position, pour calculer l'angle de rotation
            Vector2 vecteurDeDirection = new Vector2(target.transform.position.x - soldier.transform.position.x, target.transform.position.y - soldier.transform.position.y);

            ////Angle de rotation en degrée (car l'affichage de Unity veut les angles en degrée); Mathf.Rad2Deg nécessaire car Mathf.Atan2 un résultat en radiants
            float angle = Mathf.Atan2(vecteurDeDirection.y, vecteurDeDirection.x) * Mathf.Rad2Deg;

            //On applique la nouvelle rotation
            soldier.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            soldier.GetComponent<SoldierScript>().Fire();
        }

        if (target == null)
        {
            soldier.GetComponent<SoldierScript>().Target = soldier.GetComponent<SoldierScript>().InitialTarget;
            soldier.GetComponent<SoldierScript>().IsShooting = false;
            soldier.GetComponent<SoldierScript>().Kills += 1;
            Debug.Log(soldier.name + "a fait un kill!");
        }

        if (soldier.GetComponent<SoldierScript>().HealthPoints < soldier.GetComponent<SoldierScript>().maxHealthPoints)
        {
            //Le temps à attendre avant la prochaine régénération de points
            float timeToWait = 0;

            Debug.Log(soldier.name + " est planqué en forêt!!!");
            if (Time.time > timeToWait)
            {
                timeToWait = Time.time + 0.05f;
                soldier.GetComponent<SoldierScript>().HealthPoints += 1;
            }
        }
        else if (soldier.GetComponent<SoldierScript>().HealthPoints == soldier.GetComponent<SoldierScript>().maxHealthPoints)
        {
            Debug.Log(soldier.name + "est prêt à regagner le combat!!!");
            soldier.gameObject.GetComponent<SoldierScript>().IsShooting = false;
            soldier.gameObject.GetComponent<SoldierScript>().Destination = soldier.gameObject.GetComponent<SoldierScript>().InitialDestination;
            soldier.GetComponent<SoldierScript>().SoldierState = new NormalState();
        }
    }

    public void IsFiring(GameObject soldier)
    {
        //Ne sert à rien dans cet état.
    }
}

/// <summary>
/// L'état de mort est déclenché lorsque le soldat n'a plus de vie.
/// Son sprite va alors changé avant d'être supprimé.
/// </summary
public class DeathState : SoldierState
{
    //Pour déplacer un soldat
    public void MoveSoldier(GameObject soldier)
    {
        //N'est pas utilisé dans cet état.
    }

    //S'il apercoit un soldat ennemi!
    public void DetectEnemy(GameObject soldier, GameObject enemy)
    {
        //N'est pas utilisé dans cet état.
    }

    //S'il apercoit une tour ennemie!
    public void DetectTower(GameObject soldier, GameObject tower)
    {
        //N'est pas utilisé dans cet état.
    }

    //S'il apercoit une foret!
    public void DetectForest(GameObject soldier, GameObject forest)
    {
        //N'est pas utilisé dans cet état.
    }

    //Vérification des stats pour effectuer les actions
    public void Update(GameObject soldier)
    {
        soldier.GetComponent<SpriteRenderer>().sprite = soldier.GetComponent<SoldierScript>().DeathSprite;
        soldier.GetComponent<SoldierScript>().Die();
    }

    public void IsFiring(GameObject soldier)
    {
        //N'est pas utilisé dans cet état.
    }
}