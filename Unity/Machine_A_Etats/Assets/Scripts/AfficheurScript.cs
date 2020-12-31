using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AfficheurScript : MonoBehaviour
{
	// Use this for initialization
	void Start ()
    {
        //this.GetComponent<Renderer> va chercher la référence vers un component Renderer sur l'objet courant s'il y en a un
        //Sinon renvoie null
        //Le Sorting Layer n,est pas présent dans l'inspecteur pour un Mesh Renderer, mais il existe quand même.

        //On peut atteindre aisément le niveau d'affiche par son nom.

        //Les images sont tirés par Sorting Layer, puis en cas d'égalisté par Sorting Number, 
        //en cas de ré-égalité, c'est la hauteur en z, et en dernier cas, l'ordre de la hiérarchie dans la scène.

        this.GetComponent<Renderer>().sortingLayerName = "Texte";
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
