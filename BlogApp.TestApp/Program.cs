using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BlogApp.DataAccess;
using BlogApp.Domain;
using Microsoft.EntityFrameworkCore;

namespace BlogApp.TestApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            await AddBlogs();
            
            Console.WriteLine("테스트 종료");
        }

        private static async Task AddBlogs()
        {
            await using var dbContext = new BlogContext();
            await dbContext.Blogs.AddRangeAsync(new[]
            {
                new Blog
                {
                    Name = "Go Blog",
                    Url = "http://www.godev.org",
                    Posts = new List<Post>
                    {
                        new Post
                        {
                            Title = "시작하기",
                            Content = "Go 를 시작합니다"
                        },
                        new Post
                        {
                            Title = "설치하기",
                            Content = "Go 를 설치해 봅니다"
                        },
                        new Post
                        {
                            Title = "모듈 만들기",
                            Content = "Go 모듈을 만들어 봅니다 "
                        }
                    }
                },
                new Blog
                {
                    Name = "C# Blog",
                    Url = "http://www.csharpdev.org",
                },
                new Blog
                {
                    Name = "Python Blog",
                    Url = "http://www.pydev.org"
                }
            });
            await dbContext.SaveChangesAsync();
        }
    }
}