using UnityEngine;
using UnityEngine.UI;
using Harmony;

namespace ProjetSynthese
{
    public class CanvasArtefactController : GameScript
    {
        private GameObject[] artefacts;

        private int artefactsCollected = 0;
        public int ArtefactsCollected
        {
            get
            {
                return artefactsCollected;
            }

            set
            {
                if(artefactsCollected < artefacts.Length)
                {
                    artefactsCollected = value;
                }
            }
        }

        private void Awake()
        {
            artefacts = GameObject.FindGameObjectsWithTag("Artefact");
        }

        private void OnGUI()
        {
            GetComponentInChildren<Text>().text = ArtefactsCollected + " / " + artefacts.Length.ToString();
        }
        
        public void ArtefactQuantityChanged()
        {
            GetComponentInChildren<Text>().text = ArtefactsCollected + " / " + artefacts.Length.ToString();
        }
    }
}