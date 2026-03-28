int[] ints = {1, 2, 4 ,5 ,4 , 6};

Find(ints);

void Find(int[] a) 
{
    Dictionary<int, int> last = new Dictionary<int, int>();
    last.Add(0, a[0]);

    for (int i = 1; i < a.Length; i++) 
    {
        if (last.ContainsKey(a[i])) { last[a[i]]++; }
        else { last.Add(i, a[i]); }
    }

    int aa = last.Keys.Max();

    Console.WriteLine(last[aa]);
}