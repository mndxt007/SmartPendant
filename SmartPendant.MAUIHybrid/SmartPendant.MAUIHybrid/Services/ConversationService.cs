﻿using SmartPendant.MAUIHybrid.Models;

namespace SmartPendant.MAUIHybrid.Services
{
    /// <summary>
    /// Provides dummy conversation objects with simulated data for development and testing purposes.
    /// </summary>
    public class ConversationService
    {
        private readonly List<Conversation> _conversations;

        public ConversationService()
        {
            _conversations = GetSampleConversations();
        }

        /// <summary>
        /// Retrieves a single dummy conversation with comprehensive data filled in.
        /// </summary>
        public Task<Conversation> GetSampleConversationAsync()
        {
            return Task.FromResult(_conversations.First());
        }

        /// <summary>
        /// Retrieves all available conversations.
        /// </summary>
        public Task<List<Conversation>> GetAllConversationsAsync()
        {
            return Task.FromResult(_conversations);
        }

        /// <summary>
        /// Retrieves all action items across conversations.
        /// </summary>
        public Task<List<ActionItem>> GetAllTasksAsync()
        {
            var allTasks = _conversations
                .Where(c => c.AiInsights?.ActionItems != null)
                .SelectMany(c => c.AiInsights!.ActionItems!)
                .ToList();

            return Task.FromResult(allTasks);
        }

        /// <summary>
        /// Retrieves conversations that occurred on a specific date.
        /// </summary>
        public Task<List<Conversation>> GetConversationsByDateAsync(DateTime date)
        {
            var matches = _conversations
                .Where(c => c.CreatedAt.Date == date.Date)
                .ToList();

            return Task.FromResult(matches);
        }

        /// <summary>
        /// Retrieves tasks assigned to a specific person.
        /// </summary>
        public Task<List<ActionItem>> GetTasksByAssigneeAsync(string assignee)
        {
            var tasks = _conversations
                .Where(c => c.AiInsights?.ActionItems != null)
                .SelectMany(c => c.AiInsights!.ActionItems!)
                .Where(a => string.Equals(a.Assignee, assignee, StringComparison.OrdinalIgnoreCase))
                .ToList();

            return Task.FromResult(tasks);
        }

