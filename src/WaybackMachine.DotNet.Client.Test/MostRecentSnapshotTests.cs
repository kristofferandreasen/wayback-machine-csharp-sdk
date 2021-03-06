using WaybackMachine.DotNet.Client.Extensions;
using WaybackMachine.DotNet.Client.Models;
using Xunit;

namespace WaybackMachine.DotNet.Client.Test
{
    public class MostRecentSnapshotTests
    {
        private readonly WaybackMachineService _waybackMachineService;

        public MostRecentSnapshotTests()
        {
            _waybackMachineService = new WaybackMachineService();
        }

        [Fact]
        public async void Returns_snapshot_for_website()
        {
            Snapshot snapshot = await _waybackMachineService.GetMostRecentSnapshotAsync("google.com");

            Assert.NotNull(snapshot);
        }

        [Fact]
        public async void Missing_domain_returns_no_snapshot()
        {
            Snapshot snapshot = await _waybackMachineService.GetMostRecentSnapshotAsync("ieowfjo");
            bool archivedSnapshotExists = snapshot.HasProperty(nameof(ArchivedSnapshots));

            Assert.NotNull(snapshot);
            Assert.True(archivedSnapshotExists);
            Assert.Null(snapshot.ArchivedSnapshots.Closest);
        }

        [Fact]
        public async void Missing_domain_returns_empty_archive()
        {
            Snapshot snapshot = await _waybackMachineService.GetMostRecentSnapshotAsync("ieowfjo");
            bool archivedSnapshotExists = snapshot.HasProperty(nameof(ArchivedSnapshots));

            Assert.NotNull(snapshot);
            Assert.True(archivedSnapshotExists);
            Assert.Null(snapshot.ArchivedSnapshots.Closest);
        }

        [Fact]
        public async void Special_characters_return_empty_archive()
        {
            Snapshot snapshot = await _waybackMachineService.GetMostRecentSnapshotAsync("awdaw-*w//");
            bool archivedSnapshotExists = snapshot.HasProperty(nameof(ArchivedSnapshots));

            Assert.NotNull(snapshot);
            Assert.True(archivedSnapshotExists);
            Assert.Null(snapshot.ArchivedSnapshots.Closest);
        }
    }
}