using System.Collections;
using System.Collections.Generic;
using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/MetalExtractorController")]
    public class MetalExtractorController : GameScript
    {
        [SerializeField]
        [Tooltip("Rate at which the extractors mine metal.")]
        private float miningRate;

        [SerializeField]
        [Tooltip("Amount of metal extracted each time.")]
        private int amountOfMetalExtracted;

        public GameObject MineralDeposit { get; set; }
        public Metal Metal { get; set; }


        private DepositExtractionEventChannel eventChannel;
        private KillableObject killableObject;

        private void InjectMetalExtractorController([EventChannelScope] DepositExtractionEventChannel eventChannel,
                                                    [EntityScope] KillableObject killableObject)
        {
            this.eventChannel = eventChannel;
            this.killableObject = killableObject;
        }

        private void Awake()
        {
            InjectDependencies("InjectMetalExtractorController");
        }

        private void Start()
        {
            transform.parent.localScale = MineralDeposit.transform.localScale;
            StartCoroutine(Mine());
        }

        private void OnEnable()
        {
            killableObject.OnDeath += OnDeath;
        }

        private void OnDisable()
        {
            killableObject.OnDeath -= OnDeath;
        }

        private IEnumerator Mine()
        {
            while (MineralDeposit != null && !MineralDeposit.gameObject.GetComponentInChildren<MetalDepositController>()
                       .IsDepleted)
            {
                Metal.ReduceMetalQuantity(amountOfMetalExtracted);
                eventChannel.Publish(new DepositExtractionEvent(amountOfMetalExtracted));
                yield return new WaitForSeconds(miningRate);
            }
        }

        private void OnDeath()
        {
            MineralDeposit.gameObject.GetComponentInChildren<MetalDepositController>().IsOccupied = false;
        }

    }
}

