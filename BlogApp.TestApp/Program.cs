using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Channels;
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
            //await AddBlogs();
            // await GetAllBlogs();
            // await GetAllBlogsWithAllPosts();
            // await FindGoBlog();
            await UpdateGoPost();

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

        private static async Task GetAllBlogs()
        {
            Console.WriteLine("GetAllBlogs() 시작");
            
            await using var dbContext = new BlogContext();
            var blogs = await dbContext.Blogs.ToListAsync();

            Console.WriteLine("가져온 Blog 목록 : ");
            foreach (var blog in blogs)
            {
                Console.Write("Blog : ");
                Console.WriteLine(blog.ToString());
            }
            
            Console.WriteLine("GetAllBlogs() 종료");
        }
        
        private static async Task GetAllBlogsWithAllPosts()
        {
            Console.WriteLine("GetAllBlogsWithAllPosts() 시작");
            
            await using var dbContext = new BlogContext();
            var blogs = await dbContext.Blogs.Include(blog => blog.Posts).ToListAsync();

            Console.WriteLine("가져온 Blog 목록(Post포함) : ");
            foreach (var blog in blogs)
            {
                Console.Write("Blog : ");
                Console.WriteLine(blog.ToString());
                foreach (var post in blog.Posts)
                {
                    Console.Write("-- Post : ");
                    Console.WriteLine(post);
                }
            }
            
            Console.WriteLine("GetAllBlogsWithAllPosts() 종료");
        }
        
        private static async Task FindGoBlog()
        {
            Console.WriteLine("FindGoBlog() 시작");
            
            await using var dbContext = new BlogContext();
            var goBlog
                    = await dbContext.Blogs
                        // Oracle에서는 아래 처럼하면 안됨 -,.-
                        // .Where(blog => blog.Name.Contains("Go"))
                        .Where(blog => EF.Functions.Like(blog.Name, "%Go%"))
                        .FirstOrDefaultAsync()
                ;
            if (goBlog != null)
            {
                Console.WriteLine("Go Blog 를 찾음 : {0}", goBlog.ToString());
            }
            
            Console.WriteLine("FindGoBlog() 종료");
        }

        private static async Task UpdateGoPost()
        {
            Console.WriteLine("UpdateGoPost() 시작");
            
            await using var dbContext = new BlogContext();
            var blogs
                    = await dbContext.Blogs
                        .Where(blog => EF.Functions.Like(blog.Name, "%Go%"))
                        .Include(blog => blog.Posts)
                        .ToListAsync()
                ;
            foreach (var blog in blogs)
            {
                foreach (var goPost in blog.Posts)
                {
                    goPost.Content += " 생소하다 ";
                }
            }

            await dbContext.SaveChangesAsync();
            
            Console.WriteLine("UpdateGoPost() 종료");
        }
    }
}