namespace Assets.Scripts.MyGenericScripts.Framework
{
    public abstract class MainGameObjectBehaviour : ProdigyMonoBehaviour 
    {
        protected void OnEnable()
        {
            GameObjectManager.Instance.Add(this.gameObject);
        }

        protected void OnDisable()
        {
            GameObjectManager.Instance.Remove(this.gameObject);
        }
    }
}
