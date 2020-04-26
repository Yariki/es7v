using System;
using System.Collections.Generic;

namespace EStestProj
{
    public class Entity
    {
        public int Id { get; set; }
    }


    public class Post : Entity
    {
        public string Content { get; set; }        

        public DateTime Created { get; set; }

        public string Author { get; set; }

        public List<Comment> Comments { get; set; }
    }


    public class Comment : Entity
    {

        public string Content { get; set; } 

        public string Author { get; set; }
    }


    public class Author :Entity
    {

        public string FirstName { get; set; }

        public string LastName { get; set; }

    }


}