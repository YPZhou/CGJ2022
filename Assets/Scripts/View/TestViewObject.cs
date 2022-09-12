using UnityEngine;

public class TestViewObject : BaseViewObject<TestObject>
{
	protected override void Update()
	{
		base.Update();

		if (CoreObject != null)
		{
			transform.position = new Vector3(0, CoreObject.ObjectCount);
		}
	}
}