        /// <summary>
        /// Retrieves conversations containing a specific topic.
        /// </summary>
        public Task<List<Conversation>> GetConversationsByTopicAsync(string topic)
        {
            var results = _conversations
                .Where(c => c.AiInsights?.Topics != null &&
                            c.AiInsights.Topics
                            .Any(t => string.Equals(t, topic, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            return Task.FromResult(results);
        }

        /// <summary>
        /// Provides the dummy sample conversation.
        /// </summary>
        public List<Conversation> GetSampleConversations()
        {
            var conversations = new List<Conversation>();

            // Conversation 1: Project Atlas Launch
            var conversation1Id = Guid.NewGuid();
            conversations.Add(new Conversation
            {
                Id = conversation1Id,
                UserId = "user_123",
                Title = "Team Sync: Project Atlas Launch",
                CreatedAt = DateTime.Now.AddDays(-2),
                DurationMinutes = 45,
                Location = "Conference Room A",
                Summary = "The team discussed the upcoming launch of Project Atlas, finalizing the checklist and deployment strategy.",
                Transcript = new List<TranscriptEntry>
            {
                new() { SpeakerId = "alice", SpeakerLabel = "Alice", Text = "Let's finalize the launch checklist today.", Initials = "AL", Timestamp = DateTime.Now.AddDays(-2).AddMinutes(1) },
                new() { SpeakerId = "bob", SpeakerLabel = "Bob", Text = "We also need to confirm deployment slots.", Initials = "BO", Timestamp = DateTime.Now.AddDays(-2).AddMinutes(3) },
                new() { SpeakerId = "vikash", SpeakerLabel = "You", Text = "I’ll handle communication with the ops team.", Initials = "VK", Timestamp = DateTime.Now.AddDays(-2).AddMinutes(5) },
                new() { SpeakerId = "alice", SpeakerLabel = "Alice", Text = "Great, I'll update the status document by end of day.", Initials = "AL", Timestamp = DateTime.Now.AddDays(-2).AddMinutes(10) }
            },
                AiInsights = new AiInsights
                {
                    Topics = new List<string> { "Project Launch", "Deployment Strategy", "Team Coordination" },
                    ActionItems = new List<ActionItem>
                {
                    new() { ConversationId = conversation1Id, ConversationTitle = "Team Sync: Project Atlas Launch", Task = "Finalize launch checklist", Assignee = "Alice", DueDate = "2025-06-15" },
                    new() { ConversationId = conversation1Id, ConversationTitle = "Team Sync: Project Atlas Launch", Task = "Coordinate with ops for deployment", Assignee = "Vikash", DueDate = "2025-06-14" },
                    new() { ConversationId = conversation1Id, ConversationTitle = "Team Sync: Project Atlas Launch", Task = "Confirm deployment slots", Assignee = "Bob", DueDate = "2025-06-13" }
                }
                },
                Timeline = new List<TimelineEvent>
            {
                new() { Timestamp = "00:02", Description = "Checklist discussion started" },
                new() { Timestamp = "00:08", Description = "Deployment responsibilities assigned" },
                new() { Timestamp = "00:15", Description = "Ops communication planned" },
                new() { Timestamp = "00:20", Description = "Status document update discussed" }
            }
            });

            // Conversation 2: Q3 Marketing Campaign Brainstorm
            var conversation2Id = Guid.NewGuid();
            conversations.Add(new Conversation
            {
                Id = conversation2Id,
                UserId = "user_456",
                Title = "Q3 Marketing Campaign Brainstorm",
                CreatedAt = DateTime.Now.AddDays(-5),
                DurationMinutes = 60,
                Location = "Online Meeting (Zoom)",
                Summary = "Brainstorming session for the Q3 marketing campaign, focusing on new ad channels and content ideas.",
                Transcript = new List<TranscriptEntry>
            {
                new() { SpeakerId = "charlie", SpeakerLabel = "Charlie", Text = "What new ad channels should we explore for Q3?", Initials = "CH", Timestamp = DateTime.Now.AddDays(-5).AddMinutes(2) },
                new() { SpeakerId = "diana", SpeakerLabel = "Diana", Text = "I think TikTok and Instagram Reels have great potential.", Initials = "DI", Timestamp = DateTime.Now.AddDays(-5).AddMinutes(5) },
                new() { SpeakerId = "vikash", SpeakerLabel = "You", Text = "We should also consider a series of short video testimonials.", Initials = "VK", Timestamp = DateTime.Now.AddDays(-5).AddMinutes(8) },
                new() { SpeakerId = "charlie", SpeakerLabel = "Charlie", Text = "Good idea, I'll research some tools for video creation.", Initials = "CH", Timestamp = DateTime.Now.AddDays(-5).AddMinutes(15) }
            },
                AiInsights = new AiInsights
                {
                    Topics = new List<string> { "Marketing Strategy", "Ad Channels", "Content Creation", "Video Marketing" },
                    ActionItems = new List<ActionItem>
                {
                    new() { ConversationId = conversation2Id, ConversationTitle = "Q3 Marketing Campaign Brainstorm", Task = "Research new ad channels", Assignee = "Charlie", DueDate = "2025-06-18" },
                    new() { ConversationId = conversation2Id, ConversationTitle = "Q3 Marketing Campaign Brainstorm", Task = "Draft video testimonial concept", Assignee = "You", DueDate = "2025-06-20" }
                }
                },
                Timeline = new List<TimelineEvent>
            {
                new() { Timestamp = "00:03", Description = "Discussion on new ad channels" },
                new() { Timestamp = "00:09", Description = "Video testimonial idea proposed" },
                new() { Timestamp = "00:16", Description = "Action item: research video tools" }
            }
            });

            // Conversation 3: Weekly Product Development Standup
            var conversation3Id = Guid.NewGuid();
            conversations.Add(new Conversation
            {
                Id = conversation3Id,
                UserId = "user_789",
                Title = "Weekly Product Development Standup",
                CreatedAt = DateTime.Now.AddDays(-1),
                DurationMinutes = 30,
                Location = "Dev Team Office",
                Summary = "Quick standup covering sprint progress, blockers, and upcoming tasks for the product development team.",
                Transcript = new List<TranscriptEntry>
            {
                new() { SpeakerId = "eve", SpeakerLabel = "Eve", Text = "I've completed the user authentication module.", Initials = "EV", Timestamp = DateTime.Now.AddDays(-1).AddMinutes(1) },
                new() { SpeakerId = "frank", SpeakerLabel = "Frank", Text = "Still working on the backend API integration, facing a minor dependency issue.", Initials = "FR", Timestamp = DateTime.Now.AddDays(-1).AddMinutes(3) },
                new() { SpeakerId = "vikash", SpeakerLabel = "You", Text = "I can help with the dependency issue after my current task.", Initials = "VK", Timestamp = DateTime.Now.AddDays(-1).AddMinutes(5) },
                new() { SpeakerId = "eve", SpeakerLabel = "Eve", Text = "Thanks, Vikash! That would be great.", Initials = "EV", Timestamp = DateTime.Now.AddDays(-1).AddMinutes(6) }
            },
                AiInsights = new AiInsights
                {
                    Topics = new List<string> { "Sprint Progress", "Blockers", "API Integration", "User Authentication" },
                    ActionItems = new List<ActionItem>
                {
                    new() { ConversationId = conversation3Id, ConversationTitle = "Weekly Product Development Standup", Task = "Assist Frank with dependency issue", Assignee = "You", DueDate = "2025-06-14" },
                    new() { ConversationId = conversation3Id, ConversationTitle = "Weekly Product Development Standup", Task = "Review user authentication module", Assignee = "Frank", DueDate = "2025-06-16" }
                }
                },
                Timeline = new List<TimelineEvent>
            {
                new() { Timestamp = "00:01", Description = "User authentication update" },
                new() { Timestamp = "00:04", Description = "Backend API blocker discussed" },
                new() { Timestamp = "00:05", Description = "Offer to assist with dependency" }
            }
            });

            // Conversation 4: Client Feedback Session - New Feature
            var conversation4Id = Guid.NewGuid();
            conversations.Add(new Conversation
            {
                Id = conversation4Id,
                UserId = "user_101",
                Title = "Client Feedback Session - New Feature",
                CreatedAt = DateTime.Now.AddDays(-7),
                DurationMinutes = 90,
                Location = "Client Site",
                Summary = "Gathering feedback from the client on the newly implemented reporting feature.",
                Transcript = new List<TranscriptEntry>
            {
                new() { SpeakerId = "grace", SpeakerLabel = "Grace (Client)", Text = "We really like the new dashboard, especially the customizable reports.", Initials = "GR", Timestamp = DateTime.Now.AddDays(-7).AddMinutes(2) },
                new() { SpeakerId = "vikash", SpeakerLabel = "You", Text = "That's great to hear! Are there any specific improvements you'd suggest?", Initials = "VK", Timestamp = DateTime.Now.AddDays(-7).AddMinutes(5) },
                new() { SpeakerId = "henry", SpeakerLabel = "Henry (Client)", Text = "A way to export reports directly to PDF would be very helpful.", Initials = "HE", Timestamp = DateTime.Now.AddDays(-7).AddMinutes(10) },
                new() { SpeakerId = "grace", SpeakerLabel = "Grace (Client)", Text = "And perhaps more filtering options for date ranges.", Initials = "GR", Timestamp = DateTime.Now.AddDays(-7).AddMinutes(12) }
            },
                AiInsights = new AiInsights
                {
                    Topics = new List<string> { "Client Feedback", "New Features", "Reporting", "Export Functionality" },
                    ActionItems = new List<ActionItem>
                {
                    new() { ConversationId = conversation4Id, ConversationTitle = "Client Feedback Session - New Feature", Task = "Investigate PDF export for reports", Assignee = "You", DueDate = "2025-06-21" },
                    new() { ConversationId = conversation4Id, ConversationTitle = "Client Feedback Session - New Feature", Task = "Add more date range filtering options", Assignee = "Development Team", DueDate = "2025-06-28" }
                }
                },
                Timeline = new List<TimelineEvent>
            {
                new() { Timestamp = "00:03", Description = "Positive feedback on dashboard" },
                new() { Timestamp = "00:11", Description = "Feature request: PDF export" },
                new() { Timestamp = "00:13", Description = "Feature request: improved date filters" }
            }
            });

            // Conversation 5: HR Onboarding Session
            var conversation5Id = Guid.NewGuid();
            conversations.Add(new Conversation
            {
                Id = conversation5Id,
                UserId = "user_222",
                Title = "HR Onboarding Session",
                CreatedAt = DateTime.Now.AddMonths(-1),
                DurationMinutes = 120,
                Location = "HR Department",
                Summary = "Onboarding session for new employees, covering company policies, benefits, and payroll.",
                Transcript = new List<TranscriptEntry>
            {
                new() { SpeakerId = "irene", SpeakerLabel = "Irene (HR)", Text = "Welcome to the team! Today we'll review essential company policies.", Initials = "IR", Timestamp = DateTime.Now.AddMonths(-1).AddMinutes(2) },
                new() { SpeakerId = "john", SpeakerLabel = "John (New Hire)", Text = "Could you clarify the vacation policy?", Initials = "JO", Timestamp = DateTime.Now.AddMonths(-1).AddMinutes(15) },
                new() { SpeakerId = "vikash", SpeakerLabel = "You", Text = "I'll send out a detailed FAQ document regarding benefits and time off.", Initials = "VK", Timestamp = DateTime.Now.AddMonths(-1).AddMinutes(20) },
                new() { SpeakerId = "irene", SpeakerLabel = "Irene (HR)", Text = "Perfect, Vikash. And please remember to complete your W-4 forms by Friday.", Initials = "IR", Timestamp = DateTime.Now.AddMonths(-1).AddMinutes(30) }
            },
                AiInsights = new AiInsights
                {
                    Topics = new List<string> { "HR Onboarding", "Company Policies", "Employee Benefits", "Payroll" },
                    ActionItems = new List<ActionItem>
                {
                    new() { ConversationId = conversation5Id, ConversationTitle = "HR Onboarding Session", Task = "Send benefits and time off FAQ", Assignee = "You", DueDate = "2025-06-14" },
                    new() { ConversationId = conversation5Id, ConversationTitle = "HR Onboarding Session", Task = "Complete W-4 forms", Assignee = "New Hires", DueDate = "2025-06-13" }
                }
                },
                Timeline = new List<TimelineEvent>
            {
                new() { Timestamp = "00:03", Description = "Welcome and policy overview" },
                new() { Timestamp = "00:16", Description = "Vacation policy question" },
                new() { Timestamp = "00:21", Description = "Action item: send FAQ document" },
                new() { Timestamp = "00:31", Description = "Reminder: W-4 forms" }
            }
            });

            return conversations;
        }
    }
}
