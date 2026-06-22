using System.Configuration;

namespace DataLayer {
    public static class DataAccessSettings {
        public static readonly string ConnectionString =
            ConfigurationManager
                .ConnectionStrings["DefaultConnection"]
                .ConnectionString;
    }
}