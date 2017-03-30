namespace SimplyGit.Models {
    public class RepositoryModel {
        public RepositoryModel(string name, string folder) {
            Name = name;
            WorkingFolder = folder;
        }

        public string WorkingFolder { get; set; }
        public string Name { get; set; }
    }
}
