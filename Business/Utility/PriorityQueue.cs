using System;
using System.Collections.Generic;

namespace Business.Utilities
{
    public class PriorityQueue<TElement, TPriority> where TPriority : IComparable<TPriority>
    {
        private List<KeyValuePair<TElement, TPriority>> _baseHeap;
        private Dictionary<TElement, int> _indexMap;

        public PriorityQueue()
        {
            _baseHeap = new List<KeyValuePair<TElement, TPriority>>();
            _indexMap = new Dictionary<TElement, int>();
        }

        public int Count => _baseHeap.Count;

        public void Enqueue(TElement element, TPriority priority)
        {
            _baseHeap.Add(new KeyValuePair<TElement, TPriority>(element, priority));
            _indexMap[element] = _baseHeap.Count - 1;
            HeapifyFromEndToBeginning(_baseHeap.Count - 1);
        }

        public TElement Dequeue()
        {
            if (_baseHeap.Count == 0) throw new InvalidOperationException("Priority queue is empty");

            var result = _baseHeap[0].Key;
            _indexMap.Remove(result);
            if (_baseHeap.Count <= 1)
            {
                _baseHeap.Clear();
                return result;
            }

            _baseHeap[0] = _baseHeap[_baseHeap.Count - 1];
            _baseHeap.RemoveAt(_baseHeap.Count - 1);
            HeapifyFromBeginningToEnd(0);
            return result;
        }

        public void UpdatePriority(TElement element, TPriority priority)
        {
            if (!_indexMap.TryGetValue(element, out var index))
            {
                Enqueue(element, priority);
                return;
            }

            var oldPriority = _baseHeap[index].Value;
            _baseHeap[index] = new KeyValuePair<TElement, TPriority>(element, priority);
            if (priority.CompareTo(oldPriority) < 0)
            {
                HeapifyFromEndToBeginning(index);
            }
            else
            {
                HeapifyFromBeginningToEnd(index);
            }
        }

        private void HeapifyFromEndToBeginning(int pos)
        {
            if (pos >= _baseHeap.Count) return;

            var current = pos;
            while (current > 0)
            {
                var parent = (current - 1) / 2;
                if (_baseHeap[current].Value.CompareTo(_baseHeap[parent].Value) >= 0) break;
                Swap(current, parent);
                current = parent;
            }
        }

        private void HeapifyFromBeginningToEnd(int pos)
        {
            var current = pos;
            var leftChild = 2 * current + 1;
            while (leftChild < _baseHeap.Count)
            {
                var rightChild = leftChild + 1;
                var smallestChild = (rightChild < _baseHeap.Count && _baseHeap[rightChild].Value.CompareTo(_baseHeap[leftChild].Value) < 0)
                    ? rightChild
                    : leftChild;

                if (_baseHeap[smallestChild].Value.CompareTo(_baseHeap[current].Value) >= 0) break;
                Swap(current, smallestChild);
                current = smallestChild;
                leftChild = 2 * current + 1;
            }
        }

        private void Swap(int a, int b)
        {
            var temp = _baseHeap[a];
            _baseHeap[a] = _baseHeap[b];
            _baseHeap[b] = temp;

            _indexMap[_baseHeap[a].Key] = a;
            _indexMap[_baseHeap[b].Key] = b;
        }
    }
}
