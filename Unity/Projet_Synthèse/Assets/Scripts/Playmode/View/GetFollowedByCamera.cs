using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public class GetFollowedByCamera : GameScript
    {
        [SerializeField]
        [Tooltip("Number representing which player the camera is following. So it displays in right place in multiplayer")]
        [Range(1, 4)]
        private int playerNumber;

        private new Camera camera;

        private void InjectGetFollowedByCamera1([Named(R.S.GameObject.Camera1P)][TagScope(R.S.Tag.MainCamera)] Camera camera1)
        {
            camera = camera1;
        }

        private void InjectGetFollowedByCamera2([Named(R.S.GameObject.Camera2P)][TagScope(R.S.Tag.MainCamera)] Camera camera2)
        {
            camera = camera2;
        }

        private void InjectGetFollowedByCamera3([Named(R.S.GameObject.Camera3P)][TagScope(R.S.Tag.MainCamera)] Camera camera3)
        {
            camera = camera3;
        }

        private void InjectGetFollowedByCamera4([Named(R.S.GameObject.Camera4P)][TagScope(R.S.Tag.MainCamera)] Camera camera4)
        {
            camera = camera4;
        }

        private void Awake()
        {
            InjectDependencies("InjectGetFollowedByCamera" + playerNumber);
        }

        private void Start()
        {
            camera.GetComponent<FollowPlayer>().Player = gameObject.GetTopParent();
        }
    }
}
