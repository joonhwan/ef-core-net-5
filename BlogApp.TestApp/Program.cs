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
            // await UpdateGoPost();
            await ChangeVariousEntities();

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
        
        private static async Task ChangeVariousEntities()
        {
            Console.WriteLine("ChangeVariousEntities() 시작");
            
            await using var dbContext = new BlogContext();
            var blogs = await dbContext.Blogs
                    .Include(blog => blog.Posts)
                    .ToListAsync()
                ;
            
            foreach (var blog in blogs)
            {
                if (blog.Name.Contains("C#"))
                {
                    // C# 블로그에 Post 추가
                    blog.Posts.Add(new Post
                    {
                        Title = "참 쉽조잉",
                        Content = "하지만 얕보다가는 큰 코 닥침"
                    });
                }
                else if (blog.Name.Contains("Python"))
                {
                    // Python 블로그의 제목 변경
                    blog.Name += " - AI 에서 최고";
                }
                else if (blog.Name.Contains("Go"))
                {
                    // Go 블로그의 마지막 Post 제거
                    var lastPost = blog.Posts.LastOrDefault();
                    if (lastPost != null)
                    {
                        // 아래 처럼 한다고 삭제되지는 않는다.
                        // --> blog 자체는 변경추적중이지만,
                        //     blog의 Navigation속성인 Post의 항목은 Blog 내에서 추적중이 아니기 때문
                        //blog.Posts.Remove(lastPost);

                        dbContext.Remove(lastPost);
                    }
                }
            }

            await dbContext.SaveChangesAsync();
            
            Console.WriteLine("ChangeVariousEntities() 종료");
        }
    }
}