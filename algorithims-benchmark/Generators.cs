namespace algorithims_benchmark
{
    public static class Generators
    {
        public static int[] GenerateRandom(int size, int minVal, int maxVal)
        {
            int[] a = new int[size];

            var random = new Random();
            for(int i = 0; i < size; i++)
            {
                a[i] = random.Next(minVal, maxVal);
            } 
            return a;
        }
        public static int[] GenerateSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateRandom(size, minVal, maxVal);
            Array.Sort(a);
            return a;
        }
        public static int[] GenerateReversed(int size, int minVal, int maxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            Array.Reverse(a);
            return a;
        }

        public static int[] GenerateAlmostSorted(int size, int minVal, int maxVal)
        {
            int[] a = GenerateSorted(size, minVal, maxVal);
            var random = new Random();

            for(int i = 0; i < size / 20; i++)
            {
                var randomIndex1 = random.Next(0, size);
                var randomIndex2 = random.Next(0, size);
                var temp = a[randomIndex1];
                a[randomIndex1] = a[randomIndex2];
                a[randomIndex2] = temp;
            }
            return a;
        }

        public static int[] GenerateFewUnique(int size)
        {
            int[] a = GenerateRandom(size, 0, size / 10);
            return a;
        }
    }
}
