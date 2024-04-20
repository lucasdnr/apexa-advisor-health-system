namespace AdvisorHealthAPI.Caching;


public class LRUCache<TKey, TValue> where TKey : IComparable<TKey>
{
    private readonly int _capacity;
    private readonly Dictionary<TKey, LinkedListNode<CacheItem>> _cacheMap ;
    private readonly LinkedList<CacheItem> _cacheList;

    public LRUCache(int capacity = 1)
    {
        _capacity = capacity;
        _cacheMap = new Dictionary<TKey, LinkedListNode<CacheItem>>();
        _cacheList = new LinkedList<CacheItem>();
    }

    public void Set(TKey key, TValue value)
    {

        if (_cacheMap.TryGetValue(key, out var node))
            Remove(key);

        // if items doesn't exist we verify the capacity and include the new item
        if (_cacheMap.Count >= _capacity)
        {
            var lastNode = _cacheList.Last;
            _cacheList.RemoveLast();
            if(lastNode is not null )
                _cacheMap.Remove(lastNode.Value.Key);
        }

        node = new LinkedListNode<CacheItem>(new CacheItem(key, value));
        _cacheList.AddFirst(node);
        _cacheMap.Add(key, node);
    }

    public TValue? Get(TKey key)
    {
        if (_cacheMap.TryGetValue(key, out var node))
        {
            _cacheList.Remove(node);
            _cacheList.AddFirst(node);
            return node.Value.Value;
        }

        // Key not found in cache
        return default;
    }

    public void Remove(TKey key)
    {
        if (_cacheMap.TryGetValue(key, out var node))
        {
            _cacheList.Remove(node);
            _cacheMap.Remove(key);
        }
    }

    private class CacheItem
    {
        public TKey Key { get; }
        public TValue Value { get; }

        public CacheItem(TKey key, TValue value)
        {
            Key = key;
            Value = value;
        }
    }
}
