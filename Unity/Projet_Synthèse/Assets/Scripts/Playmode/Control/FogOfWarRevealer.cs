using Harmony;
using ProjetSynthese;
using UnityEngine;

public class FogOfWarRevealer : GameScript
{
    [SerializeField]
    private int radius;

    private FogOfWar fogOfWar;

    public int Radius
    {
        get { return radius; }
    }

    private void InjectFogOfWarRevealer([SceneScope] FogOfWar fogOfWar)
    {
        this.fogOfWar = fogOfWar;
    }

    private void Awake()
    {
        InjectDependencies("InjectFogOfWarRevealer");
    }

    private void Start()
    {
        fogOfWar.RegisterRevealer(this);
    }

    private void OnDestroy()
    {
        fogOfWar.UnregisterRevealer(this);
    }
}