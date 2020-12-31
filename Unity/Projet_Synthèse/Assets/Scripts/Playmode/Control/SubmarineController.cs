using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    public delegate void SubmarineStateEventHandler();

    [AddComponentMenu("Game/Control/SubmarineController")]
    public class SubmarineController : GameScript
    {
        private Metal metal;
        private DepositExtractionEventChannel eventChannel;
        private HitSensor hitSensor;

        public event SubmarineStateEventHandler OnSubDestroyed;
        public event SubmarineStateEventHandler OnSubRepaired;

        public Metal Metal
        {
            get { return metal; }
        }

        private void InjectSubmarineController([GameObjectScope] Metal metal, 
                                               [EventChannelScope] DepositExtractionEventChannel eventChannel,
                                               [EntityScope] HitSensor hitSensor)
        {
            this.metal = metal;
            this.eventChannel = eventChannel;
            this.hitSensor = hitSensor;
        }

        private void Awake()
        {
            InjectDependencies("InjectSubmarineController");
            GameObject.Find("UIManager").GetComponentInChildren<CanvasMetalQuantityController>().Metal = metal;
        }

        private void OnEnable()
        {
            metal.OnMetalQuantityDepleted += OnDestroyed;
            eventChannel.OnEventPublished += OnRepair;
            metal.OnMetalQuantityFilled += OnRepaired;
            hitSensor.OnHit += OnHit;
        }

        private void OnDisable()
        {
            metal.OnMetalQuantityDepleted -= OnDestroyed;
            eventChannel.OnEventPublished -= OnRepair;
            metal.OnMetalQuantityFilled -= OnRepaired;
            hitSensor.OnHit -= OnHit;
        }

        private void OnRepair(DepositExtractionEvent extractionEvent)
        {
            metal.IncreaseMetalQuantity(extractionEvent.MetalAmountRecolted);
        }

        private void OnRepaired()
        {
            Debug.Log("Submarine was repaired");
            if (OnSubRepaired != null) OnSubRepaired();
        }

        private void OnDestroyed()
        {
            Debug.Log("Submarine was destroyed");
            if (OnSubDestroyed != null) OnSubDestroyed();
        }

        private void OnHit(int damage)
        {
            Debug.Log("Submarine took damage: "+ damage);
            metal.ReduceMetalQuantity(damage);
        }
    }

}


