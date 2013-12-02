using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assets.Scripts.MyGenericScripts.Framework
{
    public abstract class GameObjectProperty : ProdigyMonoBehaviour 
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
