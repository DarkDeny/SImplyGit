using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using Infrastructure;
using LibGit2Sharp;

namespace SimplyGit.ViewModels {
    internal class CommitViewModel : ViewModelBase {
        public CommitViewModel(Commit commit, Repository repository) {
            _commit = commit;
            _repository = repository;

            Description = _commit.MessageShort;
            Author = commit.Author.Name;
            CommitId = commit.Sha;
            Date = commit.Author.When.DateTime;

            Tags = new ObservableCollection<string>();
            ChangedFiles = new ObservableCollection<DiffViewModel>();
            if (null != _commit.Notes) {
                foreach (var commitNote in _commit.Notes) {
                    Debug.WriteLine(commitNote);
                }
            }
        }

        public void OnDeactivate() {
            ChangedFiles.Clear();
        }

        public void OnActivate() {
            try {
                var parentsCount = 0;
                Tree prevTree = null;
                foreach (var parent in _commit.Parents) {
                    parentsCount++;
                    prevTree = parent.Tree;
                    if (parentsCount > 1) {
                        Debug.WriteLine("orly?");
                    }
                }

                var repoDifferences = _repository.Diff.Compare<Patch>(
                    prevTree, _commit.Tree
                );
                foreach (var difference in repoDifferences) {
                    var vm = new DiffViewModel(difference);
                    ChangedFiles.Add(vm);
                }

                if (ChangedFiles.Any()) {
                    SelectedFile = ChangedFiles.FirstOrDefault();
                }
            }
            catch (Exception ex) {
                Debug.WriteLine(ex.Unwind());
            }
        }

        public ObservableCollection<DiffViewModel> ChangedFiles { get; }

        private DiffViewModel _selectedFile;
        public DiffViewModel SelectedFile {
            get => _selectedFile;
            set {
                if (Equals(value, _selectedFile))
                    return;
                _selectedFile = value;
                OnPropertyChanged();
            }
        }

        public Tree Tree => _commit.Tree;

        private readonly Commit _commit;
        private readonly Repository _repository;

        public string Description { get; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string CommitId { get; set; }

        public void AddTag(Tag tag) {
            Tags.Add(tag.FriendlyName);
        }

        public ObservableCollection<string> Tags { get; }
    }
}