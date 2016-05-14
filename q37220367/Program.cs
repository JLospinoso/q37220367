using System;
using System.Collections.Generic;
using System.IO;

namespace Q37220367
{
    class Program
    {
        static IEnumerator<string> DataStream()
        {
            var original = "Tx1\nsome data1\nsome data2\nTx2\nsome data3\nsome data4\nTx3\nsome data5\nsome data6\nTx4\nTx5";
            Console.WriteLine("Original: {0}", original);
            return ((IEnumerable<string>) original.Split('\n')).GetEnumerator();
        }

        static void Main(string[] args)
        {
            Func<string, bool> isDelimiterString = token => token.Length > 2 && token[0] == 'T' && token[1] == 'x';
            var enumerable = DataStream();
            var enumeratorHasNext = enumerable.MoveNext();
            while (enumeratorHasNext)
            {
                var delimiter = enumerable.Current;
                using (var file = new StreamWriter(delimiter + ".txt"))
                {
                    enumeratorHasNext = enumerable.MoveNext();
                    while (enumeratorHasNext && !isDelimiterString(enumerable.Current))
                    {
                        file.WriteLine(enumerable.Current);
                        enumeratorHasNext = enumerable.MoveNext();
                    }
                }
            }
        }
    }
}
