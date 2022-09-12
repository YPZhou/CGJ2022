using System.Collections.Generic;
using System.Linq;
using BitButterCORE.V2;
using static Constants;

/// <summary>
/// �ؿ��࣬�ɽڵ㼰·�����
/// </summary>
public class LevelData : BaseObject
{
	public LevelData(uint id, int sequence, int maxMoveCount)
		: base(id)
	{
		Sequence = sequence;
		MaxMoveCount = maxMoveCount;
		CurrentMoveCount = 0;
	}

	/// <summary>
	/// �ؿ����
	/// </summary>
	public int Sequence { get; }

	/// <summary>
	/// �ؿ����������
	/// </summary>
	public int MaxMoveCount { get; }

	/// <summary>
	/// ��ǰ��ʹ�ò���
	/// </summary>
	public int CurrentMoveCount { get; private set; }

	/// <summary>
	/// �ؿ����нڵ��б�
	/// </summary>
	public IEnumerable<ObjectReference> Nodes => ObjectFactory.Instance.Query<NodeData>(node => node.Level.IsValidByTypeAndID<LevelData>(ID));

	/// <summary>
	/// �ؿ�����·���б�
	/// </summary>
	public IEnumerable<ObjectReference> Routes => ObjectFactory.Instance.Query<RouteData>(route => route.Level.IsValidByTypeAndID<LevelData>(ID));

	/// <summary>
	/// ��ʼ�ڵ�
	/// </summary>
	public ObjectReference StartNode => Nodes.Single(node => (node.Object as NodeData).NodeType == NodeType.Start);

	/// <summary>
	/// ���ѽڵ�
	/// </summary>
	public ObjectReference GoalNode => Nodes.Single(node => (node.Object as NodeData).NodeType == NodeType.Goal);

	/// <summary>
	/// ��ǰ��������ڵ�
	/// </summary>
	public ObjectReference CurrentPlayerNode => (ObjectFactory.Instance.Query<PlayerData>().FirstOrDefault().Object as PlayerData)?.CurrentNode ?? default;

	/// <summary>
	/// ��ǰ��������ڵ������·��
	/// </summary>
	public ObjectReference CurrentUpRoute => (CurrentPlayerNode.Object as NodeData)?.UpRoute ?? default;

	/// <summary>
	/// ��ǰ��������ڵ������·��
	/// </summary>
	public ObjectReference CurrentDownRoute => (CurrentPlayerNode.Object as NodeData)?.DownRoute ?? default;

	/// <summary>
	/// ��ǰ��������ڵ������·��
	/// </summary>
	public ObjectReference CurrentLeftRoute => (CurrentPlayerNode.Object as NodeData)?.LeftRoute ?? default;

	/// <summary>
	/// ��ǰ��������ڵ������·��
	/// </summary>
	public ObjectReference CurrentRightRoute => (CurrentPlayerNode.Object as NodeData)?.RightRoute ?? default;

	/// <summary>
	/// ��Ӧ��Ұ��� �Ϸ����
	/// </summary>
	public void OnPressUpKey()
	{
		var upRoute = CurrentUpRoute.Object as RouteData;
		SelectRouteOrMove(upRoute);
	}

	/// <summary>
	/// ��Ӧ��Ұ��� �·����
	/// </summary>
	public void OnPressDownKey()
	{
		var downRoute = CurrentDownRoute.Object as RouteData;
		SelectRouteOrMove(downRoute);
	}

	/// <summary>
	/// ��Ӧ��Ұ��� �����
	/// </summary>
	public void OnPressLeftKey()
	{
		var leftRoute = CurrentLeftRoute.Object as RouteData;
		SelectRouteOrMove(leftRoute);
	}

	/// <summary>
	/// ��Ӧ��Ұ��� �ҷ����
	/// </summary>
	public void OnPressRightKey()
	{
		var rightRoute = CurrentRightRoute.Object as RouteData;
		SelectRouteOrMove(rightRoute);
	}

	void SelectRouteOrMove(RouteData route)
	{
		if (route?.IsSelected ?? false)
		{
			SoundManagement.PlayAudio("click");
			route.Move();
			route.UnselectRoute();
			CurrentMoveCount += 1;
		}
		else
		{
			UnselectAllRoute();
			if (route?.CanPass ?? false)
			{
				route.SelectRoute();
			}
		}
	}

	void UnselectAllRoute()
	{
		(CurrentUpRoute.Object as RouteData)?.UnselectRoute();
		(CurrentDownRoute.Object as RouteData)?.UnselectRoute();
		(CurrentLeftRoute.Object as RouteData)?.UnselectRoute();
		(CurrentRightRoute.Object as RouteData)?.UnselectRoute();
	}

	public bool IsLevelWin => (CurrentPlayerNode.Object as NodeData)?.NodeType == NodeType.Goal;

	public bool IsLevelLose => (CurrentMoveCount == MaxMoveCount || !HasMovableRoute) && (CurrentPlayerNode.Object as NodeData)?.NodeType != NodeType.Goal;

	bool HasMovableRoute => CurrentUpRoute.IsValid && (CurrentUpRoute.Object as RouteData).CanPass
		|| CurrentDownRoute.IsValid && (CurrentDownRoute.Object as RouteData).CanPass
		|| CurrentLeftRoute.IsValid && (CurrentLeftRoute.Object as RouteData).CanPass
		|| CurrentRightRoute.IsValid && (CurrentRightRoute.Object as RouteData).CanPass;

	public bool IsLevelEnd => IsLevelWin || IsLevelLose;

	/// <summary>
	/// ɾ���ؿ������й�������
	/// </summary>
	public void DestroyLevel()
	{
		foreach (var nodeReference in Nodes)
		{
			if (nodeReference.IsValid)
			{
				(nodeReference.Object as NodeData).DestroyNode();
				ObjectFactory.Instance.Remove(nodeReference);
			}
		}

		foreach (var routeReference in Routes)
		{
			if (routeReference.IsValid)
			{
				(routeReference.Object as RouteData).DestroyRoute();
				ObjectFactory.Instance.Remove(routeReference);
			}
		}
	}
}
