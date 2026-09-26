using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        // Create the first video.
        Video video1 = new Video(
            "Introduction to Software Development",
            "Tech Learning Hub",
            420);

        video1.AddComment(new Comment(
            "Daniel",
            "This was a very helpful introduction to software development."));

        video1.AddComment(new Comment(
            "Sarah",
            "I learned a lot from this video."));

        video1.AddComment(new Comment(
            "Michael",
            "The explanation was simple and easy to understand."));

        video1.AddComment(new Comment(
            "Grace",
            "I am looking forward to learning more."));

        // Create the second video.
        Video video2 = new Video(
            "Understanding Object-Oriented Programming",
            "Code Academy",
            615);

        video2.AddComment(new Comment(
            "James",
            "The explanation of classes was excellent."));

        video2.AddComment(new Comment(
            "Emily",
            "This helped me understand objects much better."));

        video2.AddComment(new Comment(
            "David",
            "I will practice these concepts in my own programs."));

        video2.AddComment(new Comment(
            "Rachel",
            "Very clear explanation of object-oriented programming."));

        // Create the third video.
        Video video3 = new Video(
            "Learning C# Programming",
            "Programming Basics",
            510);

        video3.AddComment(new Comment(
            "John",
            "C# is becoming much easier to understand."));

        video3.AddComment(new Comment(
            "Jessica",
            "The examples made the lesson easier to follow."));

        video3.AddComment(new Comment(
            "Samuel",
            "I enjoyed this programming lesson."));

        video3.AddComment(new Comment(
            "Linda",
            "This video gave me some useful programming ideas."));

        // Store all videos in a list.
        List<Video> videos = new List<Video>
        {
            video1,
            video2,
            video3
        };

        // Display information for each video.
        foreach (Video video in videos)
        {
            Console.WriteLine($"Title: {video.GetTitle()}");
            Console.WriteLine($"Author: {video.GetAuthor()}");
            Console.WriteLine($"Length: {video.GetLength()} seconds");
            Console.WriteLine($"Number of comments: {video.GetCommentCount()}");
            Console.WriteLine("Comments:");

            foreach (Comment comment in video.GetComments())
            {
                Console.WriteLine(
                    $"- {comment.GetName()}: {comment.GetText()}");
            }

            Console.WriteLine();
        }
    }
}