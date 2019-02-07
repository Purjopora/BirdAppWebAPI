using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DemoRestService.Common;
namespace DemoRestService.Algorithms
{
    public static class KekMeansLocationProviderAdapter
    {
        public static readonly int DEFAULT_CLUSTER_AMOUNT = 100;

        //TODO: unnecessary and inefficient transformations? List vs array speed in algorithm?? 

        public static double[][] Cluster(List<LocationProvider> locations, int clusterAmount)
        {
            return KekMeans.Cluster(transform(locations), clusterAmount);
        }

        private static double[][] transform(List<LocationProvider> locations)
        {
            double[][] result = new double[locations.Count][];
            for (int i = 0; i < locations.Count; i++)
            {
                double[] location = new double[2];
                location[0] = locations[i].getX();
                location[1] = locations[i].getY();
                result[i] = location;
            }
            return result;
        }
    }
}