using System;
using FluentAssertions;
using NUnit.Framework;
using NzbDrone.Core.Organizer;
using NzbDrone.Core.Test.Framework;
using NzbDrone.Core.Tv;

namespace NzbDrone.Core.Test.OrganizerTests
{
    [TestFixture]
    public class GetSeasonFolderFixture : CoreTest<FileNameBuilder>
    {
        private NamingConfig namingConfig;

        [SetUp]
        public void Setup()
        {
            namingConfig = new NamingConfig();

            Mocker.GetMock<INamingConfigService>()
                  .Setup(c => c.GetConfig()).Returns(namingConfig);
        }

        [TestCase("Venture Bros.", 1, "{Series.Title}.{season:00}", "Venture.Bros.01")]
        [TestCase("Venture Bros.", 1, "{Series Title} Season {season:00}", "Venture Bros. Season 01")]
        public void should_use_seriesFolderFormat_to_build_folder_name(String seriesTitle, Int32 seasonNumber, String format, String expected)
        {
            namingConfig.SeasonFolderFormat = format;

            var series = new Series { Title = seriesTitle };

            Subject.GetSeasonFolder(series, seasonNumber, namingConfig).Should().Be(expected);
        }
    }
}