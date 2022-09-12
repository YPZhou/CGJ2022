using System.Linq;
using BitButterCORE.V2;

public class TestObject : BaseObject
{
	public TestObject(uint id)
		: base(id)
	{
		ObjectCount = ObjectFactory.Instance.Query<TestObject>().Count();
	}

	public int ObjectCount { get; }
}
