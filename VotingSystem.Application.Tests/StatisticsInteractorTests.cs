using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VotingSystem.Models;
using Xunit;

namespace VotingSystem.Application.Tests
{
    public class StatisticsInteractorTests
    {
        private readonly Mock<IVotingSystemPersistance> _mockPersistance = new Mock<IVotingSystemPersistance>();

        [Fact]
        public void DisplaysPollStatistics()
        {
            var pollId = 1;
            var poll = new VotingPoll
            {
                Title = "title",
                Description = "desc",
                Counters = new List<Counter>
                {
                    new Counter {Name = "One"},
                    new Counter {Name = "Two"}
                }
            };
            _mockPersistance.Setup(x => x.GetPoll(pollId)).Returns(poll);

            var interactor = new StatisticsInteractor(_mockPersistance.Object);

            var pollStatistics = interactor.GetStatistics(pollId);
        }
    }

    internal class StatisticsInteractor
    {
        private readonly IVotingSystemPersistance _persistance;

        public StatisticsInteractor(IVotingSystemPersistance persistance)
        {
            _persistance = persistance;
        }

        internal object GetStatistics(int pollId)
        {
            throw new NotImplementedException();
        }
    }
}