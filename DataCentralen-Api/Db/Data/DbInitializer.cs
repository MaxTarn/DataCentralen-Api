using DataCentralen_Api.DbContext;
using DataCentralen_Db.Models.DbModels;

namespace DataCentralen_Api.Db.Data
{
    public static class DbInitializer
    {
        public static void Seed(AppDbContext context)
        {
            // Check if articles exist before adding them
            var existingTitles = context.Articles.Select(a => a.Title).ToList();

            //var articles = new List<Article>
            //{
            //    new Article
            //    {
            //        Title = "Heap Sort",
            //        Author = "Emil Åberg",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>Heap Sort is a comparison-based sorting algorithm...</p>",
            //        Description = "Bygger en heap och extraherar det största/minsta elementet för att sortera listan"

            //    },
            //    new Article
            //    {
            //        Title = "Radix Sort",
            //        Author = "Emil Åberg",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>Radix Sort is a non-comparative integer sorting algorithm...</p>",
            //        Description = "Radix sort grupperar och sorterar siffror från minst signifikant till mest signifikant sifferposition, utan att jämföra element direkt."
            //    },
            //    new Article
            //    {
            //        Title = "Insertion Sort",
            //        Author = "Max Tärn",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>Insertion Sort works by building a sorted list one element at a time...</p>",
            //        Description ="Insertion sort placerar varje element direkt i sin korrekta position i en sorterad lista."

            //    },
            //    new Article
            //    {
            //        Title = "Quicksort",
            //        Author = "Johannes Nilsson",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>Quicksort is a divide-and-conquer algorithm...</p>",
            //        Description= "Dela upp listan vid ett pivot och sortera delarna rekursivt"

            //    },
            //    new Article
            //    {
            //        Title = "Shell Sort",
            //        Author = "Oscar Sommerfors",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>Shell Sort is an optimization of Insertion Sort...</p>",
            //        Description ="Shell sort sorterar genom att först jämföra element med stora mellanrum, som successivt minskas tills listan är helt sorterad."
            //    },
            //    new Article
            //    {
            //        Title = "Counting Sort",
            //        Author = "Oscar Sommerfors",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>Counting Sort is an integer sorting algorithm...</p>"
            //    },

            //    // Data Structures
            //    new Article
            //    {
            //        Title = "Array",
            //        Author = "Johannes Nilsson",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>An array is a linear data structure that stores elements of the same type...</p>"
            //    },
            //    new Article
            //    {
            //        Title = "Linked List",
            //        Author = "Johannes Nilsson",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>A linked list is a linear data structure where elements are stored in nodes...</p>"
            //    },
            //    new Article
            //    {
            //        Title = "Stack",
            //        Author = "Max Tärn",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>A stack is a Last-In-First-Out (LIFO) data structure...</p>"
            //    },
            //    new Article
            //    {
            //        Title = "Queue",
            //        Author = "Max Tärn",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>A queue is a First-In-First-Out (FIFO) data structure...</p>"
            //    },
            //    new Article
            //    {
            //        Title = "Hash Table",
            //        Author = "Emil Åberg",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>A hash table is a data structure that maps keys to values...</p>"
            //    },
            //    new Article
            //    {
            //        Title = "Heap",
            //        Author = "Oscar Sommerfors",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>A heap is a special tree-based data structure...</p>"
            //    },
            //    new Article
            //    {
            //        Title = "Graph",
            //        Author = "Oscar Sommerfors",
            //        Posted = DateTime.UtcNow,
            //        LastEdited = DateTime.UtcNow,
            //        Likes = 0,
            //        Content = "<p>A graph is a collection of nodes connected by edges...</p>"
            //    }
            //};

            var articles = new List<Article>
            {
                // Sorteringsalgoritmer
                new Article
                {
                    Title = "QuickSort",
                    Author = "Johannes Nilsson",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>QuickSort is a divide-and-conquer algorithm that selects a pivot element and sorts the partitions recursively.</p>",
                    Description = "Dela upp listan vid en pivot och sortera delarna rekursivt",
                    Type = "Sorteringsalgoritm",
                    ColorCodeOne = "#F9B66B",
                    ColorCodeTwo = "#F7E6D3"
                },
                new Article
                {
                    Title = "HeapSort",
                    Author = "Oscar Sommerfors",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>HeapSort uses a heap to sort by extracting the largest/smallest element.</p>",
                    Description = "Använder en heap för att sortera genom att extrahera största/minsta elementet.",
                    Type = "Sorteringsalgoritm",
                    ColorCodeOne = "#79ACE4",
                    ColorCodeTwo = "#D3E4F7"
                },
                new Article
                {
                    Title = "BubbleSort",
                    Author = "Max Tärn",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>BubbleSort compares and swaps adjacent elements until the list is sorted.</p>",
                    Description = "Jämför och byter intilliggande element tills listan är sorterad",
                    Type = "Sorteringsalgoritm",
                    ColorCodeOne = "#A6E386",
                    ColorCodeTwo = "#E1EEDA"
                },

                new Article
                {
                    Title = "InsertionSort",
                    Author = "Johannes Nilsson",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>InsertionSort builds the sorted list one item at a time by inserting each element in its correct position.</p>",
                    Description = "Bygger den sorterade listan genom att infoga varje element på rätt plats.",
                    Type = "Sorteringsalgoritm",
                    ColorCodeOne = "#FF6347",
                    ColorCodeTwo = "#FFBCB0"
                },
                new Article
                {
                    Title = "MergeSort",
                    Author = "Emil Åberg",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>MergeSort is a divide-and-conquer algorithm that splits the list in half and then merges the sorted parts.</p>",
                    Description = "Dela och erövra genom att dela listan och sammanfoga de sorterade delarna.",
                    Type = "Sorteringsalgoritm",
                    ColorCodeOne = "#90EE90",
                    ColorCodeTwo = "#D8FAD4"
                },
                new Article
                {
                    Title = "SelectionSort",
                    Author = "Max Tärn",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>SelectionSort selects the smallest element from the unsorted part and swaps it with the first unsorted element.</p>",
                    Description = "Väljer det minsta elementet och byter ut det med det första osorterade elementet.",
                    Type = "Sorteringsalgoritm",
                    ColorCodeOne = "#FFDD57",
                    ColorCodeTwo = "#FFF4A7"
                },
                new Article
                {
                    Title = "Counting Sort",
                    Author = "Oscar Sommerfors",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>Counting Sort counts occurrences of each element to determine their positions in the sorted output.</p>",
                    Description = "Räknar förekomster av varje element för att bygga en sorterad lista utan jämförelser.",
                    Type = "Sorteringsalgoritm",
                    ColorCodeOne = "#FFB347",
                    ColorCodeTwo = "#FFE5CC"
                },
                new Article
                {
                    Title = "Shell Sort",
                    Author = "Oscar Sommerfors",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>Shell Sort sorts elements at specific intervals, reducing the gap each iteration to improve performance.</p>",
                    Description = "Sorterar genom jämförelser med avstånd som minskar gradvis tills listan är sorterad.",
                    Type = "Sorteringsalgoritm",
                    ColorCodeOne = "#B0E0E6",
                    ColorCodeTwo = "#E0F7FA"
                }, 

                // Datastrukturer
                new Article
                {
                    Title = "Array",
                    Author = "Johannes Nilsson",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>An array is a collection of elements stored in an ordered sequence, accessible by index.</p>",
                    Description = "En samling av element lagrade i en ordnad sekvens, åtkomliga via index.",
                    Type = "Datastruktur",
                    ColorCodeOne = "#83EDBB",
                    ColorCodeTwo = "#D3F7E6"
                },
                new Article
                {
                    Title = "Stack",
                    Author = "Max Tärn",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A stack is a LIFO data structure where elements are added and removed from the top.</p>",
                    Description = "Element läggs till och tas bort i en ordning som följer Last In, First Out",
                    Type = "Datastruktur",
                    ColorCodeOne = "#DFB0F6",
                    ColorCodeTwo = "#E6E0E9"
                },
                new Article
                {
                    Title = "Hash Table",
                    Author = "Emil Åberg",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A hash table stores key-value pairs and uses a hash function for fast access.</p>",
                    Description = "Lagrar nyckel-värdepar och använder en hash-funktion för snabb åtkomst.",
                    Type = "Datastruktur",
                    ColorCodeOne = "#F5F886",
                    ColorCodeTwo = "#F6F7D3"
                },
                new Article
                {
                    Title = "Queue",
                    Author = "Johannes Nilsson",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A queue is a FIFO data structure where elements are added at the end and removed from the front.</p>",
                    Description = "En kö är en FIFO-datastruktur där element läggs till i slutet och tas bort från början.",
                    Type = "Datastruktur",
                    ColorCodeOne = "#FFD700",
                    ColorCodeTwo = "#FFF8C6"
                },
                new Article
                {
                    Title = "Linked List",
                    Author = "Max Tärn",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A linked list is a linear data structure where each element points to the next element in the sequence.</p>",
                    Description = "En linjär datastruktur där varje element pekar på nästa element i sekvensen.",
                    Type = "Datastruktur",
                    ColorCodeOne = "#FF8C00",
                    ColorCodeTwo = "#FFE5B3"
                },
                new Article
                {
                    Title = "Tree",
                    Author = "Emil Åberg",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A tree is a hierarchical data structure consisting of nodes with a parent-child relationship.</p>",
                    Description = "En hierarkisk datastruktur bestående av noder med ett föräldra-barn-förhållande.",
                    Type = "Datastruktur",
                    ColorCodeOne = "#8FBC8F",
                    ColorCodeTwo = "#C9E1D4"
                },
                new Article
                {
                    Title = "Graph",
                    Author = "Oscar Sommerfors",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A graph is a structure of nodes connected by edges, used to model relationships and networks.</p>",
                    Description = "Modellerar relationer genom noder och kanter, används inom nätverk och ruttplanering.",
                    Type = "Datastruktur",
                    ColorCodeOne = "#FF7F7F",
                    ColorCodeTwo = "#FFDCDC"
                },
                new Article
                {
                    Title = "Heap",
                    Author = "Oscar Sommerfors",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A heap is a tree-based structure used for priority queues and efficient max/min retrieval.</p>",
                    Description = "Trädbaserad struktur som används för prioritetsköer och effektiv åtkomst till min/max.",
                    Type = "Datastruktur",
                    ColorCodeOne = "#ADD8E6",
                    ColorCodeTwo = "#E3F2FD"
                },
            };


            // Filter out articles that already exist
            var newArticles = articles.Where(a => !existingTitles.Contains(a.Title)).ToList();

            if (newArticles.Any()) // Insert only if new data is present
            {
                context.Articles.AddRange(newArticles);
                context.SaveChanges();
            }
        }
    }
}