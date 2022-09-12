using UnityEngine;
using BitButterCORE.V2;

public abstract class BaseViewObject : MonoBehaviour
{
    protected virtual void Start()
    {
    }

    protected virtual void Update()
    {
    }

    public void InitializeView(ObjectReference objectReference)
	{
        InitializeViewCore(objectReference);
	}

    protected abstract void InitializeViewCore(ObjectReference objectReference);
}

public abstract class BaseViewObject<T> : BaseViewObject where T : BaseObject
{
    protected override void InitializeViewCore(ObjectReference objectReference)
	{
        CoreObject = objectReference.Object as T;
	}

    protected T CoreObject { get; private set; }
}
