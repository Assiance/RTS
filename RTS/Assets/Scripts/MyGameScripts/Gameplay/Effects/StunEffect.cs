using System.Collections;
using Assets.Scripts.MyGenericScripts.Components.Actions;
using UnityEngine;

namespace Assets.Scripts.MyGameScripts.Gameplay.Effects
{
    public class StunEffect : MonoBehaviour
    {
        [HideInInspector]
        public float StunTime = 2f;

        private Movement _movementComponent;

        void OnEnable()
        {
            _movementComponent = GetComponent<Movement>();

            if (_movementComponent != null)
                StartCoroutine(StartEffect());
        }

        IEnumerator StartEffect()
        {
            _movementComponent.enabled = false;
            yield return new WaitForSeconds(StunTime);

            _movementComponent.enabled = true;
            Destroy(this);
        }
    }
}
