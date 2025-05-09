﻿using DataCentralen_Api.DbContext;
using DataCentralen_Db.Models.DbModels;
using Microsoft.EntityFrameworkCore;

namespace DataCentralen_Api.Db.Data;

public static class DbInitializer
{
    public static void Seed(AppDbContext context)
    {
        try
        {
            // Check if articles exist before adding them
            var existingTitles = context.Articles.Select(a => a.Title).ToList();

            var articles = new List<Article>

            {

                //// Sorteringsalgoritmer
                //new Article
                //{
                //    Title = "QuickSort",
                //    Author = "Johannes Nilsson",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>QuickSort is a divide-and-conquer algorithm that selects a pivot element and sorts the partitions recursively.</p>",
                //    Description = "Dela upp listan vid en pivot och sortera delarna rekursivt",
                //    Type = "Sorteringsalgoritm",
                //    ColorCodeOne = "#F9B66B",
                //    ColorCodeTwo = "#F7E6D3"
                //},
                //new Article
                //{
                //    Title = "HeapSort",
                //    Author = "Emil Åberg",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>HeapSort uses a heap to sort by extracting the largest/smallest element.</p>",
                //    Description = "Använder en heap för att sortera genom att extrahera största/minsta elementet.",
                //    Type = "Sorteringsalgoritm",
                //    ColorCodeOne = "#79ACE4",
                //    ColorCodeTwo = "#D3E4F7"
                //},
                //new Article
                //{
                //    Title = "BubbleSort",
                //    Author = "Max Tärn",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>BubbleSort compares and swaps adjacent elements until the list is sorted.</p>",
                //    Description = "Jämför och byter intilliggande element tills listan är sorterad",
                //    Type = "Sorteringsalgoritm",
                //    ColorCodeOne = "#A6E386",
                //    ColorCodeTwo = "#E1EEDA"
                //},
                //new Article
                //{
                //    Title = "InsertionSort",
                //    Author = "Johannes Nilsson",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>InsertionSort builds the sorted list one item at a time by inserting each element in its correct position.</p>",
                //    Description = "Bygger den sorterade listan genom att infoga varje element på rätt plats.",
                //    Type = "Sorteringsalgoritm",
                //    ColorCodeOne = "#FF6347",
                //    ColorCodeTwo = "#FFBCB0"
                //},
                //new Article
                //{
                //    Title = "MergeSort",
                //    Author = "Emil Åberg",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>MergeSort is a divide-and-conquer algorithm that splits the list in half and then merges the sorted parts.</p>",
                //    Description = "Dela och erövra genom att dela listan och sammanfoga de sorterade delarna.",
                //    Type = "Sorteringsalgoritm",
                //    ColorCodeOne = "#90EE90",
                //    ColorCodeTwo = "#D8FAD4"
                //},
                //new Article
                //{
                //    Title = "SelectionSort",
                //    Author = "Max Tärn",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>SelectionSort selects the smallest element from the unsorted part and swaps it with the first unsorted element.</p>",
                //    Description = "Väljer det minsta elementet och byter ut det med det första osorterade elementet.",
                //    Type = "Sorteringsalgoritm",
                //    ColorCodeOne = "#FFDD57",
                //    ColorCodeTwo = "#FFF4A7"
                //},

                //// Datastrukturer
                //new Article
                //{
                //    Title = "Array",
                //    Author = "Johannes Nilsson",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>An array is a collection of elements stored in an ordered sequence, accessible by index.</p>",
                //    Description = "En samling av element lagrade i en ordnad sekvens, åtkomliga via index.",
                //    Type = "Datastruktur",
                //    ColorCodeOne = "#83EDBB",
                //    ColorCodeTwo = "#D3F7E6"
                //},
                //new Article
                //{
                //    Title = "Stack",
                //    Author = "Max Tärn",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>A stack is a LIFO data structure where elements are added and removed from the top.</p>",
                //    Description = "Element läggs till och tas bort i en ordning som följer Last In, First Out",
                //    Type = "Datastruktur",
                //    ColorCodeOne = "#DFB0F6",
                //    ColorCodeTwo = "#E6E0E9"
                //},
                //new Article
                //{
                //    Title = "Hash Table",
                //    Author = "Emil Åberg",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>A hash table stores key-value pairs and uses a hash function for fast access.</p>",
                //    Description = "Lagrar nyckel-värdepar och använder en hash-funktion för snabb åtkomst.",
                //    Type = "Datastruktur",
                //    ColorCodeOne = "#F5F886",
                //    ColorCodeTwo = "#F6F7D3"
                //},
                //new Article
                //{
                //    Title = "Queue",
                //    Author = "Johannes Nilsson",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>A queue is a FIFO data structure where elements are added at the end and removed from the front.</p>",
                //    Description = "En kö är en FIFO-datastruktur där element läggs till i slutet och tas bort från början.",
                //    Type = "Datastruktur",
                //    ColorCodeOne = "#FFD700",
                //    ColorCodeTwo = "#FFF8C6"
                //},
                //new Article
                //{
                //    Title = "Linked List",
                //    Author = "Max Tärn",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>A linked list is a linear data structure where each element points to the next element in the sequence.</p>",
                //    Description = "En linjär datastruktur där varje element pekar på nästa element i sekvensen.",
                //    Type = "Datastruktur",
                //    ColorCodeOne = "#FF8C00",
                //    ColorCodeTwo = "#FFE5B3"
                //},
                //new Article
                //{
                //    Title = "Tree",
                //    Author = "Emil Åberg",
                //    Posted = DateTime.UtcNow,
                //    LastEdited = DateTime.UtcNow,
                //    Likes = 0,
                //    Content = "<p>A tree is a hierarchical data structure consisting of nodes with a parent-child relationship.</p>",
                //    Description = "En hierarkisk datastruktur bestående av noder med ett föräldra-barn-förhållande.",
                //    Type = "Datastruktur",
                //    ColorCodeOne = "#8FBC8F",
                //    ColorCodeTwo = "#C9E1D4"
                //}
            };


            var newArticles = articles.Where(a => !existingTitles.Contains(a.Title)).ToList();

            if (newArticles.Any()) // Insert only if new data is present
            {
                foreach (Article article in newArticles)
                {

                    //get data from list, since both shouldnt be added at the same time
                    ArticleContentModel content = article.ArticleContent!;

                    //add article to the database
                    article.ArticleContent = null;
                    article.ArticleContentId = null;
                    context.Articles.Add(article);
                    context.SaveChanges();



                    //add article content to the database
                    content.ArticleId = article.Id;
                    context.ArticleContents.Add(content);
                    context.SaveChanges();

                    //correct the foreign key relation
                    article.ArticleContentId = content.Id;

                    //save it to db
                    context.SaveChanges();
                }
            }

            if (!context.Users.Any())
            {
                var adminUser = new AppUser
                {
                    UserName = "admin",
                    IsAdmin = true,
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
                };

                context.Users.Add(adminUser);
                context.SaveChanges();
            }


        }
        catch
        {

            throw;
        }
    }
}