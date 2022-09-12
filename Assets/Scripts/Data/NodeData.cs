using System.Collections.Generic;
using System.Linq;
using BitButterCORE.V2;
using UnityEngine;
using static Constants;

/// <summary>
/// �ڵ��࣬����ڵ��ʹ���﷢���α�
/// </summary>
public class NodeData : BaseObject
{
	public NodeData(uint id, ObjectReference level, NodeType nodeType, Vector2 position)
        : base(id)
	{
		Level = level;
		NodeType = nodeType;
		Position = position;
	}

	public ObjectReference Level { get; }

	/// <summary>
	/// �ڵ�����
	/// 1 ��ʼ�ڵ�
	/// 2 ���ѽڵ�
	/// 3 һ��ڵ�
	/// </summary>
	public NodeType NodeType { get; }

	/// <summary>
	/// �ڵ��ڳ����е�λ��
	/// </summary>
	public Vector2 Position { get; }

	/// <summary>
	/// ����
	/// </summary>
	public ObjectReference Rogue => ObjectFactory.Instance.Query<RogueData>(rogue => rogue.Node.IsValidByTypeAndID<NodeData>(ID)).FirstOrDefault();

	/// <summary>
	/// ������������
	/// </summary>
	public void ApplyRogueAbility()
	{
		if (Rogue.IsValid)
		{
			(Rogue.Object as RogueData).ApplyAbility();
		}
	}

	/// <summary>
	/// ��ڵ����ӵ�·���б�
	/// </summary>
	public IEnumerable<ObjectReference> ConnectedRoutes => ObjectFactory.Instance.Query<RouteData>(route => route.Nodes.Contains(Reference));

	public bool IsUpRouteSelected => IsRouteSelected(UpRoute);

	public bool IsDownRouteSelected => IsRouteSelected(DownRoute);

	public bool IsLeftRouteSelected => IsRouteSelected(LeftRoute);

	public bool IsRightRouteSelected => IsRouteSelected(RightRoute);

	public bool HasAnySelectedRoute => IsUpRouteSelected || IsDownRouteSelected || IsLeftRouteSelected || IsRightRouteSelected;

	bool IsRouteSelected(ObjectReference route) => route.IsValid ? (route.Object as RouteData)?.IsSelected ?? false : false;

	IEnumerable<ObjectReference> DirectlyConnectedRouteSegments => ConnectedRoutes.Select(route => (route.Object as RouteData).GetFirstRouteSegment(Reference));

	public ObjectReference UpRoute => (DirectlyConnectedRouteSegments.FirstOrDefault(segmentReference =>
	{
		var segment = segmentReference.Object as RouteSegmentData;
		var result = !segment.IsHorizontal;
		if (result)
		{
			result = segment.SegmentPositions.Any(position => position.y > Position.y);
		}
		return result;
	}).Object as RouteSegmentData)?.Route ?? default;

	public ObjectReference DownRoute => (DirectlyConnectedRouteSegments.FirstOrDefault(segmentReference =>
	{
		var segment = segmentReference.Object as RouteSegmentData;
		var result = !segment.IsHorizontal;
		if (result)
		{
			result = segment.SegmentPositions.Any(position => position.y < Position.y);
		}
		return result;
	}).Object as RouteSegmentData)?.Route ?? default;

	public ObjectReference LeftRoute => (DirectlyConnectedRouteSegments.FirstOrDefault(segmentReference =>
	{
		var segment = segmentReference.Object as RouteSegmentData;
		var result = segment.IsHorizontal;
		if (result)
		{
			result = segment.SegmentPositions.Any(position => position.x < Position.x);
		}
		return result;
	}).Object as RouteSegmentData)?.Route ?? default;

	public ObjectReference RightRoute => (DirectlyConnectedRouteSegments.FirstOrDefault(segmentReference =>
	{
		var segment = segmentReference.Object as RouteSegmentData;
		var result = segment.IsHorizontal;
		if (result)
		{
			result = segment.SegmentPositions.Any(position => position.x > Position.x);
		}
		return result;
	}).Object as RouteSegmentData)?.Route ?? default;

	public void DestroyNode()
	{
		var rogueReference = ObjectFactory.Instance.Query<RogueData>(rogue => rogue.Node == Reference).FirstOrDefault();
		if (rogueReference.IsValid)
		{
			ObjectFactory.Instance.Remove(rogueReference);
		}

		var playerReference = ObjectFactory.Instance.Query<PlayerData>(player => player.CurrentNode == Reference).FirstOrDefault();
		if (playerReference.IsValid)
		{
			ObjectFactory.Instance.Remove(playerReference);
		}
	}
}
