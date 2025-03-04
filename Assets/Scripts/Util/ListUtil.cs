using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public static class ListUtil
{
    public static List<T> Shuffled<T>(this List<T> l) {
        return l.OrderBy(_ => Guid.NewGuid().ToString()).ToList();
    }
}
