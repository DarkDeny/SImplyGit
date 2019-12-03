using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
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

            ChangedFiles = new ObservableCollection<DiffViewModel>();
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

        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Author { get; set; }
        public string CommitId { get; set; }
    }
}