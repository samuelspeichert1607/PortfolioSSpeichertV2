using UnityEngine;

namespace ProjetSynthese
{
    internal enum RectValues
    {
        Xposition = 0,
        Yposition = 1,
        Width = 2,
        Height = 3
    }
    
    public class SetCameraView : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("Number representing which player the camera is following. So it displays in right place in multiplayer")]
        private CameraPositionsConstants.PostitionSets cameraNumber;

        private CameraPositionsConstants.PlayerQuantity playerQuantity;

        private const int MINIMUM_CAMERA_QUANTITY = 1;
        private const int MAXIMUM_CAMERA_QUANTITY = 4;

        private void Start()
        {
            int playersFound = GameObject.Find("SceneDependencies").GetComponent<GameActivityParametersReception>().GameActivityParameters.NumberOfPlayers;

            playersFound = Mathf.Clamp(playersFound, MINIMUM_CAMERA_QUANTITY, MAXIMUM_CAMERA_QUANTITY);

            playerQuantity = (CameraPositionsConstants.PlayerQuantity)playersFound;


            float[] values =
                CameraPositionsConstants.GetCameraPosition(cameraNumber, playerQuantity);

            GetComponent<Camera>().rect =
                new Rect(values[(int)RectValues.Xposition], values[(int)RectValues.Yposition], values[(int)RectValues.Width], values[(int)RectValues.Height]);

        }
    }
}
