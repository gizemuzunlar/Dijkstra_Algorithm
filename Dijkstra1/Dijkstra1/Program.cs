using System;
using System.Collections.Generic;
using System.Linq;

namespace DijkstraAlgorithm
{

    public class Graf
    {
        public List<Node> cities;
        public List<edge> roads;
        public Graf()
        {
            cities = new List<Node>();
            roads = new List<edge>();
        }
        public List<Node> GetNeighbors(Node d)
        {
            List<Node> Neighbors = new List<Node>();
            foreach (var item in roads)
            {
                if (item.Node1.name == d.name)
                {
                    Neighbors.Add(item.Node2);
                }
                else if (item.Node2.name == d.name)
                {
                    Neighbors.Add(item.Node1);
                }
            }
            return Neighbors;
        }
        public double avgNeighbors()
        {
            int[] conroads = new int[cities.Count];
            int index = 0;
            foreach (var item in cities)

            {
                conroads[index] = GetNeighbors(item).Count;
                index++;
            }
            double avg = 0;
            for (int i = 0; i < conroads.Length; i++)
            {
                avg = avg + conroads[i];
            }
            avg = avg / conroads.Length;
            return avg;
        }
        public void AddNode(Node d)
        {
            cities.Add(d);
        }
        public void AddEdge(edge k)
        {
            roads.Add(k);
            k.Node1.neighbor.Add(k.Node2);
            k.Node2.neighbor.Add(k.Node1);
        }
        public int Countroads()
        {
            return roads.Count;
        }
        public Node GetCity(string name)
        {
            foreach (var item in cities)
            {
                if (item.name == name)
                {
                    return item;
                }
            }
            return null;
        }
        public string GetCity(int no)
        {
            foreach (var item in cities)
            {
                if (item.id == no)
                    return item.name;
            }
            return null;
        }
    }
    public class edge
    {
        public int distance;
        public Node Node1;
        public Node Node2;
        public edge(Node d1, Node d2, int m)
        {
            Node1 = d1;
            Node2 = d2;
            distance = m;
        }
    }
    public class Node
    {
        public int id;
        public string name;

