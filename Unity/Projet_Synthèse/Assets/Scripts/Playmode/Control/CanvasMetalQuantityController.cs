using UnityEngine;
using UnityEngine.UI;

namespace ProjetSynthese
{
    public class CanvasMetalQuantityController : GameScript
    {
        private Metal metal;
        public Metal Metal
        {
            get {   return metal; }
            set {
                    metal = value;
                    metal.OnMetalQuantityChanged += MetalQuantityChanged;
                }
        }

        private void Awake()
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            int numberOfPlayers = players.Length;
            RectTransform myRectTransform = GetComponentInChildren<RectTransform>();
            if(numberOfPlayers == 1)
            {
                //, = new Vector2(0, 180);
                myRectTransform.anchoredPosition = new Vector2(0, 180);
            }
            else if (numberOfPlayers > 1)
            {
                myRectTransform.anchoredPosition = new Vector2(0, 0);
            }

        }

        private void OnDestroy()
        {
            Metal.OnMetalQuantityChanged -= MetalQuantityChanged;
        }

        private void OnGUI()
        {
            GetComponentInChildren<Text>().text = Metal.MetalQuantity.ToString() + " / " + Metal.MaximalMetalQuantity.ToString();
        }
        
        public void MetalQuantityChanged(int oldMetalQuantity, int newMetalQuantity)
        {
            GetComponentInChildren<Text>().text = newMetalQuantity.ToString() + " / " + Metal.MaximalMetalQuantity.ToString();
        }
    }
}