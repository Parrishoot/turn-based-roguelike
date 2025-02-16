using System;
using UnityEngine;

public class SelectionCriteria
{
    
    public int MinSelections { get; private set; }

    public int MaxSelections { get; private set; }

    public Predicate<BoardSpace> Filter { get; private set; }


    public SelectionCriteria(int minSelections = 1, int maxSelections = 1, Predicate<BoardSpace> filter = null)
    {
        MinSelections = minSelections;
        MaxSelections = maxSelections;
        Filter = filter == null ? (x) => true : filter;
    }
    
    public SelectionCriteria WithMinSelections(int minSelections) {
        MinSelections = minSelections;
        return this;
    }

    public SelectionCriteria WithMaxSelections(int maxSelections) {
        MaxSelections = maxSelections;
        return this;
    }

    public SelectionCriteria WithFilter(Predicate<BoardSpace> filter) {
        Filter = filter;
        return this;
    }
}
