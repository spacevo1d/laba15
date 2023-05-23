using System.Collections;
using System.Text;
using lb15;

{
    var a = new Map<int, string>(5);
    var str = "EASYQUTION";
    for (int i = 0; i < str.Length; i++)
    {
        a.add(11 * Convert.ToInt32(str[i]) % 5, str[i].ToString());
    }
    Console.WriteLine(a.ToString());
    Console.WriteLine();
    var b = new HashMap2<int, string>(10);
    for (int i = 0; i < str.Length; i++)
    {
        b.add(11 * Convert.ToInt32(str[i]) % 5, str[i].ToString());
    }
    Console.WriteLine(b.ToString());
}
public class HashNode<K, V>
{
    public K key;
    public V value;
    public int hashCode;
    public HashNode<K, V> next;
    public HashNode(K key, V value, int hashCode)
    {
        this.key = key;
        this.value = value;
        this.hashCode = hashCode;
    }
    public override string ToString()
    {
        return value + " " + key;
    }
}
public class Map<K, V>
{
    private List<HashNode<K, V>> bucketArray;
    private int numBuckets;
    private int size;
    public Map()
    {
        bucketArray = new List<HashNode<K, V>>();
        numBuckets = 10;
        size = 0;
        for(int i = 0; i < numBuckets; i++)
        {
            bucketArray.Add(null);
        }
    }
    public Map(int a)
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
    public bool isEmpty() { return Size()==0; }
    private int HashCode(K key)
    {
        return key.GetHashCode();
    }
    private int getBucketIndex(K key)
    {
        int hashCode = HashCode(key);
        int index=hashCode%numBuckets;
        index = index < 0 ? index * -1 : index;
        return index;
    }
    public V remove(K key)
    {
        int bucketIndex=getBucketIndex(key);
        int hashCode=HashCode(key);
        HashNode<K,V> head= bucketArray[bucketIndex];
        HashNode<K, V> prev = null;
        while( head != null )
        {
            if (head.key.Equals(key) && hashCode == head.hashCode) break;
            prev = head;
            head = head.next;
        }
        if ( head== null ) { return default(V); }
        size--;
        if(prev!=null) { prev.next=head.next; }
        else
        {
            bucketArray[bucketIndex]= head.next;
        }
        return head.value;
    }
    public V get(K key)
    {
        int bucketIndex = getBucketIndex(key);
        int hashCode=HashCode(key);
        HashNode<K,V> head= bucketArray[bucketIndex];
        while( head != null )
        {
            if (head.key.Equals(key)&& head.hashCode == hashCode) {return head.value;}
            head = head.next;
        }
        return default(V);
    }
    public void add(K key,V value)
    {
        var temp=HashCode(key);
        if (bucketArray[temp] == null)
        {
            bucketArray[temp] = new HashNode<K,V>(key,value,HashCode(key));
        }
        else
        {
            var tp= bucketArray[temp];
            while( true)
            {
                if (tp.next != null)
                {
                    tp = tp.next;
                }
                else
                {
                    tp.next=new HashNode<K, V>(key, value, HashCode(key));
                    break;
                }
            }
        }
    }
    public override string ToString()
    {
        var str = new StringBuilder();
        for(int i=0; i<numBuckets; ++i)
        {
            str.Append(i+": ");
            var temp = bucketArray[i];
            while(temp != null)
            {
                
                str.Append(temp.ToString()+" ");
                temp= temp.next;
            }
            str.AppendLine();
        }
        return str.ToString();
    }
}