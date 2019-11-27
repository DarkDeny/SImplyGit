using System;
using Infrastructure;
using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class CommitViewModel : ViewModelBase {
        public CommitViewModel(Commit commit) {
            _commit = commit;

            Description = _commit.Message;
            Author = commit.Author.Name;
            CommitId = commit.Sha;
            Date = commit.Author.When.DateTime;
        }

        private readonly Commit _commit;

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string CommitId { get; set; }
    }
}