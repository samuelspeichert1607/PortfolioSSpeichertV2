using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Movement : MonoBehaviour {
    //https://unity3d.com/fr/learn/tutorials/topics/scripting/events
    public delegate void KeyPressedHandler(KeyCode key, Vector3 direction);
    public event KeyPressedHandler OnKeyPressed;

    [SerializeField]
    private int accelerationSpeed = 5;

    // Use this for initialization
    void Start () {
        OnEnable();
    }

    // Update is called once per frame
    public void Update()
    {
        // ATTENTION! L'objet ne pourra plus bouger si on appuie sur Espace! C'est pour tester la désactivation d'un évènement.
        if (Input.GetKey(KeyCode.Space))
        {
            OnDisable();
        }

            if (OnKeyPressed != null)
        {
            MoveToIfKeyPressed(KeyCode.W, Vector3.up);
            MoveToIfKeyPressed(KeyCode.A, Vector3.left);
            MoveToIfKeyPressed(KeyCode.S, Vector3.down);
            MoveToIfKeyPressed(KeyCode.D, Vector3.right);
        }
    }

    //Appelé régulièrement, mais pas à chaque "Frame".
    public void FixedUpdate()
    {
        gameObject.SetActive(true);
    }

    //LateUpdate est appelé à toutes les "Frames" 
    public void LateUpdate()
    {

    }

    //Si le script est activé. (appelé entre start et update la premiere fois)
    public void OnEnable()
    {
        //S'abonner aux évènements auquel le script est intéressé.
        OnKeyPressed += MoveToIfKeyPressed;
    }

    //Si le script est désactivé.
    public void OnDisable()
    {
        //Se désabonner aux évènements auquel le script est abonné.
        //Un enfant ne peux être activé si le parent n’est pas activé.
        OnKeyPressed -= MoveToIfKeyPressed;
    }

    //Appelée juste avant que le script est détruit. (ce qui ne veux pas dire que tout a été garbage collecté).
    public void OnDestroy()
    {
        //Faire tout le nettoyage nécéssaire 
        //genre unload les ressources etc...
    }

    public void MoveToIfKeyPressed(KeyCode key, Vector3 direction)
    {
        //Input est une classer pour gérer les entrées au clavier
        if (Input.GetKey(key))
        {
            //Ne jamais oublier de multiplier par le delta_time
            transform.Translate(direction * accelerationSpeed * Time.deltaTime);
           
        }
    }
}
