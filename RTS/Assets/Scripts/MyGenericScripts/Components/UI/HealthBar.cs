using Assets.Scripts.MyGenericScripts.Components.General;
using Assets.Scripts.MyGenericScripts.Framework;
using UnityEngine;

namespace Assets.Scripts.MyGenericScripts.Components.UI
{
    [RequireComponent(typeof(Stats))]
    public class HealthBar : ProdigyMonoBehaviour
    {
        public Texture2D HealthBarImage;
        public Vector3 HealthBarOffset = new Vector3(-0.5f, 0.75f, 0f);
        public GameObject ParentObject;

        private Stats _stats;
        private SpriteRenderer _healthBar;
        private Vector3 _healthScale;
        private Quaternion _initialHealthBarRotation;


        protected void OnEnable()
        {
            _stats = GetComponent<Stats>();
            _initialHealthBarRotation = new Quaternion();
        }

        protected void Start()
        {
            _healthBar = new GameObject("HealthBar").AddComponent<SpriteRenderer>();
            _healthBar.sprite = Sprite.Create(HealthBarImage, new Rect(0, 0, 100, 20), Vector2.zero, 100);

            if (ParentObject == null)
            {
                CreateHealthBarParentContainer();
            }
            else
            {
                _healthBar.gameObject.transform.parent = ParentObject.transform;
            }

            _healthScale = _healthBar.transform.localScale;

            _healthBar.gameObject.transform.position = transform.position + HealthBarOffset;
            _healthBar.transform.localPosition = Vector3.zero + HealthBarOffset;
            _initialHealthBarRotation = _healthBar.transform.rotation;

            UpdateHealthBar();
        }

        private void CreateHealthBarParentContainer()
        {
            var container = GameObject.Find("HealthBar Container");

            if (container == null)
                container = new GameObject("HealthBar Container");

            _healthBar.gameObject.transform.parent = container.transform;
        }

        protected void LateUpdate()
        {
            _healthBar.transform.position = transform.position + HealthBarOffset;
            _healthBar.transform.rotation = _initialHealthBarRotation;
        }

        public void UpdateHealthBar()
        {
            _healthBar.material.color = Color.Lerp(Color.green, Color.red, 1 - _stats.CurrentHealth * 0.01f);
            _healthBar.transform.localScale = new Vector3(_healthScale.x * _stats.CurrentHealth * 0.01f, 1, 1);
        }
    }
}

