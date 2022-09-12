using System.Linq;
using UnityEngine;
using BitButterCORE.V2;
using static Constants;

public class Test : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Start");

        var levelReference = ObjectFactory.Instance.Create<LevelData>(1);
        var nodeReference1 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Start, new Vector2(0, 0));
        var nodeReference2 = ObjectFactory.Instance.Create<NodeData>(levelReference, NodeType.Goal, new Vector2(10, 10));
        var routeReference = ObjectFactory.Instance.Create<RouteData>(levelReference, nodeReference1, nodeReference2);
        var segmentReference1 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference, true, 0);
        var segmentReference2 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference, false, 5);
        var segmentReference3 = ObjectFactory.Instance.Create<RouteSegmentData>(routeReference, true, 10);

        (segmentReference1.Object as RouteSegmentData).SetConnected(new[] { nodeReference1, segmentReference2 });
        (segmentReference2.Object as RouteSegmentData).SetConnected(new[] { segmentReference1, segmentReference3 });
        (segmentReference3.Object as RouteSegmentData).SetConnected(new[] { segmentReference2, nodeReference2 });

        var guardReference = ObjectFactory.Instance.Create<GuardData>(routeReference);

        var routeSegments = (routeReference.Object as RouteData).RouteSegments;
        foreach (var segmentReference in routeSegments)
		{
            var segment = segmentReference.Object as RouteSegmentData;
            Debug.Log(string.Format("{0}", string.Join(" - ", segment.SegmentPositions)));
		}

        Debug.Log(string.Format("路线段1是否连接到起始节点 {0}", (segmentReference1.Object as RouteSegmentData).IsConnectedToNode(nodeReference1)));
        Debug.Log(string.Format("路线段1是否连接到逃脱节点 {0}", (segmentReference1.Object as RouteSegmentData).IsConnectedToNode(nodeReference2)));

        Debug.Log(string.Format("路线段2是否连接到起始节点 {0}", (segmentReference2.Object as RouteSegmentData).IsConnectedToNode(nodeReference1)));
        Debug.Log(string.Format("路线段2是否连接到逃脱节点 {0}", (segmentReference2.Object as RouteSegmentData).IsConnectedToNode(nodeReference2)));

        Debug.Log(string.Format("路线段3是否连接到起始节点 {0}", (segmentReference3.Object as RouteSegmentData).IsConnectedToNode(nodeReference1)));
        Debug.Log(string.Format("路线段3是否连接到逃脱节点 {0}", (segmentReference3.Object as RouteSegmentData).IsConnectedToNode(nodeReference2)));

        var firstRouteSegmentFromStartNode = (routeReference.Object as RouteData).GetFirstRouteSegment(nodeReference1).Object as RouteSegmentData;
        var firstRouteSegmentFromGoalNode = (routeReference.Object as RouteData).GetFirstRouteSegment(nodeReference2).Object as RouteSegmentData;
        Debug.Log(string.Format("路线在起始节点一侧的第一路线段 {0}", string.Join("-", firstRouteSegmentFromStartNode.SegmentPositions)));
        Debug.Log(string.Format("路线在逃脱节点一侧的第一路线段 {0}", string.Join("-", firstRouteSegmentFromGoalNode.SegmentPositions)));

        var upRouteFromStartNode = (nodeReference1.Object as NodeData).UpRoute;
        var downRouteFromStartNode = (nodeReference1.Object as NodeData).DownRoute;
        var leftRouteFromStartNode = (nodeReference1.Object as NodeData).LeftRoute;
        var rightRouteFromStartNode = (nodeReference1.Object as NodeData).RightRoute;
        Debug.Log(string.Format("起始节点是否存在向上路线 {0}", upRouteFromStartNode.IsValid));
        Debug.Log(string.Format("起始节点是否存在向下路线 {0}", downRouteFromStartNode.IsValid));
        Debug.Log(string.Format("起始节点是否存在向左路线 {0}", leftRouteFromStartNode.IsValid));
        Debug.Log(string.Format("起始节点是否存在向右路线 {0}", rightRouteFromStartNode.IsValid));

        var upRouteFromGoalNode = (nodeReference2.Object as NodeData).UpRoute;
        var downRouteFromGoalNode = (nodeReference2.Object as NodeData).DownRoute;
        var leftRouteFromGoalNode = (nodeReference2.Object as NodeData).LeftRoute;
        var rightRouteFromGoalNode = (nodeReference2.Object as NodeData).RightRoute;
        Debug.Log(string.Format("逃脱节点是否存在向上路线 {0}", upRouteFromGoalNode.IsValid));
        Debug.Log(string.Format("逃脱节点是否存在向下路线 {0}", downRouteFromGoalNode.IsValid));
        Debug.Log(string.Format("逃脱节点是否存在向左路线 {0}", leftRouteFromGoalNode.IsValid));
        Debug.Log(string.Format("逃脱节点是否存在向右路线 {0}", rightRouteFromGoalNode.IsValid));

        var playerReference = ObjectFactory.Instance.Create<PlayerData>();
        if (playerReference.Object is PlayerData playerData)
        {
            playerData.CurrentNode = nodeReference1;
        }

    }

    void Update()
    {
  //      if (Input.GetKeyDown(KeyCode.LeftArrow))
		//{
  //          ObjectFactory.Instance.Create<TestObject>();
  //          SoundManagement.PlayAudio("click");
  //      }
  //      else if (Input.GetKeyDown(KeyCode.RightArrow))
		//{
  //          var objectToRemove = ObjectFactory.Instance.Query<TestObject>().LastOrDefault();
  //          if (objectToRemove != null && objectToRemove.IsValid)
  //          {
  //              ObjectFactory.Instance.Remove(objectToRemove);
  //          }
  //      }
    }
}
