using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using VotingSystem.Application;
using VotingSystem.Database.Tests.Infrastructure;
using VotingSystem.Models;
using Xunit;
using static Xunit.Assert;

namespace VotingSystem.Database.Tests
{
    public class VotingSystemPersistanceTests
    {
        [Fact]
        public void PersistsVotingPoll()
        {
            var poll = new VotingPoll
            {
                Title = "title",
                Description = "desc",
                Counters = new List<Counter> {
                    new Counter { Name = "One" },
                    new Counter { Name = "Two" }
                }
            };

            using (var ctx = DbContextFactory.Create(nameof(PersistsVotingPoll)))
            {
                IVotingSystemPersistance persistance = new VotingSystemPersistance(ctx);
                persistance.SaveVotingPoll(poll);
            }

            using (var ctx = DbContextFactory.Create(nameof(PersistsVotingPoll)))
            {
                var savedPoll = ctx.VotingPolls
                    .Include(x => x.Counters)
                    .Single();

                Equal(poll.Title, savedPoll.Title);
                Equal(poll.Description, savedPoll.Description);
                Equal(poll.Counters.Count(), savedPoll.Counters.Count());

                foreach (var name in poll.Counters.Select(x => x.Name))
                {
                    Contains(name, savedPoll.Counters.Select(x => x.Name));
                }
            }
        }
    }

    internal class VotingSystemPersistance : IVotingSystemPersistance
    {
        private AppDbContext _ctx;

        public VotingSystemPersistance(AppDbContext ctx)
        {
            _ctx = ctx;
        }

        public void SaveVotingPoll(VotingPoll votingPoll)
        {
            _ctx.VotingPolls.Add(votingPoll);
            _ctx.SaveChanges();
        }
    }
}