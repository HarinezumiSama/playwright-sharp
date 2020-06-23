using System;
using System.Linq;
using System.Threading.Tasks;
using PlaywrightSharp.Tests.BaseTests;
using PlaywrightSharp.Tests.Helpers;
using Xunit;
using Xunit.Abstractions;

namespace PlaywrightSharp.Tests.Launcher
{
    ///<playwright-file>launcher.spec.js</playwright-file>
    ///<playwright-describe>Playwright.defaultArguments</playwright-describe>
    [Trait("Category", "chromium")]
    [Trait("Category", "firefox")]
    [Collection(TestConstants.TestFixtureCollectionName)]
    public class DefaultArgumentsTests : PlaywrightSharpBaseTest
    {
        /// <inheritdoc/>
        public DefaultArgumentsTests(ITestOutputHelper output) : base(output)
        {
        }

        ///<playwright-file>launcher.spec.js</playwright-file>
        ///<playwright-describe>Playwright.defaultArguments</playwright-describe>
        ///<playwright-it>should return the default arguments</playwright-it>
        [Retry]
        public void ShouldReturnTheDefaultArguments()
        {
            if (TestConstants.IsChromium)
            {
                Assert.Contains("--no-first-run", Playwright.GetDefaultArgs(TODO, TODO));
            }

            Assert.Contains(TestConstants.IsFirefox ? "-headless" : "--headless", Playwright.GetDefaultArgs(TODO, TODO));
            Assert.DoesNotContain(
                TestConstants.IsFirefox ? "-headless" : "--headless",
                Playwright.GetDefaultArgs(new LaunchOptionsBase { Headless = false }, TODO, TODO));

            Assert.Contains(
                TestConstants.IsFirefox ? "foo" : "--user-data-dir=\"foo\"",
                Playwright.GetDefaultArgs(new LaunchOptionsBase { UserDataDir = "foo" }, TODO, TODO));

        }

        ///<playwright-file>launcher.spec.js</playwright-file>
        ///<playwright-describe>Playwright.defaultArguments</playwright-describe>
        ///<playwright-it>should filter out ignored default arguments</playwright-it>
        [Retry]
        public async Task ShouldFilterOutIgnoredDefaultArguments()
        {
            string[] defaultArgsWithoutUserDataDir = Playwright.GetDefaultArgs(TestConstants.GetDefaultBrowserOptions(), TODO, TODO);

            var withUserDataDirOptions = TestConstants.GetDefaultBrowserOptions();
            withUserDataDirOptions.UserDataDir = "fake-profile";
            string[] defaultArgsWithUserDataDir = Playwright.GetDefaultArgs(withUserDataDirOptions, TODO, TODO);

            var launchOptions = TestConstants.GetDefaultBrowserOptions();
            launchOptions.UserDataDir = "fake-profile";
            launchOptions.IgnoreDefaultArgs = true;
            launchOptions.IgnoredDefaultArgs = defaultArgsWithUserDataDir.Where(x => !defaultArgsWithoutUserDataDir.Contains(x)).ToArray();

            using var browserApp = await Playwright.LaunchBrowserAppAsync(launchOptions);

            Assert.DoesNotContain("fake-profile", browserApp.Process.StartInfo.Arguments);
        }
    }
}
