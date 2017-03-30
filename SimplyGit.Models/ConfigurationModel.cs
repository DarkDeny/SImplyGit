using System.Collections.Generic;

namespace SimplyGit.Models {
    public class ConfigurationModel {
        public ICollection<RepositoryModel> Repositories { get; set; }
    }
}