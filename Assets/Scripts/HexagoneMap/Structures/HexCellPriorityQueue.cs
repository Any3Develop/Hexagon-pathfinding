using System.Collections.Generic;

public class HexCellPriorityQueue
{
    private int _minimum = int.MaxValue;

    private List<ICell> _list = new List<ICell>();

    public int Count { get; private set; }

    public void Enqueue(ICell cell)
    {
        Count++;
        int priority = cell.SearchPriority;

        if (priority < _minimum)
        {
            _minimum = priority;
        }

        while (priority >= _list.Count)
        {
            _list.Add(null);
        }
        cell.NextWithSamePriority = _list[priority];
        _list[priority] = cell;
    }

    public ICell Dequeue()
    {
        Count--;
        for (; _minimum < _list.Count; _minimum++)
        {
            ICell cell = _list[_minimum];
            if (cell != null)
            {
                _list[_minimum] = cell.NextWithSamePriority;
                return cell;
            }
        }
        return null;
    }

    public void Clear()
    {
        _list.Clear();
        Count = 0;
        _minimum = int.MaxValue;
    }

    public void Change(ICell cell, int oldPriority)
    {
        ICell current = _list[oldPriority];
        ICell next = current.NextWithSamePriority;
        if (current == cell)
        {
            _list[oldPriority] = next;
        }
        else
        {
            while (next != cell)
            {
                current = next;
                next = current.NextWithSamePriority;
            }
        }
        //while (next != cell)
        //{
        //    current = next;
        //    next = current.NextWithSamePriority;
        //}
        current.NextWithSamePriority = cell.NextWithSamePriority;
        Enqueue(cell);
        Count--;
    }
}
