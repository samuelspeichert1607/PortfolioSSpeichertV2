using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPlayerHealth : MonoBehaviour {

    private GameObject player;
    private HealthController healthController;

	private void Awake () {
        player = GameObject.Find("Player");
        healthController = player.GetComponent<HealthController>();
    }
    
    private void Update () {
        gameObject.GetComponentInChildren<Text>().text = healthController.Health + " / "
            + healthController.MaxHealth;
    }
}
