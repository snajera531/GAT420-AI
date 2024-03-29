using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntCondition : Condition
{
	IntRef parameter;
	int condition;
	Predicate predicate;

	public IntCondition(IntRef parameter, Predicate predicate, int condition)
	{
		this.parameter = parameter;
		this.predicate = predicate;
		this.condition = condition;
	}

	public override bool IsTrue()
	{
		bool result = false;

		switch (predicate)
		{
			case Predicate.EQUAL:
				result = ((int)parameter == condition);
				break;
			case Predicate.LESS:
				result = ((int)parameter < condition);
				break;
			case Predicate.LESS_EQUAL:
				result = (parameter <= condition);
				break;
			case Predicate.GREATER:
				result = (parameter > condition);
				break;
			case Predicate.GREATER_EQUAL:
				result = (parameter >= condition);
				break;
			default:
				break;
		}

		return result;
	}
}
