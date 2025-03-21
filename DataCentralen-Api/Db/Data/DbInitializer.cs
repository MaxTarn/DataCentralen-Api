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

            var articles = new List<Article>
            {
                new Article
                {
                    Title = "Heap Sort",
                    Author = "Emil Åberg",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>Heap Sort is a comparison-based sorting algorithm...</p>",
                    Description = "Bygger en heap och extraherar det största/minsta elementet för att sortera listan"

                },
                new Article
                {
                    Title = "Radix Sort",
                    Author = "Emil Åberg",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>Radix Sort is a non-comparative integer sorting algorithm...</p>",
                    Description = "Radix sort grupperar och sorterar siffror från minst signifikant till mest signifikant sifferposition, utan att jämföra element direkt."
                },
                new Article
                {
                    Title = "Insertion Sort",
                    Author = "Max Tärn",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>Insertion Sort works by building a sorted list one element at a time...</p>",
                    Description ="Insertion sort placerar varje element direkt i sin korrekta position i en sorterad lista."

                },
                new Article
                {
                    Title = "Quicksort",
                    Author = "Johannes Nilsson",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>Quicksort is a divide-and-conquer algorithm...</p>",
                    Description= "Dela upp listan vid ett pivot och sortera delarna rekursivt"

                },
                new Article
                {
                    Title = "Shell Sort",
                    Author = "Oscar Sommerfors",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>Shell Sort is an optimization of Insertion Sort...</p>",
                    Description ="Shell sort sorterar genom att först jämföra element med stora mellanrum, som successivt minskas tills listan är helt sorterad."
                },
                new Article
                {
                    Title = "Counting Sort",
                    Author = "Oscar Sommerfors",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>Counting Sort is an integer sorting algorithm...</p>"
                },

                // Data Structures
                new Article
                {
                    Title = "Array",
                    Author = "Johannes Nilsson",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>An array is a linear data structure that stores elements of the same type...</p>"
                },
                new Article
                {
                    Title = "Linked List",
                    Author = "Johannes Nilsson",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A linked list is a linear data structure where elements are stored in nodes...</p>"
                },
                new Article
                {
                    Title = "Stack",
                    Author = "Max Tärn",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A stack is a Last-In-First-Out (LIFO) data structure...</p>"
                },
                new Article
                {
                    Title = "Queue",
                    Author = "Max Tärn",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A queue is a First-In-First-Out (FIFO) data structure...</p>"
                },
                new Article
                {
                    Title = "Hash Table",
                    Author = "Emil Åberg",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A hash table is a data structure that maps keys to values...</p>"
                },
                new Article
                {
                    Title = "Heap",
                    Author = "Oscar Sommerfors",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A heap is a special tree-based data structure...</p>"
                },
                new Article
                {
                    Title = "Graph",
                    Author = "Oscar Sommerfors",
                    Posted = DateTime.UtcNow,
                    LastEdited = DateTime.UtcNow,
                    Likes = 0,
                    Content = "<p>A graph is a collection of nodes connected by edges...</p>"
                }
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