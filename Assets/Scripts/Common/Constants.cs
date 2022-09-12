public static class Constants
{
    public enum InventoryPosition
	{
		TopRight,
		TopLeft,
		BottomRight,
		BottomLeft,
	}

	public static InventoryPosition MoveLoot(this InventoryPosition position, bool isHorizontal)
	{
		var result = position;
		switch (position)
		{
			case InventoryPosition.TopRight:
				result = isHorizontal ? InventoryPosition.TopLeft : InventoryPosition.BottomRight;
				break;
			case InventoryPosition.TopLeft:
				result = isHorizontal ? InventoryPosition.TopRight : InventoryPosition.BottomLeft;
				break;
			case InventoryPosition.BottomLeft:
				result = isHorizontal ? InventoryPosition.BottomRight : InventoryPosition.TopLeft;
				break;
			case InventoryPosition.BottomRight:
				result = isHorizontal ? InventoryPosition.BottomLeft : InventoryPosition.TopRight;
				break;
		}
		return result;
	}

	public enum LootIcon
	{
		Hat = 0,	// 帽子
		Mask,		// 面具
		Rabbit,		// 兔子
		Card,		// 卡牌
		Coin,		// 硬币
	}

	public enum NodeType
	{
		Start,		// 起始节点
		Goal,		// 逃脱节点
		Normal,		// 一般节点
	}

	public enum RogueAbility
	{
		MoveLeft,	// 向左移动
		MoveRight,	// 向右移动
		MoveUp,		// 向上移动
		MoveDown,	// 向下移动
	}
}
