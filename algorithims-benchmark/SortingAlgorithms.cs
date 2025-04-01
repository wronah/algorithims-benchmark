using BenchmarkDotNet.Attributes;

namespace algorithims_benchmark
{
    [MemoryDiagnoser]
    public class SortingAlgorithms
    {
        private int[] array;

        [Params(10, 1000, 100000)]
        public int ArraySize { get; set; }

        [ParamsSource(nameof(ArrayTypes))]
        public string ArrayType { get; set; }

        public string[] ArrayTypes => new string[] { "Random", "Sorted", "Reversed", "Almost sorted", "Few unique" };

        public int MinVal { get; set; } = 0;
        public int MaxVal { get; set; } = 100000;



        [GlobalSetup]
        public void Setup()
        {
            switch(ArrayType)
            {
                case "Random":
                    array = Generators.GenerateRandom(ArraySize, MinVal, MaxVal);
                    break;
                case "Sorted":
                    array = Generators.GenerateSorted(ArraySize, MinVal, MaxVal);
                    break;
                case "Reversed":
                    array = Generators.GenerateReversed(ArraySize, MinVal, MaxVal);
                    break;
                case "AlmostSorted":
                    array = Generators.GenerateAlmostSorted(ArraySize, MinVal, MaxVal);
                    break;
                case "FewUnique":
                    array = Generators.GenerateFewUnique(ArraySize);
                    break;
            }
            array = Generators.GenerateRandom(ArraySize, 0, 100000);
        }

        [Benchmark]
        public int[] InsertionSort()
        {
            var arr = array;
            int n = arr.Length;
            for (int i = 1; i < n; ++i)
            {
                int key = arr[i];
                int j = i - 1;

                while (j >= 0 && arr[j] > key)
                {
                    arr[j + 1] = arr[j];
                    j = j - 1;
                }
                arr[j + 1] = key;
            }
            return arr;
        }

        [Benchmark]
        public int[] MergeSort()
        {
            return MergeSortRecursive(array);
        }

        private int[] MergeSortRecursive(int[] arr)
        {
            if(arr.Length <= 1)
            {
                return arr;
            }

            int mid = arr.Length / 2;
            int[] left = new int[mid];
            int[] right = new int[arr.Length - mid];

            Array.Copy(arr, 0, left, 0, mid);
            Array.Copy(arr, mid, right, 0, arr.Length - mid);

            left = MergeSortRecursive(left);
            right = MergeSortRecursive(right);

            return Merge(left, right);
        }

        private int[] Merge(int[] left, int[] right)
        {
            int[] result = new int[left.Length + right.Length];

            int i = 0;
            int j = 0;
            int k = 0;

            while (i < left.Length && j < right.Length)
            {
                if (left[i] <= right[j])
                {
                    result[k] = left[i];
                    i++;
                }
                else
                {
                    result[k] = right[j];
                    j++;
                }
                k++;
            }

            while (i < left.Length)
            {
                result[k] = left[i];
                i++;
                k++;
            }

            while (j < right.Length)
            {
                result[k] = right[j];
                j++;
                k++;
            }

            return result;
        }


        [Benchmark]
        public int[] QuickSortClassical()
        {

            return QuickSortClassicalRecursive(array);
        }
        
        private int[] QuickSortClassicalRecursive(int[] arr)
        {
            if(arr.Length <= 1)
                return arr;

            int pi = Partition(arr, 0, arr.Length - 1);

            int[] left = QuickSortClassicalRecursive(arr[0..pi]);
            int[] right = QuickSortClassicalRecursive(arr[(pi + 1)..arr.Length]);

            return Merge(left, right);
        }

        private int Partition(int[] arr, int low, int high)
        {

            int pivot = arr[high];

            int i = low - 1;

            for (int j = low; j <= high - 1; j++)
            {
                if (arr[j] < pivot)
                {
                    i++;
                    Swap(arr, i, j);
                }
            }

            Swap(arr, i + 1, high);
            return i + 1;
        }

        private void Swap(int[] arr, int i, int j)
        {
            int temp = arr[i];
            arr[i] = arr[j];
            arr[j] = temp;
        }

        [Benchmark]
        public int[] QuickSort()
        {
            Array.Sort(array);
            return array;
        }
    }
}
