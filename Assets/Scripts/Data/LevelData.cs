using System.Collections.Generic;
using System.Linq;
using BitButterCORE.V2;
using static Constants;

/// <summary>
/// 关卡类，由节点及路线组成
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
	/// 关卡序号
	/// </summary>
	public int Sequence { get; }

	/// <summary>
	/// 关卡最大步数限制
	/// </summary>
	public int MaxMoveCount { get; }

	/// <summary>
	/// 当前已使用步数
	/// </summary>
	public int CurrentMoveCount { get; private set; }

	/// <summary>
	/// 关卡所有节点列表
	/// </summary>
	public IEnumerable<ObjectReference> Nodes => ObjectFactory.Instance.Query<NodeData>(node => node.Level.IsValidByTypeAndID<LevelData>(ID));

	/// <summary>
	/// 关卡所有路线列表
	/// </summary>
	public IEnumerable<ObjectReference> Routes => ObjectFactory.Instance.Query<RouteData>(route => route.Level.IsValidByTypeAndID<LevelData>(ID));

	/// <summary>
	/// 起始节点
	/// </summary>
	public ObjectReference StartNode => Nodes.Single(node => (node.Object as NodeData).NodeType == NodeType.Start);

	/// <summary>
	/// 逃脱节点
	/// </summary>
	public ObjectReference GoalNode => Nodes.Single(node => (node.Object as NodeData).NodeType == NodeType.Goal);

	/// <summary>
	/// 当前玩家所处节点
	/// </summary>
	public ObjectReference CurrentPlayerNode => (ObjectFactory.Instance.Query<PlayerData>().FirstOrDefault().Object as PlayerData)?.CurrentNode ?? default;

	/// <summary>
	/// 当前玩家所处节点的向上路线
	/// </summary>
	public ObjectReference CurrentUpRoute => (CurrentPlayerNode.Object as NodeData)?.UpRoute ?? default;

	/// <summary>
	/// 当前玩家所处节点的向下路线
	/// </summary>
	public ObjectReference CurrentDownRoute => (CurrentPlayerNode.Object as NodeData)?.DownRoute ?? default;

	/// <summary>
	/// 当前玩家所处节点的向左路线
	/// </summary>
	public ObjectReference CurrentLeftRoute => (CurrentPlayerNode.Object as NodeData)?.LeftRoute ?? default;

	/// <summary>
	/// 当前玩家所处节点的向右路线
	/// </summary>
	public ObjectReference CurrentRightRoute => (CurrentPlayerNode.Object as NodeData)?.RightRoute ?? default;

	/// <summary>
	/// 响应玩家按下 上方向键
	/// </summary>
	public void OnPressUpKey()
	{
		var upRoute = CurrentUpRoute.Object as RouteData;
		SelectRouteOrMove(upRoute);
	}

	/// <summary>
	/// 响应玩家按下 下方向键
	/// </summary>
	public void OnPressDownKey()
	{
		var downRoute = CurrentDownRoute.Object as RouteData;
		SelectRouteOrMove(downRoute);
	}

	/// <summary>
	/// 响应玩家按下 左方向键
	/// </summary>
	public void OnPressLeftKey()
	{
		var leftRoute = CurrentLeftRoute.Object as RouteData;
		SelectRouteOrMove(leftRoute);
	}

	/// <summary>
	/// 响应玩家按下 右方向键
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
	/// 删除关卡的所有关联数据
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
