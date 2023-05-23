using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lb15
{
    internal class HashMap2<K,V>
    {
        private List<HashNode<K, V>> bucketArray;
        private int numBuckets;
        private int size;
        public HashMap2()
        {
            bucketArray = new List<HashNode<K, V>>();
            numBuckets = 10;
            size = 0;
            for (int i = 0; i < numBuckets; i++)
            {
                bucketArray.Add(null);
            }
        }
        public HashMap2(int a)
        {
            bucketArray = new List<HashNode<K, V>>();
            numBuckets = a;
            size = 0;
            for (int i = 0; i < numBuckets; i++)
            {
                bucketArray.Add(null);
            }
        }
        public int Size() { return size; }
        public bool isEmpty() { return Size() == 0; }
        private int HashCode(K key)
        {
            return key.GetHashCode();
        }
        private int getBucketIndex(K key)
        {
            int hashCode = HashCode(key);
            int index = hashCode % numBuckets;
            index = index < 0 ? index * -1 : index;
            return index;
        }
        public V remove(K key)
        {
            int bucketIndex = getBucketIndex(key);
            int hashCode = HashCode(key);
            HashNode<K, V> head = bucketArray[bucketIndex];
            HashNode<K, V> prev = null;
            while (head != null)
            {
                if (head.key.Equals(key) && hashCode == head.hashCode) break;
                prev = head;
                head = head.next;
            }
            if (head == null) { return default(V); }
            size--;
            if (prev != null) { prev.next = head.next; }
            else
            {
                bucketArray[bucketIndex] = head.next;
            }
            return head.value;
        }
        public V get(K key)
        {
            int bucketIndex = getBucketIndex(key);
            int hashCode = HashCode(key);
            HashNode<K, V> head = bucketArray[bucketIndex];
            while (head != null)
            {
                if (head.key.Equals(key) && head.hashCode == hashCode) { return head.value; }
                head = head.next;
            }
            return default(V);
        }
        public void add(K key, V value)
        {
            var temp = HashCode(key);
            if (bucketArray[temp] == null)
            {
                bucketArray[temp] = new HashNode<K, V>(key, value, HashCode(key));
            }
            else
            {
                
                for (int i = 0; i < size+1; i++)
                {
                    if (bucketArray[i] == null)
                    {
                        bucketArray[i] = new HashNode<K, V>(key, value, HashCode(key));
                        break;
                    }
                }
            }
            size++;
        }
        public override string ToString()
        {
            var str = new StringBuilder();
            for (int i = 0; i < size; ++i)
            {
                str.Append(i + ": ");
                var temp = bucketArray[i];
                while (temp != null)
                {

                    str.Append(temp.ToString() + " ");
                    temp = temp.next;
                }
                str.AppendLine();
            }
            return str.ToString();
        }
    }
}
