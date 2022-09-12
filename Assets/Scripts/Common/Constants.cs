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
		Hat = 0,	// ñ��
		Mask,		// ���
		Rabbit,		// ����
		Card,		// ����
		Coin,		// Ӳ��
	}

	public enum NodeType
	{
		Start,		// ��ʼ�ڵ�
		Goal,		// ���ѽڵ�
		Normal,		// һ��ڵ�
	}

	public enum RogueAbility
	{
		MoveLeft,	// �����ƶ�
		MoveRight,	// �����ƶ�
		MoveUp,		// �����ƶ�
		MoveDown,	// �����ƶ�
	}
}
