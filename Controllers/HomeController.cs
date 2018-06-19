using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


using LPChallenge.Models;

namespace LPChallenge.Controllers
{
    public class HomeController : Controller
    {
        private LPChallengeContext _context;

        public HomeController(LPChallengeContext context){
            _context = context;
        }

        // Loading 'login' page
        [HttpGet]
        [Route("/")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [Route("/")]
        public IActionResult Login(string password)
        {
            if(password == "123")
            {
                // Creating a new Border instance once user 'logs in'
                Border newBorder = new Border{};
                _context.Borders.Add(newBorder);
                _context.SaveChanges();
                HttpContext.Session.SetInt32("BorderId", newBorder.Id);

                // Directing user to Segment creation
                return RedirectToAction("SegmentCreator");
            }
            else
            {
                return RedirectToAction("Index");
            }
        }
        
        // Loading our Segment creation form
        [HttpGet]
        [Route("segment-creator")]
        public IActionResult SegmentCreator()
        {
            return View();
        }

        [HttpPost]
        [Route("segment-creator")]
        public IActionResult Create(SegmentViewModel segment)
        {
            // If the submitted Segment form is valid
            if(ModelState.IsValid)
            {
                // Creating the new Segment instance
                Segment newSegment = new Segment
                {
                    distance = segment.distance,
                    feature = segment.feature
                };
                // Tying the Segment to the current Border via Border Id
                newSegment.BorderId = HttpContext.Session.GetInt32("BorderId");
                _context.Segments.Add(newSegment);
                _context.SaveChanges();


                // Staying on Segment creation page to allow for additional segments - "border product consists of 1 to n steps"
                return RedirectToAction("SegmentCreator");
            }
            else
            {
                return View("SegmentCreator", segment);
            }
        }

        // Making sure the Border has at least one Segment
        [HttpPost]
        [Route("segment-test")]
        public IActionResult Test()
        {
            int? BorderNumber = HttpContext.Session.GetInt32("BorderId");

            // If the current Border has no Segments
            if(! _context.Segments.Where(s => s.BorderId == BorderNumber).Any())
            {
                // Return to Segment creation form - a Border must have at least 1 Segment
                return RedirectToAction("SegmentCreator");
            }
            // If the current Border has at least one Segment
            else
            {
                // Display the finished Border product
                return RedirectToAction("ShowBorder");
            }
        }

        [HttpGet]
        [Route("show-border")]
        public IActionResult ShowBorder()
        {
            int? BorderNumber = HttpContext.Session.GetInt32("BorderId");

            // Grabbing collection of Segments tied to current Border product
            IQueryable<Segment> Segments = _context.Segments.Where(s => s.BorderId == BorderNumber);
            
            // Passing our data to the front end
            ViewBag.Segments = Segments;

            // Displaying our final Border product
            return View();
        }
    }
}
