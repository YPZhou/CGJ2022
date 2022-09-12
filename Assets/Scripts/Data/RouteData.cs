using System.Collections.Generic;
using System.Linq;
using BitButterCORE.V2;

/// <summary>
/// ·���࣬��·���ƶ���Ҫ����ͨ������
/// ��¼·�����˵Ľڵ㣬�Լ�·���ϵľ���
/// </summary>
public class RouteData : BaseObject
{
	public RouteData(uint id, ObjectReference level, ObjectReference startNode, ObjectReference endNode, int maxPassCount)
		: base(id)
	{
		Level = level;
		Nodes.Add(startNode);
		Nodes.Add(endNode);

		MaxPassCount = maxPassCount;
		RoutePassedCount = 0;
	}

	public ObjectReference Level { get; }

	/// <summary>
	/// �����ڵ�
	/// </summary>
	public List<ObjectReference> Nodes => nodes ?? (nodes = new List<ObjectReference>());
	List<ObjectReference> nodes;

	public int MaxPassCount { get; }

	public int RoutePassedCount { get; private set; }

	/// <summary>
	/// ·�߶�
	/// </summary>
	public IEnumerable<ObjectReference> RouteSegments => ObjectFactory.Instance.Query<RouteSegmentData>(routeSegment => routeSegment.Route.IsValidByTypeAndID<RouteData>(ID));

	/// <summary>
	/// ����·�ߵĵ�һ��·�߶�
	/// </summary>
	/// <param name="fromNode">·�߿�ʼ�ڵ�</param>
	/// <returns>��һ��·�߶�</returns>
	public ObjectReference GetFirstRouteSegment(ObjectReference fromNode)
	{
		return Nodes.Contains(fromNode)
			? RouteSegments.FirstOrDefault(route => (route.Object as RouteSegmentData).IsConnectedToNode(fromNode))
			: default;
	}

	/// <summary>
	/// ����
	/// </summary>
	public ObjectReference Guard => ObjectFactory.Instance.Query<GuardData>(guard => guard.Route.IsValidByTypeAndID<RouteData>(ID)).FirstOrDefault();

	/// <summary>
	/// ·�߿�ͨ��
	/// </summary>
	public bool CanPass
	{
		get
		{
			var result = RoutePassedCount < MaxPassCount;
			if (result)
			{
				var playerReference = ObjectFactory.Instance.Query<PlayerData>().FirstOrDefault();
				if (playerReference.IsValid && Guard.IsValid)
				{
					var player = playerReference.Object as PlayerData;
					result = (Guard.Object as GuardData).LootChecks.All(lootCheck => lootCheck.CanPassCheck(player.LootPosition, player.CurrentLootIcon));
				}
			}
			return result;
		}
	}

	/// <summary>
	/// ��·���ƶ�
	/// </summary>
	public void Move()
	{
		if (CanPass)
		{
			var playerReference = ObjectFactory.Instance.Query<PlayerData>().FirstOrDefault();
			if (playerReference.IsValid)
			{
				var player = playerReference.Object as PlayerData;
				if (Nodes.Contains(player.CurrentNode))
				{
					player.CurrentNode = Nodes.FirstOrDefault(node => node != player.CurrentNode);
					player.AdvanceCurrentLootIcon();
					var currentNode = player.CurrentNode.Object as NodeData;
					currentNode.ApplyRogueAbility();
					RoutePassedCount += 1;
				}
			}
		}
	}

	public void SelectRoute()
	{
		IsSelected = true;
	}

	public void UnselectRoute()
	{
		IsSelected = false;
	}

	public bool IsSelected { get; private set; }

	public void DestroyRoute()
	{
		var guardReference = ObjectFactory.Instance.Query<GuardData>(guard => guard.Route == Reference).FirstOrDefault();
		if (guardReference.IsValid)
		{
			ObjectFactory.Instance.Remove(guardReference);
		}

		foreach (var segmentReference in RouteSegments)
		{
			if (segmentReference.IsValid)
			{
				ObjectFactory.Instance.Remove(segmentReference);
			}
		}
	}
}
