#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using backend.Resources;

namespace backend.Controllers
{
    [Route("api/v1/posts")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Posts
        [Route("")]
        [IgnoreAntiforgeryToken]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> GetPost([FromQuery(Name = "sort")] string sort = null)
        {
            var userName = "";
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            }

            var posts = new List<Post>();

            switch (sort)
            {
                case "new": posts = await _context.Post.OrderByDescending(post => post.Created).Take(50).ToListAsync(); break;
                default: posts = await _context.Post.OrderByDescending(post => post.Rating).Take(50).ToListAsync(); break;
            }


            var publicPosts = new List<dynamic>();

            foreach (var post in posts)
            {
                var userVote = isAuthenticated ? _context.Vote.Where(vote => vote.UserFK == userName && vote.PostFK == post.Id).FirstOrDefault() : null;

                publicPosts.Add(
                new
                {
                    Id = post.Id,
                    Ticker = post.Ticker,
                    Rating = post.Rating,
                    Analysis = post.Analysis,
                    UserRating = (userVote != null ? userVote.Rating : 0)
                });
                
            }

            return publicPosts;
        }

        // GET: api/Posts
        [Route("search")]
        [IgnoreAntiforgeryToken]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<dynamic>>> SearchPosts([FromQuery(Name = "q")] string query, [FromQuery(Name = "sort")] string sort = null)
        {
            var userName = "";
            bool isAuthenticated = User.Identity.IsAuthenticated;
            if (isAuthenticated)
            {
                userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            }

            var posts = new List<Post>();

            switch (sort)
            {
                case "new": posts = await _context.Post.Where(post => post.Ticker.ToLower().Contains(query.ToLower()) || post.Analysis.ToLower().Contains(query.ToLower())).OrderByDescending(post => post.Created).Take(50).ToListAsync(); break;
                default: posts = await _context.Post.Where(post => post.Ticker.ToLower().Contains(query.ToLower()) || post.Analysis.ToLower().Contains(query.ToLower())).OrderByDescending(post => post.Rating).Take(50).ToListAsync(); break;
            }

            var publicPosts = new List<dynamic>();

            foreach (var post in posts)
            {
                var userVote = isAuthenticated ? _context.Vote.Where(vote => vote.UserFK == userName && vote.PostFK == post.Id).FirstOrDefault() : null;

                publicPosts.Add(
                new
                {
                    Id = post.Id,
                    Ticker = post.Ticker,
                    Rating = post.Rating,
                    Analysis = post.Analysis,
                    UserRating = (userVote != null ? userVote.Rating : 0)
                });

            }

            return publicPosts;

        }

        // GET: api/Posts/5
        [IgnoreAntiforgeryToken]
        [HttpGet("{id}")]
        public async Task<ActionResult<Post>> GetPost(Guid id)
        {
            var post = await _context.Post.FindAsync(id);

            if (post == null)
            {
                return NotFound();
            }

            return post;
        }

        // PUT: api/Posts/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPost(Guid id, Post post)
        {
            if (id != post.Id)
            {
                return BadRequest();
            }

            _context.Entry(post).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Posts
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [Authorize]
        [HttpPost]
        public async Task<ActionResult<Post>> PostPost([FromBody]PostResource postResource)
        {
            var userName = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var post = new Post()
            {
                Ticker = postResource.Ticker,
                Analysis = postResource.Analysis,
                Rating = 0,
                UserFK = userName,
                Created = DateTime.UtcNow,
                Updated = DateTime.UtcNow,
            };

            _context.Post.Add(post);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPost", new { id = post.Id }, post);
        }

        // DELETE: api/Posts/5
        [Authorize]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePost(Guid id)
        {
            var post = await _context.Post.FindAsync(id);
            if (post == null)
            {
                return NotFound();
            }

            _context.Post.Remove(post);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PostExists(Guid id)
        {
            return _context.Post.Any(e => e.Id == id);
        }
    }
}
