using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceExpression {

    int first = 0;
    int second= 0;

    public DiceExpression(int a, int b)
    {
        first = a;
        second = b;
    }

    public int Roll()
    {
        return Random.Range(first, second);
    }

}
