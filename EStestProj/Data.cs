using System;
using System.Collections.Generic;

namespace EStestProj
{
    public class Data
    {
        public static List<Post> Posts => new List<Post>()
        {
            new Post()
            {
                Id = 1,
                Author = "Yariki",
                Content = "Bla",
                Comments = new List<Comment>() 
                {
                    new Comment()
                    {
                        Id = 1,
                        Content = "sdlfjsdf",
                        Author = "Sveta"
                    }
                }
            },
            new Post()
            {
                Id = 2,
                Author = "Sveta",
                Content = "sdgfksogldsjsjd",
                Comments = new List<Comment>() 
                {
                    new Comment()
                    {
                        Id = 2,
                        Content = "sdlfjsdf",
                        Author = "Yariki"
                    }
                }
            }
        };    

        public static List<Author> Authors => new List<Author>()
        {
            new Author()
            {
                Id = 1,
                FirstName = "Yariki",
                LastName = "Lisovskyi"
            },
            new Author()
            {
                Id = 2,
                FirstName = "Sveta",
                LastName = "Lisovska"
            }
        };

    }
}