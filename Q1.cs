		/// <summary>
        /// Given an array of size n-1 and given that there are numbers from 1 to n with one missing, write an function to find the missing number
        /// </summary>
        public void FindNumberMiss()
        {
            int[] ar = { 2, 3, 2, 1, 5, 6, 9, 7, 8, 10, 7 };
            Array.Sort(ar);
            int miss = -1;
            for (int i = 0; i < ar.Length - 2; i++)
            {
                if (ar[i + 1] - ar[i] == 2)
                {
                    miss = ar[i] + 1;
                    break;
                }
            }
            if (miss > 0)
            {
                Console.Write(String.Format("Miss number: {0}", miss));
            }
            else
            {
                Console.Write(String.Format("No miss number"));
            }
            for (int i = 0; i < ar.Length - 1; i++)
            {
                Console.WriteLine(String.Format("number: {0}", ar[i]));
            }
            Console.Read();
        }