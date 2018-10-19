using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace AI_Project
{
    
    class Program
    {
        public static int[,] resulti;
        public static int[,] resultj;
        public static int[,] H;
        public static int[,] obstacle;
        public static int readBlue = 0;
        public static int readRed = 1;
        public static int m;
        public static int n;
        public static int i;
        public static int j;
        public static int[,] blue;
        public static int[,] red;
        public static int k;
        public static int preI ;
        public static int preJ ;
        static void Main(string[] args)
        {
            StreamReader file = new StreamReader("file.txt");
            n = Int32.Parse(file.ReadLine());
            m = Int32.Parse(file.ReadLine());
            string[] character = { " ", ".","," };
            string s = file.ReadLine();
            string[] ij = s.Split(character, StringSplitOptions.RemoveEmptyEntries);
            i = Int32.Parse(ij[0])+1;
            j = Int32.Parse(ij[1])+1;
            preI = i;
            preJ = j;
            k = Int32.Parse(file.ReadLine());
            blue = new int[2,k];
            red = new int[2, k];
            for (int q=0;q<k;q++)
            {
                s = file.ReadLine();
                ij = s.Split(character, StringSplitOptions.RemoveEmptyEntries);
                blue[0, q] = int.Parse(ij[0])+1;
                blue[1, q] = int.Parse(ij[1])+1;
            }

           
            obstacle = new int[2, m * n];
            for(int q=0;q<m*n;q++)
            {
                obstacle[0, q] = -1;
                obstacle[1, q] = -1;

            }
            int qq = 0;
            while((s = file.ReadLine()) != null)
            {
                ij = s.Split(character, StringSplitOptions.RemoveEmptyEntries);
                obstacle[0, qq] = int.Parse(ij[0])+1;
                obstacle[1, qq] = int.Parse(ij[1])+1;
                qq++;

            }

            resulti = new int[n + 2,m + 2];
            resultj = new int[n + 2, m + 2];
            H = new int[n + 2, m + 2]   ;
            for (int y = 0; y < n + 2; y++)
            {
                for (int z = 0; z < m + 2; z++)
                {
                    if (y == 0 || z == 0 || y == n + 1 || z == m + 1)
                    {
                        H[y, z] = int.MaxValue -5;
                    }
                    else
                        H[y, z] = 0;
                }
            }
            for(int y=0;y<m*n;y++)
            {
                if(obstacle[0,y] != -1)
                {
                    int o = obstacle[0, y];
                    int r = obstacle[1, y];
                    H[o, r] = int.MaxValue - 5;
                }

            }

            LRTAstar();
            Console.ReadKey();

        }

        public static void LRTAstar()
        {
            searchBlue();
            searchRed();
            if(readBlue>0 && readRed>0)
            {
                return;
            }
            if(H[i,j] == 0)
            {
                H[i,j] = setH(i,j);
            }
            
            lrtaStarCost();
        }

        public static void searchBlue()
        {
            for(int f =0;f<k;f++)
                if(blue[0,f] == i && blue[1,f] == j)
                {
                    readBlue++;
                    blue[0, f] = -100;
                    blue[1, f] = -100;
                }
            
        }
        public static void searchRed()
        {
            for (int f = 0; f < k; f++)
                if (red[0, f] == i && red[1, f] == j)
                {
                    readRed++;
                    red[0, f] = -100;
                    red[1, f] = -100;
                }

        }
        public static void lrtaStarCost()
        {
            int []hehe = new int[4];
                if (H[i - 1, j] != 0)
                    hehe[0] = H[i-1, j] + 1;
                if (H[i + 1, j] != 0)
                    hehe[1] = H[i+1, j] + 1;
                if (H[i, j - 1] != 0)
                    hehe[2] = H[i, j-1] + 1;
                if (H[i, j + 1] != 0)
                    hehe[3] = H[i, j+1] + 1;
            
            if (H[i - 1, j] == 0)
                hehe[0] = setH(i, j);
            if (H[i + 1, j] == 0)
                hehe[1] = setH(i , j);
            if (H[i, j - 1] == 0)
                hehe[2] = setH(i, j );
            if (H[i, j + 1] == 0)
                hehe[3] = setH(i, j );

            int min = int.MaxValue;
            int action = 5;
            for (int g = 0; g < 4; g++)
                if (min > hehe[g])
                {
                    min = hehe[g];
                    action = g;
                }
            //if (preI != -100 && preJ != -100)
                H[preI, preJ] = min;
            preI = i;
            preJ = j;
            if (action == 0)
                i--;
            if (action == 1)
                i++;
            if (action == 2)
                j--;
            if (action == 3)
                j++;

            resulti[preI, preJ] = i;
            resultj[preI, preJ] = j;

            for (int b=0;b<n+2;b++)
            {
                for(int bb=0;bb<m+2;bb++)
                {

                    Console.Write(H[b,bb]+"  ");
                }
                Console.WriteLine();
            }
            Console.WriteLine("\n\n\n");
            LRTAstar();

        }
        public static int setH(int row,int column)
        {
            int min = int.MaxValue;
            for(int e=0;e<k;e++)
            {
                int manhattan = Math.Abs(blue[0, e] - row) + Math.Abs(blue[1, e] - column);
                if (min > manhattan)
                    min = manhattan;
                manhattan = Math.Abs(red[0, e] - row) + Math.Abs(red[1, e] - column);
                if (min > manhattan)
                    min = manhattan;
            }
            return min;
        }
        
    }
}
