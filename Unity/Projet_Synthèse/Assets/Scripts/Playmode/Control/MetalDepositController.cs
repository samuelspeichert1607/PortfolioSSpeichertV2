using Harmony;
using UnityEngine;

namespace ProjetSynthese
{
    [AddComponentMenu("Game/Control/MetalDepositController")]
    public class MetalDepositController : GameScript
    {
        [SerializeField]
        [Tooltip("Prefab of the metal extractor.")]
        private GameObject metalExtractorPrefab;

        private GameObject metalExtractor;
        private Metal metal;
        private PurchasableObject extractorPrice;

        public bool IsDepleted { get; private set; }
        public bool IsOccupied { get; set; }

        private void InjectMineralDepositController([GameObjectScope] Metal metal)
        {
            this.metal = metal;
        }

        private void Awake()
        {
            InjectDependencies("InjectMineralDepositController");
            IsDepleted = false;
            metal.OnMetalQuantityDepleted += OnEmptied;
            IsOccupied = false;
            extractorPrice = metalExtractorPrefab.GetComponentInChildren<PurchasableObject>();
        }

        public void Configure()
        {
            metal.Reset();
        }

        public void CreateExtractor()
        {
            if (!IsOccupied)
            {
                metalExtractor = Instantiate(metalExtractorPrefab);
                metalExtractor.GetComponentInChildren<MetalExtractorController>().MineralDeposit = this.transform.parent.gameObject;
                metalExtractor.GetComponentInChildren<MetalExtractorController>().Metal = metal;
                metalExtractor.transform.position = transform.position;
                IsOccupied = true;
            }
        }

        public void OnEmptied()
        {
            IsDepleted = true;
            Debug.Log("Is Empty");
        }

        public int GetExtractorPrice()
        {
            return extractorPrice.GetPrice();
        }
    }
}

