using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoopingQueue<T>
{
    private Queue<T> queue;

    public LoopingQueue(IEnumerable<T> items) {
        queue = new Queue<T>(items);
    }

    public T GetNext() {

        if(queue.Count == 0) {
            return default;
        }

        T next = queue.Dequeue();
        queue.Enqueue(next);

        return next;
    }
}
