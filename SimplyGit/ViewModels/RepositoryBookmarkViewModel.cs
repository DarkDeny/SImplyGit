using System;
using Infrastructure;
using LibGit2Sharp;
using Microsoft.Practices.Prism.Commands;
using SimplyGit.Interfaces;
using SimplyGit.Models;

namespace SimplyGit.ViewModels {
    internal class RepositoryBookmarkViewModel : ViewModelBase {
        private readonly RepositoryModel _repositoryModel;
        private readonly IRepositoryActivator _activator;

        public RepositoryStatusViewModelBase RepositoryStatus { get; }

        public RepositoryModel Model => _repositoryModel;

        public RepositoryBookmarkViewModel(RepositoryModel repositoryModel, IRepositoryActivator activator) {
            _repositoryModel = repositoryModel;
            _activator = activator;
            try {
                var repository = new Repository(_repositoryModel.WorkingFolder);
                // TODO: loading a repository can take some noticeble time, we need to show "loading" busy indicator in this case.
                RepositoryStatus = new RepositoryStatusViewModel(repository);
            }
            catch (Exception ex) {
                RepositoryStatus = new BrokenRepositoryStatusViewModel(ex.Message);
            }

            OpenRepositoryTab = new DelegateCommand(DoOpenRepositoryTab, CanOpenRepositoryTab);
        }

        private void DoOpenRepositoryTab() {
            _activator.Activate(this);
        }

        private bool CanOpenRepositoryTab() {
            return RepositoryStatus.CanOpenRepositoryTab;
        }

        public string Name {
            get { return _repositoryModel.Name; }
            set {
                if (value != _repositoryModel.Name) {
                    _repositoryModel.Name = value;
                    OnPropertyChanged();
                }
            }
        }

        public string WorkingFolder => _repositoryModel.WorkingFolder;

        public DelegateCommand OpenRepositoryTab { get; }
    }
}