        public List<Node> neighbor;
        public Node(string cityName, int no)
        {
            neighbor = new List<Node>();
            name = cityName;
            id = no;
        }
    }
    public class Program
    {
        public static List<int> DijkstraAlgorithm(int[,] graph, int sourceNode, int
       destinationNode)
        {
            var n = graph.GetLength(0);
            var distance = new int[n];
            for (int i = 0; i < n; i++)
            {
                distance[i] = int.MaxValue;
            }
            distance[sourceNode] = 0;
            var used = new bool[n]; var previous = new int?[n];
            while (true)
            {
                var minDistance = int.MaxValue;
                var minNode = 0;
                for (int i = 0; i < n; i++)
                {
                    if (!used[i] && minDistance > distance[i])
                    {
                        minDistance = distance[i];
                        minNode = i;
                    }
                }
                if (minDistance == int.MaxValue)
                {
                    break;
                }
                used[minNode] = true;
                for (int i = 0; i < n; i++)
                {
                    if (graph[minNode, i] > 0)
                    {
                        var shavgestToMinNode = distance[minNode];
                        var distanceToNextNode = graph[minNode, i];
                        var totalDistance = shavgestToMinNode + distanceToNextNode;
                        if (totalDistance < distance[i])
                        {
                            distance[i] = totalDistance;
                            previous[i] = minNode;
                        }
                    }
                }
            }
            if (distance[destinationNode] == int.MaxValue)
            {
                return null;
            }
            var path = new LinkedList<int>();
            int? currentNode = destinationNode;
            while (currentNode != null)
            {
                path.AddFirst(currentNode.Value);

                currentNode = previous[currentNode.Value];
            }
            return path.ToList();
        }
        public static void PrintPath(int[,] graph, Node source, Node destination,
       Graf g)
        {
            int sourceNode = source.id;
            int destinationNode = destination.id;
            Console.Write("En Kısa Mesafe {0} -> {1}: ", source.name, destination.name);
            var path = DijkstraAlgorithm(graph, sourceNode, destinationNode);
            if (path == null)
            {
                Console.WriteLine("No Path");
            }
            else
            {
                int pathLength = 0;
                for (int i = 0; i < path.Count - 1; i++)
                {
                    pathLength += graph[path[i], path[i + 1]];
                }
                var formattedPath = string.Join("->", path);
                string format = "";
                foreach (var item in path)
                {
                    format = format + "->" + g.GetCity(item);
                }
                Console.WriteLine("{0} ({1} km)", format, pathLength);
            }
            public static int[,] DGraph(Graf g)
            {
                int[,] map = new int[g.cities.Count, g.cities.Count];
                int i = 0, j = 0;
                foreach (var sourcenode in g.cities)
                {
                    j = 0;
                    foreach (var desNode in g.cities)
                    {
                        if (sourcenode == desNode)
                            map[i, j] = 0;
                        else
                            foreach (var item in g.GetNeighbors(sourcenode))
                                if (item == desNode)
                                {
                                    foreach (var a in g.roads)
                                        if (a.Node1 == sourcenode && a.Node2 == desNode || a.Node1 == desNode &&
                                       a.Node2 == sourcenode)
                                            map[i, j] = a.distance;
                                }
                        j++;
                    }
                    i++;
                }
                return map;
            }
            public static void Main()
            {
                Graf graph = new Graf();
                graph.AddNode(new Node("Edirne", 0));
                graph.AddNode(new Node("Kırklareli", 1));
                graph.AddNode(new Node("Tekirdağ", 2));
                graph.AddNode(new Node("İstanbul", 3));

                graph.AddNode(new Node("Kocaeli", 4));
                graph.AddNode(new Node("Sakarya", 5));
                graph.AddNode(new Node("Bursa", 6));
                graph.AddNode(new Node("Bilecik", 7));
                graph.AddNode(new Node("Eskişehir", 8));
                graph.AddNode(new Node("Kütahya", 9));
                graph.AddNode(new Node("Afyon", 10));
                graph.AddNode(new Node("Isparta", 11));
                graph.AddNode(new Node("Antalya", 12));
                graph.AddNode(new Node("Uşak", 13));
                graph.AddNode(new Node("Denizli", 14));
                graph.AddNode(new Node("Burdur", 15));
                graph.AddNode(new Node("Muğla", 16));
                graph.AddNode(new Node("Manisa", 17));
                graph.AddNode(new Node("Aydın", 18));
                graph.AddNode(new Node("İzmir", 19));
                graph.AddNode(new Node("Balıkesir", 20));
                graph.AddNode(new Node("Çanakkale", 21));
                graph.AddEdge(new edge(graph.GetCity("Edirne"), graph.GetCity("Kırklareli"),
               62));
                graph.AddEdge(new edge(graph.GetCity("Edirne"), graph.GetCity("Tekirdağ"),
               140));
                graph.AddEdge(new edge(graph.GetCity("Tekirdağ"), graph.GetCity("İstanbul"),
               132));
                graph.AddEdge(new edge(graph.GetCity("İstanbul"), graph.GetCity("Kocaeli"),
               111));
                graph.AddEdge(new edge(graph.GetCity("Kocaeli"), graph.GetCity("Bursa"),
               132));
                graph.AddEdge(new edge(graph.GetCity("Kocaeli"), graph.GetCity("Sakarya"),
               37));
                graph.AddEdge(new edge(graph.GetCity("Bursa"), graph.GetCity("Sakarya"),
               159));
                graph.AddEdge(new edge(graph.GetCity("Bursa"), graph.GetCity("Bilecik"),
               95));
                graph.AddEdge(new edge(graph.GetCity("Bursa"), graph.GetCity("Kütahya"),
               177));
                graph.AddEdge(new edge(graph.GetCity("Bursa"), graph.GetCity("Balıkesir"),
               151));
                graph.AddEdge(new edge(graph.GetCity("Bilecik"), graph.GetCity("Eskişehir"),
               82));
                graph.AddEdge(new edge(graph.GetCity("Bilecik"), graph.GetCity("Sakarya"),
               99));
                graph.AddEdge(new edge(graph.GetCity("Kütahya"), graph.GetCity("Uşak"),
               139));
                graph.AddEdge(new edge(graph.GetCity("Kütahya"), graph.GetCity("Manisa"),
               317));
                graph.AddEdge(new edge(graph.GetCity("Kütahya"), graph.GetCity("Afyon"),
               100));
                graph.AddEdge(new edge(graph.GetCity("Kütahya"), graph.GetCity("Eskişehir"),
               78));
                graph.AddEdge(new edge(graph.GetCity("Afyon"), graph.GetCity("Isparta"),
               169));
                graph.AddEdge(new edge(graph.GetCity("Afyon"), graph.GetCity("Eskişehir"),
               144));
                graph.AddEdge(new edge(graph.GetCity("Afyon"), graph.GetCity("Burdur"),
               170));
                graph.AddEdge(new edge(graph.GetCity("Afyon"), graph.GetCity("Denizli"),
               223));
                graph.AddEdge(new edge(graph.GetCity("Afyon"), graph.GetCity("Uşak"), 116));
                graph.AddEdge(new edge(graph.GetCity("Denizli"), graph.GetCity("Aydın"),
               126));
                graph.AddEdge(new edge(graph.GetCity("Denizli"), graph.GetCity("Manisa"),
               208));

                graph.AddEdge(new edge(graph.GetCity("Denizli"), graph.GetCity("Muğla"),
               145));
                graph.AddEdge(new edge(graph.GetCity("Denizli"), graph.GetCity("Burdur"),
               150));
                graph.AddEdge(new edge(graph.GetCity("Burdur"), graph.GetCity("Antalya"),
               122));
                graph.AddEdge(new edge(graph.GetCity("Burdur"), graph.GetCity("Isparta"),
               51));
                graph.AddEdge(new edge(graph.GetCity("Burdur"), graph.GetCity("Muğla"),
               241));
                graph.AddEdge(new edge(graph.GetCity("Antalya"), graph.GetCity("Muğla"),
               311));
                graph.AddEdge(new edge(graph.GetCity("Antalya"), graph.GetCity("Isparta"),
               130));
                graph.AddEdge(new edge(graph.GetCity("Aydın"), graph.GetCity("Muğla"), 99));
                graph.AddEdge(new edge(graph.GetCity("Manisa"), graph.GetCity("İzmir"), 35));
                graph.AddEdge(new edge(graph.GetCity("Manisa"), graph.GetCity("Balıkesir"),
               141));
                graph.AddEdge(new edge(graph.GetCity("Manisa"), graph.GetCity("Uşak"), 150));
                graph.AddEdge(new edge(graph.GetCity("İzmir"), graph.GetCity("Aydın"), 126));
                graph.AddEdge(new edge(graph.GetCity("İzmir"), graph.GetCity("Balıkesir"),
               176));
                graph.AddEdge(new edge(graph.GetCity("Balıkesir"),
               graph.GetCity("Çanakkale"), 199));

                Console.WriteLine("Toplam kenar Sayısı: {0}", graph.Countroads());
                Console.WriteLine("Tüm İllerin ortalama Komşuluk Sayısı: {0}",
               graph.avgNeighbors());
                PrintPath(DGraph(graph), graph.GetCity("Çanakkale"), graph.GetCity("İzmir"),
               graph);
                PrintPath(DGraph(graph), graph.GetCity("Muğla"), graph.GetCity("Uşak"),
               graph);
                PrintPath(DGraph(graph), graph.GetCity("Edirne"), graph.GetCity("Antalya"),
               graph);
                PrintPath(DGraph(graph), graph.GetCity("Eskişehir"),
               graph.GetCity("Kırklareli"), graph);
                PrintPath(DGraph(graph), graph.GetCity("İstanbul"), graph.GetCity("Aydın"),
               graph);
            }
        }
    }
}

