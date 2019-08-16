using FakeItEasy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AudioToggle.Models.Tests
{
    public class DefaultDisplayManagerTests
    {
        [Fact]
        public void TestThatPollingCanBeStarted()
        {
            // Assign

            Int32 pollingInterval = 50;
            Boolean pollingStarted = false;
            var pulseGiverMock = A.Fake<IPulseGiver<Int32>>();
            A.CallTo(() => pulseGiverMock.Start(pollingInterval)).Invokes(_ => pollingStarted = true);

            var systemUnderTest = new DefaultDisplayManager(pulseGiverMock, null);

            // Act 

            systemUnderTest.StartPolling();

            // Assert

            Assert.True(pollingStarted);
        }

        [Fact]
        public void TestThatPollingCanBeStopped()
        {
            // Assign

            Boolean pollingStopped = false;
            var pulseGiverMock = A.Fake<IPulseGiver<Int32>>();
            A.CallTo(() => pulseGiverMock.Stop()).Invokes(_ => pollingStopped = true);

            var systemUnderTest = new DefaultDisplayManager(pulseGiverMock, null);

            // Act 

            systemUnderTest.StopPolling();

            // Assert

            Assert.True(pollingStopped);
        }

        [Fact]
        public void TestThatYouCanGetTheCurrentPresentationDisplayMode()
        {
            // Assign

            var expected = PresentationDisplayMode.Clone;
            var presentationDisplayModeReaderMock = A.Fake<IPresentationDisplayModeReader>();
            A.CallTo(() => presentationDisplayModeReaderMock.ReadPresentationDisplayMode()).Returns(expected);

            var systemUnderTest = new DefaultDisplayManager(null, presentationDisplayModeReaderMock);

            // Act

            var actual = systemUnderTest.GetCurrentDisplayMode();

            // Assert

            Assert.Equal(expected, actual.PresentationDisplayMode);
            Assert.Equal(expected.ToString(), actual.DisplayModeName);
        }

        [Fact]
        public void TestThatYouCanGetAllPresentationDisplayModes()
        {
            // Assign

            var expected = Enum.GetValues(typeof(PresentationDisplayMode))
                .Cast<PresentationDisplayMode>()
                .Where(x => x != PresentationDisplayMode.ForceUint32 && x != PresentationDisplayMode.Zero)
                .OrderBy(x => x)
                .Select(x => new DisplayMode(x));            

            var systemUnderTest = new DefaultDisplayManager(null, null);

            // Act

            var actual = systemUnderTest.GetAllDisplayModes();

            // Assert

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void TestThatPresentationDisplayModeGetsPublishedOnPulse()
        {
            // Assign

            DisplayMode actual = null;
            var expected = PresentationDisplayMode.Clone;

            var presentationDisplayModeReaderMock = A.Fake<IPresentationDisplayModeReader>();
            var pulseGiverMock = A.Fake<IPulseGiver<Int32>>();

            A.CallTo(() => presentationDisplayModeReaderMock.ReadPresentationDisplayMode()).Returns(expected);
            
            var systemUnderTest = new DefaultDisplayManager(pulseGiverMock, presentationDisplayModeReaderMock);

            // Act

            systemUnderTest.DisplayModeChanged += mode => actual = mode;
            pulseGiverMock.Pulse += Raise.FreeForm.With();

            // Assert

            Assert.Equal(expected, actual.PresentationDisplayMode);
        }
    }
}
