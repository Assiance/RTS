namespace Assets.Scripts.MyGenericScripts.Framework
{
    public abstract class MainGameObjectBehaviour : ProdigyMonoBehaviour 
    {
        protected virtual void OnEnable()
        {
            GameObjectManager.Instance.Add(this.gameObject);
        }

        protected virtual void OnDisable()
        {
            GameObjectManager.Instance.Remove(this.gameObject);
        }
    }
}
