using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;

namespace localSearch
{
    class Program
    {
        public static int k, m, n;

        public static person[,] kk;
        public static person[] k2;
        public static person[] k3;
        public static person[] k4;
        public static person[] k1;
        public static person[] kmin;
        public static double minP = double.MaxValue;
        
        public static Random random;
        static void Main(string[] args)
        {
            //read m,n,k from input

            k=Int32.Parse(Console.ReadLine());
            m = Int32.Parse(Console.ReadLine());
            n = Int32.Parse(Console.ReadLine());


            kk = new person[k, k];
            k2 = new person[k*k];
            k3 = new person[k * k];
            k4 = new person[k * k];
            k1 = new person[k * k];
            kmin = new person[k * k];
            random = new Random(DateTime.Now.Millisecond);

            //initialize the kk and k2

            for (int i = 0; i < m; i++)
            {
                int myx, myy;
                do
                {
                    myx = random.Next(0, k);
                    myy = random.Next(0, k);
                } while (kk[myx, myy] != null);
                kk[myx, myy] = new person(myx, myy, 0, i);
            }

            for (int i = 0; i < n; i++)
            {
                int myx, myy;
                do
                {
                    myx = random.Next(0, k);
                    myy = random.Next(0, k);
                } while (kk[myx, myy] != null);
                kk[myx, myy] = new person(myx, myy, 1, i);
            }
            int count = 0;
            for(int i=0;i< k;i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if(kk[i,j]== null)
                    {
                        kk[i, j] = new person(i, j, 3, count);
                        count++;
                    }
                }
            }
            count = 0;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    k2[count] = kk[i, j];
                    count++;
                }
            }
            if (minP > Displeasure(k2))
            {
                minP = Displeasure(k2);
                for(int i=0;i<k2.Length;i++)
                {
                    kmin[i] = k2[i];
                }
            }

            //initialize the kk and k1

            for (int i = 0; i < k; i++)
                for (int j = 0; j < k; j++)
                    kk[i, j] = null;



            for (int i = 0; i < m; i++)
            {
                int myx, myy;
                do
                {
                    myx = random.Next(0, k);
                    myy = random.Next(0, k);
                } while (kk[myx, myy] != null);
                kk[myx, myy] = new person(myx, myy, 0, i);
            }

            for (int i = 0; i < n; i++)
            {
                int myx, myy;
                do
                {
                    myx = random.Next(0, k);
                    myy = random.Next(0, k);
                } while (kk[myx, myy] != null);
                kk[myx, myy] = new person(myx, myy, 1, i);
            }
             count = 0;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    if (kk[i, j] == null)
                    {
                        kk[i, j] = new person(i, j, 3, count);
                        count++;
                    }
                }
            }
            count = 0;
            for (int i = 0; i < k; i++)
            {
                for (int j = 0; j < k; j++)
                {
                    k1[count] = kk[i, j];
                    count++;
                }
            }
            if (minP > Displeasure(k1))
            {
                minP = Displeasure(k1);
                for (int i = 0; i < k1.Length; i++)
                {
                    kmin[i] = k1[i];
                }
            }
            // cross over

            for(int i= 0; i<100000;i++)
            {
                k3[0] = k1[0];
                k3[1] = k1[1];
                int mycount = 2;
                for (int j = 0; j < k2.Length; j++)
                    if (k2[j].type == k3[0].type && k2[j].number == k3[0].number)
                        continue;
                    else if (k2[j].type == k3[1].type && k2[j].number == k3[1].number)
                        continue;
                    else
                    {
                        k3[mycount] = k2[j];
                        mycount++;
                    }
                // jahesh
                if(i%7 == 0)
                {
                    int myrand1 = random.Next(0, k * k);
                    int myrand2 = random.Next(0, k * k);
                    person temp = k3[myrand1];
                    k3[myrand1] = k3[myrand2];
                    k3[myrand2] = temp;
                }
                if(minP> Displeasure(k3))
                {
                    minP = Displeasure(k3);
                    for (int s = 0; s < k3.Length; s++)
                    {
                        kmin[s] = k3[s];
                    }
                }

                k4[0] = k2[0];
                k4[1] = k2[1];
                mycount = 2;
                for (int j = 0; j < k1.Length; j++)
                    if (k1[j].type == k4[0].type && k1[j].number == k4[0].number)
                        continue;
                    else if (k1[j].type == k4[1].type && k1[j].number == k4[1].number)
                        continue;
                    else
                    {
                        k4[mycount] = k1[j];
                        mycount++;
                    }
                // jahesh
                if (i % 13 == 0)
                {
                    int myrand1 = random.Next(0, k * k);
                    int myrand2 = random.Next(0, k * k);
                    person temp = k4[myrand1];
                    k4[myrand1] = k4[myrand2];
                    k4[myrand2] = temp;
                }
                if (minP > Displeasure(k4))
                {
                    minP = Displeasure(k4);
                    for (int s = 0; s < k4.Length; s++)
                    {
                        kmin[s] = k4[s];
                    }
                }

                for(int j=0;j<k1.Length;j++)
                {
                    k1[j] = k3[j];
                    k2[j] = k4[j];
                }
            }
            Console.Write("The Min Displeasure Is: "+minP+"\n");
            for (int q = 0; q < k*k; q++)
            {
                if (q % k == 0)
                    Console.WriteLine();
                Console.Write(kmin[q].type+"  ");
                
            }

            Console.ReadKey();

        }


        public static double Displeasure(person[] x)
        {
            double displeasure = 0;
            for(int i=0;i<x.Length;i++)
            {

                for(int j=i+1;j<x.Length;j++)
                {
                    if (x[i].type == 3)
                        continue;
                    if(x[i].type == 0)
                    {
                        if (x[j].type == 3)
                            continue;
                        else
                        {
                            displeasure += 1/(Math.Sqrt(((x[i].x - x[j].x)* (x[i].x - x[j].x)+ (x[i].y - x[j].y) * (x[i].y - x[j].y))));

                        }
                    }
                    if(x[i].type == 1)
                    {

                        if (x[j].type == 3 || x[j].type == 1)
                            continue;
                        else
                        {
                            displeasure += 1 / (Math.Sqrt(((x[i].x - x[j].x) * (x[i].x - x[j].x) + (x[i].y - x[j].y) * (x[i].y - x[j].y))));

                        }
                    }

                }
            }

            return displeasure;
        }

    }
}

