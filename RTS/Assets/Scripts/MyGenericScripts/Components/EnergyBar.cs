using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components
{
    [RequireComponent(typeof(Stats))]
    public class EnergyBar : ProdigyMonoBehaviour
    {
        public Texture2D EnergyBarImage;
        public float RechargeRate;
		public Color BarColor;
        public Vector3 EnergyBarOffset = new Vector3(-0.5f, 1f, 0f);
        public float XPosition = 0f;
        public float YPosition = 0f;
        public GameObject ParentObject;

        private Stats _stats;
        private SpriteRenderer _energyBar;
        private Vector3 _energyScale;
        private Quaternion _initialEnergyBarRotation;


        protected void OnEnable()
        {
            _stats = GetComponent<Stats>();
            _initialEnergyBarRotation = new Quaternion();
        }

        protected void Start()
        {
            _energyBar = new GameObject("EnergyBar").AddComponent<SpriteRenderer>();
            _energyBar.sprite = Sprite.Create(EnergyBarImage, new Rect(0, 0, 100, 20), Vector2.zero, 100);

            if (ParentObject == null)
            {
                CreateEnergyBarParentContainer();
            }
            else
            {
                _energyBar.gameObject.transform.parent = ParentObject.transform;
            }

            _energyScale = _energyBar.transform.localScale;

            _energyBar.gameObject.transform.position = transform.position + EnergyBarOffset;
            _energyBar.transform.localPosition = Vector3.zero + EnergyBarOffset;
            _initialEnergyBarRotation = _energyBar.transform.rotation;

            UpdateEnergyBar();
        }

        private void CreateEnergyBarParentContainer()
        {
            var container = GameObject.Find("EnergyBar Container");

            if (container == null)
                container = new GameObject("EnergyBar Container");

            _energyBar.gameObject.transform.parent = container.transform;
        }

        protected void LateUpdate()
        {
            _energyBar.transform.position = transform.position + EnergyBarOffset;
            _energyBar.transform.rotation = _initialEnergyBarRotation;
        }

        public void UpdateEnergyBar()
        {
            _energyBar.material.color = BarColor;
            _energyBar.transform.localScale = new Vector3(_energyScale.x * _stats.CurrentEnergy * 0.01f, 1, 1);
        }

        void OnGUI()
        {
            GUI.color = BarColor;
            GUI.DrawTexture(new Rect(XPosition, YPosition, _stats.CurrentEnergy, 25), EnergyBarImage);

            GUI.color = Color.black;
            string healthRatio = _stats.CurrentEnergy.ToString() + " / " + _stats.MaxEnergy;
            GUI.Label(new Rect(XPosition + 20, YPosition + 2f, 100, 20), healthRatio);
        }
    }
}



