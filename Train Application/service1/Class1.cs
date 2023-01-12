using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace service1 
{
    public class Class1
    {
        public string checkvalue(int value)
        {
        if (value > 50)
        {
        return "red";
        }
        else
        {
        return "white";
        }
        }


        public int ShortDistance(List<string> station1, List<string> station2, List<string> distance, string from, string to, int rows)
        {
        int distanceadd = 0;
        string currentstation = "";
        bool startcal = false;
        int calc;

        for (calc = 0; calc < rows; calc++)
        {
                if (calc == rows)
                {
                    calc = 0;
                }

        string stationone = station1[calc];
        string stationtwo = station2[calc];
        int miles = Int32.Parse(distance[calc]);

            if (to == from)
            {
            return 0;
            }

            if (from == stationone && to == stationtwo)
            {
            return miles;
            }

            if (to == stationtwo && startcal == true)
            {
            distanceadd = distanceadd + miles;
            return distanceadd;
            }

            if (from == stationone)
            {
            distanceadd = distanceadd + miles;
            currentstation = stationtwo;
            startcal = true;
            }

            if (currentstation == stationone && startcal == true)
            {
            distanceadd = distanceadd + miles;
            currentstation = stationtwo;
            }
        }
        return 10000000;
    }
}
}

