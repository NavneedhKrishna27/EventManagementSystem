﻿using EventManagementSystem_Merged_.Repos;
using EventManagementSystemMerged.Data;
using EventManagementSystemMerged.Models;
using EventManagementSystemMerged.Repos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace EventManagementAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        [Authorize]
        [HttpGet]
        public IActionResult GetFeedbacks()
        {
            FeedbackService feedbackService = new FeedbackService(new AppDbContext());
            var feedbacks = feedbackService.ViewFeedbacks();
            return Ok(feedbacks);
        }

        [Authorize(Policy = "UserOnly")]
        [HttpPost]
        public IActionResult AddFeedback([FromBody] Feedback feedback)
        {
            FeedbackService feedbackService = new FeedbackService(new AppDbContext());
            feedbackService.addFeedback(feedback);
            return Ok(new { message = "Feedback added successfully" });
        }

        [Authorize(Policy = "UserOnly")]
        [HttpGet("average-rating/{eventId}")]
        public IActionResult GetAverageRating(int eventId)
        {
            FeedbackService feedbackService = new FeedbackService(new AppDbContext());
            var averageRating = feedbackService.GetAverageRating(eventId);
            return Ok(new { averageRating });
        }

        [Authorize]
        [HttpGet("rating-counts/{eventId}")]
        public IActionResult GetRatingCounts(int eventId)
        {
            FeedbackService feedbackService = new FeedbackService(new AppDbContext());
            var ratingCounts = feedbackService.GetRatingCounts(eventId);
            return Ok(ratingCounts);
        }
    }
}
